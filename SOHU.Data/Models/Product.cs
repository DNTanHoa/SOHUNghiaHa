using System;
using System.Collections.Generic;

namespace SOHU.Data.Models
{
    public partial class Product : BaseModel
    {
        public int? CategoryId { get; set; }
        public string Title { get; set; }
        public string Urlcode { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public string Tags { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public string ImageThumbnail { get; set; }
        public string Description { get; set; }
        public string ContentMain { get; set; }
        public decimal? Price { get; set; }
        public int? PriceUnitID { get; set; }
    }
}
