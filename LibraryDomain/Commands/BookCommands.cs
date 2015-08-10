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
    public class BookCommands : DomainCommands, IBookCommands
    {
        private readonly IBookDalCommands _bookDalCommands;

        public BookCommands(IBookDalCommands BookDalCommands)
        {
            _bookDalCommands = BookDalCommands;
        }

        public override int Save(BaseDomain Domain)
        {
            int bookId = 0;
            BookDomain bookDomain = Domain as BookDomain;
            if (Domain == null)
                return bookId;
            if (!DomainValid(bookDomain))
                return bookId;
            using (ICommandContext dbContext = _bookDalCommands.DbContext)
            {
                if (dbContext.Database == null || !dbContext.Database.Exists())
                    return 0;
                bool commit = false;
                using (DbContextTransaction transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        Book bookDal = null;
                        if (bookDomain.Id != 0)
                            bookDal = _bookDalCommands.Find(bookDomain.Id);
                        if (bookDal == null)
                        {
                            bookId = BookNewTransaction(bookDomain);
                            commit = bookId != 0 ;
                        }
                        else
                        {
                            bookId = BookUpdateTransaction(bookDal, bookDomain);
                            commit = bookId != 0;
                        }

                        if (commit)
                        {
                            dbContext.SaveChanges();
                            transaction.Commit();
                        }
                        else
                            transaction.Rollback();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        bookId = 0;
                    }
                }
            }
            return bookId;
        }

        private int BookUpdateTransaction(Book BookDal, BookDomain BookDomain)
        {
            int bookId = 0;
            try
            {
                BookDal.Name = BookDomain.Name;
                if (BookDomain.BookSeries != null)
                    BookDal.BookSeriesId = BookDomain.BookSeries.Id;
                else
                    BookDal.BookSeriesId = BookDomain.BookSeries.Id;
                BookDal.AuthorId = BookDomain.Author.Id;
                BookDal.PageNumber = BookDomain.PageCount;
                BookDal.PublisherHouseId = BookDomain.PublishingHouse.Id;
                _bookDalCommands.Update(BookDal);
                bookId = BookDal.Id;
            }
            catch (Exception)
            {
                bookId = 0;
            }
            return bookId;
        }

        public override int Delete(BaseDomain Domain)
        {
            int bookId = 0;
            BookDomain bookDomain = Domain as BookDomain;
            if (bookDomain == null)
                return bookId;
            if (bookDomain.Id == 0)
                return bookId;
            if (!DomainValid(bookDomain))
                return bookId;
            using (ICommandContext dbContext = _bookDalCommands.DbContext)
            {
                if (dbContext.Database == null || !dbContext.Database.Exists())
                    return 0;
                using (DbContextTransaction transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        Book bookDal = null;
                        bookDal = _bookDalCommands.Find(bookDomain.Id);

                        if (bookDal != null)
                        {
                            _bookDalCommands.Delete(bookDal);
                            dbContext.SaveChanges();
                            transaction.Commit();
                            bookId = bookDal.Id;
                        }

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        bookId = 0;
                    }
                }
            }
            return bookId;

        }

        private int BookNewTransaction(IBookDomain bookDomain)
        {
            int bookId = 0;
            Book bookDal = new Book() { Id = _bookDalCommands.GetNewId(), Name = bookDomain.Name.Trim() };
            bookDal.PageNumber = bookDomain.PageCount;
            bookDal.PublisherHouseId = bookDomain.PublishingHouse.Id;
            bookDal.AuthorId = bookDomain.Author.Id;
            if (bookDomain.BookSeries != null)
                bookDal.BookSeriesId = bookDomain.BookSeries.Id;
            _bookDalCommands.Add(bookDal);
            bookId = bookDal.Id;
            return bookId;
        }

        private bool DomainValid(IBookDomain bookDomain)
        {
            return bookDomain.Author != null && bookDomain.PublishingHouse != null && _bookDalCommands != null;
            ;
        }
    }
}
