//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryDal.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class PublishingHouse
    {
        public PublishingHouse()
        {
            this.Books = new HashSet<Book>();
            this.BookSeries = new HashSet<BookSerie>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<BookSerie> BookSeries { get; set; }
    }
}