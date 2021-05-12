using System;
using InvestorStore.Core.Messages;

namespace InvestorStore.Sales.Application.Events
{
    public class OrderItemAddedEvent : Event
    {
        public Guid CustomerId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public decimal Amount { get; private set; }
        public int Quantity { get; private set; }

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