using LibraryDomain.Core.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryDomain.Domains
{
    public interface IPublishingHouseDomain :IBaseDomain
    {
        IEnumerable<IBookLink> Books { get; }

        IEnumerable<IBookSeriesLink> BookSeries { get; }

        IEnumerable<IAuthorLink> Authors { get; }

    }
}
