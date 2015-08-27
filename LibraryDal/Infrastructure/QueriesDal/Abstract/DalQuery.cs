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

namespace LibraryDal.Infrastructure.QueriesDal.Abstract
{
    public abstract class DalQuery<TEntity>:IDalQuery<TEntity> where TEntity :  class, IBaseDal
    {
        private readonly IQueryContext _uow;

        public DalQuery(IQueryContext UoW)
        {
            _uow = UoW;
        }

        public TEntity GetById(int EntityId)
        {
            if (EntityId < 1 || _uow == null)
                return null;

            return GetByFilter(x => x.Id == EntityId);
        }

        public IEnumerable<TEntity> GetAllByName(string EntityName)
        {
            if(String.IsNullOrWhiteSpace(EntityName))
                return new List<TEntity>().AsEnumerable();

            if (_uow == null)
                return new List<TEntity>().AsEnumerable();

            return this.GetAllByFilter(x => x.Name.ToLower().Trim().Contains(EntityName.ToLower().Trim())).ToList().AsReadOnly();
        }

        public TEntity GetByFilter(Func<TEntity, bool> Filter)
        {
            if (Filter == null || _uow == null)
                return null;

            return _uow.GetEntity<TEntity>(Filter);
        }

        public IEnumerable<TEntity> GetAllByFilter(Func<TEntity, bool> Filter)
        {
            if (_uow == null || Filter == null)
                return new List<TEntity>().AsEnumerable();

            return _uow.GetEntities<TEntity>(Filter);
        }

        public IEnumerable<TEntity> GetAll()
        {
            if (_uow == null)
                return new List<TEntity>().AsEnumerable();
 
            return _uow.GetEntities<TEntity>();
        }
    }
}
