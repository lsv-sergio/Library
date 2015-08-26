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

namespace Library.Tests.Domains.Queries
{
    [TestClass]
    public class BaseQueriesTest
    {
        IDalQueryFactory _queryFactory = new DalQueryFactoryStub();
        ILinkFacade _linkFactory = new LiknFactoryStub();

        [TestMethod]
        public void GetAllMethod_ReturnNotEmptyListWithDomains()
        {
            IAuthorDomainQueries authorsQueries = new AuthorDomainQueries(_queryFactory, _linkFactory, null);

            IEnumerable<IBaseDomain> domains = authorsQueries.GetAll();

            Assert.IsTrue(domains.Count() > 0);
            Assert.IsInstanceOfType(domains.First(), typeof(IBaseDomain));

        }

        [TestMethod]
        public void GetAllByFilterMethod_Name_ReturnNotEmptyListWithDomainsIfFound()
        {
            IAuthorDomainQueries authorsQueries = new AuthorDomainQueries(_queryFactory, _linkFactory, null);

            IEnumerable<IBaseDomain> domains = authorsQueries.GetAllByFilter(x => x.Name.ToLower().Trim().Contains("Author"));

            Assert.IsTrue(domains.Count() > 0);
            Assert.IsInstanceOfType(domains.First(), typeof(IBaseDomain));

        }

        [TestMethod]
        public void GetAllByFilterMethod_null_ReturnEmptyList()
        {
            IAuthorDomainQueries authorsQueries = new AuthorDomainQueries(_queryFactory, _linkFactory, null);

            IEnumerable<IBaseDomain> domains = authorsQueries.GetAllByFilter(null);

            Assert.IsTrue(domains.Count() == 0);

        }
    }
}
