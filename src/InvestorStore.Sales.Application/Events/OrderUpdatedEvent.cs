using System;
using InvestorStore.Core.Messages;

namespace InvestorStore.Sales.Application.Events
{
    public class OrderUpdatedEvent : Event
    {
        public Guid CustomerId { get; private set; }
        public Guid OrderId { get; private set; }
        public decimal TotalAmount { get; private set; }

        public OrderUpdatedEvent(Guid customerId, Guid orderId, decimal totalAmount)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
            TotalAmount = totalAmount;
        }
    }
}