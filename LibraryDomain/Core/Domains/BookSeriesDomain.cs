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
    public class BookSeriesDomain : BaseDomain, IBookSeriesDomain
    {
        private IPublishingHouseLink _publishingHouseDomain;
        private IEnumerable<IBookLink> _books;
        private IEnumerable<IAuthorLink> _authors;

        public BookSeriesDomain(int Id, string Name, 
            IPublishingHouseLink PublishingHouseLink,
            IEnumerable<IBookLink> Books,
            IEnumerable<IAuthorLink> Authors,
            IDomainCommands SaveDeleteHandler)
            : base(Id, Name,SaveDeleteHandler)
        {
            _publishingHouseDomain = PublishingHouseLink;
            _books = Books;
            _authors = Authors;
        }

        public IEnumerable<IAuthorLink> Authors
        {
            get 
            {
                if (_authors == null)
                    _authors = new List<IAuthorLink>();

                return _authors; 
            }
        }

        public IPublishingHouseLink PublishingHouse
        {
            get { 
                return _publishingHouseDomain;
            }
        }

        public IEnumerable<IBookLink> Books
        {
            get
            {
                if (_books == null)
                    _books = new List<IBookLink>();

                return _books;
            }
        }

        public void SetPublishingHouse(IPublishingHouseLink PublisingHouseLink)
        {
            _publishingHouseDomain = PublisingHouseLink;
        }

        public override void Save()
        {
            base.Save();
        }

    }
}
