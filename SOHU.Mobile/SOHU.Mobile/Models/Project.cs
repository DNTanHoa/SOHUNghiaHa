using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Mobile.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string BuyName { get; set; }

        public string BuyAddress { get; set; }

        public string InvoiceName { get; set; }

        public string HopDongTitle { get; set; }

        public string HopDongTitleSub { get; set; }

        public decimal TotalNoTax { get; set; }

        public decimal TotalTax { get; set; }

        public decimal Total { get; set; }

        public decimal TotalPaid { get; set; }

        public decimal TotalDebt { get; set; }
    }
}
