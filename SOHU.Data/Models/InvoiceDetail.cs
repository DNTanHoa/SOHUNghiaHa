using System;
using System.Collections.Generic;

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
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalNoTax { get; set; }
        public decimal? TotalDiscount { get; set; }
        public decimal? Tax { get; set; }
        public decimal? TotalTax { get; set; }
        public decimal? Total { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? Gbpexchange { get; set; }
    }
}
