using LibraryDal.Infrastructure.QueriesDal.Factory;
using LibraryDomain.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Queries.ViewsQueries.Factory
{
    public class ViewQueriesFactory:IViewQueriesFactory
    {
       
        private readonly IDalQueryFactory _dalQueryFactory;

        Dictionary<Type, Type> _viewQueries = new Dictionary<Type, Type>();

        public ViewQueriesFactory(IDalQueryFactory DalQueryFactory)
        {
            _dalQueryFactory = DalQueryFactory;

            var linkQueriesTypes = Assembly.GetAssembly(this.GetType()).DefinedTypes
                .Where(x => x.BaseType != null)
                .Where(x => x.BaseType.IsAbstract && x.BaseType.IsGenericType)
                .Where(x => x.BaseType.GetGenericArguments().Count() > 1)
                .Where(x => x.GetGenericTypeDefinition() == typeof(BaseViewQueries<,>)).ToList();

            Type interfaceType;
            foreach(Type type in linkQueriesTypes)
            {
                interfaceType = type.BaseType.GetGenericArguments().FirstOrDefault(x => x.IsInterface && x.IsInstanceOfType(typeof(IBaseDomainView)));
                if (interfaceType == null)
                    continue;
                if (_viewQueries.Keys.Contains(interfaceType))
                    continue;
                _viewQueries.Add(interfaceType, type);
            }
        }
       
        public IViewQueries<TView> GetQuery<TView>() where TView : IBaseDomainView
        {
            if (!_viewQueries.Keys.Contains(typeof(TView)))
                return null;
            return (IViewQueries<TView>)Activator.CreateInstance(_viewQueries[typeof(TView)], new object[] { _dalQueryFactory });
        }
    
    }
}
