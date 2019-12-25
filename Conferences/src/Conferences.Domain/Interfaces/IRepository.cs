using Conferences.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Conferences.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(Guid id);        
        void Add(TEntity obj);
        void Update(TEntity obj);
        void Remove(Guid id);
        IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);
        int SaveChanges();
    }
}
