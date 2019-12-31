using Conferences.Domain.Interfaces;
using Conferences.Infra.Data.Context;

namespace Conferences.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ConferenceContext _context;

        public UnitOfWork(ConferenceContext context)
        {
            _context = context;
        }


        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
