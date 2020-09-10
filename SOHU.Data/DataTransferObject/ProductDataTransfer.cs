using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.DataTransferObject
{
    public class ProductDataTransfer : Product
    {
        public string Category { get; set; }

        public string PriceUnit { get; set; }
    }
}
