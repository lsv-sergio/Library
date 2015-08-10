using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal.Abstract;
using LibraryDal.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDal.Infrastructure.QueriesDal
{
    public class BookDalQuery : DalQuery<BooksView>, IBookDalQuery
    {
        public BookDalQuery(IQueryContext UoW)
            : base(UoW)
        {
        }

        public IEnumerable<BooksView> GetByAuthor(int AuthorId)
        {
            return base.GetAllByFilter(x => x.AuthorId == AuthorId);
        }

        public IEnumerable<BooksView> GetByBookSeries(int BookSeriesId)
        {
            return base.GetAllByFilter(x => x.BookSeriesId == BookSeriesId);
        }

        public IEnumerable<BooksView> GetByPublishingHouse(int PublishingHouseId)
        {
            return base.GetAllByFilter(x => x.PublishingHouseId == PublishingHouseId);
        }
    }
}
