using Conferences.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conferences.Domain.Conferences.Commands
{
    public class UpdateAddressConferenceCommand : Command
    {
        public Guid Id { get; private set; }
        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string Address3 { get; private set; }
        public string Number { get; private set; }
        public string Postcode { get; private set; }
        public string City { get; private set; }
        public string County { get; private set; }
        public Guid? ConferenceId { get; private set; }

        public UpdateAddressConferenceCommand(Guid id, string address1, string address2, string address3, string number,
            string postcode, string city, string county, Guid? conferenceId)
        {
            Id = id;
            Address1 = address1;
            Address2 = address2;
            Address3 = address3;
            Number = number;
            Postcode = postcode;
            City = city;
            County = county;
            ConferenceId = conferenceId;
        }
    }
}
