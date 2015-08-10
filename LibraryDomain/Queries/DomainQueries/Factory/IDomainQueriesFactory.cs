using LibraryDomain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryDomain.Queries.DomainQueries.Factory
{
    public interface IDomainQueriesFactory
    {
        IDomainQueries<TDomain> GetQuery<TDomain>() where TDomain : IBaseDomain;
    }
}
