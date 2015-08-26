using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal.Factory;
using LibraryDomain.Commands;
using LibraryDomain.Domains;
using LibraryDomain.Core.Links;
using LibraryDomain.Queries.LinksQueries;
using LibraryDomain.Queries.LinksQueries.Factory;
using LibraryDal.Infrastructure.QueriesDal;
using LibraryDal.Infrastructure.QueriesDal.Abstract;

namespace LibraryDomain.Queries.DomainQueries
{
    public class BookDomainQueries : BaseDomainQueries<BooksView, IBookDomain>, IBookDomainQueries
    {

        public BookDomainQueries(IDalQueryFactory QueryFactory, ILinkFacade LinkFactory, IBookCommands BookCommands)
            : base(QueryFactory, LinkFactory, BookCommands)
        {
            if (QueryFactory != null)
                _dalQueries = (IDalQuery<BooksView>)QueryFactory.GetQueryByInterface<IBookDalQuery>();
        }

        public IBookDomain Create(int Id, string Name, int PageCount,
            IAuthorLink Author,
            IBookSeriesLink BookSeries,
            IPublishingHouseLink PublishingHouse)
        {
            if (!CheckServices())
                return null;
            return new BookDomain(Id, Name, PageCount, Author, BookSeries, PublishingHouse, _domainCommands);
        }

        public override IBookDomain Create()
        {
            return Create(0, "", 0, null, null, null);
        }

        protected override IBookDomain DalToQuery(BooksView Dal)
        {
            if (_linkFactory == null)
                return null;

            IAuthorLink author = null;
            IBookSeriesLink bookSeries = null;
            IPublishingHouseLink publishingHouse = null;

            author = _linkFactory.MakeLink<IAuthorLink>(Dal.AuthorId, Dal.AuthorName);
            bookSeries = _linkFactory.MakeLink<IBookSeriesLink>(Dal.BookSeriesId, Dal.BookSeriesName);
            publishingHouse = _linkFactory.MakeLink<IPublishingHouseLink>(Dal.PublishingHouseId, Dal.PublishingHouseName);

            return Create(Dal.Id, Dal.Name, Dal.PageNumber, author, bookSeries, publishingHouse);
        }

    }
}
