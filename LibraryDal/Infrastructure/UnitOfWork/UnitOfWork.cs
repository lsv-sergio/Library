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
            IDbSet<TEntity> dbSet = null;
            if (_dbContext == null)
                return dbSet;
            try
            {
                dbSet = _dbContext.Set<TEntity>();
            }
            catch (Exception ex)
            {
            }
            return dbSet;
        }

        public TEntity FindEntity<TEntity>(params object[] KeyValues) where TEntity : class, IBaseDal
        {
                IDbSet<TEntity> dbSet = GetDbSet<TEntity>();
                if (dbSet == null)
                    return null;
            try
            {
                return dbSet.Find(KeyValues);
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public TEntity GetEntity<TEntity>(int EntityId) where TEntity : class, IBaseDal
        {
            if (EntityId < 1)
                return null;
            IDbSet<TEntity> dbSet = GetDbSet<TEntity>();
            if (dbSet == null)
                return null;
            try
            {
                return dbSet.FirstOrDefault(x => x.Id == EntityId);
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public TEntity GetEntity<TEntity>(Func<TEntity, bool> filter) where TEntity : class, IBaseDal
        {
            if (filter == null)
                return null;

            IDbSet<TEntity> dbSet = GetDbSet<TEntity>();
            if (dbSet == null)
                return null;
            try
            {
                return dbSet.FirstOrDefault(filter);
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public IEnumerable<TEntity> GetEntities<TEntity>() where TEntity : class, IBaseDal
        {
            IEnumerable<TEntity> emptyList = new List<TEntity>();

            IDbSet<TEntity> dbSet = GetDbSet<TEntity>();
            if (dbSet == null)
                return emptyList;

            try
            {
                return dbSet.AsEnumerable();
            }
            catch (Exception ex)
            {
            }
            return emptyList;
        }

        public IEnumerable<TEntity> GetEntities<TEntity>(Func<TEntity, bool> filter) where TEntity : class, IBaseDal
        {
            IEnumerable<TEntity> emptyList = new List<TEntity>();
            if (filter == null)
                return emptyList;

            IDbSet<TEntity> dbSet = GetDbSet<TEntity>();
            if (dbSet == null)
                return emptyList;
            try
            {
                return dbSet.Where(filter).ToList().AsEnumerable();
            }
            catch (Exception ex)
            {
            }
            return emptyList;
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
                }
                return null;
        }

        public void SetModifiedEntityState<TEntity>(TEntity Entity) where TEntity : class, IBaseDal
        {
            if(_dbContext != null)
                try
                {
                    _dbContext.Entry(Entity).State = System.Data.Entity.EntityState.Modified;
                }
                catch (Exception ex)
                {
                }
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
            }
            return null;
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
            try
            {
                return dbSet.Max(x => x.Id) + 1;
            }
            catch (Exception ex)
            {
            }
            return 1;
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
