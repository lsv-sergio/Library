using LibraryDomain.Commands;
using LibraryDomain.Core.Links;
using LibraryDomain.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Domains
{
    public class BookDomain : BaseDomain, IBookDomain
    {
        private IPublishingHouseLink _publishingHouses;
        private IBookSeriesLink _bookSeries;
        private IAuthorLink _author;
        private int _pageCount;

        public BookDomain(int Id, string Name, int PageCount, 
            IAuthorLink AuthorLink,
            IBookSeriesLink BookSeriesLink,
            IPublishingHouseLink PublishingHouseLink,
             IDomainCommands SaveDeleteHandler)
            : base(Id, Name, SaveDeleteHandler)
        {
            _publishingHouses = PublishingHouseLink;
            _bookSeries = BookSeriesLink;
            _pageCount = PageCount;
            _author = AuthorLink;
        }

        public int PageCount
        {
            get { 
                return _pageCount; 
            }
        }

        public IPublishingHouseLink PublishingHouse
        {
            get
            {
                return _publishingHouses;
            }
        }

        public IBookSeriesLink BookSeries
        {
            get
            {
                return _bookSeries;
            }
        }

        public IAuthorLink Author
        {
            get
            {
                return _author;
            }
        }

        public void SetPageCount(int PageCount)
        {
            _pageCount = PageCount;
        }

        public void SetPublishingHouse(IPublishingHouseLink PublishingHouseView)
        {
            _publishingHouses = PublishingHouseView;
        }

        public void SetBookSeries(IBookSeriesLink BookSeriesView)
        {
            _bookSeries = BookSeriesView;
        }

        public void SetAuthors(IAuthorLink AuthorView)
        {
            _author = AuthorView;
        }

        public override void Save()
        {
            base.Save();
        } 

    }
}
