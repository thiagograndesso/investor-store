using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace InvestorStore.Catalog.Domain.Events
{
    public class ProductEventHandler : INotificationHandler<ProductBelowInventoryEvent>
    {
        private readonly IProductRepository _productRepository;

        public ProductEventHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public async Task Handle(ProductBelowInventoryEvent notification, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(notification.AggregateId);
            
            // Send notification to external systems, alert personnel, and more.
        }
    }
}