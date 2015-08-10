using LibraryDomain.Core.Links;
using LibraryDomain.Queries.LinksQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Core.Views
{
    public class BookDomainView : BaseDomainView, IBookDomainView
    {
        private int _pageCount;

        private IAuthorLink  _author;

        private IBookSeriesLink _bookSeries;

        private IPublishingHouseLink _publishingHouseLink;

        public BookDomainView(int Id, string Name, int PageCount, IAuthorLink AuthorLink, IBookSeriesLink BookSeriesLink, IPublishingHouseLink PublishingHouseLik)
            : base(Id, Name)
        {
            _pageCount = PageCount;
            this._author = AuthorLink;
            this._bookSeries = BookSeriesLink;
            this._publishingHouseLink = PublishingHouseLik;
        }

        public int PageCount
        {
            get
            {
                return _pageCount;
            }
        }

        public IPublishingHouseLink PublishingHouse
        {
            get { 
                return _publishingHouseLink; 
            }
            protected set 
            { 
                _publishingHouseLink = value; 
            }
        }
        
        public IBookSeriesLink BookSeries
        {
            get 
            { 
                return _bookSeries; 
            }
            protected set
            {
                _bookSeries = value;
            }
        }
        
        public IAuthorLink Author
        {
            get 
            { 
                return _author; 
            }
            protected set
            {
                _author = value;
            }

        }
        
    }
}
