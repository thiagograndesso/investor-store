using System;

namespace InvestorStore.Core.Data.EventSourcing
{
    public class StoredEvent
    {
        public Guid Id { get; private set; }
        public string Type { get; private set; }
        public DateTimeOffset Timestamp { get; private set; }
        public string Data { get; private set; }
        
        public StoredEvent(Guid id, string type, DateTimeOffset timestamp, string data)
        {
            Id = id;
            Type = type;
            Timestamp = timestamp;
            Data = data;
        }
    }
}