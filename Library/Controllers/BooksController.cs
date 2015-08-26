using Library.Models;
using LibraryDomain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Library.Utils;
using LibraryDomain.Queries.DomainQueries;
using LibraryDomain.Queries.ViewsQueries;
using LibraryDomain.Core.Views;
using LibraryDomain.Core.Links;
using LibraryDomain.Queries.LinksQueries;
using LibraryDomain.Queries.LinksQueries.Factory;

namespace Library.Controllers
{
    public class BooksController : BaseController
    {
        IBookDomainQueries _bookQueries;
        IBookViewQueries _bookViewQueries;

        public BooksController(IBookDomainQueries BookQueries, IBookViewQueries BookViewQueries, ILinkFacade LinkFactory)
            : base(LinkFactory)
        {
            _bookQueries = BookQueries;
            _bookViewQueries = BookViewQueries;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (_bookQueries == null)
                if (Request.IsAjaxRequest())
                    return PartialView("Error");
                else
                    return View("Error");
            IEnumerable<IBookDomainView> bookDomainsList = _bookViewQueries.GetAll();
            IEnumerable<BooksListModel> model = new List<BooksListModel>();
            if (bookDomainsList.Count() != 0)
                model = bookDomainsList.Select(x => ViewToListModel(x)).OrderBy(x => x.Name).ToList();

            ViewBag.Title = "Список книг";
            if(Request.IsAjaxRequest())
                return PartialView("PartialIndex",model);
            return View("Index", model);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            if (Id == 0 || _bookQueries == null)
                return RedirectToAction("Index");

            IBookDomain bookDomain = _bookQueries.GetById(Id);
            if (bookDomain == null)
                return RedirectToAction("Index");

            BookEditModel model = DomainToEditModel(bookDomain);

            ViewBag.Title = "Изменение книги";
            if(Request.IsAjaxRequest())
                return PartialView("PartialEdit",model);
            return View("Edit",model);
        }

