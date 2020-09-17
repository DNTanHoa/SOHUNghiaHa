﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SOHU.Data.Models
{
    public partial class Invoice : BaseModel
    {
      
        public int? CategoryId { get; set; }
        public string ManageCode { get; set; }
        public string InvoiceCode { get; set; }
        public string MakeCode { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? InvoiceCreated { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateBegin { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateEnd { get; set; }
        public int? MakeSideId { get; set; }
        public int? BuyId { get; set; }
        public string BuyName { get; set; }
        public string BuyAddress { get; set; }
        public string BuyEmail { get; set; }
        public string BuyPhone { get; set; }
        public int? SellId { get; set; }
        public string SellName { get; set; }
        public string SellAddress { get; set; }
        public string SellEmail { get; set; }
        public string SellPhone { get; set; }
        public string HopDong { get; set; }
        public string ChaoGia { get; set; }
        public string NghiemThu { get; set; }
        public string ThanhLy { get; set; }
        public int? StatusId { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? TotalNoTax { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? Tax { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? TotalTax { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? TotalShipCost { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? TotalDiscount { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? Total { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? TotalPaid { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? TotalDebt { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? Gbpexchange { get; set; }
    }
}
