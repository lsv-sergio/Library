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
    public class AuthorsStatisticDal : DalQuery<AuthorsStatistic>, IAuthorsStatisticDal 
    {
        public AuthorsStatisticDal(IQueryContext UoW):base(UoW)
        {
        }

        public AuthorView GetByBook(int BookId)
        {
            AuthorsStatistic authorStatistic = base.GetByFilter(x => x.BookId == BookId);
            return StatisticToView(authorStatistic);
        }

        public IEnumerable<AuthorView> GetByBookSeries(int BookSeriesId)
        {
            IEnumerable<AuthorView> authorsStatistic = base.GetAllByFilter(x => x.BookSeriesId == BookSeriesId).GroupBy(x => x.Id).Select(x => StatisticToView(x.FirstOrDefault()));
            return authorsStatistic;
        }

        public IEnumerable<AuthorView> GetByPblishingHouse(int PublishingHouseId)
        {
            IEnumerable<AuthorView> authorsStatistic = base.GetAllByFilter(x => x.PublishingHouseId == PublishingHouseId).GroupBy(x => x.Id).Select(x => StatisticToView(x.FirstOrDefault()));
            return authorsStatistic;
        }

        private AuthorView StatisticToView(AuthorsStatistic AuthorStatistic)
        {
            AuthorView author = null;
            if (AuthorStatistic != null)
                author = new AuthorView() { Id = AuthorStatistic.Id, Name = AuthorStatistic.Name };
            return author;
        }

    }
}
