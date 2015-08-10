using LibraryDomain.Core.Links;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Library.Models
{
    public class BookDetailsModel
    {
        public int Id { get; set; }

        [Display(Name = "Название книги")]
        public virtual string Name { get; set; }

        [Display(Name = "Количество страниц")]
        public int PageCount { get; set; }

        [Display(Name = "Издательство")]
        public IPublishingHouseLink PublishingHouse { get; set; }

        [Display(Name = "Книжная серия")]
       public IBookSeriesLink BookSeries{ get; set; }

        [Display(Name = "Автор")]
        public IAuthorLink Author { get; set; }

    }
}
