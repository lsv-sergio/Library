using LibraryDomain.Core.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryDomain.Queries.LinksQueries.Factory
{
    public interface ILinkFacade
    {
        TLink MakeLink<TLink>(int Id, string Name) where TLink : ILink;

        TLink GetById<TLink>(int Id) where TLink : ILink;

        IEnumerable<TLink> GetAll<TLink>() where TLink : ILink;

        ILinkQueries<TLink> GetQuery<TLink>() where TLink : ILink;
    }
}
