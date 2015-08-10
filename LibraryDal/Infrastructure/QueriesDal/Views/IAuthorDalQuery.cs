using LibraryDal.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace LibraryDal.Infrastructure.QueriesDal
{
    public interface IAuthorDalQuery
    {
        AuthorView GetById(int Id);

        AuthorView GetByFilter(Func<AuthorView, bool> Filter);

        IEnumerable<AuthorView> GetAllByFilter(Func<AuthorView, bool> Filter);

        IEnumerable<AuthorView> GetAll();
    }
}
