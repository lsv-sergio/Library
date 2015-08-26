using LibraryDomain.Core;
using LibraryDomain.Core.Links;
using LibraryDomain.Core.Views;
using LibraryDomain.Domains;
using LibraryDomain.Queries.DomainQueries;
using LibraryDomain.Queries.DomainQueries.Factory;
using LibraryDomain.Queries.LinksQueries;
using LibraryDomain.Queries.LinksQueries.Factory;
using LibraryDomain.Queries.ViewsQueries;
using LibraryDomain.Queries.ViewsQueries.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ILinkFacade _linkFactory;
        public BaseController(ILinkFacade LinkFactory)
        {
            _linkFactory = LinkFactory;
        }

        [NonAction]
        protected IEnumerable<SelectListItem> GetPublishingHousesList(IPublishingHouseLink CurrentPublishingHouse)
        {
            int PublishingHouseId = CurrentPublishingHouse == null ? 0 : CurrentPublishingHouse.Id;

            return GetPublishingHousesList(PublishingHouseId);
        }
        
        [NonAction]
        protected IEnumerable<SelectListItem> GetBookSeriesList(IBookSeriesLink CurrentBookSeriesId)
        {
            int bookSeriesId = CurrentBookSeriesId == null ? 0 : CurrentBookSeriesId.Id;
            
            return GetBookSeriesList(bookSeriesId);
        }

        [NonAction]
        protected IEnumerable<SelectListItem> GetAuthorsList(IAuthorLink CurrentAuthor)
        {

            int authorId = CurrentAuthor == null ? 0 : CurrentAuthor.Id;

            return GetAuthorsList(authorId);
        }

        [NonAction]
        protected IEnumerable<SelectListItem> GetPublishingHousesList(int CurrentPublishingHouseId)
        {
            IEnumerable<SelectListItem> allPublishingHouse = new List<SelectListItem>() { new SelectListItem() { Text = "Выбрать издательство", Value = "0", Selected = CurrentPublishingHouseId == 0 } };
            if (_linkFactory != null)
            {
                IEnumerable<IPublishingHouseLink> publishingHouseLinks = _linkFactory.GetAll<IPublishingHouseLink>().OrderBy(x => x.Name).ToList();
                if (publishingHouseLinks.Count() > 0)
                    allPublishingHouse = allPublishingHouse.Union(publishingHouseLinks.Select(x => CreatesSelectListItem(CurrentPublishingHouseId, x))).ToList().AsEnumerable();
            }
            return allPublishingHouse;
        }
        
        [NonAction]
        protected IEnumerable<SelectListItem> GetBookSeriesList(int CurrentBookSeriesId)
        {
            IEnumerable<SelectListItem> allBookSeries = new List<SelectListItem>() { new SelectListItem() { Text = "Без серии", Value = "0", Selected = CurrentBookSeriesId == 0 } };

            if (_linkFactory != null)
            {
                IEnumerable<IBookSeriesLink> bookSeriesDomains = _linkFactory.GetAll<IBookSeriesLink>().OrderBy(x => x.Name);
                if (bookSeriesDomains.Count() > 0)
                    allBookSeries = allBookSeries.Union(bookSeriesDomains.Select(x => CreatesSelectListItem(CurrentBookSeriesId, x))).ToList().AsEnumerable();
            }
            return allBookSeries;
        }

        [NonAction]
        protected IEnumerable<SelectListItem> GetAuthorsList(int CurrentAuthorId)
        {

            IEnumerable<SelectListItem> allAuthors = new List<SelectListItem>() { new SelectListItem() { Text = "Выбрать автора", Value = "0", Selected = CurrentAuthorId == 0 } };
            if (_linkFactory != null)
            {
                IEnumerable<IAuthorLink> allAuthorLinks = _linkFactory.GetAll<IAuthorLink>().OrderBy(x => x.Name).ToList().AsReadOnly();
                if (allAuthorLinks.Count() > 0)
                    allAuthors = allAuthors.Union(allAuthorLinks.Select(x => CreatesSelectListItem(CurrentAuthorId, x))).ToList().AsEnumerable();
            }
            return allAuthors;
        }

        [NonAction]
        protected SelectListItem CreatesSelectListItem(int Id, ILink elem)
        {
            return new SelectListItem()
            {
                Text = elem.Name,
                Value = elem.Id.ToString(),
                Selected = elem.Id == Id
            };
        }
    }
}