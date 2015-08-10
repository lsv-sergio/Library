using LibraryDal.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryDal.Infrastructure.QueriesDal
{
    public interface IAuthorsStatisticDal
    {
        AuthorView GetByBook(int BookId);

        IEnumerable<AuthorView> GetByBookSeries(int BookSeriesId);

        IEnumerable<AuthorView> GetByPblishingHouse(int PublishingHouseId);
    }
}
