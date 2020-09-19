using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.DataTransferObject
{
    public class InvoiceDetailDataTransfer : InvoiceDetail
    {
        public bool IsChaoGia { get; set; }
        public bool IsThiCong { get; set; }
        public string ProductTitle { get; set; }
        public string UnitName { get; set; }
        public string FullName { get; set; }
        public string ParentName { get; set; }
        public ModelTemplate Employee { get; set; }
        public ModelTemplate Product { get; set; }
        public ModelTemplate Parent { get; set; }
        public ModelTemplate Unit { get; set; }
    }
}
