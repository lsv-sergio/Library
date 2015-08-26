using Library.Models;
using LibraryDomain.Domains;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Library.Utils;
using LibraryDomain.Queries.DomainQueries;
using LibraryDomain.Core.Links;
using LibraryDomain.Queries.ViewsQueries;
using LibraryDomain.Core.Views;
using LibraryDomain.Queries.LinksQueries.Factory;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using LibraryDomain.Queries.LinksQueries;

namespace Library.Controllers
{
    public class BookSeriesController : BaseController
    {
        private IBookSeriesDomainQueries _domainQueries;
        protected IBookSeriesViewQueries _viewQueries;

        public BookSeriesController(IBookSeriesDomainQueries DomainQueries, IBookSeriesViewQueries ViewQueries, ILinkFacade LinkFactory)
            : base(LinkFactory)
        {
            _domainQueries = DomainQueries;
            _viewQueries = ViewQueries;
        }

        [HttpGet]
        public ActionResult Index()
        {
            //if (_bookSeriesQueries == null)
            if (_viewQueries == null)
                if (Request.IsAjaxRequest())
                    return PartialView("Error");
                else
                    return View("Error");
            IEnumerable<IBookSeriesDomainView> bookSeriesViewsList = _viewQueries.GetAll();
            IEnumerable<BookSeriesListModel> model = new List<BookSeriesListModel>();
            if (bookSeriesViewsList.Count() != 0)
                model = bookSeriesViewsList.Select(x => ViewToListModel(x)).OrderBy(x => x.Name).AsEnumerable();

            ViewBag.Title = "Список серий книг";
            if (Request.IsAjaxRequest())
                return PartialView("PartialIndex", model);
            return View("Index", model);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            if (Id == 0 || _domainQueries == null)

                return Index();

            IBookSeriesDomain bookSeriesDomain = _domainQueries.GetById(Id);
            if (bookSeriesDomain == null)
                return Index();
            BookSeriesEditModel model = DomainToEditModel(bookSeriesDomain);

            ViewBag.Title = "Изменение серии книг";

            if (Request.IsAjaxRequest())
                return PartialView("PartialEdit", model);
            return View("Edit", model);
        }

        [HttpGet]
        public ActionResult Details(int Id)
        {
            if (Id == 0 || _domainQueries == null)
                return Index();

            IBookSeriesDomain bookSeriesDomain = _domainQueries.GetById(Id);
            if (bookSeriesDomain == null)
                return Index();

            BookSeriesDetailsModel model = DomainToDetailsModel(bookSeriesDomain);

            ViewBag.Title = "Статистика серии книг";

            if (Request.IsAjaxRequest())
                return PartialView("PartialDetails", model);
            return View("Details", model);
        }

        [HttpPost]
        public ActionResult Edit(BookSeriesEditModel model)
        {
            if (_domainQueries == null)
            {
                ModelState.AddModelError("ErrorCreateQueries", "Ошибка создания запроса");
                if (Request.IsAjaxRequest())
                    return PartialView("PartialEdit", model);
                return View("Edit", model);
            }
            if (ModelState.IsValid)
            {
                IBookSeriesDomain bookSeriesDomain;
                if (model.Id == 0)
                {
                    bookSeriesDomain = _domainQueries.Create();
                    bookSeriesDomain.SetPublishingHouse(_linkFactory.GetById<IPublishingHouseLink>(model.PublishingHouseId));
                    bookSeriesDomain.SetName(model.Name.Trim());
                }
                else
                {
                    bookSeriesDomain = _domainQueries.GetById(model.Id);

                    if (!bookSeriesDomain.Name.Trim().ToLower().Equals(model.Name.Trim().ToLower()))
                        bookSeriesDomain.SetName(model.Name.Trim());

                    if (model.PublishingHouseId != bookSeriesDomain.PublishingHouse.Id)
                        bookSeriesDomain.SetPublishingHouse(_linkFactory.GetById<IPublishingHouseLink>(model.PublishingHouseId));
                }
                bookSeriesDomain.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Изменение серии книг";
            model = CompleetModel(model);

            if (Request.IsAjaxRequest())
                return PartialView("PartialEdit", model);
            return View("Edit", model);
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            if (Id == 0 || _domainQueries == null)
                return RedirectToAction("Index");

            IBookSeriesDomain bookSeriesDomain = _domainQueries.GetById(Id);
            if (bookSeriesDomain != null)
                bookSeriesDomain.Delete();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (_domainQueries == null)
                return Index();
            IBookSeriesDomain domain = _domainQueries.Create();
            BookSeriesEditModel model = DomainToEditModel(domain);

            ViewBag.Title = "Добавление серии книг";

            if (Request.IsAjaxRequest())
                return PartialView("PartialEdit", model);
            return View("Edit", model);
        }

        [HttpGet]
        public string GetPublisherBookSeries(int Id)
        {
            IEnumerable<SelectListItem> bookSeriesByPublishingHouse = new List<SelectListItem>() { new SelectListItem() { Text = "Без серии", Value = "0", Selected = true } };

            if (_linkFactory == null)
                return new JavaScriptSerializer().Serialize(bookSeriesByPublishingHouse);
            var linkQueries = (IBookSeriesLinkQueries)_linkFactory.GetQuery<IBookSeriesLink>();
            if (linkQueries == null)
                return new JavaScriptSerializer().Serialize(bookSeriesByPublishingHouse);
            bookSeriesByPublishingHouse = bookSeriesByPublishingHouse.Union(linkQueries.GetByPublishingHouseId(Id).Select(x => CreatesSelectListItem(0, x)));

            return new JavaScriptSerializer().Serialize(bookSeriesByPublishingHouse);
        }

        [NonAction]
        private BookSeriesEditModel CompleetModel(BookSeriesEditModel model)
        {
            model.PublishingHousesList = GetPublishingHousesList(model.PublishingHouseId);
            return model;
        }

        [NonAction]
        private BookSeriesEditModel DomainToEditModel(IBookSeriesDomain domain)
        {
            int publisingHouseId = domain.PublishingHouse == null ? 0 : domain.PublishingHouse.Id;
            return new BookSeriesEditModel()
            {
                Id = domain.Id,
                Name = domain.Name.Trim(),
                PublishingHouseId = publisingHouseId,
                PublishingHousesList = GetPublishingHousesList(publisingHouseId)
            };
        }

        [NonAction]
        private BookSeriesListModel ViewToListModel(IBookSeriesDomainView View)
        {
            return new BookSeriesListModel() { Id = View.Id, Name = View.Name.Trim(), PublishingHouse = View.PublishingHouse };
        }

        [NonAction]
        private BookSeriesDetailsModel DomainToDetailsModel(IBookSeriesDomain Domain)
        {

            BookSeriesDetailsModel model = new BookSeriesDetailsModel() { Id = Domain.Id, Name = Domain.Name.Trim() };
            model.Books = Domain.Books;
            model.PublishingHouse = Domain.PublishingHouse;
            model.Authors = Domain.Authors;

            return model;
        }

    }
}