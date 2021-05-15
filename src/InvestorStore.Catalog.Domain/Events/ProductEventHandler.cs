using System.Threading;
using System.Threading.Tasks;
using InvestorStore.Core.Communication.Mediator;
using InvestorStore.Core.Messages.CommonMessages.IntegrationEvents;
using MediatR;

namespace InvestorStore.Catalog.Domain.Events
{
    public class ProductEventHandler : 
        INotificationHandler<ProductBelowInventoryEvent>,
        INotificationHandler<OrderOpenedEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryService _inventoryService;
        private readonly IMediatorHandler _mediatorHandler;


        public ProductEventHandler(
            IProductRepository productRepository, 
            IInventoryService inventoryService, 
            IMediatorHandler mediatorHandler)
        {
            _productRepository = productRepository;
            _inventoryService = inventoryService;
            _mediatorHandler = mediatorHandler;
        }
        
        public async Task Handle(ProductBelowInventoryEvent message, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(message.AggregateId);
            
            // Send message to external systems, alert personnel, and more.
        }

        public async Task Handle(OrderOpenedEvent message, CancellationToken cancellationToken)
        {
            var result = await _inventoryService.DebitOrderProducts(message.OrderProducts);

            if (result)
            {
                await _mediatorHandler.PublishEvent(new OrderInventoryConfirmedEvent(message.OrderId, message.CustomerId, message.Total, message.OrderProducts, message.CardName, message.CardNumber, message.CardExpiryDate, message.CardCvvCode));
            }
            else
            {
                await _mediatorHandler.PublishEvent(new OrderInventoryRejectEvent(message.OrderId, message.CustomerId));
            }
        }
    }
}