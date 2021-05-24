using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvestorStore.Core.Messages;

namespace InvestorStore.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        Task PersistEvent<TEvent>(TEvent @event) where TEvent : Event;
        Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId);
    }
}