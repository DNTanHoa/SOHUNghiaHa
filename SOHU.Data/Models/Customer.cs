using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Models
{
    public class Customer : BaseModel
    {
        public string FullName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string TaxCode { get; set; }

        public string Phone { get; set; }

        public string IdentityNumber { get; set; }

        public string CitizenNumber { get; set; }

        public string Passport { get; set; }
    }
}
