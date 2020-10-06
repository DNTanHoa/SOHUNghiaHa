using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Euronailsupply.Models
{
    public class BaseViewModel
    {
        public int ID { get; set; }
        public int YearFinance { get; set; }
        public int MonthFinance { get; set; }
        public string MenuTop { get; set; }
    }
}
