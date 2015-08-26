using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal;
using LibraryDal.Infrastructure.QueriesDal.Factory;
using LibraryDomain.Core.Links;
using System.Collections.Generic;
using LibraryDal.Infrastructure.QueriesDal.Abstract;
using LibraryDomain.Queries.AbstractQuery;
using LibraryDomain.Core.Views;
using LibraryDomain.Queries.LinksQueries;
using LibraryDomain.Queries.LinksQueries.Factory;

namespace LibraryDomain.Queries.ViewsQueries
{
    public class BookViewQueries : BaseViewQueries<BooksView, IBookDomainView>, IBookViewQueries
    {
        private readonly ILinkFacade _linkFactory;

        public BookViewQueries(IDalQueryFactory QueryFactory, ILinkFacade LinkFactory)
            : base(QueryFactory)
        {
            if (LinkFactory != null)
                _linkFactory = LinkFactory;
        }

        public IBookDomainView MakeView(int Id, string Name, int PageCount, IAuthorLink Author, IBookSeriesLink BookSeries, IPublishingHouseLink PublishingHouse)
        {
            if (!IsValidValues(Id, Name))
                return null;
            return new BookDomainView(Id, Name, PageCount, Author, BookSeries, PublishingHouse);
        }

        protected override IBookDomainView DalToQuery(BooksView Dal)
        {
            if (Dal == null)
                return null;
            return MakeView(
                Dal.Id, Dal.Name, Dal.PageNumber,
                _linkFactory.MakeLink<IAuthorLink>(Dal.AuthorId, Dal.AuthorName),
                _linkFactory.MakeLink<IBookSeriesLink>(Dal.BookSeriesId, Dal.BookSeriesName),
                _linkFactory.MakeLink<IPublishingHouseLink>(Dal.PublishingHouseId, Dal.PublishingHouseName)
                );
        }

    }
}
