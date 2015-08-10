using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal;
using LibraryDal.Infrastructure.QueriesDal.Factory;
using LibraryDomain.Commands;
using LibraryDomain.Core.Links;
using LibraryDomain.Domains;
using LibraryDomain.Queries.AbstractQuery;
using LibraryDomain.Queries.LinksQueries;
using LibraryDomain.Queries.LinksQueries.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Queries.DomainQueries
{
    public abstract class BaseDomainQueries<TDal, TQuery> : BaseQueries<TDal, TQuery> 
        where TDal : class, IBaseDal
        where TQuery :  IBaseDomain
     {
         protected readonly IPublishingHouseDalQuery _publishingHouseDalQuery;
         protected readonly IBookDalQuery _bookDalQuery;
         protected readonly IAuthorsStatisticDal _authorsStatisticDal;
         protected readonly IBookSeriesStatisticDal _bookSeriesStatisticDal;
         protected readonly IPublishingHousesStatisticDal _publishingHousesStatisticDal;

         protected readonly ILinkFactory _linkFactory;

         protected readonly IDomainCommands _domainCommands;

         public BaseDomainQueries(IDalQueryFactory DalQueryFactory, ILinkFactory LinkFactory, IDomainCommands DomainCommands):base(DalQueryFactory)
         {

             if (DalQueryFactory != null)
             {
                 _publishingHouseDalQuery = DalQueryFactory.GetQueryByInterface<IPublishingHouseDalQuery>();
                 _bookDalQuery = DalQueryFactory.GetQueryByInterface<IBookDalQuery>();
                 _authorsStatisticDal = DalQueryFactory.GetQueryByInterface<IAuthorsStatisticDal>();
                 _bookSeriesStatisticDal = DalQueryFactory.GetQueryByInterface<IBookSeriesStatisticDal>();
                 _publishingHousesStatisticDal = DalQueryFactory.GetQueryByInterface<IPublishingHousesStatisticDal>();
             }

             _linkFactory = LinkFactory;

             _domainCommands = DomainCommands;
         }

        protected bool CheckServices()
         {
             return _publishingHouseDalQuery != null && _bookDalQuery != null
                 && _authorsStatisticDal != null && _bookSeriesStatisticDal != null;
         }
    }
}
