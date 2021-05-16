using System;
using InvestorStore.Core.Messages;

namespace InvestorStore.Sales.Application.Events
{
    public class OrderItemAddedEvent : Event
    {
        public Guid CustomerId { get; }
        public Guid OrderId { get; }
        public Guid ProductId { get; }
        public decimal Amount { get; }
        public int Quantity { get; }

        public OrderItemAddedEvent(Guid customerId, Guid orderId, Guid productId, decimal amount, int quantity)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
            ProductId = productId;
            Amount = amount;
            Quantity = quantity;
        }
    }
}