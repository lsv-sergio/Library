using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDal.EF
{
    public interface IBaseDal
    {
        int Id { get;}
        string Name { get; }
    }
}
