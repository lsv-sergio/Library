using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Domains.Queries.Stubs
{
    class BookSeriesStatisticDalBookDalQueryStub : IBookSeriesStatisticDal, IStub
    {
        private BookSeriesView _bookSeries = new BookSeriesView() { Id = 10, Name = "BookSeries" };
   
        private IList<BookSeriesView> _bookSeriesList = new List<BookSeriesView>();

        public BookSeriesStatisticDalBookDalQueryStub()
        {
            _bookSeriesList.Add(_bookSeries);
        }

        public IEnumerable<BookSeriesView> GetByAuthor(int AuthorId)
        {
            return _bookSeriesList;
        }

        public BookSeriesView GetByBook(int BookId)
        {
            return _bookSeries;
        }

        public IEnumerable<BookSeriesView> GetByPublishingHouse(int PublisherHouseId)
        {
            return _bookSeriesList;
        }
    }
}
