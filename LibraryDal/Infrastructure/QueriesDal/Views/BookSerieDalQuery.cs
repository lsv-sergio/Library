﻿using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal.Abstract;
using LibraryDal.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDal.Infrastructure.QueriesDal
{
    public class BookSerieDalQuery : DalQuery<BookSeriesView>, IBookSerieDalQuery
    {
        public BookSerieDalQuery(IQueryContext UoW)
            : base(UoW)
        {
        }
    }
}
