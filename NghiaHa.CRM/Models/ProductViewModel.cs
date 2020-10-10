using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NghiaHa.CRM.Web.Models
{
    public class ProductViewModel : Product
    {
        public bool IsBarcodePrint { get; set; }
    }
}
