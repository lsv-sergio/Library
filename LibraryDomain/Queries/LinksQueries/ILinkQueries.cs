using LibraryDomain.Core.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryDomain.Queries.LinksQueries
{
    public interface ILinkQueries<TLink> where TLink : ILink
    {
        TLink Create(int Id, string Name);

        TLink GetById(int Id);

        IEnumerable<TLink> GetAll();
    }
}
