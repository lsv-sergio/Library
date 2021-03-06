﻿using LibraryDal.EF;
using LibraryDal.Infrastructure.QueriesDal;
using LibraryDal.Infrastructure.QueriesDal.Factory;
using LibraryDomain.Core.Links;
using LibraryDal.Infrastructure.QueriesDal.Abstract;
using LibraryDomain.Queries.LinksQueries;
using LibraryDomain.Core.Views;
using LibraryDomain.Queries.AbstractQuery;
using LibraryDomain.Queries.LinksQueries.Factory;
using System.Collections.Generic;
using System.Linq;

namespace LibraryDomain.Queries.ViewsQueries
{
    public class BookSeriesViewQueries : BaseViewQueries<BookSeriesView, IBookSeriesDomainView>, IBookSeriesViewQueries
    {
        private ILinkFacade _linkFactory;
        private IDalQueryFactory _dalQueryFactory;

        public BookSeriesViewQueries(IDalQueryFactory DalQueryFactory, ILinkFacade LinkFactory)
            : base(DalQueryFactory)
        {
            _dalQueryFactory = DalQueryFactory;
            _linkFactory = LinkFactory;
        }

        public IBookSeriesDomainView MakeView(int Id, string Name, IPublishingHouseLink PublishingHouse)
        {
            if (!IsValidValues(Id, Name))
                return null;
            return new BookSeriesDomainView(Id, Name, PublishingHouse);
        }

        protected override IBookSeriesDomainView DalToQuery(BookSeriesView Dal)
        {
            if (Dal == null || _linkFactory == null)
                return null;
            return MakeView(Dal.Id, Dal.Name, _linkFactory.MakeLink<IPublishingHouseLink>(Dal.PublishingHouseId, Dal.PublishingHouseName));
        }
        
    }
}
