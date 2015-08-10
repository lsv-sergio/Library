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
    public class BookSeriesCommands:DomainCommands, IBookSeriesCommands
    {
        private readonly IBookSeriesDalCommands _bookSeriesDalCommands;

        public BookSeriesCommands(IBookSeriesDalCommands BookSerieDalCommands)
        {
            _bookSeriesDalCommands = BookSerieDalCommands;
        }

        public override int Save(BaseDomain Domain)
        {
            int bookSerieId = 0;
            BookSeriesDomain bookSerieDomain = Domain as BookSeriesDomain;
            if (bookSerieDomain == null)
                return bookSerieId;
            using (ICommandContext dbContext = _bookSeriesDalCommands.DbContext)
            {
                if (dbContext.Database == null || !dbContext.Database.Exists())
                    return 0;
                using (DbContextTransaction transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        if (bookSerieDomain.PublishingHouse == null || bookSerieDomain.PublishingHouse.Id == 0)
                            return bookSerieId;
                        int publishingHouseId = bookSerieDomain.PublishingHouse.Id;
                        BookSerie bookSerieDal = null;
                        if (bookSerieDomain.Id == 0)
                            bookSerieId = _bookSeriesDalCommands.GetNewId();
                        else
                            bookSerieDal = _bookSeriesDalCommands.Find(bookSerieDomain.Id);

                        if (bookSerieDal == null)
                            _bookSeriesDalCommands.Add(new BookSerie() { Id = bookSerieId, Name = bookSerieDomain.Name, PublishingHouseId = publishingHouseId });
                        else
                        {
                            bookSerieDal.Name = bookSerieDomain.Name;
                            bookSerieDal.PublishingHouseId = publishingHouseId;
                            _bookSeriesDalCommands.Update(bookSerieDal);
                        }
                        dbContext.SaveChanges();
                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        bookSerieId = 0;
                    }
                }
            }
            return bookSerieId;
        }

        public override int Delete(BaseDomain Domain)
        {
            int authorId = 0;
            BookSeriesDomain bookSerieDomain = Domain as BookSeriesDomain;
            if (Domain == null)
                return authorId;
            if (Domain.Id == 0)
                return authorId;
            using (ICommandContext dbContext = _bookSeriesDalCommands.DbContext)
            {
                if (dbContext.Database == null || !dbContext.Database.Exists())
                    return 0;
                using (DbContextTransaction transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        BookSerie authorDal = _bookSeriesDalCommands.Find(bookSerieDomain.Id);
                        if (authorDal == null)
                        {
                            transaction.Rollback();
                            return authorId;
                        }
                        if (CheckOnDelete(authorDal))
                        {
                            _bookSeriesDalCommands.Delete(authorDal);
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

        private bool CheckOnDelete(BookSerie BookSerieDal)
        {
            return BookSerieDal.Books.Count() == 0;
        }

    }
}
