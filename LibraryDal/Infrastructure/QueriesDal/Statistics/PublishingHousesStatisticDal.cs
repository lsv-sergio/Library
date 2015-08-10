using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal.Abstract;
using LibraryDal.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDal.Infrastructure.QueriesDal
{
    public class PublishingHousesStatisticDal : DalQuery<PublishingHousesStatistic>, IPublishingHousesStatisticDal
    {

        public PublishingHousesStatisticDal(IQueryContext UoW)
            : base(UoW)
        {
        }

        public PublishingHouseView GetByBook(int BookId)
        {
            PublishingHousesStatistic publishingHousesStatistic = base.GetByFilter(x => x.BookId == BookId);
            return StatisticToView(publishingHousesStatistic);
        }

        public PublishingHouseView GetByBookSeries(int BookSeriesId)
        {
            PublishingHousesStatistic publishingHousesStatistic = base.GetByFilter(x => x.BookSeriesId == BookSeriesId);
            return StatisticToView(publishingHousesStatistic);
        }

        public IEnumerable<PublishingHouseView> GetByAuthor(int AuthorId)
        {
            IEnumerable<PublishingHouseView> publishingHousesStatistics = base.GetAllByFilter(x => x.AuthorId == AuthorId).GroupBy(x => x.Id).Select(x => StatisticToView(x.FirstOrDefault()));
            return publishingHousesStatistics;
        }

        private PublishingHouseView StatisticToView(PublishingHousesStatistic PublishingHousesStatistic)
        {
            PublishingHouseView publishingHouseView = null;
            if (PublishingHousesStatistic != null)
                publishingHouseView = new PublishingHouseView() { Id = PublishingHousesStatistic.Id, Name = PublishingHousesStatistic.Name };
            return publishingHouseView;
        }

    }
}
