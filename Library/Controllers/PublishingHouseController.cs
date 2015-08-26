using Library.Models;
using LibraryDomain.Domains;
using LibraryDomain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Utils;
using LibraryDomain.Queries.DomainQueries;
using LibraryDomain.Queries.ViewsQueries;
using LibraryDomain.Queries.LinksQueries;
using LibraryDomain.Core.Views;
using LibraryDomain.Queries.LinksQueries.Factory;

namespace Library.Controllers
{
    public class PublishingHouseController : BaseController
    {
        IPublishingHouseDomainQueries _publishingHouseQueries;
        IPublishingHouseViewQueries _publishingHouseViewQueries;

        public PublishingHouseController(IPublishingHouseDomainQueries PublishingHouseQueries, IPublishingHouseViewQueries PublishingHouseViewQueries, ILinkFacade LinkFactory)
            :base(LinkFactory)
        {
            _publishingHouseQueries = PublishingHouseQueries;
            _publishingHouseViewQueries = PublishingHouseViewQueries;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (_publishingHouseQueries == null)
                if (Request.IsAjaxRequest())
                    return PartialView("Error");
                else
                    return View("Error");
            IEnumerable<IPublishingHouseDomainView> publishingHouseViewsList = _publishingHouseViewQueries.GetAll();
            IEnumerable<PublishingHouseModel> model = new List<PublishingHouseModel>();
            if (publishingHouseViewsList.Count() != 0)
                model = publishingHouseViewsList.Select(x => ViewToListModel(x)).OrderBy(x => x.Name).AsEnumerable();

            ViewBag.Title = "Список издательств";
            if (Request.IsAjaxRequest())
                return PartialView("PartialIndex", model);
            return View("Index", model);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            if (Id == 0 || _publishingHouseQueries == null)
                return Index();

            IPublishingHouseDomain publishingHouseDomain = _publishingHouseQueries.GetById(Id);
            if (publishingHouseDomain == null)
                return Index();

            PublishingHouseModel model = DomainToModel(publishingHouseDomain);

            ViewBag.Title = "Изменение издательства";

            if (Request.IsAjaxRequest())
                return PartialView("PartialEdit", model);
            return View("Edit", model);
        }

        [HttpGet]
        public ActionResult Details(int Id)
        {
            if (Id == 0 || _publishingHouseQueries == null)
                return Index();

            IPublishingHouseDomain publishingHouseDomain = _publishingHouseQueries.GetById(Id);
            if (publishingHouseDomain == null)
                return Index();

            PublishingDetailsModel model = DomainToDetailModel(publishingHouseDomain);

            ViewBag.Title = "Статистика издательства";

            if (Request.IsAjaxRequest())
                return PartialView("PartialDetails", model);
            return View("Details", model);
        }

        [HttpPost]
        public ActionResult Edit(PublishingHouseModel model)
        {
            if (_publishingHouseQueries == null)
            {
                ModelState.AddModelError("ErrorCreateQueries", "Ошибка создания запроса");
                if (Request.IsAjaxRequest())
                    return PartialView("PartialEdit", model);
                return View("Edit", model);
            }
            if (ModelState.IsValid)
            {
                IPublishingHouseDomain publishingHouseDomain;
                if (model.Id == 0)
                    publishingHouseDomain = _publishingHouseQueries.Create();
                else
                    publishingHouseDomain = _publishingHouseQueries.GetById(model.Id);

                if (publishingHouseDomain == null)
                {
                    ModelState.AddModelError("ErrorCreateDomain", "Ошибка создания обекта");
                    if (Request.IsAjaxRequest())
                        return PartialView("PartialEdit", model);
                    return View("Edit", model);
                }
                if (!publishingHouseDomain.Name.Trim().ToLower().Equals(model.Name.Trim().ToLower()))
                    publishingHouseDomain.SetName(model.Name.Trim());
                
                publishingHouseDomain.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Изменение издательства";

            if (Request.IsAjaxRequest())
                return PartialView("PartialEdit", model);
            return View("Edit", model);
        }

        public ActionResult Delete(int Id)
        {
            if (Id == 0 || _publishingHouseQueries == null)
                return RedirectToAction("Index");

            IPublishingHouseDomain publishingHouseDomain = _publishingHouseQueries.GetById(Id);
            if (publishingHouseDomain != null)
                publishingHouseDomain.Delete();

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            if (_publishingHouseQueries == null)
                return Index();
            PublishingHouseModel model = DomainToModel(_publishingHouseQueries.Create());

            ViewBag.Title = "Добавление издательства";

            if (Request.IsAjaxRequest())
                return PartialView("PartialEdit", model);
            return View("Edit", model);
        }

        [NonAction]
        private PublishingHouseModel ViewToListModel(IPublishingHouseDomainView View)
        {
            return new PublishingHouseModel() { Id = View.Id, Name = View.Name.Trim() };
        }

        [NonAction]
        private PublishingHouseModel DomainToModel(IPublishingHouseDomain domain)
        {
            return new PublishingHouseModel() { Id = domain.Id, Name = domain.Name.Trim() };
        }

        [NonAction]
        private PublishingDetailsModel DomainToDetailModel(IPublishingHouseDomain publishingHouseDomain)
        {
            PublishingDetailsModel model = new PublishingDetailsModel() { Id = publishingHouseDomain.Id, Name = publishingHouseDomain.Name.Trim() };

            model.Authors = publishingHouseDomain.Authors;
            model.Books = publishingHouseDomain.Books;
            model.BookSeries = publishingHouseDomain.BookSeries;
            return model;
        }

    }
}