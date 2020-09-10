using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.DataTransferObject
{
    public class CartDetailDataTransfer : CartDetail
    {
        public string ProductTitle { get; set; }

        public string ProductImage { get; set; }

        public string ProductImageThumbnail { get; set; }

        public string ProductMetaTitle { get; set; }
    }
}
