using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal.Factory;
using LibraryDomain.Core.Views;
using LibraryDomain.Queries.AbstractQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Queries.ViewsQueries
{
    public abstract class BaseViewQueries<TDal, TView> : BaseQueries<TDal, TView>, IViewQueries<TView>
        where TDal : class, IBaseDal
        where TView : IBaseDomainView
    {
        public BaseViewQueries(IDalQueryFactory DalQueryFactory):base(DalQueryFactory)
        {

        }

    }
}
