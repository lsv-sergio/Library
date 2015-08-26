using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryDomain.Queries.DomainQueries;
using Moq;
using LibraryDomain.Domains;
using LibraryDomain.Core.Links;
using System.Collections.Generic;
using System.Linq;

namespace Library.Tests.Domains.Core.Domains
{
    [TestClass]
    public class BookSeriesDomainTest
    {
        IBookSeriesDomainQueries _queries;

        [TestInitialize]
        public void Init()
        {
            var moqQuery = new Mock<IBookSeriesDomainQueries>();
            moqQuery.Setup(x => x.Create()).Returns(new BookSeriesDomain(0, "", null, new List<IBookLink>(), new List<IAuthorLink>(),null));

            _queries = moqQuery.Object;
        }

        [TestMethod]
        public void NewBookSeries_Has_Not_BooksLink()
        {
            var bookSeries = _queries.Create();

            Assert.AreEqual(0, bookSeries.Books.Count());
        }

        [TestMethod]
        public void NewBookSeries_Has_Not_BooksList()
        {
            var bookSeries = _queries.Create();

            Assert.AreEqual(0, bookSeries.Books.Count());
        }

        [TestMethod]
        public void NewBookSeries_Has_Not_PublishingHouse()
        {
            var bookSeries = _queries.Create();

            Assert.IsNull(bookSeries.PublishingHouse);
        }

        [TestMethod]
        public void SetPublishingHouseMethod_PublishingHoiseLink_SetNewPublishingHouse()
        {
            var bookSeries = _queries.Create();

            bookSeries.SetPublishingHouse(new PublishingHouseLink(50, "new house"));
            bookSeries.SetPublishingHouse(new PublishingHouseLink(54, "new house"));

            Assert.AreEqual(54, bookSeries.PublishingHouse.Id);
            Assert.AreEqual("new house", bookSeries.PublishingHouse.Name);

        }

        [TestMethod]
        public void SetPublishingHouseMethod_null_DoNothing()
        {
            var bookSeries = _queries.Create();

            bookSeries.SetPublishingHouse(new PublishingHouseLink(54, "new house"));
            bookSeries.SetPublishingHouse(null);

            Assert.AreEqual(54, bookSeries.PublishingHouse.Id);
            Assert.AreEqual("new house", bookSeries.PublishingHouse.Name);

        }
    }
}
