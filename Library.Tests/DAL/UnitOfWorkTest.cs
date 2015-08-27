using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryDal.Infrastructure.UnitOfWork;
using LibraryDal.EF;
using System.Linq;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace Library.Tests.DAL
{
    [TestClass]
    public class UnitOfWorkTest
    {
            Mock<EFLibraryDbContext> _moqDbContext;
            Mock<DbSet<Author>> _moqDbSet;

        [TestInitialize]
        public void Init()
        {

            List<Author> authors = new List<Author>()
            {
                new Author(){Id = 1, Name = "Борис Акунин"},
                new Author(){Id = 2, Name = "Джереми Стронг"},
                new Author(){Id = 3, Name = "Всеволод Нестайко"},
                new Author(){Id = 4, Name = "Валентин Пикуль"}
            };
            IQueryable<Author> authorsQueryable = authors.AsQueryable();

            _moqDbSet = new Mock<DbSet<Author>>();
            _moqDbSet.As<IQueryable<Author>>().Setup(x => x.Provider).Returns(authorsQueryable.Provider);
            _moqDbSet.As<IQueryable<Author>>().Setup(x => x.Expression).Returns(authorsQueryable.Expression);
            _moqDbSet.As<IQueryable<Author>>().Setup(x => x.GetEnumerator()).Returns(authorsQueryable.GetEnumerator());
            _moqDbSet.As<IQueryable<Author>>().Setup(x => x.ElementType).Returns(authorsQueryable.ElementType);
            _moqDbSet.Setup(x => x.Find(new object[] { It.IsAny<object>() })).Returns<object[]>(
                x =>
                {
                    if(x.Count() != 1)
                        return null;
                    return authors.FirstOrDefault(y => y.Id == (int)x.First());
                });
            _moqDbSet.Setup(x => x.Add(It.IsAny<Author>()));
            _moqDbSet.Setup(x => x.Remove(It.IsAny<Author>())).Returns<Author>(x => x);

            _moqDbContext = new Mock<EFLibraryDbContext>();
            _moqDbContext.Setup(x => x.Set<Author>()).Returns(_moqDbSet.Object);
        }

        [TestMethod]
        public void If_Context_Is_Null_All_Methods_Returns_Null_Or_EmptyLists()
        {
            UnitOfWork uow = new UnitOfWork(null);

            Assert.IsNull(uow.FindEntity<Author>(2));

            Assert.IsNull(uow.GetEntity<Author>(1));

            Assert.IsNull(uow.GetEntity<Author>(x => x.Id == 1));

            Assert.AreEqual(0, uow.GetEntities<Author>().Count());

            Assert.AreEqual(0, uow.GetEntities<Author>(x => x.Id > 0).Count());

            Assert.IsNull(uow.UpdateEntity<Author>(new Author() { Id = 50, Name = "New" }));

            Assert.IsNull(uow.AddEntity<Author>(new Author() { Id = 50, Name = "New" }));

            Assert.IsNull(uow.DeleteEntity<Author>(new Author() { Id = 50, Name = "New" }));

            Assert.AreEqual(0, uow.GetNewId<Author>());

            Assert.IsNull(uow.Database);

            Assert.AreEqual(0, uow.SaveChanges());
        }

        [TestMethod]
        public void FindMethod_Id_ReturnsEnityOrNullIfNotFound()
        {
            UnitOfWork uow = new UnitOfWork(_moqDbContext.Object);

            Author author1 = uow.FindEntity<Author>(1);
            Author author2 = uow.FindEntity<Author>(50);
             Author author3 = uow.FindEntity<Author>(0);
             Author author4 = uow.FindEntity<Author>(-2);
          
            Assert.IsNotNull(author1);
            Assert.IsNull(author2);
            Assert.IsNull(author3);
            Assert.IsNull(author4);
        }

        [TestMethod]
        public void GetEntityMethod_Id_ReturnsEnityOrNullIfNotFound()
        {
            UnitOfWork uow = new UnitOfWork(_moqDbContext.Object);

            Author author1 = uow.GetEntity<Author>(1);
            Author author2 = uow.GetEntity<Author>(50);
            Author author3 = uow.GetEntity<Author>(0);

            Assert.IsNotNull(author1);
            Assert.IsNull(author2);
            Assert.IsNull(author3);
        }

        [TestMethod]
        public void GetEntityMethod_FuncEntityBool_ReturnsFirstEnityOrNullIfNotFound()
        {
            UnitOfWork uow = new UnitOfWork(_moqDbContext.Object);

            Author author1 = uow.GetEntity<Author>(x => x.Name.ToLower().Trim().Contains("ст"));
            Author author2 = uow.GetEntity<Author>(x => x.Name.ToLower().Trim().Contains("нестайко"));
            Author author3 = uow.GetEntity<Author>(x => x.Name.ToLower().Trim().Contains("перельман"));
            Author author4 = uow.GetEntity<Author>(null);

            Assert.IsNotNull(author1);
            Assert.IsNotNull(author2);
            Assert.AreEqual(2, author1.Id);
            Assert.AreEqual(3, author2.Id);
            Assert.IsNull(author3);
            Assert.IsNull(author4);
        }

        [TestMethod]
        public void GetEntitiesMethod_None_ReturnsIEnumerableEnity()
        {
            UnitOfWork uow = new UnitOfWork(_moqDbContext.Object);

            var list = uow.GetEntities<Author>();

            Assert.IsInstanceOfType(list, typeof(IEnumerable<Author>));
            Assert.IsTrue(list.Count() > 0);
        }
        
        [TestMethod]
        public void GetEntitiesMethod_FuncEntityBool_ReturnsIEnumerableOrEmptyIEnumerableIfNotFound()
        {
            UnitOfWork uow = new UnitOfWork(_moqDbContext.Object);

            var list1 = uow.GetEntities<Author>(x => x.Name.ToLower().Trim().Contains("ст"));
            var list2 = uow.GetEntities<Author>(x => x.Name.ToLower().Trim().Contains("перельман"));
            var list3 = uow.GetEntities<Author>(null);

            Assert.IsInstanceOfType(list1, typeof(IEnumerable<Author>));
            Assert.IsTrue(list1.Count() > 0);

            Assert.IsInstanceOfType(list2, typeof(IEnumerable<Author>));
            Assert.IsTrue(list2.Count() == 0);

            Assert.IsInstanceOfType(list3, typeof(IEnumerable<Author>));
            Assert.IsTrue(list3.Count() == 0);
        }

        [TestMethod]
        public void UpdateEntity_NewEntity_ReturnsEntityAndAddEntityToDb()
        {
            UnitOfWork uow = new UnitOfWork(_moqDbContext.Object);

            Author author = uow.UpdateEntity<Author>(new Author() { Id = 5, Name = "Артур Конан Дойл" });

            Assert.IsNotNull(author);
        }

        [TestMethod]
        public void UpdateEntity_Null_ReturnsNull()
        {
            UnitOfWork uow = new UnitOfWork(_moqDbContext.Object);


            Author updatedAuthor = uow.UpdateEntity<Author>(null);

            Assert.IsNull(updatedAuthor);
        }

        [TestMethod]
        public void AddEntity_NewEntity_ReturnsEntityAndAddEntityToDb()
        {
            UnitOfWork uow = new UnitOfWork(_moqDbContext.Object);

            Author author = uow.UpdateEntity<Author>(new Author() { Id = 5, Name = "Артур Конан Дойл" });

            Assert.IsNotNull(author);

            _moqDbSet.Verify(x => x.Add(It.IsAny<Author>()), Times.Once);
        }
        
        [TestMethod]
        public void AddEntity_Null_ReturnsNull()
        {
            UnitOfWork uow = new UnitOfWork(_moqDbContext.Object);


            Author newAuthor = uow.AddEntity<Author>(null);

            Assert.IsNull(newAuthor);

            _moqDbSet.Verify(x => x.Add(It.IsAny<Author>()), Times.Never);
        }

        [TestMethod]
        public void DeleteMethod_NewEntity_ReturnNullDoNothing()
        {
            UnitOfWork uow = new UnitOfWork(_moqDbContext.Object);

            Author author = uow.DeleteEntity<Author>(new Author() { Id = 5, Name = "Артур Конан Дойл" });

            Assert.IsNull(author);

            _moqDbSet.Verify(x => x.Remove(It.IsAny<Author>()), Times.Never);
        }
        
        [TestMethod]
        public void DeleteMethod_ExistEntity_ReturnEntityRemovedEntity()
        {
            UnitOfWork uow = new UnitOfWork(_moqDbContext.Object);

            Author author = uow.DeleteEntity<Author>(uow.GetEntity<Author>(1));

            Assert.IsNotNull(author);

            _moqDbSet.Verify(x => x.Remove(It.IsAny<Author>()), Times.Once);
        }

        [TestMethod]
        public void DeleteMethod_Null_ReturnNull()
        {
            UnitOfWork uow = new UnitOfWork(_moqDbContext.Object);

            Author author = uow.DeleteEntity<Author>(null);

            Assert.IsNull(author);

            _moqDbSet.Verify(x => x.Remove(It.IsAny<Author>()), Times.Never);
        }

        [TestMethod]
        public void GetNewId_None_ReturnUniqId()
        {
            UnitOfWork uow = new UnitOfWork(_moqDbContext.Object);

            int newId = uow.GetNewId<Author>();

            Author author = uow.GetEntity<Author>(newId);

            Assert.IsNull(author);
        }
    }
}
