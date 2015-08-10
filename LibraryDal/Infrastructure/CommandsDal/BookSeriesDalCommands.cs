using LibraryDal.EF;
using LibraryDal.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDal.Infrastructure.CommandsDal
{
    public class BookSeriesDalCommands : DalCommand<BookSerie>, IBookSeriesDalCommands
    {
        public BookSeriesDalCommands(ICommandContext UoW):base(UoW)
        {

        }
    }
}
