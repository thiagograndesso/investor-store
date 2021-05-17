using System.Threading;
using System.Threading.Tasks;
using InvestorStore.Core.Communication.Mediator;
using InvestorStore.Core.Messages.CommonMessages.IntegrationEvents;
using InvestorStore.Sales.Application.Commands;
using MediatR;

namespace InvestorStore.Sales.Application.Events
{
    public class OrderEventHandler :
        INotificationHandler<DraftOrderCreatedEvent>,
        INotificationHandler<OrderItemAddedEvent>,
        INotificationHandler<OrderUpdatedEvent>,
        INotificationHandler<OrderInventoryConfirmedEvent>,
        INotificationHandler<OrderInventoryRejectedEvent>,
        INotificationHandler<OrderPaymentConfirmedEvent>,
        INotificationHandler<OrderPaymentRejectedEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public OrderEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }
        
        public Task Handle(DraftOrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderItemAddedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderInventoryConfirmedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(OrderInventoryRejectedEvent notification, CancellationToken cancellationToken)
        {
            await _mediatorHandler.SendCommand(new CancelOrderCommand(notification.OrderId, notification.CustomerId));
        }

        public async Task Handle(OrderPaymentConfirmedEvent notification, CancellationToken cancellationToken)
        {
            await _mediatorHandler.SendCommand(new CompleteOrderCommand(notification.OrderId, notification.CustomerId));
        }

        public async Task Handle(OrderPaymentRejectedEvent notification, CancellationToken cancellationToken)
        {
            await _mediatorHandler.SendCommand(new CancelOrderAndRefillInventoryCommand(notification.OrderId, notification.CustomerId));
        }
    }
}