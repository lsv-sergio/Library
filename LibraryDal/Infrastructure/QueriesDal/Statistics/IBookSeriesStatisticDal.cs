using LibraryDal.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryDal.Infrastructure.QueriesDal
{
    public interface IBookSeriesStatisticDal
    {
        IEnumerable<BookSeriesView> GetByAuthor(int AuthorId);

        BookSeriesView GetByBook(int BookId);

        IEnumerable<BookSeriesView> GetByPublishingHouse(int PublisherHouseId);

    }
}
