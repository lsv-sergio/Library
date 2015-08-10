using LibraryDal.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace LibraryDal.Infrastructure.QueriesDal
{
    public interface IBookSerieDalQuery 
    {
        BookSeriesView GetById(int Id);

        BookSeriesView GetByFilter(Func<BookSeriesView, bool> Filter);

        IEnumerable<BookSeriesView> GetAllByFilter(Func<BookSeriesView, bool> Filter);

        IEnumerable<BookSeriesView> GetAll();

    }
}
