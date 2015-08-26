using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Domains.Queries.Stubs
{
    class BookSeriesDalQueryStub : IBookSerieDalQuery, IStub
    {
        private BookSeriesView _bookSeries = new BookSeriesView() { Id = 10, Name = "Book" };
        private IList<BookSeriesView> _bookSeriesList = new List<BookSeriesView>();

        public BookSeriesDalQueryStub()
        {
            _bookSeriesList.Add(_bookSeries);
        }

        public BookSeriesView GetById(int Id)
        {
            _bookSeries.Id = Id;
            return _bookSeries;
        }

        public IEnumerable<BookSeriesView> GetAllByName(string Name)
        {
            return _bookSeriesList;
        }

        public IEnumerable<BookSeriesView> GetAll()
        {
            return _bookSeriesList;
        }

        public BookSeriesView GetByFilter(Func<BookSeriesView, bool> Filter)
        {
            return _bookSeries;
        }

        public IEnumerable<BookSeriesView> GetAllByFilter(Func<BookSeriesView, bool> Filter)
        {
            return _bookSeriesList;
        }

        public IEnumerable<BookSeriesView> GetByAuthor(int AuthorId)
        {
            return _bookSeriesList;
        }

        public IEnumerable<BookSeriesView> GetByBookSeries(int BookSeriesId)
        {
            return _bookSeriesList;
        }

        public IEnumerable<BookSeriesView> GetByPublishingHouse(int PublishingHouseId)
        {
            return _bookSeriesList;
        }
    }
}