        [HttpPost]
        public ActionResult Edit(BookEditModel model)
        {
            if (_bookQueries == null)
            {
                ModelState.AddModelError("ErrorCreateQueries", "Ошибка создания запроса");
                if (Request.IsAjaxRequest())
                    return PartialView("PartialEdit", model);
                return View("Edit", model);
            }
            if (ModelState.IsValid)
            {
                IBookDomain bookDomain;
                if (model.Id == 0)
                {
                    bookDomain = _bookQueries.Create();
                    bookDomain.SetName(model.Name.Trim());
                    bookDomain.SetPageCount(model.PageCount);
                    bookDomain.SetAuthors(_linkFactory.GetById<IAuthorLink>(model.AuthorId));
                    bookDomain.SetPublishingHouse(_linkFactory.GetById<IPublishingHouseLink>(model.PublishingHouseId));
                    bookDomain.SetBookSeries(_linkFactory.GetById<IBookSeriesLink>(model.BookSeriesId));
                }
                else
                {
                    bookDomain = _bookQueries.GetById(model.Id);
                    if (bookDomain == null)
                    {
                        ModelState.AddModelError("ErrorFindDomain", "Объект с кодом " + model.Id.ToString() + " - не найден");
                        if (Request.IsAjaxRequest())
                            return PartialView("PartialEdit", model);
                        return View("Edit", model);

                    }
                    if (!bookDomain.Name.Trim().ToLower().Equals(model.Name.Trim().ToLower()))
                        bookDomain.SetName(model.Name.Trim());

                    if (bookDomain.PageCount != model.PageCount)
                        bookDomain.SetPageCount(model.PageCount);

                    if (bookDomain.Author.Id != model.AuthorId)
                        bookDomain.SetAuthors(_linkFactory.GetById<IAuthorLink>(model.AuthorId));

                    if (bookDomain.PublishingHouse.Id != model.PublishingHouseId)
                        bookDomain.SetPublishingHouse(_linkFactory.GetById<IPublishingHouseLink>(model.PublishingHouseId));

                    if (bookDomain.BookSeries.Id != model.BookSeriesId)
                        bookDomain.SetBookSeries(_linkFactory.GetById<IBookSeriesLink>(model.BookSeriesId));
                }

                bookDomain.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Изменение книги";

            model = CompleetEditModel(model);

            if (Request.IsAjaxRequest())
                return PartialView("PartialEdit", model);
            return View("Edit", model);

        }

        [HttpGet]
        public ActionResult Find()
        {
            var model = new BookFindModel();
            model.AllAuthors = GetAuthorsList(0);
            model.AllPublishingHouses = GetPublishingHousesList(0);
            model.AllBookSeries = new List<SelectListItem>() { new SelectListItem() { Text = "Без серии", Value = "0", Selected = true } };
            ViewBag.Title = "Поиск книги";
            if (Request.IsAjaxRequest())
                return PartialView("PartialFind", model);
            return View("Find", model);
        }

        [HttpPost]
        public ActionResult Find(BookFindModel SearchModel)
        {
            if (_bookQueries == null)
                if (Request.IsAjaxRequest())
                    return PartialView("Error");
                else
                    return View("Error");
            IEnumerable<IBookDomainView> filteredList = _bookViewQueries.GetAll();
            IList<Func<IBookDomainView, bool>> filters = new List<Func<IBookDomainView, bool>>();

            if (!String.IsNullOrEmpty(SearchModel.Name))
                filters.Add((x) => x.Name.ToLower().Contains(SearchModel.Name.ToLower().Trim()));

            if (SearchModel.PublishingHouseId > 0)
                filters.Add((x) => x.PublishingHouse.Id == SearchModel.PublishingHouseId);

            if (SearchModel.BookSeriesId > 0)
                filters.Add((x) => x.BookSeries.Id == SearchModel.BookSeriesId);

            if (SearchModel.AuthorId > 0)
                filters.Add((x) => x.Author.Id == SearchModel.AuthorId);

            foreach (Func<IBookDomainView, bool> actions in filters)
                filteredList = filteredList.Where(x => actions(x)).ToList();

            var model = filteredList.Select(x => ViewToListModel(x)).OrderBy(x => x.Name).ToList();
            ViewBag.Title = "Найденные книги";

            if (Request.IsAjaxRequest())
                return PartialView("PartialIndex", model);
            return View("Index", model);
        }

        [HttpGet]
        public ActionResult Details(int Id)
        {
            if (Id == 0 || _bookQueries == null)
                return RedirectToAction("Index");

            IBookDomain bookDomain = _bookQueries.GetById(Id);
            if (bookDomain == null)
                return RedirectToAction("Index");

            BookDetailsModel model = DomainToDetailsModel(bookDomain);

            ViewBag.Title = "Статистика книги";

            if (Request.IsAjaxRequest())
                return PartialView("PartialDetails", model);
            return View("Details", model);
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            if (Id == 0 || _bookQueries == null)
                return RedirectToAction("Index");

            IBookDomain bookDomain = _bookQueries.GetById(Id);
            if (bookDomain != null)
                bookDomain.Delete();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (_bookQueries == null)
                return Index();
            BookEditModel model = DomainToEditModel(_bookQueries.Create());

            ViewBag.Title = "Добавление книги";

            if (Request.IsAjaxRequest())
                return PartialView("PartialEdit", model);
            return View("Edit", model);
        }

        [NonAction]
        private BookEditModel CompleetEditModel(BookEditModel model)
        {
            model.AllPublishingHouses = GetPublishingHousesList(model.PublishingHouseId);

            IEnumerable<SelectListItem> allBookSeries = new List<SelectListItem>() { new SelectListItem() { Text = "Без серии", Value = "0", Selected = model.BookSeriesId == 0 } };
            if (model.PublishingHouseId != 0)
                allBookSeries = allBookSeries.Union(GetBookSeriesList(model.BookSeriesId)).ToList().AsEnumerable();

            model.AllBookSeries = allBookSeries;

            model.AllAuthors = GetAuthorsList(model.AuthorId);

            return model;
        }

        [NonAction]
        private BookEditModel DomainToEditModel(IBookDomain bookDomain)
        {
            BookEditModel model = new BookEditModel()
            {
                Id = bookDomain.Id,
                Name = bookDomain.Name.Trim(),
                PageCount = bookDomain.PageCount
            };
            if (bookDomain.PublishingHouse != null)
                model.PublishingHouseId = bookDomain.PublishingHouse.Id;

            if (bookDomain.BookSeries != null)
                model.BookSeriesId = bookDomain.BookSeries.Id;

            if (bookDomain.Author != null)
                model.AuthorId = bookDomain.Author.Id;

            model.AllPublishingHouses = GetPublishingHousesList(bookDomain.PublishingHouse);
            model.AllBookSeries = GetBookSeriesList(bookDomain.BookSeries);
            model.AllAuthors = GetAuthorsList(bookDomain.Author);

            return model;
        }

        [NonAction]
        private BookDetailsModel DomainToDetailsModel(IBookDomain bookDomain)
        {
            BookDetailsModel model = new BookDetailsModel();
            model.Name = bookDomain.Name.Trim();
            model.Id = bookDomain.Id;
            model.PageCount = bookDomain.PageCount;
            model.PublishingHouse = bookDomain.PublishingHouse;
            model.BookSeries = bookDomain.BookSeries;
            model.Author = bookDomain.Author;

            return model;
        }

        [NonAction]
        private BooksListModel ViewToListModel(IBookDomainView View)
        {
            return new BooksListModel()
            {
                Id = View.Id,
                Name = View.Name.Trim(),
                PageCount = View.PageCount,
                Author = View.Author,
                BookSeries = View.BookSeries,
                PublishingHouse = View.PublishingHouse
            };
        }

    }
}