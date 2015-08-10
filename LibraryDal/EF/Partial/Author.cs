using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDal.EF
{
    public partial class Author: IBaseDal
    {
        public Author(int Id, string Name):this()
        {
            this.Id = Id;
            this.Name = Name;
        }

    }
}
