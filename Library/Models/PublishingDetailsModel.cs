using LibraryDomain.Core.Links;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Library.Models
{
    public class PublishingDetailsModel
    {
        public int Id { get; set; }

        [Display(Name="Название")]
        public string Name { get; set; }

        public IEnumerable<IBookLink> Books { get; set; }

        public IEnumerable<IBookSeriesLink> BookSeries { get; set; }

        public IEnumerable<IAuthorLink> Authors { get; set; }
 
    }
}
