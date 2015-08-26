using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Domains.Queries.Stubs
{
    class BookDalQueryStub : IBookDalQuery, IStub
    {
        private BooksView _book = new BooksView() { Id = 10, Name = "Book" };
        private IList<BooksView> _booksList = new List<BooksView>();

        public BookDalQueryStub()
        {
            _booksList.Add(_book);
        }

        public BooksView GetById(int Id)
        {
            _book.Id = Id;
            return _book;
        }

        public IEnumerable<BooksView> GetAllByName(string Name)
        {
            return _booksList;
        }

        public IEnumerable<BooksView> GetAll()
        {
            return _booksList;
        }

        public BooksView GetByFilter(Func<BooksView, bool> Filter)
        {
            return _book;
        }

        public IEnumerable<BooksView> GetAllByFilter(Func<BooksView, bool> Filter)
        {
            return _booksList;
        }

        public IEnumerable<BooksView> GetByAuthor(int AuthorId)
        {
            return _booksList;
        }

        public IEnumerable<BooksView> GetByBookSeries(int BookSeriesId)
        {
            return _booksList;
        }

        public IEnumerable<BooksView> GetByPublishingHouse(int PublishingHouseId)
        {
            return _booksList;
        }
    }
}
