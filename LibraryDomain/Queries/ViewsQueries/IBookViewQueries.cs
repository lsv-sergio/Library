using LibraryDomain.Core.Links;
using LibraryDomain.Core.Views;
using LibraryDomain.Queries.AbstractQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryDomain.Queries.ViewsQueries
{
    public interface IBookViewQueries:IViewQueries<IBookDomainView>
    {
        IBookDomainView MakeView(int Id, string Name, int PageCount, IAuthorLink Author, IBookSeriesLink BookSeries, IPublishingHouseLink PublishingHouse);
    }
}
