using Conferences.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conferences.Domain.Conferences.Repository
{
    public interface IConferenceRepository : IRepository<Conference>
    {
        IEnumerable<Conference> GetConferencesByOrganizer(Guid organizerId);
        Address GetAddressById(Guid id);
        void AddAddress(Address address);
        void UpdateAddress(Address address);
        IEnumerable<Category> GetCategories();
        Conference GetMyConferenceById(Guid id, Guid organizerId);
    }
}
