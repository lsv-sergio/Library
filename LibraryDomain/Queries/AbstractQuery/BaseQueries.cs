using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal;
using LibraryDal.Infrastructure.QueriesDal.Abstract;
using LibraryDal.Infrastructure.QueriesDal.Factory;
using LibraryDomain.Commands;
using LibraryDomain.Core;
using LibraryDomain.Core.Links;
using LibraryDomain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Queries.AbstractQuery
{
    public abstract class BaseQueries<TDal,TQuery>
        where TDal : class, IBaseDal
        where TQuery : ICoreDomain
    {
        protected IDalQuery<TDal> _dalQueries;

        public BaseQueries(IDalQueryFactory QueryFactory)
        {
            if (QueryFactory != null)
                _dalQueries = QueryFactory.GetQueryByClass<TDal>();
        }

        public virtual TQuery GetById(int Id)
        {
            if (Id == 0)
                return default(TQuery);
            if (_dalQueries == null)
                return default(TQuery);
            return DalToQuery(_dalQueries.GetById(Id));
        }

        public virtual IEnumerable<TQuery> GetAll()
        {
            if (_dalQueries == null)
                return new List<TQuery>();
            return _dalQueries.GetAll().Select(x => DalToQuery(x)).ToList().AsEnumerable(); 
        }

        protected abstract TQuery DalToQuery(TDal Dal);

        protected bool IsValidValues(int Id, string Name)
        {
            return (Id > 0) && (!String.IsNullOrWhiteSpace(Name));
        }

    }
}
