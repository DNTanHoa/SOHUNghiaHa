using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Mobile.Models
{
    public class Project
    {
        public string Code { get; set; }

        public decimal? Cost { get; set; }

        public int Id { get; set; }

        public Customer customer { get; set; }
    }
}
