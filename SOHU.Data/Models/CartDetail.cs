using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Models
{
    public class CartDetail : BaseModel
    {
        public int? CartId { get; set; }
        public int? ProductId { get; set; }
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

        public void InitValue()
        {
            this.TotalTax = this.UnitPrice * this.Quantity * this.Tax;
            this.TotalNoTax = this.UnitPrice * this.Quantity;
            this.Total = this.TotalNoTax + this.Tax - this.TotalDiscount;
        }
    }
}
