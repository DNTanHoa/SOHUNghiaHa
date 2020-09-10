using System;
using System.Collections.Generic;

namespace SOHU.Data.Models
{
    public partial class InvoicePayment : BaseModel
    {
      
        public int? InvoiceId { get; set; }
        public int? PaymentId { get; set; }
        public DateTime? PaymentCreated { get; set; }
        public decimal? TotalPayment { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? Gbpexchange { get; set; }
    }
}
