using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryDomain.Queries.DomainQueries;
using Moq;
using LibraryDomain.Domains;
using LibraryDomain.Core.Links;
using System.Collections.Generic;
using System.Linq;
using LibraryDal.Infrastructure.QueriesDal.Factory;
using LibraryDomain.Queries.LinksQueries.Factory;
using LibraryDomain.Commands;
using LibraryDal.Infrastructure.QueriesDal;
using LibraryDal.EF;
using Library.Tests.Domains.Queries.Stubs;

namespace Library.Tests.Domains.Queries.DomainsQueries
{
    [TestClass]
    public class PublishingHouseDomainQueriesTest
    {
        IDalQueryFactory _queryFactory = new DalQueryFactoryStub();
        ILinkFacade _linkFactory = new LiknFactoryStub();

        [TestMethod]
        public void CreateMethod_None_ReturnNewPublishingHouseWithEmptyLists()
        {
            IPublishingHouseDomainQueries publishingHouseQueries = new PublishingHouseDomainQueries(_queryFactory, _linkFactory, null);

            var publishingHouse = publishingHouseQueries.Create();

            Assert.AreEqual(0, publishingHouse.Id);
            Assert.IsTrue(String.IsNullOrWhiteSpace(publishingHouse.Name));
            Assert.AreEqual(0, publishingHouse.Books.Count());
            Assert.AreEqual(0, publishingHouse.BookSeries.Count());
            Assert.AreEqual(0, publishingHouse.Authors.Count());
        }

        [TestMethod]
        public void GetByIdMethod_Id_ReturnExistPublishingHouse()
        {
            IPublishingHouseDomainQueries publishingHouseQueries = new PublishingHouseDomainQueries(_queryFactory, _linkFactory, null);

            var publishingHouse = publishingHouseQueries.GetById(2);

            Assert.AreNotEqual(0, publishingHouse.Id);
            Assert.IsFalse(String.IsNullOrWhiteSpace(publishingHouse.Name));
            Assert.AreNotEqual(0, publishingHouse.Books.Count());
            Assert.AreNotEqual(0, publishingHouse.BookSeries.Count());
            Assert.AreNotEqual(0, publishingHouse.Authors.Count());

        }

    }
}
