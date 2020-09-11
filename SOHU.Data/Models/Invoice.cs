using System;
using System.Collections.Generic;

namespace SOHU.Data.Models
{
    public partial class Invoice : BaseModel
    {
      
        public int? CategoryId { get; set; }
        public string ManageCode { get; set; }
        public string InvoiceCode { get; set; }
        public string MakeCode { get; set; }
        public DateTime? InvoiceCreated { get; set; }
        public int? MakeSideId { get; set; }
        public int? BuyId { get; set; }
        public string BuyName { get; set; }
        public string BuyAddress { get; set; }
        public string BuyEmail { get; set; }
        public string BuyPhone { get; set; }
        public int? SellId { get; set; }
        public string SellName { get; set; }
        public string SellAddress { get; set; }
        public string SellEmail { get; set; }
        public string SellPhone { get; set; }
        public int? StatusId { get; set; }
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
