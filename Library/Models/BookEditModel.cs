using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Library.Models
{
    public class BookEditModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название книги")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
        public virtual string Name { get; set; }

        [Required]
        [Display(Name = "Количество страниц")]
        [Range(1, Int32.MaxValue, ErrorMessage ="Ошибка диапазона значений")]
        public int PageCount { get; set; }

        [Required]
        [Display(Name = "Издательство")]
        [Range(1, Int32.MaxValue, ErrorMessage ="Ошибка выбора значения")]
        public int PublishingHouseId { get; set; }

        [Display(Name = "Книжная серия")]
         [Range(0, Int32.MaxValue, ErrorMessage ="Ошибка выбора значения")]
       public int BookSeriesId { get; set; }

        [Display(Name = "Автор")]
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage ="Ошибка выбора значения")]
        public int AuthorId { get; set; }

        public IEnumerable<SelectListItem> AllAuthors { get; set; }
        public IEnumerable<SelectListItem> AllPublishingHouses { get; set; }
        public IEnumerable<SelectListItem> AllBookSeries { get; set; }


    }
}
