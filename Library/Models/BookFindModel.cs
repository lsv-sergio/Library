using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Models
{
    public class BookFindModel
    {
        [Display(Name="Название")]
        public string Name { get; set; }

        [Display(Name = "Издательство")]
        public int PublishingHouseId { get; set; }
        [Display(Name = "Книжная серия")]
        public int BookSeriesId { get; set; }
        [Display(Name = "Автор")]
        public int AuthorId { get; set; }

        public IEnumerable<SelectListItem> AllAuthors { get; set; }
        public IEnumerable<SelectListItem> AllPublishingHouses { get; set; }
        public IEnumerable<SelectListItem> AllBookSeries { get; set; }

    }
}