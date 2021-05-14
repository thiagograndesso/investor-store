using System;
using InvestorStore.Core.Messages;

namespace InvestorStore.Sales.Application.Events
{
    public class ProductUpdatedOrderEvent : Event
    {
        public Guid CustomerId { get; }
        public Guid OrderId { get; }
        public Guid ProductId { get; }
        public int Quantity { get; }

        public ProductUpdatedOrderEvent(Guid customerId, Guid orderId, Guid productId, int quantity)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}