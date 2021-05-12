using System;
using InvestorStore.Core.Messages;

namespace InvestorStore.Sales.Application.Events
{
    public class DraftOrderCreatedEvent : Event
    {
        public Guid CustomerId { get; private set; }
        public Guid OrderId { get; private set; }

        public DraftOrderCreatedEvent(Guid customerId, Guid orderId)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
        }
    }
}