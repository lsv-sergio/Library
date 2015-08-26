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
    public abstract class BaseQueries<TDal,TQuery>:IBaseQuery<TDal, TQuery>
        where TDal : class, IBaseDal
        where TQuery : ICoreDomain
    {
        protected IDalQuery<TDal> _dalQueries;

        public BaseQueries(IDalQueryFactory QueryFactory)
        {
            if (QueryFactory != null)
                _dalQueries = QueryFactory.GetQueryByClass<TDal>();
        }

        public virtual IEnumerable<TQuery> GetAll()
        {
            if (_dalQueries == null)
                return new List<TQuery>();
            return _dalQueries.GetAll().Select(x => DalToQuery(x)).AsEnumerable(); 
        }

        public virtual IEnumerable<TQuery> GetAllByFilter(Func<TDal, bool> Filter)
        {
            if (Filter == null)
                return new List<TQuery>();
            return _dalQueries.GetAllByFilter(Filter).Select(x => DalToQuery(x)).AsEnumerable();
        }

        public virtual TQuery GetByFilter(Func<TDal,bool> Filter)
        {
            if (Filter == null)
                return default(TQuery);
            return DalToQuery(_dalQueries.GetByFilter(Filter));
        }

        protected abstract TQuery DalToQuery(TDal Dal);

        protected bool IsValidValues(int Id, string Name)
        {
            return (Id > 0) && (!String.IsNullOrWhiteSpace(Name));
        }

    }
}
