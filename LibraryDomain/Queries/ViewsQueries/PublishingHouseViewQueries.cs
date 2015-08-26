using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal;
using LibraryDal.Infrastructure.QueriesDal.Factory;
using System.Collections.Generic;
using LibraryDal.Infrastructure.QueriesDal.Abstract;
using LibraryDomain.Core.Views;
using LibraryDomain.Queries.AbstractQuery;

namespace LibraryDomain.Queries.ViewsQueries
{
    public class PublishingHouseViewQueries : BaseViewQueries<PublishingHouseView, IPublishingHouseDomainView>, IPublishingHouseViewQueries
    {

        public PublishingHouseViewQueries(IDalQueryFactory QueryFactory)
            : base(QueryFactory)
        {
            }

        public IPublishingHouseDomainView MakeView(int Id, string Name)
        {
            return new PublishingHouseDomainView(Id, Name);
        }

        protected override IPublishingHouseDomainView DalToQuery(PublishingHouseView Dal)
        {
            if (Dal == null)
                return null;
            return MakeView(Dal.Id, Dal.Name);
        }
    }
}
