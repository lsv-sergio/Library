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
    public class BookSeriesDomainQueriesTest
    {
        IDalQueryFactory _queryFactory = new DalQueryFactoryStub();
        ILinkFacade _linkFactory = new LiknFactoryStub();

        [TestMethod]
        public void CreateMethod_None_ReturnNewBookSeriesWithNullLinksAndEmptyLists()
        {
            IBookSeriesDomainQueries bookSeriesQueries = new BookSeriesDomainQueries(_queryFactory, _linkFactory, null);

            var bookSeries = bookSeriesQueries.Create();

            Assert.AreEqual(0, bookSeries.Id);
            Assert.IsTrue(String.IsNullOrWhiteSpace(bookSeries.Name));
            Assert.AreEqual(0, bookSeries.Authors.Count());
            Assert.AreEqual(0, bookSeries.Books.Count());
            Assert.IsNull(bookSeries.PublishingHouse);
        }

        [TestMethod]
        public void GetByIdMethod_Id_ReturnExistBookSeriesWithNotNullLinksAndNotEmptyLists()
        {
            IBookSeriesDomainQueries bookSeriesQueries = new BookSeriesDomainQueries(_queryFactory, _linkFactory, null);

            var bookSeries = bookSeriesQueries.GetById(5);

            Assert.AreNotEqual(0, bookSeries.Id);
            Assert.IsFalse(String.IsNullOrWhiteSpace(bookSeries.Name));
            Assert.AreNotEqual(0, bookSeries.Authors.Count());
            Assert.AreNotEqual(0, bookSeries.Books.Count());
            Assert.IsNotNull(bookSeries.PublishingHouse);
        }
    }
}
