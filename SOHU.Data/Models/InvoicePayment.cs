using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SOHU.Data.Models
{
    public partial class InvoicePayment : BaseModel
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public int? InvoiceID { get; set; }
        public int? PaymentID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PaymentCreated { get; set; }
        public decimal? TotalPayment { get; set; }
        public int? CurrencyID { get; set; }
        public decimal? Gbpexchange { get; set; }
    }
}
