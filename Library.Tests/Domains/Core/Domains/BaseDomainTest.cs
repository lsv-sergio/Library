using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryDomain.Commands;
using Moq;
using LibraryDomain.Domains;
using LibraryDomain.Queries.DomainQueries;
using LibraryDomain.Core.Links;
using System.Collections.Generic;

namespace Library.Tests.Domains.Core.Domains
{
    [TestClass]
    public class BaseDomainTest
    {
        Mock<IAuthorDomainQueries> _queries;

        [TestInitialize]
        public void Init()
        {

            var moqAuthorQuery = new Mock<IAuthorDomainQueries>();
            moqAuthorQuery.Setup(x => x.Create()).Returns(new AuthorDomain(0, "", new List<IBookLink>(),
                new List<IBookSeriesLink>(), new List<IPublishingHouseLink>(), null));

            moqAuthorQuery.Setup(x => x.GetById(It.IsAny<int>())).Returns<int>(x => 
                new AuthorDomain(x, "Author1",null,null,null, null));

            _queries = moqAuthorQuery;
        }

        [TestMethod]
        public void MethodSetName_Name_ChangeDomainsName()
        {
            IBaseDomain domain = _queries.Object.Create() as IBaseDomain;

            domain.SetName("NewName");

            Assert.AreEqual("NewName", domain.Name);
        }

        [TestMethod]
        public void SaveMethod_BaseDomain_CallHandlerSaveCommand()
        {
            var moqCommand = new Mock<IDomainCommands>();
            moqCommand.Setup(x => x.Save(It.IsAny<BaseDomain>()));

            _queries.Setup(x => x.Create()).Returns(new AuthorDomain(0, "", new List<IBookLink>(),
                new List<IBookSeriesLink>(), new List<IPublishingHouseLink>(), moqCommand.Object));

            IBaseDomain domain = _queries.Object.Create() as IBaseDomain;

            domain.Save();

            moqCommand.Verify(x => x.Save(domain), Times.Once);
        }

        [TestMethod]
        public void DeleteMethod_BaseDomain_CallHandlerSaveCommand()
        {
            var moqCommand = new Mock<IDomainCommands>();
            moqCommand.Setup(x => x.Save(It.IsAny<BaseDomain>()));

            _queries.Setup(x => x.Create()).Returns(new AuthorDomain(0, "", new List<IBookLink>(),
                new List<IBookSeriesLink>(), new List<IPublishingHouseLink>(), moqCommand.Object));

            IBaseDomain domain = _queries.Object.Create() as IBaseDomain;

            domain.Delete();

            moqCommand.Verify(x => x.Delete(domain), Times.Once);
        }

        [TestMethod]
        public void NewDomains_Has_Zero_Id_And_Empty_Name()
        {
            IBaseDomain domain = _queries.Object.Create() as IBaseDomain;

            Assert.AreEqual(0, domain.Id);
            Assert.IsTrue(String.IsNullOrWhiteSpace(domain.Name));

        }

        [TestMethod]
        public void ExistDomains_Has_Id_And_Empty_Name()
        {
            IBaseDomain domain = _queries.Object.GetById(5) as IBaseDomain;

            Assert.AreEqual(5, domain.Id);
            Assert.IsTrue(domain.Name.Length > 0);

        }

    }
}
