using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using LibraryDal.EF;
using System.Collections.Generic;
using Moq;
using LibraryDal.Infrastructure.UnitOfWork;
using LibraryDal.Infrastructure.CommandsDal;

namespace Library.Tests.DAL
{
    [TestClass]
    public class DalCommandTest
    {
        ICommandContext _uow;
        int _maxId;

        [TestInitialize]
        public void Init()
        {
            IList<Author> authors = new List<Author>()
            {
                new Author(){Id = 1, Name = "Борис Акунин"},
                new Author(){Id = 2, Name = "Джереми Стронг"},
                new Author(){Id = 3, Name = "Всеволод Нестайко"},
                new Author(){Id = 4, Name = "Валентин Пикуль"}
            };

            var moqUoW = new Mock<ICommandContext>();
            moqUoW.Setup(x => x.AddEntity<Author>(It.IsAny<Author>())).Returns<Author>(
                x =>
                {
                    if (authors.FirstOrDefault(y => y.Id == x.Id) != null)
                        return null;
                    return x;
                }
                );
            moqUoW.Setup(x => x.UpdateEntity<Author>(It.IsAny<Author>())).Returns<Author>(
                x => 
                {
                    var author = authors.FirstOrDefault(y => y.Id == x.Id) ;
                    if (author != null)
                        return x;
                    return null; 
                });

            moqUoW.Setup(x => x.DeleteEntity<Author>(It.IsAny<Author>())).Returns<Author>(
                x =>
                {
                    if (authors.FirstOrDefault(y => y.Id == x.Id) == null)
                        return null;
                    return x;
                });

            moqUoW.Setup(x => x.GetNewId<Author>()).Returns( authors.Max(y => y.Id) + 1);

            moqUoW.Setup(x => x.FindEntity<Author>(new object[] {It.IsAny<object>()})).Returns<object[]>(
                x =>
                    {
                        if(x.Count() != 1)
                            return null;
                        return authors.FirstOrDefault(y => y.Id == (int)x.First());
                    });

            _uow = moqUoW.Object;
            _maxId = authors.Max(x => x.Id) + 1;
        }

        [TestMethod]
        public void If_UoW_Is_Null_All_Methods_Return_Null()
        {
            var baseCommands = new AuthorDalCommands(null);

            Author author = new Author() { Id = 5, Name = "New" };

            Assert.IsFalse(baseCommands.Add(author));
            Assert.IsFalse(baseCommands.Update(author));
            Assert.AreEqual(0,baseCommands.GetNewId());
            Assert.IsNull(baseCommands.Find(5));
  
        }

        [TestMethod]
        public void AddEntityMethod_Entity_ReturnOrNull()
        {
            var authorDalCommands = new AuthorDalCommands(_uow);

            Author newAuthor = new Author() { Id = 5, Name = "New" };
            Author existAuthor = new Author(){Id = 3, Name = "Всеволод Нестайко"};

            Assert.IsTrue(authorDalCommands.Add(newAuthor));
            Assert.IsFalse(authorDalCommands.Add(existAuthor));            
        }

        [TestMethod]
        public void UpdateMethod_Entity_RetunEntityOrNull() 
        {
            var authorDalCommands = new AuthorDalCommands(_uow);

            Author newAuthor = new Author() { Id = 5, Name = "New" };
            Author existAuthor = new Author(){ Id = 3, Name = "Всеволод Нестайко" };

            Assert.IsFalse(authorDalCommands.Update(newAuthor));
            Assert.IsTrue(authorDalCommands.Update(existAuthor));            
        }

        [TestMethod]
        public void DeleteMethod_Entity_RetunEntityOrNull()
        {
            var authorDalCommands = new AuthorDalCommands(_uow);

            Author newAuthor = new Author() { Id = 5, Name = "New" };
            Author existAuthor = new Author() { Id = 3, Name = "Всеволод Нестайко" };

            Assert.IsFalse(authorDalCommands.Delete(newAuthor));
            Assert.IsTrue(authorDalCommands.Delete(existAuthor));
        }

        [TestMethod]
        public void GetNewIdMethod_None_RetunNewMaxId()
        {
            var authorDalCommands = new AuthorDalCommands(_uow);

            Assert.AreEqual(_maxId,authorDalCommands.GetNewId());
        }

        [TestMethod]
        public void FindMethod_Id_RetunEntityOrNull()
        {
            var authorDalCommands = new AuthorDalCommands(_uow);

            Author author1 = authorDalCommands.Find(3);
            Author author2 = authorDalCommands.Find(0);
            Author author3 = authorDalCommands.Find(50);

            Assert.IsNotNull(author1);
            Assert.IsNull(author2);
            Assert.IsNull(author3);
        }
    }
}
