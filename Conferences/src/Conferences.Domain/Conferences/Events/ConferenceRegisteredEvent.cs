using System;

namespace Conferences.Domain.Conferences.Events
{
    public class ConferenceRegisteredEvent : BaseConferenceEvent
    {
        public ConferenceRegisteredEvent(Guid id, string name, DateTime startDate, DateTime endDate, bool free, 
            decimal value, bool online, string companyName)
        {
            Id = id;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Free = free;
            Value = value;
            Online = online;
            CompanyName = companyName;

            AggregateId = id;
        }
    }
}
