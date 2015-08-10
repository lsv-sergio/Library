using LibraryDal.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace LibraryDal.Infrastructure.QueriesDal
{
    public interface IPublishingHouseDalQuery
    {
        PublishingHouseView GetById(int Id);

        PublishingHouseView GetByFilter(Func<PublishingHouseView, bool> Filter);

        IEnumerable<PublishingHouseView> GetAllByFilter(Func<PublishingHouseView, bool> Filter);

        IEnumerable<PublishingHouseView> GetAll();

    }
}
