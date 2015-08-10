using LibraryDomain.Core.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryDomain.Domains
{
    public interface IBookDomain : IBaseDomain
    {
        int PageCount { get; }

        void SetPageCount(int PageCount);

        IPublishingHouseLink PublishingHouse { get; }

        IBookSeriesLink BookSeries { get; }

        IAuthorLink Author { get; }

        void SetPublishingHouse(IPublishingHouseLink PublishingHouseView);

        void SetBookSeries(IBookSeriesLink BookSeriesView);

        void SetAuthors(IAuthorLink AuthorView);

    }
}
