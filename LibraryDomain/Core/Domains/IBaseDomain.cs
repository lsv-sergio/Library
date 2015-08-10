using LibraryDomain.Core;
using LibraryDomain.Core.Links;
using LibraryDomain.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Domains
{
    public interface IBaseDomain:ICoreDomain
    {
        void Save();

        void Delete();

        void SetName(string NewName);

    }
}
