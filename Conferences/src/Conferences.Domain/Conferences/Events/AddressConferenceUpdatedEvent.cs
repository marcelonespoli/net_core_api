using Conferences.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conferences.Domain.Conferences.Events
{
    public class AddressConferenceUpdatedEvent : Event
    {
        public Guid Id { get; private set; }
        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string Address3 { get; private set; }
        public string Number { get; private set; }
        public string Postcode { get; private set; }
        public string City { get; private set; }
        public string County { get; private set; }

        public AddressConferenceUpdatedEvent(Guid addressId, string address1, string address2, string address3, string number, 
            string postcode, string city, string county, Guid eventId)
        {
            Id = addressId;
            Address1 = address1;
            Address2 = address2;
            Address3 = address3;
            Number = number;
            Postcode = postcode;
            City = city;
            County = county;
            AggregateId = eventId;
        }
    }
}
