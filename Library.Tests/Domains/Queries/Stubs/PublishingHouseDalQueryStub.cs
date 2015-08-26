using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Domains.Queries.Stubs
{
    class PublishingHouseDalQueryStub : IPublishingHouseDalQuery, IStub
    {
        private PublishingHouseView _publishingHouse = new PublishingHouseView() { Id = 5, Name = "PublishingHouse" };
        private IList<PublishingHouseView> _publishingHousesList = new List<PublishingHouseView>();

        public PublishingHouseDalQueryStub()
        {
            _publishingHousesList.Add(_publishingHouse);
        }

        public PublishingHouseView GetById(int Id)
        {
            _publishingHouse.Id = Id;
            return _publishingHouse;
        }

        public IEnumerable<PublishingHouseView> GetAllByName(string Name)
        {
            return _publishingHousesList;
        }

        public IEnumerable<PublishingHouseView> GetAll()
        {
            return _publishingHousesList;
        }

        public PublishingHouseView GetByFilter(Func<PublishingHouseView, bool> Filter)
        {
            return _publishingHouse;
        }

        public IEnumerable<PublishingHouseView> GetAllByFilter(Func<PublishingHouseView, bool> Filter)
        {
            return _publishingHousesList;
        }
    }
}
