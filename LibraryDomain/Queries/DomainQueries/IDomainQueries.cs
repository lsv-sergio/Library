using LibraryDal.EF;
using LibraryDomain.Domains;
using LibraryDomain.Queries.AbstractQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Queries.DomainQueries
{
    public interface IDomainQueries<TDal, TDomain>:IBaseQuery<TDal,TDomain>
        where TDal : class, IBaseDal
        where TDomain : IBaseDomain
    {
        TDomain Create();

        TDomain GetById(int Id);

    }
}
