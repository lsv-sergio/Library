using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryDomain.Core.Links
{
    public abstract class BaseLink: CoreDomain, ILink
    {
        public BaseLink(int Id,string Name):base(Id, Name)
        {
        }

    }
}
