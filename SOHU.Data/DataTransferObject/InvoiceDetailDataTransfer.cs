using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.DataTransferObject
{
    public class InvoiceDetailDataTransfer : InvoiceDetail
    {
        public string ProductTitle { get; set; }
        public string UnitName { get; set; }
        public ModelTemplate Product { get; set; }
        public ModelTemplate Unit { get; set; }
    }
}
