using LibraryDomain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryDomain.Commands
{
    public interface IDomainCommands
    {
        int Save(BaseDomain Domain);
        int Delete(BaseDomain Domain);
    }
}
