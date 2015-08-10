using LibraryDal.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryDal.Infrastructure.QueriesDal
{
    public interface IPublishingHousesStatisticDal
    {

        PublishingHouseView GetByBook(int BookId);
        
        PublishingHouseView GetByBookSeries(int BookSeriesId);

        IEnumerable<PublishingHouseView> GetByAuthor(int AuthorId);

    }
}
