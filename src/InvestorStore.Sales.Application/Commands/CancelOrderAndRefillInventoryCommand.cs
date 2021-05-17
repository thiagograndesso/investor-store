using System;
using InvestorStore.Core.Messages;

namespace InvestorStore.Sales.Application.Commands
{
    public class CancelOrderAndRefillInventoryCommand : Command
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public CancelOrderAndRefillInventoryCommand(Guid orderId, Guid customerId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}