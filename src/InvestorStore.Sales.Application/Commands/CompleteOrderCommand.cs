using System;
using InvestorStore.Core.Messages;

namespace InvestorStore.Sales.Application.Commands
{
    public class CompleteOrderCommand : Command
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public CompleteOrderCommand(Guid orderId, Guid customerId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}