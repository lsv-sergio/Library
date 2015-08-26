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
    public class AuthorDomainQueriesTest
    {
        IDalQueryFactory _queryFactory = new DalQueryFactoryStub();
        ILinkFacade _linkFactory = new LiknFactoryStub();

        [TestInitialize]
        public void Init()
        {
            var moqAuthorCommands = new Mock<IAuthorCommands>();
        }

        [TestMethod]
        public void CreateMethod_None_ReturnNewAuthorWithEmptyLists()
        {
            IAuthorDomainQueries authorsQueries = new AuthorDomainQueries(_queryFactory, _linkFactory, null);

            var author = authorsQueries.Create();

            Assert.AreEqual(0, author.Id);
            Assert.IsTrue(String.IsNullOrWhiteSpace(author.Name));
            Assert.AreEqual(0, author.Books.Count());
            Assert.AreEqual(0, author.BookSeries.Count());
            Assert.AreEqual(0, author.PublishingHouses.Count());
        }

        [TestMethod]
        public void GetByIdMethod_Id_ReturnExistAuthor()
        {
            IAuthorDomainQueries authorsQueries = new AuthorDomainQueries(_queryFactory, _linkFactory, null);

            var author = authorsQueries.GetById(2);

            Assert.AreNotEqual(0, author.Id);
            Assert.IsFalse(String.IsNullOrWhiteSpace(author.Name));
            Assert.AreNotEqual(0, author.Books.Count());
            Assert.AreNotEqual(0, author.BookSeries.Count());
            Assert.AreNotEqual(0, author.PublishingHouses.Count());

        }

    }
}
