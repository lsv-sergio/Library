using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal;
using LibraryDal.Infrastructure.QueriesDal.Abstract;
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
    public class BookSeriesLinkQueries : BaseLinkQueries<BookSeriesView, IBookSeriesLink>, IBookSeriesLinkQueries
    {

        public BookSeriesLinkQueries(IDalQueryFactory QueryFactory)
            : base(QueryFactory)
        {
        }

        public IBookSeriesLink Create(int Id, string Name)
        {
            return new BookSeriesLink(Id, Name.Trim());
        }

        public IEnumerable<IBookSeriesLink> GetByPublishingHouseId(int PublishingHouseId)
        {
            IEnumerable<IBookSeriesLink> bookSeriesList = new List<IBookSeriesLink>().AsEnumerable();
            if ( _dalQueries == null)
                return bookSeriesList;
            return _dalQueries.GetAllByFilter(x => x.PublishingHouseId == PublishingHouseId).Select(x => DalToQuery(x));
        }

        protected override IBookSeriesLink DalToQuery(BookSeriesView Dal)
        {
            return Create(Dal.Id, Dal.Name);
        }

    }
}
