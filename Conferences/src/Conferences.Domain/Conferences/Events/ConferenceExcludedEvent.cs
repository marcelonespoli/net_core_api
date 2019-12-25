using System;

namespace Conferences.Domain.Conferences.Events
{
    public class ConferenceExcludedEvent : BaseConferenceEvent
    {
        public ConferenceExcludedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
