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
    public class PublishingHouseDomain : BaseDomain, IPublishingHouseDomain
    {

        private IEnumerable<IBookLink> _books;
        private IEnumerable<IBookSeriesLink> _bookSeries;
        private IEnumerable<IAuthorLink> _authors;

        public PublishingHouseDomain(int Id, string Name,
            IEnumerable<IBookLink> Books,
            IEnumerable<IBookSeriesLink> BookSeries,
            IEnumerable<IAuthorLink> Authors,
            IDomainCommands SaveDeleteHandler)
            : base(Id, Name, SaveDeleteHandler)
        {
            _books = Books;
            _bookSeries = BookSeries;
            _authors = Authors;

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
            get
            {
                if (_bookSeries == null)
                    _bookSeries = new List<IBookSeriesLink>();

                return _bookSeries;
            }
        }

        public IEnumerable<IAuthorLink> Authors 
        {
            get { return _authors; }
        }

    }
}
