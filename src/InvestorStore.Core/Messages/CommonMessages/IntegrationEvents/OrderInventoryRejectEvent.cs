using System;

namespace InvestorStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class OrderInventoryRejectEvent : IntegrationEvent
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public OrderInventoryRejectEvent(Guid orderId, Guid customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}