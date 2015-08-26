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
    public class PublishingHouseCommands : DomainCommands, IPublishingHouseCommands
    {
        private readonly IPublishingHouseDalCommands _publishingHouseDalCommands;

        public PublishingHouseCommands(IPublishingHouseDalCommands PublishingHouseDalCommands)
        {
            _publishingHouseDalCommands = PublishingHouseDalCommands;
        }

        public override int Save(IBaseDomain Domain)
        {
            int publishingHouseId = 0;
            PublishingHouseDomain publishingHouseDomain = Domain as PublishingHouseDomain;
            if (publishingHouseDomain == null)
                return publishingHouseId;
            using (ICommandContext dbContext = _publishingHouseDalCommands.DbContext)
            {
                if (dbContext.Database == null || !dbContext.Database.Exists())
                    return 0;
                using (DbContextTransaction transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        PublishingHouse publishingHouseDal = null;
                        if (publishingHouseDomain.Id == 0)
                            publishingHouseId = _publishingHouseDalCommands.GetNewId();
                        else
                            publishingHouseDal = _publishingHouseDalCommands.Find(publishingHouseDomain.Id);

                        if (publishingHouseDal == null)
                        {
                            publishingHouseDal = new PublishingHouse() { Id = publishingHouseId, Name = publishingHouseDomain.Name };
                            _publishingHouseDalCommands.Add(publishingHouseDal);
                        }
                        else
                        {
                            publishingHouseDal.Name = publishingHouseDomain.Name;
                            _publishingHouseDalCommands.Update(publishingHouseDal);
                        }
                        dbContext.SaveChanges();
                        transaction.Commit();

                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        publishingHouseId = 0;
                    }
                }
            }
            return publishingHouseId;
        }

        public override int Delete(IBaseDomain Domain)
        {
            int publishingHouseId = 0;
            PublishingHouseDomain publishingHouseDomain = Domain as PublishingHouseDomain;
            if (publishingHouseDomain == null)
                return publishingHouseId;
            if (Domain.Id == 0)
                return publishingHouseId;
            using (ICommandContext dbContext = _publishingHouseDalCommands.DbContext)
            {
                if (dbContext.Database == null || !dbContext.Database.Exists())
                    return 0;
                using (DbContextTransaction transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        PublishingHouse publishingHouseDal = _publishingHouseDalCommands.Find(publishingHouseDomain.Id);
                        if (publishingHouseDal == null)
                        {
                            transaction.Rollback();
                            return publishingHouseId;
                        }
                        if (CheckOnDelete(publishingHouseDal))
                        {
                            _publishingHouseDalCommands.Delete(publishingHouseDal);
                            dbContext.SaveChanges();
                            transaction.Commit();
                        }
                        else
                            transaction.Rollback();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        publishingHouseId = 0;
                    }
                }
            }
            return publishingHouseId;
        }

        private bool CheckOnDelete(PublishingHouse PublishingHouseDal)
        {
            return  PublishingHouseDal.BookSeries.Count() == 0
            && PublishingHouseDal.Books.Count() == 0;
        }

    }
}
