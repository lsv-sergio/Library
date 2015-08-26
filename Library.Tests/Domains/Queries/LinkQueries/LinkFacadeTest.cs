using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryDal.Infrastructure.QueriesDal.Factory;
using LibraryDomain.Queries.LinksQueries.Factory;
using Library.Tests.Domains.Queries.Stubs;
using LibraryDomain.Core.Links;
using System.Collections.Generic;
using System.Linq;

namespace Library.Tests.Domains.Queries.LinkQueries
{
    [TestClass]
    public class LinkFacadeTest
    {
        ILinkFacade _linkFacade;

        [TestInitialize]
        public void Init()
        {

            IDalQueryFactory queryFactory = new DalQueryFactoryStub();
            _linkFacade = new LinkFacade(queryFactory);

        }

        [TestMethod]
        public void MakeLinkMethod_IdName_ReturnLink()
        {
            ILink link = _linkFacade.MakeLink<IAuthorLink>(5,"Author");

            Assert.IsNotNull(link);
        }

        [TestMethod]
        public void MakeLinkMethod_ReturnNullIfIdLessOneOrNameIsEmpty()
        {
            ILink link1 = _linkFacade.MakeLink<IAuthorLink>(0,"Author");
            ILink link2 = _linkFacade.MakeLink<IAuthorLink>(5,"");
            ILink link3 = _linkFacade.MakeLink<IAuthorLink>(-1,"");

            Assert.IsNull(link1);
            Assert.IsNull(link2);
            Assert.IsNull(link3);
        }

        [TestMethod]
        public void GetByIdMethod_Id_ReturnLink()
        {
            ILink link = _linkFacade.GetById<IAuthorLink>(5);

            Assert.IsNotNull(link);
        }

        [TestMethod]
        public void GetByIdMethod_IdLessOne_ReturnNull()
        {
            ILink link1 = _linkFacade.GetById<IAuthorLink>(0);
            ILink link2 = _linkFacade.GetById<IAuthorLink>(0);

            Assert.IsNull(link1);
            Assert.IsNull(link2);
        }

        [TestMethod]
        public void GetAllMethod_None_ReturnNotEmptyList()
        {
            IEnumerable<ILink> links = _linkFacade.GetAll<IAuthorLink>();

            Assert.IsTrue(links.Count() > 0);
        }

        [TestMethod]
        public void GetQueryMethod_None_ReturnNullIfInternalDictionaryNotContainType()
        {
            var link = _linkFacade.GetQuery<ILink>();

            Assert.IsNull(link);
        }

    }
}
