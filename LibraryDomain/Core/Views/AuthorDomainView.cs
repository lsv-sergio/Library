using LibraryDomain.Core.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Core.Views
{
    public class AuthorDomainView : BaseDomainView, IAuthorDomainView
    {

        public AuthorDomainView(int Id, string Name):base(Id,Name)
        {
        }
 
    }
}
