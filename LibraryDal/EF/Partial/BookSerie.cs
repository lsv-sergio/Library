using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDal.EF
{
    public partial class BookSerie : IBaseDal
    {
        public BookSerie(int Id, string Name, int PublishingHouseId)
            : this()
        {
            this.Id = Id;
            this.Name = Name;
            this.PublishingHouseId = PublishingHouseId;
        }
    }
}
