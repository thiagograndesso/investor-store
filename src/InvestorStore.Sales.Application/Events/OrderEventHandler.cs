using System.Threading;
using System.Threading.Tasks;
using InvestorStore.Core.Messages.CommonMessages.IntegrationEvents;
using MediatR;

namespace InvestorStore.Sales.Application.Events
{
    public class OrderEventHandler :
        INotificationHandler<DraftOrderCreatedEvent>,
        INotificationHandler<OrderItemAddedEvent>,
        INotificationHandler<OrderUpdatedEvent>,
        INotificationHandler<OrderInventoryConfirmedEvent>,
        INotificationHandler<OrderInventoryRejectEvent>
    {
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

        public Task Handle(OrderInventoryRejectEvent notification, CancellationToken cancellationToken)
        {
            // TODO: stop order processing and return error message to customer
            return Task.CompletedTask;
        }
    }
}