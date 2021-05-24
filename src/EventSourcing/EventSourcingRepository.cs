using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using InvestorStore.Core.Data.EventSourcing;
using InvestorStore.Core.Messages;
using Newtonsoft.Json;

namespace EventSourcing
{
    public class EventSourcingRepository : IEventSourcingRepository
    {
        private readonly IEventStoreService _eventStoreService;

        public EventSourcingRepository(IEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }
        
        public async Task PersistEvent<TEvent>(TEvent @event) where TEvent : Event
        {
            await _eventStoreService.GetConnection().AppendToStreamAsync(
                @event.AggregateId.ToString(), 
                ExpectedVersion.Any, 
                ToEventData(@event));
        }

        public async Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId)
        {
            var events = await _eventStoreService.GetConnection().ReadStreamEventsForwardAsync(aggregateId.ToString(), 0, 500, false);
            
            var storedEvents = new List<StoredEvent>();
            
            foreach (var resolvedEvent in events.Events)
            {
                var dataEncoded = Encoding.UTF8.GetString(resolvedEvent.Event.Data);
                var jsonData = JsonConvert.DeserializeObject<BaseEvent>(dataEncoded);

                var @event = new StoredEvent(
                    resolvedEvent.Event.EventId,
                    resolvedEvent.Event.EventType,
                    jsonData.Timestamp,
                    dataEncoded);

                storedEvents.Add(@event);
            }

            return storedEvents.OrderBy(e => e.Timestamp);
        }

        private static IEnumerable<EventData> ToEventData<TEvent>(TEvent @event) where TEvent : Event
        {
            yield return new EventData(
                Guid.NewGuid(),
                @event.MessageType,
                isJson: true,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event)),
                metadata: null);
        }
    }
    
    internal class BaseEvent
    {
        public DateTime Timestamp { get; set; }
    }
}