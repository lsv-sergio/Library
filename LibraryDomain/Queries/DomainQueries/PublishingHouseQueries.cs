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
    public class PublishingHouseDomainQueries : BaseDomainQueries<PublishingHouseView, IPublishingHouseDomain>, IPublishingHouseDomainQueries
    {
        public PublishingHouseDomainQueries(IDalQueryFactory QueryFactory, ILinkFactory LinkFactory, IPublishingHouseCommands PublishingHouseCommands)
            : base(QueryFactory, LinkFactory, PublishingHouseCommands)
        {
        }

        public IPublishingHouseDomain Create(int Id, string Name)
        {
            return Create(Id, Name, new List<IBookLink>(), new List<IBookSeriesLink>(), new List<IAuthorLink>());
        }

        public IPublishingHouseDomain Create()
        {
            return Create(0, "");
        }

        protected override IPublishingHouseDomain DalToQuery(PublishingHouseView Dal)
        {
            if (!CheckServices())
                return null;

            List<IBookLink> books = 
                _bookDalQuery.GetByPublishingHouse(Dal.Id).Select(x => _linkFactory.CreateLink<IBookLink>(x.Id,x.Name)).ToList();

            List<IBookSeriesLink> bookSeries =
                _bookSeriesStatisticDal.GetByPublishingHouse(Dal.Id).Select(x => _linkFactory.CreateLink<IBookSeriesLink>(x.Id, x.Name)).ToList();

            List<IAuthorLink> authors =
                _authorsStatisticDal.GetByPblishingHouse(Dal.Id).Select(x => _linkFactory.CreateLink<IAuthorLink>(x.Id, x.Name)).ToList();

            return Create(Dal.Id, Dal.Name, books, bookSeries, authors);
        }

        private IPublishingHouseDomain Create(int Id, string Name,
            IEnumerable<IBookLink> Books,
            IEnumerable<IBookSeriesLink> BookSeries,
            IEnumerable<IAuthorLink> Authors)
        {
            return new PublishingHouseDomain(Id, Name, Books, BookSeries, Authors, _domainCommands);
        }

    }
}
