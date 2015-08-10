using LibraryDal.Infrastructure.UnitOfWork;
using LibraryDomain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Commands
{
    public abstract class DomainCommands:IDomainCommands
    {
        public DomainCommands()
        {
        }
        public abstract int Save(BaseDomain Domain);
        public abstract int Delete(BaseDomain Domain);
    }

}
