using LibraryDomain.Core.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryDomain.Queries.LinksQueries
{
    public interface IBookSeriesLinkQueries:ILinkQueries<IBookSeriesLink>
    {
        IEnumerable<IBookSeriesLink> GetByPublishingHouseId(int PublishingHouseId);
    }
}
