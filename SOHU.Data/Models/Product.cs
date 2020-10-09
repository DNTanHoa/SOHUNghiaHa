using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SOHU.Data.Models
{
    public partial class Product : BaseModel
    {
        public int? CategoryID { get; set; }
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
        public int? Discount { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? QuantityInStockRoot { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? QuantityInStock { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? QuantityInput { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? QuantityOutput { get; set; }
        public decimal? PriceWebsite { get; set; }
        public decimal? PriceWebsitePromotion { get; set; }
        public decimal? PricePromotion { get; set; }
    }
}
