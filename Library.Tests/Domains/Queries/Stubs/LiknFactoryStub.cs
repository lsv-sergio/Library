using LibraryDomain.Core.Links;
using LibraryDomain.Queries.LinksQueries;
using LibraryDomain.Queries.LinksQueries.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Domains.Queries.Stubs
{
    class LiknFactoryStub:ILinkFacade
    {
        Dictionary<Type, Type> _links = new Dictionary<Type, Type>();

        public LiknFactoryStub()
        {
            _links.Add(typeof(IAuthorLink), typeof(AuthorLink));
            _links.Add(typeof(IBookLink), typeof(BookLink));
            _links.Add(typeof(IBookSeriesLink), typeof(BookSeriesLink));
            _links.Add(typeof(IPublishingHouseLink), typeof(PublishingHouseLink));
        }

        public TLink MakeLink<TLink>(int Id, string Name) where TLink : ILink
        {
            if (!_links.Keys.Contains(typeof(TLink)))
                return default(TLink);
            return (TLink)Activator.CreateInstance(_links[typeof(TLink)], new object[]{Id, Name});
        }

        public TLink GetById<TLink>(int Id) where TLink : ILink
        {
            if (!_links.Keys.Contains(typeof(TLink)))
                return default(TLink);
            return (TLink)Activator.CreateInstance(_links[typeof(TLink)], new object[] { Id, Id.ToString() });
        }

        public IEnumerable<TLink> GetAll<TLink>() where TLink : ILink
        {
            List<TLink> list = new List<TLink>();
            if (!_links.Keys.Contains(typeof(TLink)))
                return list;
            TLink link = (TLink)Activator.CreateInstance(_links[typeof(TLink)], new object[] { 8, "Link" });
            list.Add(link);
            return list;
        }

        public ILinkQueries<TLink> GetQuery<TLink>() where TLink : ILink
        {
            return null;
        }
    }
}
