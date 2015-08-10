using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Core.Links
{
    public class PublishingHouseLink : BaseLink, IPublishingHouseLink
    {
        public PublishingHouseLink(int Id, string Name)
            : base(Id, Name)
        {

        }
    }
}
