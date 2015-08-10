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
    public class UnitOfWork : IQueryContext, ICommandContext
    {
        private EFLibraryDbContext _dbContext;

        public UnitOfWork(EFLibraryDbContext DbContext)
        {
            _dbContext = DbContext;
        }

        private IDbSet<TEntity> GetDbSet<TEntity>() where TEntity : class, IBaseDal
        {
            if (_dbContext == null)
                return null;
            return _dbContext.Set<TEntity>();
        }

        public TEntity FindEntity<TEntity>(params object[] KeyValues) where TEntity : class, IBaseDal
        {
            IDbSet<TEntity> dbSet = GetDbSet<TEntity>();
            if (dbSet == null)
                return null;
            return dbSet.Find(KeyValues);
        }

        public TEntity GetEntity<TEntity>(int EntityId) where TEntity : class, IBaseDal
        {
            if (EntityId < 1)
                return null;
            IDbSet<TEntity> dbSet = GetDbSet<TEntity>();
            if (dbSet == null)
                return null;
            return dbSet.FirstOrDefault(x => x.Id == EntityId);
        }

        public TEntity GetEntity<TEntity>(Func<TEntity, bool> filter) where TEntity : class, IBaseDal
        {
            if (filter == null)
                return null;

            IDbSet<TEntity> dbSet = GetDbSet<TEntity>();
            if (dbSet == null)
                return null;
            return dbSet.FirstOrDefault(filter);
        }

        public IQueryable<TEntity> GetEntities<TEntity>() where TEntity : class, IBaseDal
        {
            IDbSet<TEntity> dbSet = GetDbSet<TEntity>();
            if (dbSet == null)
                return new List<TEntity>().AsQueryable();
            return dbSet;
        }

        public IEnumerable<TEntity> GetEntities<TEntity>(Func<TEntity, bool> filter) where TEntity : class, IBaseDal
        {
            if (filter == null)
                return new List<TEntity>().AsEnumerable();

            IDbSet<TEntity> dbSet = GetDbSet<TEntity>();
            if (dbSet == null)
                return new List<TEntity>().AsEnumerable();
            return dbSet.Where(filter).ToList().AsEnumerable();
        }

        public TEntity UpdateEntity<TEntity>(TEntity Entity) where TEntity : class, IBaseDal
        {
            if (Entity == null)
                return null;
            IDbSet<TEntity> dbSet = GetDbSet<TEntity>();
            if (dbSet == null)
                return null;
                try
            {
                if (FindEntity<TEntity>(Entity.Id) != null)
                    SetModifiedEntityState<TEntity>(Entity);
                else
                    dbSet.Add(Entity);
                return Entity;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void SetModifiedEntityState<TEntity>(TEntity Entity) where TEntity : class, IBaseDal
        {
            if(_dbContext != null)
            _dbContext.Entry(Entity).State = System.Data.Entity.EntityState.Modified;
        }

        public TEntity AddEntity<TEntity>(TEntity Entity) where TEntity : class, IBaseDal
        {
            if (Entity == null)
                return null;
            try
            {
                 IDbSet<TEntity> dbSet = GetDbSet<TEntity>();
               if (dbSet == null)
                    return null;
                return dbSet.Add(Entity);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void AddRangeEntities<TEntity>(IEnumerable<TEntity> Entities) where TEntity : class, IBaseDal
        {
            return;
        }

        public TEntity DeleteEntity<TEntity>(TEntity Entity) where TEntity : class, IBaseDal
        {
            if (Entity == null)
                return null;
            try
            {
                IDbSet<TEntity> dbSet = GetDbSet<TEntity>();
                if (dbSet == null)
                    return null;

                if (FindEntity<TEntity>(Entity.Id) != null)
                    return dbSet.Remove(Entity);

            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public int GetNewId<TEntity>() where TEntity : class, IBaseDal
        {
            IDbSet<TEntity> dbSet = GetDbSet<TEntity>();
            if (dbSet == null)
                return 0;
            if (dbSet.Count() == 0)
                return 1;
            return dbSet.Max(x => x.Id) + 1;
        }

        public void Dispose()
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }

        public Database Database
        {
            get
            {
                if (_dbContext == null)
                    return null;

                return _dbContext.Database;
            }
        }

        public int SaveChanges()
        {
            if (_dbContext == null)
                return 0;

            _dbContext.ChangeTracker.DetectChanges();
            return _dbContext.SaveChanges();
        }
    }
}
