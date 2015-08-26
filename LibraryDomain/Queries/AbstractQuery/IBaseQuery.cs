using LibraryDal.EF;
using LibraryDomain.Core;
using LibraryDomain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryDomain.Queries.AbstractQuery
{
    public interface IBaseQuery<TDal, TQuery>
        where TDal : class, IBaseDal
        where TQuery : ICoreDomain
    {

        IEnumerable<TQuery> GetAll();

        IEnumerable<TQuery> GetAllByFilter(Func<TDal, bool> Filter);

        TQuery GetByFilter(Func<TDal, bool> Filter);
    }
}
