using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Core.Links
{
    public class BookSeriesLink : BaseLink, IBookSeriesLink
    {
        public BookSeriesLink(int Id, string Name)
            : base(Id, Name)
        {

        }
    }
}
