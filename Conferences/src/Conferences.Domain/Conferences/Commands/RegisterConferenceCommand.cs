using System;
using System.Collections.Generic;
using System.Text;

namespace Conferences.Domain.Conferences.Commands
{
    public class RegisterConferenceCommand : BaseConferenceCommand
    {
        public AddAddressConferenceCommand Address { get; private set; }

        public RegisterConferenceCommand(
            string name, 
            string shortDescription, 
            string longDescription,
            DateTime startDate, 
            DateTime endDate, 
            bool free, 
            decimal value, 
            bool online, 
            string companyName,
            Guid organizerId,
            Guid categoryId,
            AddAddressConferenceCommand address)
        {
            Name = name;
            ShortDescription = shortDescription;
            LongDescription = longDescription;
            StartDate = startDate;
            EndDate = endDate;
            Free = free;
            Value = value;
            Online = online;
            CompanyName = companyName;
            OrganizerId = organizerId;            
            CategoryId = categoryId;
            Address = address;
        }
    }
}
