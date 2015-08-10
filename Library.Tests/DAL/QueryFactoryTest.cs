using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryDal.Infrastructure.UnitOfWork;
using Moq;
using LibraryDal.Infrastructure.QueriesDal.Factory;
using LibraryDal.Infrastructure.QueriesDal;
using LibraryDal.EF;
using LibraryDomain.Core.Views;
using LibraryDal.Infrastructure.QueriesDal.Abstract;

namespace Library.Tests.DAL
{
    [TestClass]
    public class DalQueryFactoryTest
    {
        [TestMethod]
        public void GetQueryByInterface_InterfaceType_ReturnImplementationInterfaceOrNull()
        {
            var uowMock = new Mock<IQueryContext>();
            IDalQueryFactory queryFactory= new DalQueryFactory(uowMock.Object);
            
            var authorQuery = queryFactory.GetQueryByInterface<IAuthorDalQuery>();
            var authorUoW = queryFactory.GetQueryByInterface<IQueryContext>();

            Assert.IsNotNull(authorQuery);
            Assert.IsInstanceOfType(authorQuery, typeof(IDalQuery<AuthorView>));
            Assert.IsNull(authorUoW);
        }

        [TestMethod]
        public void GetQueryByClass_ClassType_ReturnImplementationInterfaceOrNull()
        {
            var uowMock = new Mock<IQueryContext>();
            IDalQueryFactory queryFactory = new DalQueryFactory(uowMock.Object);

            var authorQuery = queryFactory.GetQueryByClass<AuthorView>();

            Assert.IsNotNull(authorQuery);
            Assert.IsInstanceOfType(authorQuery, typeof(IDalQuery<AuthorView>));
        }

    }
}
