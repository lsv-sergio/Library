using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Domains.Queries.Stubs
{
    class AuthorDalQueryStub : IAuthorDalQuery, IStub
    {
        private AuthorView _author = new AuthorView() { Id = 5, Name = "Author" };
        private IList<AuthorView> _authorsList = new List<AuthorView>();

        public AuthorDalQueryStub()
        {
            _authorsList.Add(_author);
        }

        public AuthorView GetById(int Id)
        {
            _author.Id = Id;
            return _author;
        }

        public IEnumerable<AuthorView> GetAllByName(string Name)
        {
            return GetAllByFilter(x => x.Name.ToLower().Contains(Name.Trim().ToLower()));
        }

        public IEnumerable<AuthorView> GetAll()
        {
            return _authorsList;
        }

        public AuthorView GetByFilter(Func<AuthorView, bool> Filter)
        {
            return _author;
        }

        public IEnumerable<AuthorView> GetAllByFilter(Func<AuthorView, bool> Filter)
        {
            return _authorsList;
        }
    }
}
