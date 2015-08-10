using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal;
using LibraryDal.Infrastructure.QueriesDal.Abstract;
using LibraryDal.Infrastructure.QueriesDal.Factory;
using LibraryDomain.Core.Views;
using LibraryDomain.Queries.AbstractQuery;
using System.Collections.Generic;

namespace LibraryDomain.Queries.ViewsQueries
{
    public class AuthorViewQueries:BaseViewQueries<AuthorView, IAuthorDomainView>, IAuthorViewQueries
    {
        public AuthorViewQueries(IDalQueryFactory QueryFactory):base(QueryFactory)
        {
        }
       
        public IAuthorDomainView Create(int Id, string Name)
        {
            if (!IsValidValues(Id, Name))
                return null;
            return new AuthorDomainView(Id, Name);
        }

        protected override IAuthorDomainView DalToQuery(AuthorView Dal)
        {

            if (Dal == null)
                return null;
            return Create(Dal.Id, Dal.Name);

        }

    }
}
