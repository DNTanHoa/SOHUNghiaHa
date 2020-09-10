using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Models
{
    public class Cart : BaseModel
    {
        public string ManageCode { get; set; }
        public string CartCode { get; set; }
        public decimal? TotalNoTax { get; set; }
        public decimal? Tax { get; set; }
        public decimal? TotalTax { get; set; }
        public decimal? TotalShipCost { get; set; }
        public decimal? TotalDiscount { get; set; }
        public decimal? Total { get; set; }
        public decimal? TotalPaid { get; set; }
        public decimal? TotalDebt { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? Gbpexchange { get; set; }
    }
}
