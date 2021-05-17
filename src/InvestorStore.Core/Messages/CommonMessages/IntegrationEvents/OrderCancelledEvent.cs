using System;
using InvestorStore.Core.DomainObjects.Dtos;

namespace InvestorStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class OrderCancelledEvent : IntegrationEvent
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public OrderProducts OrderProducts { get; private set; }

        public OrderCancelledEvent(Guid orderId, Guid customerId, OrderProducts orderProducts)
        {
            AggregateId = orderId;
            OrderId = orderId;
            CustomerId = customerId;
            OrderProducts = orderProducts;
        }
    }
}