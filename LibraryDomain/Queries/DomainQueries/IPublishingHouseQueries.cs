using LibraryDomain.Domains;
using LibraryDomain.Core.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryDomain.Queries.DomainQueries;
using LibraryDal.EF;

namespace LibraryDomain.Queries.DomainQueries
{
    public interface IPublishingHouseDomainQueries : IDomainQueries<PublishingHouseView, IPublishingHouseDomain>
    {
    }
}
