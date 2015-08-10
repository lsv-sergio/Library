using LibraryDomain.Core.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryDomain.Core.Views
{
    public interface IBookDomainView : IBaseDomainView
    {
        int PageCount { get; }

        IPublishingHouseLink PublishingHouse { get; }

        IBookSeriesLink BookSeries { get; }

        IAuthorLink Author { get; }
    }
}
