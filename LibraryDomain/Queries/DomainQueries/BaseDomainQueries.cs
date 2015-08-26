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
    public abstract class BaseDomainQueries<TDal, TDomain> : BaseQueries<TDal, TDomain>, IDomainQueries<TDal,TDomain>
        where TDal : class, IBaseDal
        where TDomain :  IBaseDomain
     {
         protected readonly IPublishingHouseDalQuery _publishingHouseDalQuery;
         protected readonly IBookDalQuery _bookDalQuery;
         protected readonly IAuthorsStatisticDal _authorsStatisticDal;
         protected readonly IBookSeriesStatisticDal _bookSeriesStatisticDal;
         protected readonly IPublishingHousesStatisticDal _publishingHousesStatisticDal;

         protected readonly ILinkFacade _linkFactory;

         protected readonly IDomainCommands _domainCommands;

         public BaseDomainQueries(IDalQueryFactory DalQueryFactory, ILinkFacade LinkFactory, IDomainCommands DomainCommands):base(DalQueryFactory)
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

         public abstract TDomain Create();

         public TDomain GetById(int Id)
         {
             if (Id <= 0)
                 return default(TDomain);
             if (_dalQueries == null)
                 return default(TDomain);
             return DalToQuery(_dalQueries.GetById(Id));
         }

         protected bool CheckServices()
         {
             return _publishingHouseDalQuery != null && _bookDalQuery != null
                 && _authorsStatisticDal != null && _bookSeriesStatisticDal != null;
         }
    }
}
