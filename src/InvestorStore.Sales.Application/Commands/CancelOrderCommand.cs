using System;
using InvestorStore.Core.Messages;

namespace InvestorStore.Sales.Application.Commands
{
    public class CancelOrderCommand : Command
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public CancelOrderCommand(Guid orderId, Guid customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}