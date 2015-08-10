using LibraryDal.EF;
using LibraryDal.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDal.Infrastructure.CommandsDal
{
    public class BookDalCommands:DalCommand<Book>, IBookDalCommands
    {
        public BookDalCommands(ICommandContext UoW):base(UoW)
        {

        }
    }
}
