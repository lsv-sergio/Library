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
    
    public partial class AuthorsStatistic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public Nullable<int> BookSeriesId { get; set; }
        public string BookSeriesName { get; set; }
        public int PublishingHouseId { get; set; }
        public string PublishingHouseName { get; set; }
    }
}
