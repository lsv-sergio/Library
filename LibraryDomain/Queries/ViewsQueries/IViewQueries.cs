using LibraryDomain.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Queries.ViewsQueries
{
    public interface IViewQueries<TQuery> where TQuery :IBaseDomainView
    {

        TQuery GetById(int Id);

        IEnumerable<TQuery> GetAll();

    }
}
