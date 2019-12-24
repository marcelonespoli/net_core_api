using System;

namespace Events.Domain.Core.Events
{
    public class StoredEvent : Event
    {
        public Guid Id { get; private set; }
        public string Data { get; private set; }
        public string User { get; private set; }
        
        // EF Constructor
        protected StoredEvent() { }

        public StoredEvent(Event runEvent, string data, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = runEvent.AggregateId;
            MessageType = runEvent.MessageType;
            Data = data;
            User = user;
        }
        
    }
}
