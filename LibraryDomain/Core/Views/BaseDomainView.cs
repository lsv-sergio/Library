using LibraryDomain.Core.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Core.Views
{
    public abstract class BaseDomainView : CoreDomain, IBaseDomainView
    {

        public BaseDomainView(int Id, string Name)
            : base(Id, Name)
        {
        }
    }
}
