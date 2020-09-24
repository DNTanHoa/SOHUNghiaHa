using SOHU.Data.Helpers;
using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.DataTransferObject
{
    public class ConfigDataTransfer : Config
    {
        public string ParentName { get; set; }

        public ModelTemplate Parent { get; set; }
    }
}
