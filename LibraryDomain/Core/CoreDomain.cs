using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Core
{
    public abstract class CoreDomain:ICoreDomain
    {
        private int _id;

        private string _name;

        public CoreDomain(int Id, string Name)
        {
            _id = Id;
            _name = Name;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            protected set
            {
                _name = value;
            }
        }

        public int Id
        {
            get { return _id; }
        }

    }
}
