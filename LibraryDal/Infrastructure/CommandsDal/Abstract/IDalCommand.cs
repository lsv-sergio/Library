using LibraryDal.EF;
using LibraryDal.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDal.Infrastructure.CommandsDal
{
    public interface IDalCommand<TEntity> where TEntity : class, IBaseDal
    {
        TEntity Find(int Id);

        bool Add(TEntity Author);

        bool Update(TEntity Author);

        bool Delete(TEntity Author);

        ICommandContext DbContext { get; }

        int GetNewId();

    }
}
