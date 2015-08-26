using LibraryDomain.Core.Links;
using LibraryDomain.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryDomain.Queries.ViewsQueries
{
    public interface IPublishingHouseViewQueries : IViewQueries<IPublishingHouseDomainView>
    {
        IPublishingHouseDomainView MakeView(int Id, string Name);
    }
}
