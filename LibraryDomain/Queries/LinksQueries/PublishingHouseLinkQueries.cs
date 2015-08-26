using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal;
using LibraryDal.Infrastructure.QueriesDal.Abstract;
using LibraryDal.Infrastructure.QueriesDal.Factory;
using LibraryDomain.Core.Links;
using LibraryDomain.Queries.AbstractQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Queries.LinksQueries
{
    public class PublishingHouseLinkQueries : BaseLinkQueries<PublishingHouseView, IPublishingHouseLink>, IPublishingHouseLinkQueries
    {
        public PublishingHouseLinkQueries(IDalQueryFactory QueryFactory):base(QueryFactory)
        {
        }

        public override IPublishingHouseLink MakeLink(int Id, string Name)
        {
            return new PublishingHouseLink(Id, Name.Trim());
        }

        protected override IPublishingHouseLink DalToQuery(PublishingHouseView Dal)
        {
            return MakeLink(Dal.Id, Dal.Name);
        }
    }
}
