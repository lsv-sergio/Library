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
using LibraryDomain.Core.Views;
using LibraryDomain.Queries.LinksQueries;
using LibraryDomain.Queries.LinksQueries.Factory;

namespace Library.Controllers
{
    public class AuthorsController : BaseController
    {
        IAuthorDomainQueries _authorDomainQueries;
        IAuthorViewQueries _authorViewQueries;

        public AuthorsController(IAuthorDomainQueries AuthorDomainQueries, IAuthorViewQueries AuthorViewQueries, ILinkFactory LinkFactory)
            : base(LinkFactory)
        {
            _authorDomainQueries = AuthorDomainQueries;
            _authorViewQueries = AuthorViewQueries;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (_authorDomainQueries == null)
                if (Request.IsAjaxRequest())
                    return PartialView("Error");
                else
                    return View("Error");
            IEnumerable<IAuthorDomainView> authorDomainsList = _authorViewQueries.GetAll();
            IEnumerable<AuthorModel> model = new List<AuthorModel>();
            if (authorDomainsList.Count() != 0)
                model = authorDomainsList.Select(x => ViewToListModel(x)).OrderBy(x => x.Name).AsEnumerable();
            ViewBag.Title = "Список авторов";
            if (Request.IsAjaxRequest())
                return PartialView("PartialIndex", model);
            return View("Index", model);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            if (Id == 0 || _authorDomainQueries == null)
                return Index();

            IAuthorDomain authorDomain = _authorDomainQueries.GetById(Id);
            if (authorDomain == null)
                return Index();

            AuthorModel model = DomainToEditModel(authorDomain);


            ViewBag.Title = "Изменение автора";

            if (Request.IsAjaxRequest())
                return PartialView("PartialEdit", model);
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(AuthorModel model)
        {
            if (_authorDomainQueries == null)
            {
                ModelState.AddModelError("ErrorCreateQueries", "Ошибка создания запроса");
                if (Request.IsAjaxRequest())
                    return PartialView("PartialEdit", model);
                return View("Edit", model);
            }
            if (ModelState.IsValid)
            {
                IAuthorDomain authorDomain;
                if (model.Id == 0)
                    authorDomain = _authorDomainQueries.Create();
                else
                {
                    authorDomain = _authorDomainQueries.GetById(model.Id);
                    if (authorDomain == null)
                    {
                        ModelState.AddModelError("ErrorFindDomain", "Автор с id = " + model.Id.ToString() + " - не найден");
                        if (Request.IsAjaxRequest())
                            return PartialView("PartialEdit", model);
                        return View("Edit", model);
                    }
                }
                if (!authorDomain.Name.Trim().ToLower().Equals(model.Name.Trim().ToLower()))
                {
                    authorDomain.SetName(model.Name.Trim());
                    authorDomain.Save();
                }
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Изменение автора";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int Id)
        {
            if (Id == 0 || _authorDomainQueries == null)
                return Index();

            IAuthorDomain authorDomain = _authorDomainQueries.GetById(Id);
            if (authorDomain == null)
                return Index();

            AuthorDetailsModel model = DomainToDetailModel(authorDomain);

            ViewBag.Title = "Статистика автора";

            if (Request.IsAjaxRequest())
                return PartialView("PartialDetails", model);
            return View("Details", model);
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            if (Id == 0 || _authorDomainQueries == null)
                return RedirectToAction("Index");

            IAuthorDomain authorDomain = _authorDomainQueries.GetById(Id);
            if (authorDomain != null)
                authorDomain.Delete();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (_authorDomainQueries == null)
                return Index();
            AuthorModel model = DomainToEditModel(_authorDomainQueries.Create());

            ViewBag.Title = "Добавление автора";

            if (Request.IsAjaxRequest())
                return PartialView("PartialEdit", model);
            return View("Edit", model);
        }

        private AuthorModel ViewToListModel(IAuthorDomainView View)
        {
            return new AuthorModel() { Id = View.Id, Name = View.Name.Trim() };
        }

        private AuthorModel DomainToEditModel(IAuthorDomain Domain)
        {
            return new AuthorModel() { Id = Domain.Id, Name = Domain.Name.Trim() };
        }

        private AuthorDetailsModel DomainToDetailModel(IAuthorDomain Domain)
        {
            AuthorDetailsModel model = new AuthorDetailsModel() { Id = Domain.Id, Name = Domain.Name };

            model.Books = Domain.Books;
            model.BookSeries = Domain.BookSeries;
            model.PublishingHouse = Domain.PublishingHouses;

            return model;
        }



    }

}