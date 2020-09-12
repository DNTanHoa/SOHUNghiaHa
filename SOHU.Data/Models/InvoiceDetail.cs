using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SOHU.Data.Models
{
    public partial class InvoiceDetail : BaseModel
    {
       
        public int? InvoiceId { get; set; }
        public int? ProductId { get; set; }
        public string ManageCode { get; set; }
        public string ProductCode { get; set; }
        public string ManufacturingCode { get; set; }
        public int? UnitId { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? Quantity { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? UnitPrice { get; set; }
        public decimal? TotalNoTax { get; set; }
        public decimal? TotalDiscount { get; set; }
        public decimal? Tax { get; set; }
        public decimal? TotalTax { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? Total { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? Gbpexchange { get; set; }
    }
}
