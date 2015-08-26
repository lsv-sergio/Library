using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace LibraryDal.Infrastructure.QueriesDal
{
    public interface IPublishingHouseDalQuery : IDalQuery<PublishingHouseView>
    {
        //PublishingHouseView GetById(int Id);

        //PublishingHouseView GetByFilter(Func<PublishingHouseView, bool> Filter);

        //IEnumerable<PublishingHouseView> GetAllByFilter(Func<PublishingHouseView, bool> Filter);

        //IEnumerable<PublishingHouseView> GetAll();

    }
}
