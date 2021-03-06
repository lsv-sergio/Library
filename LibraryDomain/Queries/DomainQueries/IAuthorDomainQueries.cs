﻿using LibraryDomain.Domains;
using LibraryDomain.Core.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryDal.EF;

namespace LibraryDomain.Queries.DomainQueries
{
    public interface IAuthorDomainQueries : IDomainQueries<AuthorView, IAuthorDomain>
    {
    }
}
