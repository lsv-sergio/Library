using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryDomain.Core
{
    public interface ICoreDomain
    {
        int Id { get; }
        string Name { get; }
    }
}
