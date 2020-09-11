using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NghiaHa.CRM.Web.Models
{
    public class DashboardViewModel
    {
        public int CustomerCount { get; set; }

        public int InvoiceCount { get; set; }

        public decimal Revenue { get; set; }

        public decimal EmployeeCount { get; set; }
    }
}
