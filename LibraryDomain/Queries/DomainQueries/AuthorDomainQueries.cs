using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal.Factory;
using LibraryDomain.Commands;
using LibraryDomain.Domains;
using LibraryDomain.Core.Links;
using System.Collections.Generic;
using System.Linq;
using LibraryDomain.Queries.LinksQueries;
using LibraryDomain.Queries.LinksQueries.Factory;
using LibraryDal.Infrastructure.QueriesDal;
using LibraryDal.Infrastructure.QueriesDal.Abstract;

namespace LibraryDomain.Queries.DomainQueries
{
    public class AuthorDomainQueries : BaseDomainQueries<AuthorView, IAuthorDomain>, IAuthorDomainQueries
    {

        public AuthorDomainQueries(IDalQueryFactory QueryFactory, ILinkFacade LinkFactory, IAuthorCommands AuthorCommands)
            : base(QueryFactory, LinkFactory, AuthorCommands)
        {
            if (QueryFactory != null)
                _dalQueries = (IDalQuery<AuthorView>)QueryFactory.GetQueryByInterface<IAuthorDalQuery>();
        }

        public IAuthorDomain Create(int Id, string Name)
        {
            return Create(Id, Name,
                new List<IBookLink>(),
                new List<IBookSeriesLink>(),
                new List<IPublishingHouseLink>());
        }

        public override IAuthorDomain Create()
        {
            return Create(0, "");
        }

        protected override IAuthorDomain DalToQuery(AuthorView Dal)
        {
             if (!CheckServices())
                return null;

           List<IBookLink> books = new List<IBookLink>();
            List<IPublishingHouseLink> publishingHouses = new List<IPublishingHouseLink>();
            List<IBookSeriesLink> bookSeries = new List<IBookSeriesLink>();

            books = _bookDalQuery.GetByAuthor(Dal.Id).Select(x => _linkFactory.MakeLink<IBookLink>(x.Id, x.Name)).ToList();

            bookSeries = _bookSeriesStatisticDal.GetByAuthor(Dal.Id).Select(x => _linkFactory.MakeLink<IBookSeriesLink>(x.Id, x.Name)).ToList();

            publishingHouses = _publishingHousesStatisticDal.GetByAuthor(Dal.Id).Select(x => _linkFactory.MakeLink<IPublishingHouseLink>(x.Id, x.Name)).ToList();

            return Create(Dal.Id, Dal.Name, books, bookSeries, publishingHouses);
        }

        private IAuthorDomain Create(int Id, string Name,
            IEnumerable<IBookLink> Books,
            IEnumerable<IBookSeriesLink> BookSeries,
            IEnumerable<IPublishingHouseLink> PublishingHouses)
        {
            return new AuthorDomain(Id, Name, Books, BookSeries, PublishingHouses, _domainCommands);
        }

    }
}
