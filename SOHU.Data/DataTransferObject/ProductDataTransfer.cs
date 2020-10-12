using SOHU.Data.Helpers;
using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.DataTransferObject
{
    public class ProductDataTransfer : Product
    {
        public string Category { get; set; }
        public string URL
        {
            get
            {
                return AppGlobal.DomainWebsite + MetaTitle + "-" + ID + ".html";
            }
        }
        public string FullName { get; set; }
        public string PriceUnit { get; set; }
    }
}
