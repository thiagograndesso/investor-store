using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace InvestorStore.Sales.Application.Events
{
    public class OrderEventHandler :
        INotificationHandler<DraftOrderCreatedEvent>,
        INotificationHandler<OrderItemAddedEvent>,
        INotificationHandler<OrderUpdatedEvent>
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
    }
}