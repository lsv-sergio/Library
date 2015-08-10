using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LibraryDal.EF;
using LibraryDal.Infrastructure.UnitOfWork;
using Moq;
using System.Linq;
using LibraryDal.Infrastructure.QueriesDal;
using LibraryDal.Infrastructure.QueriesDal.Abstract;

namespace Library.Tests.DAL
{
    [TestClass]
    public class DalQueriesTest
    {
        private IQueryContext _uow;
        [TestInitialize]
        public void Init()
        {
            IList<AuthorView> authors = new List<AuthorView>()
            {
                new AuthorView(){Id = 1, Name = "Борис Акунин"},
                new AuthorView(){Id = 2, Name = "Джереми Стронг"},
                new AuthorView(){Id = 3, Name = "Всеволод Нестайко"},
                new AuthorView(){Id = 4, Name = "Валентин Пикуль"}
            };

            var moqUoW = new Mock<IQueryContext>();
            moqUoW.Setup(x => x.GetEntity<AuthorView>(It.IsAny<int>())).Returns<int>(x => authors.FirstOrDefault(y => y.Id == x));
            moqUoW.Setup(x => x.GetEntity<AuthorView>(It.IsAny<Func<AuthorView, bool>>())).Returns<Func<AuthorView, bool>>(filter => authors.FirstOrDefault(filter));
            moqUoW.Setup(x => x.GetEntities<AuthorView>()).Returns(authors.AsQueryable());
            moqUoW.Setup(x => x.GetEntities<AuthorView>(It.IsAny<Func<AuthorView, bool>>())).Returns<Func<AuthorView, bool>>(filter => authors.Where(filter));

            _uow = moqUoW.Object;
        }

        [TestMethod]
        public void If_UoW_Is_Null_All_Methods_Return_Null_OrEmptyList()
        {
            IDalQuery<AuthorView> baseQueries = new AuthorDalQuery(null);

            Assert.IsTrue(baseQueries.GetAll().Count() == 0);
            Assert.IsTrue(baseQueries.GetAllByFilter(x => x.Name.Length > 0).Count() == 0);
            Assert.IsTrue(baseQueries.GetAllByName(" ").Count() == 0);
            Assert.IsNull(baseQueries.GetByFilter(x => x.Id == 1));
            Assert.IsNull(baseQueries.GetById(1));
        }

        [TestMethod]
        public void GetByIdMethod_Id_ReturnEntityOrNull()
        {
            IDalQuery<AuthorView> baseQueries = new AuthorDalQuery(_uow);

            AuthorView authorView1 = baseQueries.GetById(1);
            AuthorView authorView2 = baseQueries.GetById(50);
            AuthorView authorView3 = baseQueries.GetById(0);
            AuthorView authorView4 = baseQueries.GetById(-2);

            Assert.IsNotNull(authorView1);
            Assert.IsNull(authorView2);
            Assert.IsNull(authorView3);
            Assert.IsNull(authorView4);

        }

        [TestMethod]
        public void GetAllByNameMethod_Name_ReturnIEnumerable()
        {
            IDalQuery<AuthorView> baseQueries = new AuthorDalQuery(_uow);

            var authorViewsList1 = baseQueries.GetAllByName("ст");
            var authorViewsList2 = baseQueries.GetAllByName("Перельман");
            var authorViewsList3 = baseQueries.GetAllByName(null);

            Assert.IsInstanceOfType(authorViewsList1, typeof(IEnumerable<AuthorView>));
            Assert.IsInstanceOfType(authorViewsList2, typeof(IEnumerable<AuthorView>));
            Assert.IsInstanceOfType(authorViewsList3, typeof(IEnumerable<AuthorView>));
            Assert.IsTrue(authorViewsList1.Count() > 0);
            Assert.IsTrue(authorViewsList2.Count() == 0);
            Assert.IsTrue(authorViewsList3.Count() == 0);

        }

        [TestMethod]
        public void GetAllByFilterMethod_FuncEntityBool_ReturnIEnumerable()
        {
            IDalQuery<AuthorView> baseQueries = new AuthorDalQuery(_uow);

            var authorViewsList1 = baseQueries.GetAllByFilter(x => x.Name.ToLower().Contains("ст"));
            var authorViewsList2 = baseQueries.GetAllByFilter(x => x.Name.Contains("Перельман"));
            var authorViewsList3 = baseQueries.GetAllByFilter(null);

            Assert.IsInstanceOfType(authorViewsList1, typeof(IEnumerable<AuthorView>));
            Assert.IsInstanceOfType(authorViewsList2, typeof(IEnumerable<AuthorView>));
            Assert.IsInstanceOfType(authorViewsList3, typeof(IEnumerable<AuthorView>));
            Assert.IsTrue(authorViewsList1.Count() > 0);
            Assert.IsTrue(authorViewsList2.Count() == 0);
            Assert.IsTrue(authorViewsList3.Count() == 0);

        }

        [TestMethod]
        public void GetByFilterMethod_FuncEntityBool_ReturnIEnumerable()
        {
            IDalQuery<AuthorView> baseQueries = new AuthorDalQuery(_uow);

            var author1 = baseQueries.GetByFilter(x => x.Name.ToLower().Contains("ст"));
            var author2 = baseQueries.GetByFilter(x => x.Name.Contains("Перельман"));
            var author3 = baseQueries.GetByFilter(null);

            Assert.IsNotNull(author1);
            Assert.IsNull(author2);
            Assert.IsNull(author3);

        }

        [TestMethod]
        public void GetAllMethod_None_ReturnNotEmptyIEnumerable()
        {
            IDalQuery<AuthorView> baseQueries = new AuthorDalQuery(_uow);

            var authorViewsList = baseQueries.GetAll();

            Assert.IsInstanceOfType(authorViewsList, typeof(IEnumerable<AuthorView>));
            Assert.IsTrue(authorViewsList.Count() > 0);
        }
    }
}
