using LibraryDomain.Queries.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Queries.LinksQueries
{
    public class LinkValidator:IValidator
    {
        private int _id;
        private string _name;
        public LinkValidator(int Id, string Name)
        {
            _id = Id;
            _name = Name;
        }

        public bool IsValid()
        {
            return _id > 0 && !String.IsNullOrWhiteSpace(_name);
        }
    }
}
