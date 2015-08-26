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
    public class BookDomainQueriesTest
    {
        IDalQueryFactory _queryFactory = new DalQueryFactoryStub();
        ILinkFacade _linkFactory = new LiknFactoryStub();

        [TestMethod]
        public void CreateMethod_None_ReturnNewBookWithNullLinks()
        {
            IBookDomainQueries booksQueries = new BookDomainQueries(_queryFactory, _linkFactory, null);

            var book = booksQueries.Create();

            Assert.AreEqual(0, book.Id);
            Assert.IsTrue(String.IsNullOrWhiteSpace(book.Name));
            Assert.IsNull(book.Author);
            Assert.IsNull(book.BookSeries);
            Assert.IsNull(book.PublishingHouse);
        }

        [TestMethod]
        public void GetByIdMethod_Id_ReturnExistBookWithNotNullLinks()
        {
            IBookDomainQueries booksQueries = new BookDomainQueries(_queryFactory, _linkFactory, null);

            var book = booksQueries.GetById(5);

            Assert.AreNotEqual(0, book.Id);
            Assert.IsFalse(String.IsNullOrWhiteSpace(book.Name));
            Assert.IsNotNull(book.Author);
            Assert.IsNotNull(book.BookSeries);
            Assert.IsNotNull(book.PublishingHouse);
        }
    }
}
