using LibraryDomain.Core.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Core.Views
{
    public class BookSeriesDomainView : BaseDomainView, IBookSeriesDomainView
    {
        private IPublishingHouseLink _publishingHouseLink;
      
        public BookSeriesDomainView(int Id, string Name, IPublishingHouseLink PublishingHouse):base(Id,Name)
        {
            this._publishingHouseLink = PublishingHouse;
        }

        public IPublishingHouseLink PublishingHouse 
        { 
            get
            {
                return _publishingHouseLink;
            }
        protected set
            {
                _publishingHouseLink = value;
            }
       }
    }
}
