using System;
using InvestorStore.Core.Messages;

namespace InvestorStore.Sales.Application.Events
{
    public class OrderCompletedEvent : Event
    {
        public Guid OrderId { get; private set; }

        public OrderCompletedEvent(Guid orderId)
        {
            AggregateId = orderId;
            OrderId = orderId;
        }
    }
}