using System;
using InvestorStore.Core.Messages;

namespace InvestorStore.Sales.Application.Events
{
    public class ProductRemovedOrderEvent : Event
    {
        public Guid CustomerId { get; }
        public Guid OrderId { get; }
        public Guid ProductId { get; }

        public ProductRemovedOrderEvent(Guid customerId, Guid orderId, Guid productId)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
            ProductId = productId;
        }
    }
}