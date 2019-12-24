using System;

namespace Conferences.Domain.Core.Events
{
    public class StoredEvent : Event
    {
        public Guid Id { get; private set; }
        public string Data { get; private set; }
        public string User { get; private set; }
        
        // EF Constructor
        protected StoredEvent() { }

        public StoredEvent(Event conference, string data, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = conference.AggregateId;
            MessageType = conference.MessageType;
            Data = data;
            User = user;
        }
        
    }
}
