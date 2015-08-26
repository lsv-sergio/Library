using Library.Tests.Domains.Queries.Stubs;
using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal;
using LibraryDal.Infrastructure.QueriesDal.Abstract;
using LibraryDal.Infrastructure.QueriesDal.Factory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Domains.Queries.Stubs
{
    class DalQueryFactoryStub:IDalQueryFactory
    {
        private Dictionary<Type, IStub> _queries = new Dictionary<Type, IStub>();
        
        public DalQueryFactoryStub()
        {
        
            _queries.Add(typeof(IAuthorDalQuery), new AuthorDalQueryStub());
            _queries.Add(typeof(IPublishingHouseDalQuery), new PublishingHouseDalQueryStub());
            _queries.Add(typeof(IBookDalQuery), new BookDalQueryStub());
            _queries.Add(typeof(IBookSerieDalQuery), new BookSeriesDalQueryStub());
            _queries.Add(typeof(IAuthorsStatisticDal), new AuthorsStatisticDalQueryStub());
            _queries.Add(typeof(IBookSeriesStatisticDal), new BookSeriesStatisticDalBookDalQueryStub());
            _queries.Add(typeof(IPublishingHousesStatisticDal), new PublishingHouseStatisticDalQueryStub());
            _queries.Add(typeof(AuthorView), new AuthorDalQueryStub());
            _queries.Add(typeof(PublishingHouseView), new PublishingHouseDalQueryStub());
            _queries.Add(typeof(BooksView), new BookDalQueryStub());
            _queries.Add(typeof(BookSeriesView), new BookSeriesDalQueryStub());

        }

        public TQuery GetQueryByInterface<TQuery>()
        {
            if (!_queries.Keys.Contains(typeof(TQuery)))
                return default(TQuery);
            return (TQuery)_queries[typeof(TQuery)];
        }

        public IDalQuery<TQuery> GetQueryByClass<TQuery>() where TQuery : class, IBaseDal
        {
            if (!_queries.Keys.Contains(typeof(TQuery)))
                return null;
            return (IDalQuery<TQuery>)_queries[typeof(TQuery)];
        }
    }
}
