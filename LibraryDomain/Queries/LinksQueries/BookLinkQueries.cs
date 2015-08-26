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
    public class BookLinkQueries : BaseLinkQueries<BooksView, IBookLink>, IBookLinkQueries
    {
        public BookLinkQueries(IDalQueryFactory QueryFactory):base(QueryFactory)
        {
        }

        public override IBookLink MakeLink(int Id, string Name)
        {
            return new BookLink(Id, Name.Trim());
        }

        protected override IBookLink DalToQuery(BooksView Dal)
        {
            return MakeLink(Dal.Id, Dal.Name);
        }
    }
}
