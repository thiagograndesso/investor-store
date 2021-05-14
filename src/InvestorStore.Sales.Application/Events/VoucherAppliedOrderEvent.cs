using System;
using InvestorStore.Core.Messages;

namespace InvestorStore.Sales.Application.Events
{
    public class VoucherAppliedOrderEvent : Event
    {
        public Guid CustomerId { get; }
        public Guid OrderId { get; }
        public Guid VoucherId { get; }

        public VoucherAppliedOrderEvent(Guid customerId, Guid orderId, Guid voucherId)
        {
            CustomerId = customerId;
            OrderId = orderId;
            VoucherId = voucherId;
        }
    }
}