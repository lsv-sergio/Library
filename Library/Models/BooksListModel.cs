using LibraryDomain.Core.Links;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class BooksListModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название книги")]
        public virtual string Name { get; set; }

        public bool Checked { get; set; }

        [Display(Name = "Количество страниц")]
        public int PageCount { get; set; }
        
        [Display(Name = "Издатель")]
        public IPublishingHouseLink PublishingHouse { get; set; }

        [Display(Name = "Книжная серия ")]
        public IBookSeriesLink BookSeries { get; set; }

        [Display(Name = "Автор")]
        public IAuthorLink Author { get; set; }

    
    }
}