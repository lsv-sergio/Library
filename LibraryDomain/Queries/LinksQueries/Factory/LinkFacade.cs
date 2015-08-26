using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal;
using LibraryDal.Infrastructure.QueriesDal.Factory;
using LibraryDomain.Core.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Queries.LinksQueries.Factory
{
    public class LinkFacade:ILinkFacade
    {
        private readonly IDalQueryFactory _dalQueryFactory;

        Dictionary<Type, Type> _linkQueries = new Dictionary<Type, Type>();

        public LinkFacade(IDalQueryFactory DalQueryFactory)
        {
            _dalQueryFactory = DalQueryFactory;

            var linkQueriesTypes = Assembly.GetAssembly(this.GetType()).DefinedTypes
                .Where(x => x.BaseType != null)
                .Where(x => x.BaseType.IsAbstract && x.BaseType.IsGenericType)
                .Where(x => x.BaseType.GetGenericArguments().Count() > 1)
                .Where(x => x.BaseType.GetGenericTypeDefinition() == typeof(BaseLinkQueries<,>)).ToList();

            Type interfaceType;
            foreach(Type type in linkQueriesTypes)
            {
                interfaceType = type.BaseType.GetGenericArguments().FirstOrDefault(x => x.IsInterface && x.GetInterfaces().Contains(typeof(ILink)));
                if (interfaceType == null)
                    continue;
                if (_linkQueries.Keys.Contains(interfaceType))
                    continue;
                _linkQueries.Add(interfaceType, type);
            }
        }
       
        public TLink MakeLink<TLink>(int Id, string Name) where TLink :ILink
        {
            if (!new LinkValidator(Id, Name).IsValid())
                return default(TLink);
            ILinkQueries<TLink> linkQueries = GetQuery<TLink>();
            if (linkQueries == null)
                return default(TLink);
            return linkQueries.MakeLink(Id, Name);
        }

        public TLink GetById<TLink>(int Id) where TLink : ILink
        {
            if (Id == 0)
                return default(TLink);
            ILinkQueries<TLink> linkQueries = GetQuery<TLink>();
            if (linkQueries == null)
                return default(TLink);
            return linkQueries.GetById(Id);
        }

        public IEnumerable<TLink> GetAll<TLink>() where TLink : ILink
        {
            ILinkQueries<TLink> linkQueries = GetQuery<TLink>();
            if (linkQueries == null)
                return new List<TLink>();
            return linkQueries.GetAll();
        }
 
        public ILinkQueries<TLink> GetQuery<TLink>() where TLink : ILink
        {
            if (!_linkQueries.Keys.Contains(typeof(TLink)))
                return null;
            return (ILinkQueries<TLink>)Activator.CreateInstance(_linkQueries[typeof(TLink)], new object[] { _dalQueryFactory });
        }
    }
}
