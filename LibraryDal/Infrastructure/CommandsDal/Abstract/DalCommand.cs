using LibraryDal.EF;
using LibraryDal.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDal.Infrastructure.CommandsDal
{
    public abstract class DalCommand<TEntity>:IDalCommand<TEntity> where TEntity :  class, IBaseDal
    {
        private readonly ICommandContext _uow;

        public DalCommand(ICommandContext UoW)
        {
            _uow = UoW;
        }

        public virtual bool Add(TEntity Entity)
        {
            if (_uow == null)
                return false;
            TEntity entity = _uow.AddEntity(Entity);
            return entity != null;
        }

        public virtual bool Update(TEntity Entity)
        {
            if (_uow == null)
                return false;
            TEntity entity = _uow.UpdateEntity(Entity);
            return entity != null;
        }

        public virtual bool Delete(TEntity Entity)
        {
            TEntity entity = null;
            if (_uow == null)
                return entity == null;
            entity = _uow.DeleteEntity<TEntity>(Entity);
            return entity != null;
        }

        public virtual int GetNewId()
        {
            if (_uow == null)
                return 0;
            return _uow.GetNewId<TEntity>();
        }

        public ICommandContext DbContext
        {
            get
            {
                return (ICommandContext)_uow;
            }
        }

        public virtual TEntity Find(int Id)
        {
            if (_uow == null)
                return default(TEntity);
            return _uow.FindEntity<TEntity>(Id);
        }

    }
}
