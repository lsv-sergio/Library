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
    public class AuthorLinkQueries : BaseLinkQueries<AuthorView, IAuthorLink>, IAuthorLinkQueries
    {
        public AuthorLinkQueries(IDalQueryFactory QueryFactory):base(QueryFactory)
        {
        }

        public IAuthorLink Create(int Id, string Name)
        {
            return new AuthorLink(Id, Name.Trim());
        }

        protected override IAuthorLink DalToQuery(AuthorView Dal)
        {
            return Create(Dal.Id, Dal.Name);
        }
    }
}
