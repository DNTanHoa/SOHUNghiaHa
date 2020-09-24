using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SOHU.Data.Models
{
    public partial class InvoiceProperty : BaseModel
    {
        public int? InvoiceID { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
    }
}
