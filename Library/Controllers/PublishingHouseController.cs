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

        public PublishingHouseController(IPublishingHouseDomainQueries PublishingHouseQueries, IPublishingHouseViewQueries PublishingHouseViewQueries, ILinkFactory LinkFactory)
            :base(LinkFactory)
        {
            _publishingHouseQueries = PublishingHouseQueries;
            _publishingHouseViewQueries = PublishingHouseViewQueries;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (_publishingHouseQueries == null)
                return View("Error");
            IEnumerable<IPublishingHouseDomainView> publishingHouseViewsList = _publishingHouseViewQueries.GetAll();
            IEnumerable<PublishingHouseModel> model = new List<PublishingHouseModel>();
            if (publishingHouseViewsList.Count() != 0)
                model = publishingHouseViewsList.Select(x => ViewToListModel(x)).OrderBy(x => x.Name).AsEnumerable();

            ViewBag.Title = "Список издательств";

            return View(model);
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

            return View(model);
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

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PublishingHouseModel model)
        {
            if (_publishingHouseQueries == null)
            {
                ModelState.AddModelError("ErrorCreateQueries", "Ошибка создания запроса");
                return View(model);
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
                    return View(model);
                }
                if (!publishingHouseDomain.Name.Trim().ToLower().Equals(model.Name.Trim().ToLower()))
                    publishingHouseDomain.SetName(model.Name.Trim());
                
                publishingHouseDomain.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Изменение издательства";

            return View(model);
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