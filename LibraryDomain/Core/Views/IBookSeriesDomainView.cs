using LibraryDomain.Core.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryDomain.Core.Views
{
    public interface IBookSeriesDomainView:IBaseDomainView
    {
        IPublishingHouseLink PublishingHouse { get; }
    }
}
