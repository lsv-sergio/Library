using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace LibraryDal.Infrastructure.QueriesDal
{
    public interface IBookDalQuery:IDalQuery<BooksView>
    {

        //BooksView GetById(int Id);

        //BooksView GetByFilter(Func<BooksView, bool> Filter);

        //IEnumerable<BooksView> GetAllByFilter(Func<BooksView, bool> Filter);

        //IEnumerable<BooksView> GetAll();

        IEnumerable<BooksView> GetByAuthor(int AuthorId);

        IEnumerable<BooksView> GetByBookSeries(int BookSeriesId);

        IEnumerable<BooksView> GetByPublishingHouse(int PublishingHouseId);

    }
}
