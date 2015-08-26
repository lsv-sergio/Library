using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Domains.Queries.Stubs
{
    class PublishingHouseStatisticDalQueryStub : IPublishingHousesStatisticDal, IStub
    {
        private PublishingHouseView _publishingHouse = new PublishingHouseView() { Id = 5, Name = "PublishingHouse" };
        private IList<PublishingHouseView> _publishingHousesList = new List<PublishingHouseView>();

        public PublishingHouseStatisticDalQueryStub()
        {
            _publishingHousesList.Add(_publishingHouse);
        }

        public PublishingHouseView GetByBook(int BookId)
        {
            return _publishingHouse;
        }

        public PublishingHouseView GetByBookSeries(int BookSeriesId)
        {
            return _publishingHouse;
        }

        public IEnumerable<PublishingHouseView> GetByAuthor(int AuthorId)
        {
            return _publishingHousesList;
        }
    }
}
