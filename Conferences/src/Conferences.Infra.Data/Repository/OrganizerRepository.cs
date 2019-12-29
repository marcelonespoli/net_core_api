using Conferences.Domain.Organizers;
using Conferences.Domain.Organizers.Repository;
using Conferences.Infra.Data.Context;

namespace Conferences.Infra.Data.Repository
{
    public class OrganizerRepository : Repository<Organizer>, IOrganizerRepository
    {
        public OrganizerRepository(ConferenceContext context) : base(context)
        {

        }
    }
}
