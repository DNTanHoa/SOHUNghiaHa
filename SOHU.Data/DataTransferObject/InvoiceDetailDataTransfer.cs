using SOHU.Data.Helpers;
using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.DataTransferObject
{
    public class InvoiceDetailDataTransfer : InvoiceDetail
    {
        public DateTime? InvoiceCreated { get; set; }
        public int? ProductCategoryID { get; set; }
        public string InvoiceCode { get; set; }
        public string InvoiceName { get; set; }
        public bool IsChaoGia { get; set; }
        public bool IsThiCong { get; set; }
        public string ProductTitle { get; set; }
        public string MetaTitle { get; set; }
        public string UnitName { get; set; }
        public string FullName { get; set; }
        public string ParentName { get; set; }
        public string Image { get; set; }
        public string ImageURLFull
        {
            get
            {
                return AppGlobal.Domain + "images/Product/" + Image;
            }
        }
        public string ContentMain { get; set; }
        public decimal? PriceInput { get; set; }
        public decimal? TotalInput { get; set; }
        public ModelTemplate Employee { get; set; }
        public ModelTemplate Product { get; set; }
        public ModelTemplate Parent { get; set; }
        public ModelTemplate Unit { get; set; }
    }
}
