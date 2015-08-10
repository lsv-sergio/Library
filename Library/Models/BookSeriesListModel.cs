using LibraryDomain.Core.Links;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Library.Models
{
    public class BookSeriesListModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название серии")]
        public virtual string Name { get; set; }

        public bool Checked { get; set; }

        [Display(Name = "Издательство")]
        public IPublishingHouseLink  PublishingHouse { get; set; }

    }
}
