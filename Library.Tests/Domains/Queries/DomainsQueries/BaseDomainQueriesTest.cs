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
    public class BaseDomainQueriesTest
    {
        IDalQueryFactory _queryFactory = new DalQueryFactoryStub();
        ILinkFacade _linkFactory = new LiknFactoryStub();

        [TestMethod]
        public void CreateMethod_None_ReturnNewDomainWithZeroIdAndEmptyName()
        {
            IAuthorDomainQueries authorsQueries = new AuthorDomainQueries(_queryFactory, _linkFactory, null);

            IBaseDomain domain = authorsQueries.Create();

            Assert.AreEqual(0, domain.Id);
            Assert.IsTrue(String.IsNullOrWhiteSpace(domain.Name));
        }

        [TestMethod]
        public void GetByIdMethod_Id_ReturnExistDomainWithNotZeroIdAndNotEmptyName()
        {
            IAuthorDomainQueries authorsQueries = new AuthorDomainQueries(_queryFactory, _linkFactory, null);

            IBaseDomain domain = authorsQueries.GetById(2);

            Assert.AreNotEqual(0, domain.Id);
            Assert.IsFalse(String.IsNullOrWhiteSpace(domain.Name));

        }

        [TestMethod]
        public void GetByIdMethod_LessOne_ReturnNull()
        {
            IAuthorDomainQueries authorsQueries = new AuthorDomainQueries(_queryFactory, _linkFactory, null);

            IBaseDomain domain1 = authorsQueries.GetById(0);
            IBaseDomain domain2 = authorsQueries.GetById(-5);

            Assert.IsNull(domain1);
            Assert.IsNull(domain2);

        }

    }
}
