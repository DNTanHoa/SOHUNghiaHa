using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NghiaHa.CRM.Web.Models
{
    public class BaseViewModel
    {
        public string Content { get; set; }
        public int ID { get; set; }
        public int YearFinance { get; set; }
        public int MonthFinance { get; set; }
    }
}
