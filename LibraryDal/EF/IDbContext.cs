using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace LibraryDal.EF
{
    public interface IDbContext : IDisposable
    {
        DbEntityEntry Entry(object entity);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
