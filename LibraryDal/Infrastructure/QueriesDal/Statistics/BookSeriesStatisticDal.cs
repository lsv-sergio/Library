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
    public class BookSeriesStatisticDal : DalQuery<BookSeriesStatistic>, IBookSeriesStatisticDal
    {

        public BookSeriesStatisticDal(IQueryContext UoW):base(UoW)
        {
        }

        public IEnumerable<BookSeriesView> GetByAuthor(int AuthorId)
        {
            IEnumerable<BookSeriesView> bookSeriesViewsList = base.GetAllByFilter(x => x.AuthorId == AuthorId).GroupBy(x => x.Id).Select(x => StatisticToView(x.FirstOrDefault()));
            return bookSeriesViewsList;
        }

        public BookSeriesView GetByBook(int BookId)
        {
            BookSeriesStatistic bookSeriesView = base.GetByFilter(x => x.BookId == BookId);
            return StatisticToView(bookSeriesView);
        }

        public IEnumerable<BookSeriesView> GetByPublishingHouse(int PublisherHouseId)
        {
            IEnumerable<BookSeriesView> bookSeriesViews = 
                base.GetAllByFilter(x => x.PublishingHouseId == PublisherHouseId).GroupBy(x => x.Id).Select(x => StatisticToView(x.FirstOrDefault()));
            return bookSeriesViews;
        }

        private BookSeriesView StatisticToView(BookSeriesStatistic BookSeriesStatistic)
        {
            BookSeriesView bookSeriesView = null;
            if (BookSeriesStatistic != null)
                bookSeriesView = new BookSeriesView()
                {
                    Id = BookSeriesStatistic.Id,
                    Name = BookSeriesStatistic.Name,
                    PublishingHouseId = BookSeriesStatistic.PublishingHouseId,
                    PublishingHouseName = BookSeriesStatistic.PublishingHouseName
                };
            return bookSeriesView;
        }

    }
}
