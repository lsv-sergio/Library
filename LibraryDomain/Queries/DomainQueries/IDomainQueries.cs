using LibraryDomain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Queries.DomainQueries
{
    public interface IDomainQueries<TDomain> where TDomain : IBaseDomain
    {
        TDomain Create();

        TDomain GetById(int Id);

        IEnumerable<TDomain> GetAll();
    }
}
