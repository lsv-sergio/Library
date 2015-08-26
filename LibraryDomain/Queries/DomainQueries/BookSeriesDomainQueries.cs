using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal.Factory;
using LibraryDomain.Commands;
using LibraryDomain.Domains;
using LibraryDomain.Core.Links;
using System.Collections.Generic;
using System.Linq;
using LibraryDomain.Queries.LinksQueries;
using LibraryDomain.Queries.LinksQueries.Factory;

namespace LibraryDomain.Queries.DomainQueries
{
    public class BookSeriesDomainQueries : BaseDomainQueries<BookSeriesView, IBookSeriesDomain>, IBookSeriesDomainQueries
    {

        public BookSeriesDomainQueries(IDalQueryFactory QueryFactory, ILinkFacade LinkFactory, IBookSeriesCommands BookSeriesCommands)
            : base(QueryFactory,LinkFactory,BookSeriesCommands)
        {
        }

        public IBookSeriesDomain Create(int Id, string Name, IPublishingHouseLink PublishingHouse)
        {
            return Create(Id, Name, PublishingHouse, new List<IBookLink>(), new List<IAuthorLink>());
        }

        public override IBookSeriesDomain Create()
        {
            return Create(0, "", null);
        }

        protected override IBookSeriesDomain DalToQuery(BookSeriesView Dal)
        {
            if (!CheckServices())
                return null;

            IPublishingHouseLink publishingHouse = _linkFactory.MakeLink<IPublishingHouseLink>(Dal.PublishingHouseId, Dal.PublishingHouseName);

            List<IBookLink> books = _bookDalQuery.GetByBookSeries(Dal.Id).Select(x => _linkFactory.MakeLink<IBookLink>(x.Id, x.Name)).ToList();

            List<IAuthorLink> authors = _authorsStatisticDal.GetByBookSeries(Dal.Id).Select(x => _linkFactory.MakeLink<IAuthorLink>(x.Id,x.Name) ).ToList();

            return Create(Dal.Id, Dal.Name, publishingHouse, books, authors);
        }

        private IBookSeriesDomain Create(int Id, string Name,
            IPublishingHouseLink PublishingHouse,
            IEnumerable<IBookLink> Books, IEnumerable<IAuthorLink> Authors)
        {
            return new BookSeriesDomain(Id, Name, PublishingHouse, Books, Authors, _domainCommands);
        }

    }
}
