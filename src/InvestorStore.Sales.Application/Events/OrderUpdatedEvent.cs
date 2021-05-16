using System;
using InvestorStore.Core.Messages;

namespace InvestorStore.Sales.Application.Events
{
    public class OrderUpdatedEvent : Event
    {
        public Guid CustomerId { get; }
        public Guid OrderId { get; }
        public decimal TotalAmount { get; }

        public OrderUpdatedEvent(Guid customerId, Guid orderId, decimal totalAmount)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
            TotalAmount = totalAmount;
        }
    }
}