using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Mobile.Models
{
    public class Invoice
    {
        public string ManageCode { get; set; }

        public string InvoiceCode { get; set; }

        public int Id { get; set; }

        public Customer customer { get; set; }
    }
}
