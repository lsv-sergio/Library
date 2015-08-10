using LibraryDomain.Commands;
using LibraryDomain.Core;
using LibraryDomain.Core.Links;
using LibraryDomain.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Domains
{
    public abstract class BaseDomain : CoreDomain, IBaseDomain
    {
        private IDomainCommands _saveDeleteHandler;

        public BaseDomain(int Id, string Name, IDomainCommands SaveDeleteHandler):base(Id,Name)
        {
            _saveDeleteHandler = SaveDeleteHandler;
        }

        public virtual void SetName(string NewName)
        {
            this.Name = NewName;
        }

        public virtual void Save()
        {
            if (_saveDeleteHandler != null)
                _saveDeleteHandler.Save(this);
        }

        public virtual void Delete()
        {
            if (_saveDeleteHandler != null)
                _saveDeleteHandler.Delete(this);
        }

    }
}
