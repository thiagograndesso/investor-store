using System;
using InvestorStore.Core.Messages;

namespace InvestorStore.Sales.Application.Events
{
    public class ProductAddedOrderEvent : Event
    {
        public Guid CustomerId { get; }
        public Guid OrderId { get; }
        public Guid ProductId { get; }
        public int Quantity { get; }

        public ProductAddedOrderEvent(Guid customerId, Guid orderId, Guid productId, int quantity)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}