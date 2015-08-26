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
    public class BookDomainTest
    {
        IBookDomainQueries _queries;

        [TestInitialize]
        public void Init()
        {
            var moqQuery = new Mock<IBookDomainQueries>();
            moqQuery.Setup(x => x.Create()).Returns(new BookDomain(0, "", 200, new AuthorLink(10, "new author"), new BookSeriesLink(30, "new book series"), new PublishingHouseLink(50, "new house"), null));

            _queries = moqQuery.Object;
        }

        [TestMethod]
        public void SetPublishingHouseMethod_PublishingHouseLink_SetNewPublishingHouse()
        {
            var book = _queries.Create();

            book.SetPublishingHouse(new PublishingHouseLink(54, "new house"));

            Assert.AreEqual(54, book.PublishingHouse.Id);
            Assert.AreEqual("new house", book.PublishingHouse.Name);

        }

        [TestMethod]
        public void SetPublishingHouseMethod_null_DoNothing()
        {
            var book = _queries.Create();

            book.SetPublishingHouse(null);

            Assert.IsNotNull(book.PublishingHouse);

        }
        
        [TestMethod]
        public void SetAuthorsMethod_AuthorLink_SetNewAuthor()
        {
            var book = _queries.Create();

            book.SetAuthors(new AuthorLink(20, "new author1"));

            Assert.AreEqual(20, book.Author.Id);
            Assert.AreEqual("new author1", book.Author.Name);

        }

        [TestMethod]
        public void SetAuthorsMethod_null_DoNothing()
        {
            var book = _queries.Create();

            book.SetAuthors(null);

            Assert.IsNotNull(book.Author);

        }

        [TestMethod]
        public void SetBookSeriesMethod_BookSeriesLink_SetNewBookSeries()
        {
            var book = _queries.Create();

            book.SetBookSeries(new BookSeriesLink(44, "BookSeries1"));

            Assert.AreEqual(44, book.BookSeries.Id);
            Assert.AreEqual("BookSeries1", book.BookSeries.Name);

        }

        [TestMethod]
        public void SetBookSeriesMethod_null_SetNewBookSeriesInNull()
        {
            var book = _queries.Create();

            book.SetPublishingHouse(null);

            Assert.IsNotNull(book.BookSeries);

        }

        [TestMethod]
        public void SetPageCountMethod_PageCount_SetNewPageCount()
        {
            var book = _queries.Create();

            book.SetPageCount(500);

            Assert.AreEqual(500, book.PageCount);

        }

        [TestMethod]
        public void SetPageCountMethod_ZeroOrLessZero_DoNothing()
        {
            var book = _queries.Create();

            book.SetPageCount(0);

            Assert.AreEqual(200, book.PageCount);

        }

        [TestMethod]
        public void SetPageCountMethod_LessZero_DoNothing()
        {
            var book = _queries.Create();

            book.SetPageCount(-100);

            Assert.AreEqual(200, book.PageCount);

        }

    }
}
