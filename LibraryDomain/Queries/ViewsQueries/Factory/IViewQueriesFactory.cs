using LibraryDomain.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryDomain.Queries.ViewsQueries.Factory
{
    public interface IViewQueriesFactory
    {
        IViewQueries<TView> GetQuery<TView>() where TView : IBaseDomainView;
    }
}
