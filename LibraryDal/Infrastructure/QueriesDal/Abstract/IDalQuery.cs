using LibraryDal.EF;
using LibraryDal.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDal.Infrastructure.QueriesDal.Abstract
{
    public interface IDalQuery<TEntity> where TEntity : class, IBaseDal
    {

        TEntity GetById(int Id);

        IEnumerable<TEntity> GetAllByName(string Name);

        IEnumerable<TEntity> GetAll();

        TEntity GetByFilter(Func<TEntity, bool> Filter);

        IEnumerable<TEntity> GetAllByFilter(Func<TEntity, bool> Filter);

    }
}
