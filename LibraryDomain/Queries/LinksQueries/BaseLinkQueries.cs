using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal.Factory;
using LibraryDomain.Core.Links;
using LibraryDomain.Queries.AbstractQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Queries.LinksQueries
{
    public abstract class BaseLinkQueries<TDal, TLink> : BaseQueries<TDal, TLink>, ILinkQueries<TLink>
        where TDal : class, IBaseDal
        where TLink : ILink
    {
        public BaseLinkQueries(IDalQueryFactory QueryFactory):base(QueryFactory)
        {
        }

        public abstract TLink MakeLink(int Id, string Name);

        public TLink GetById(int Id)
        {
            if (Id <= 0)
                return default(TLink);
            if (_dalQueries == null)
                return default(TLink);
            return DalToQuery(_dalQueries.GetById(Id));
        }


    }
}
