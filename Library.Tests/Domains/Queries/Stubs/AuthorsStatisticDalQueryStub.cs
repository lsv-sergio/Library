using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Domains.Queries.Stubs
{
    class AuthorsStatisticDalQueryStub : IAuthorsStatisticDal, IStub
    {
        private AuthorView _author = new AuthorView() { Id = 5, Name = "Author" };
        private IList<AuthorView> _authorsList = new List<AuthorView>();

        public AuthorsStatisticDalQueryStub()
        {
            _authorsList.Add(_author);
        }

        public AuthorView GetByBook(int BookId)
        {
            return _author;
        }

        public IEnumerable<AuthorView> GetByBookSeries(int BookSeriesId)
        {
            return _authorsList;
        }

        public IEnumerable<AuthorView> GetByPblishingHouse(int PublishingHouseId)
        {
            return _authorsList;
        }
    }
}
