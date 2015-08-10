using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Library.Models
{
    public class AuthorModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Автор")]
        [StringLength(50,MinimumLength=3, ErrorMessage="Длина строки должна быть от 3 до 50 символов")]
        public virtual string Name { get; set; }

        public bool Checked { get; set; }

    }
}
