using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Library.Models
{
    public class BookSeriesEditModel
    {

        public int Id { get; set; }

        [Required]
        [Display(Name = "Название серии")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
        public virtual string Name { get; set; }

        [Required]
        [Display(Name = "Издательство")]
        [Range(1, Int32.MaxValue, ErrorMessage ="Ошибка выбора значения")]
        public int PublishingHouseId { get; set; }

        public IEnumerable<SelectListItem> PublishingHousesList { get; set; }

    }
}
