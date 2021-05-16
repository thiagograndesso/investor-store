using System;
using InvestorStore.Core.Messages;

namespace InvestorStore.Sales.Application.Events
{
    public class DraftOrderCreatedEvent : Event
    {
        public Guid CustomerId { get; }
        public Guid OrderId { get; }

        public DraftOrderCreatedEvent(Guid customerId, Guid orderId)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
        }
    }
}