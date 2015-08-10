using LibraryDal.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace LibraryDal.Infrastructure.UnitOfWork
{
    public interface ICommandContext : IDisposable
    {
        TEntity FindEntity<TEntity>(params object[] KeyValues) where TEntity : class, IBaseDal;
        TEntity AddEntity<TEntity>(TEntity Entity) where TEntity : class, IBaseDal;
        void AddRangeEntities<TEntity>(IEnumerable<TEntity> Entities) where TEntity : class, IBaseDal;
        TEntity UpdateEntity<TEntity>(TEntity Entity) where TEntity : class, IBaseDal;
        TEntity DeleteEntity<TEntity>(TEntity Entity) where TEntity : class, IBaseDal;
        int GetNewId<TEntity>() where TEntity : class, IBaseDal;
        Database Database { get; }
        int SaveChanges();
    }
}
