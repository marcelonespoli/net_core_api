using Conferences.Domain.Conferences;
using Conferences.Domain.Conferences.Repository;
using Conferences.Infra.Data.Context;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Conferences.Infra.Data.Repository
{
    public class ConferenceRepository : Repository<Conference>, IConferenceRepository
    {
        public ConferenceRepository(ConferenceContext context) : base(context)
        {

        }

        public override IEnumerable<Conference> GetAll()
        {
            var sql = @"SELECT * FROM Conferences C" +
                      "WHERE C.Excluded = 0 " +
                      "ORDER BY C.EndDate DESC";

            return Db.Database.GetDbConnection().Query<Conference>(sql);
        }

        public override Conference GetById(Guid id)
        {
            var sql = @"SELECT * FROM Conferences C " +
                      "LEFT JOIN Addresses A " +
                      "ON C.Id = A.ConferenceId " +
                      "WHERE C.Id = @uid";

            var conference = Db.Database.GetDbConnection().Query<Conference, Address, Conference>(sql,
                (c, a) =>
                {
                    if (a != null)
                        c.AddAddress(a);

                    return c;
                }, new { uid = id });

            return conference.FirstOrDefault();
        }

        public override void Remove(Guid id)
        {
            var conference = GetById(id);
            conference.ExcludeConference();
            Update(conference);
        }

        public IEnumerable<Conference> GetConferencesByOrganizer(Guid organizerId)
        {
            var sql = @"SELECT * FROM Conferences C" +
                      "WHERE C.Excluded = 0 " +
                      "AND C.OrganizerId = @oid " +
                      "ORDER BY C.EndDate DESC";

            return Db.Database.GetDbConnection().Query<Conference>(sql, new { oid = organizerId });
        }

        public Conference GetMyConferenceById(Guid id, Guid organizerId)
        {
            var sql = @"SELECT * FROM Conferences C " +
                      "LEFT JOIN Addresses A " +
                      "ON C.Id = A.ConferenceId " +
                      "WHERE C.Excluded = 0 " +
                      "AND C.OrganizerId = @oid " +
                      "AND C.Id = @aid";

            var conference = Db.Database.GetDbConnection().Query<Conference, Address, Conference>(sql,
                (c, a) =>
                {
                    if (a != null)
                        c.AddAddress(a);

                    return c;
                },
                new { oid = organizerId, aid = id });

            return conference.FirstOrDefault();
        }

        public Address GetAddressById(Guid id)
        {
            var sql = @"SELECT * FROM Adresses A " +
                      "WHERE A.Id = @uid";

            var address = Db.Database.GetDbConnection().Query<Address>(sql, new { uid = id });

            return address.SingleOrDefault();
        }

        public IEnumerable<Category> GetCategories()
        {
            var sql = @"SELECT * FROM Categories";
            return Db.Database.GetDbConnection().Query<Category>(sql);
        }

        public void AddAddress(Address address)
        {
            Db.Addresses.Add(address);
        }

        public void UpdateAddress(Address address)
        {
            Db.Addresses.Update(address);
        }
        
        
    }
}
