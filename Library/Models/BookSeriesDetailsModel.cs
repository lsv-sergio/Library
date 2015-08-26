using LibraryDomain.Core.Links;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class BookSeriesDetailsModel
    {
        public int Id { get; set; }

        [Display(Name = "Название серии")]
        public string Name { get; set; }

        [Display(Name = "Издательство")]
        public IPublishingHouseLink PublishingHouse { get; set; }

        public IEnumerable<IBookLink> Books { get; set; }

        public IEnumerable<IBookSeriesLink> BookSeries { get; set; }

        public IEnumerable<IAuthorLink> Authors { get; set; }
        
    }
}
