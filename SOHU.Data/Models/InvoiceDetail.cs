using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SOHU.Data.Models
{
    public partial class InvoiceDetail : BaseModel
    {
        public int? CategoryID { get; set; }
        public int? InvoiceID { get; set; }
        public int? ProductID { get; set; }
        public string ManageCode { get; set; }
        public string ProductCode { get; set; }
        public string ManufacturingCode { get; set; }
        public int? UnitID { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? Quantity { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? Quantity01 { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? UnitPrice { get; set; }
        public decimal? TotalNoTax { get; set; }
        public decimal? TotalDiscount { get; set; }
        public decimal? Tax { get; set; }
        public decimal? TotalTax { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? Total { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? Total01 { get; set; }
        public int? CurrencyID { get; set; }

        public decimal? Gbpexchange { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateTrack { get; set; }
        public int? EmployeeID { get; set; }
        public int? Shift01 { get; set; }
        public int? Shift02 { get; set; }
        public int? Shift03 { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? Discount { get; set; }
    }
}
