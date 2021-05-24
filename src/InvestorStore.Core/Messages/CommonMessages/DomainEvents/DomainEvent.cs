using System;
using MediatR;

namespace InvestorStore.Core.Messages.CommonMessages.DomainEvents
{
    public abstract class DomainEvent : Message, INotification
    {
        public DateTimeOffset TimeStamp { get; }
        
        protected DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
            TimeStamp = DateTimeOffset.Now;
        }
    }
}