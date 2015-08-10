using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal.Abstract;
using LibraryDal.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDal.Infrastructure.QueriesDal.Factory
{
    public class DalQueryFactory : IDalQueryFactory
    {
        private readonly IQueryContext _uow;
        Dictionary<Type, Type> _queriesList = new Dictionary<Type, Type>();

        public DalQueryFactory(IQueryContext UoW)
        {
            _uow = UoW;
            var instaledTypes = Assembly.GetAssembly(this.GetType()).DefinedTypes
                .Where(x => x.BaseType != null)
                .Where(x => x.BaseType.IsAbstract && x.BaseType.IsGenericType)
                .Where(x => x.BaseType.GetGenericTypeDefinition() == typeof(DalQuery<>))
                .ToList();
            Type interfaceType;

            foreach (Type type in instaledTypes)
            {
                interfaceType = type.GetInterfaces()
                    .Where(x => x.Name.ToLower().Contains(type.Name.ToLower().Trim()))
                    .FirstOrDefault();
                AddTypes(interfaceType, type);
                if (!type.BaseType.IsGenericType)
                    continue;
                interfaceType = type.BaseType.GetGenericArguments().FirstOrDefault();
                AddTypes(interfaceType, type);
            }
        }

        public TQuery GetQueryByInterface<TQuery>()
        {
            if (!_queriesList.Keys.Contains(typeof(TQuery)))
                return default(TQuery);
            return (TQuery)Activator.CreateInstance(_queriesList[typeof(TQuery)], new[] { _uow });
        }

        public IDalQuery<TQuery> GetQueryByClass<TQuery>() where TQuery : class, IBaseDal
        {
            if (!_queriesList.Keys.Contains(typeof(TQuery)))
                return null;
            return (IDalQuery<TQuery>)Activator.CreateInstance(_queriesList[typeof(TQuery)], new[] { _uow });
        }

        private void AddTypes(Type interfaceType, Type type)
        {
            if (interfaceType != null)
                if (!_queriesList.Keys.Contains(interfaceType))
                    _queriesList.Add(interfaceType, type);
        }

    }
}
