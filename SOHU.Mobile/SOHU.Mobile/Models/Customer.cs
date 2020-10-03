using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Mobile.Models
{
    public class Customer
    {
        public string FullName { get; set; }

        public string Code { get; set; }

        public int Id { get; set; }

        public string Address { get; set; }

        public string TaxCode { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int ParentID { get; set; }

        public string ContactFullName { get; set; }

        public string ContactPosition { get; set; }

        public string BankAccount { get; set; }

        public string BankName { get; set; }
    }
}
