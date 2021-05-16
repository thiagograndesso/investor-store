using System;

namespace InvestorStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class OrderInventoryRejectedEvent : IntegrationEvent
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }

        public OrderInventoryRejectedEvent(Guid orderId, Guid customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}