using LibraryDal.EF;
using LibraryDal.Infrastructure.CommandsDal;
using LibraryDal.Infrastructure.UnitOfWork;
using LibraryDomain.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Commands
{
    public class AuthorCommands : DomainCommands, IAuthorCommands
    {
        private readonly IAuthorDalCommands _authorDalCommands;

        public AuthorCommands(IAuthorDalCommands AuthorCommands)
        {
            _authorDalCommands = AuthorCommands;
        }

        public override int Save(BaseDomain Domain)
        {
            int authorId = 0;
            AuthorDomain authorDomain = Domain as AuthorDomain;
            if (authorDomain == null)
                return authorId;
            using (ICommandContext dbContext = _authorDalCommands.DbContext)
            {
                if (dbContext.Database == null || !dbContext.Database.Exists())
                    return 0;
                using (DbContextTransaction transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        Author authorDal = null;
                        if (authorDomain.Id == 0)
                            authorId = _authorDalCommands.GetNewId();
                        else
                            authorDal = _authorDalCommands.Find(authorDomain.Id);

                        if (authorDal == null)
                            _authorDalCommands.Add(new Author() { Id = authorId, Name = authorDomain.Name });
                        else
                        {
                            authorDal.Name = authorDomain.Name;
                            _authorDalCommands.Update(authorDal);
                        }
                        dbContext.SaveChanges();
                        transaction.Commit();

                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        authorId = 0;
                    }
                }
                
            } return authorId;
        }

        public override int Delete(BaseDomain Domain)
        {
            int authorId = 0;
            AuthorDomain authorDomain = Domain as AuthorDomain;
            if (Domain == null)
                return authorId;
            if (Domain.Id == 0)
                return authorId;
            using (ICommandContext dbContext = _authorDalCommands.DbContext)
            {
                if (dbContext.Database == null || !dbContext.Database.Exists())
                    return 0;
                using (DbContextTransaction transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        Author authorDal = _authorDalCommands.Find(authorDomain.Id);
                        if (authorDal == null)
                        {
                            transaction.Rollback();
                            return authorId;
                        }
                        if (CheckOnDelete(authorDal))
                        {
                            _authorDalCommands.Delete(authorDal);
                            dbContext.SaveChanges();
                            transaction.Commit();
                        }
                        else
                            transaction.Rollback();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        authorId = 0;
                    }
                }
            }
            return authorId;
        }

        private bool CheckOnDelete(Author AuthorDal)
        {
            return AuthorDal.Books.Count() == 0;
        }

    }
}
