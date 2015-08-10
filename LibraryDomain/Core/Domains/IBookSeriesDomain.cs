using LibraryDomain.Core.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryDomain.Domains
{
    public interface IBookSeriesDomain : IBaseDomain 
    {
        IPublishingHouseLink PublishingHouse{ get; }

        IEnumerable<IBookLink> Books { get; }

        IEnumerable<IAuthorLink> Authors { get; }

        void SetPublishingHouse(IPublishingHouseLink PublisingHouse);
    }
}
