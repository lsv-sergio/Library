using LibraryDal.Infrastructure.QueriesDal.Factory;
using LibraryDomain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Queries.DomainQueries.Factory
{
    public class DomainQueriesFactory : IDomainQueriesFactory
    {
        
        private readonly IDalQueryFactory _dalQueryFactory;

        Dictionary<Type, Type> _domainQueries = new Dictionary<Type, Type>();

        public DomainQueriesFactory(IDalQueryFactory DalQueryFactory)
        {
            _dalQueryFactory = DalQueryFactory;

            var linkQueriesTypes = Assembly.GetAssembly(this.GetType()).DefinedTypes
                .Where(x => x.BaseType != null)
                .Where(x => x.BaseType.IsAbstract && x.BaseType.IsGenericType)
                .Where(x => x.BaseType.GetGenericArguments().Count() > 1)
                .Where(x => x.GetGenericTypeDefinition() == typeof(BaseDomainQueries<,>)).ToList();

            Type interfaceType;
            foreach(Type type in linkQueriesTypes)
            {
                interfaceType = type.BaseType.GetGenericArguments().FirstOrDefault(x => x.IsInterface && x.IsInstanceOfType(typeof(IBaseDomain)));
                if (interfaceType == null)
                    continue;
                if (_domainQueries.Keys.Contains(interfaceType))
                    continue;
                _domainQueries.Add(interfaceType, type);
            }
        }
       
        public IDomainQueries<TDomain> GetQuery<TDomain>() where TDomain : IBaseDomain
        {
            if (!_domainQueries.Keys.Contains(typeof(TDomain)))
                return null;
            return (IDomainQueries<TDomain>)Activator.CreateInstance(_domainQueries[typeof(TDomain)], new object[] { _dalQueryFactory });
        }

    }
}
