using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Library.Models
{
    public class PublishingHouseModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название издательства")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
        public virtual string Name { get; set; }

        public bool Checked { get; set; }

    }
}
