using LibraryDal.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDal.Infrastructure.UnitOfWork
{
    public interface IQueryContext
    {
        TEntity GetEntity<TEntity>(int EntityId) where TEntity : class, IBaseDal;
        TEntity GetEntity<TEntity>(Func<TEntity, bool> filter) where TEntity : class, IBaseDal;
        IQueryable<TEntity> GetEntities<TEntity>() where TEntity : class, IBaseDal;
        IEnumerable<TEntity> GetEntities<TEntity>(Func<TEntity, bool> filter) where TEntity : class, IBaseDal;
    }
}
