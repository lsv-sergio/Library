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
    public sealed class AuthorDomain : BaseDomain, IAuthorDomain
    {

        private IEnumerable<IBookLink> _books;
        private IEnumerable<IBookSeriesLink> _bookSeries;
        private IEnumerable<IPublishingHouseLink> _publishingHouses;

        public AuthorDomain(int Id, string Name,
            IEnumerable<IBookLink> Books,
            IEnumerable<IBookSeriesLink> BookSeries,
            IEnumerable<IPublishingHouseLink> PublishingHouses,
            IDomainCommands SaveDeleteHandler)
            : base(Id, Name, SaveDeleteHandler)
        {
            _books = Books;
            _bookSeries = BookSeries;
            _publishingHouses = PublishingHouses;
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

        public IEnumerable<IBookSeriesLink> BookSeries
        {
                            

            get { 
                if (_bookSeries == null)
                    _bookSeries = new List<IBookSeriesLink>();
                return _bookSeries; 
            }
        }

        public IEnumerable<IPublishingHouseLink> PublishingHouses
        {
            get 
            {
                if (_publishingHouses == null)
                    _publishingHouses = new List<IPublishingHouseLink>();


                return _publishingHouses; 
            }
        }

    }
}
