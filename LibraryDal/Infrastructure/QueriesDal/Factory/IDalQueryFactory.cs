using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryDal.Infrastructure.QueriesDal.Factory
{
    public interface IDalQueryFactory
    {
        TQuery GetQueryByInterface<TQuery>();
        IDalQuery<TQuery> GetQueryByClass<TQuery>() where TQuery : class, IBaseDal;
    }
}
