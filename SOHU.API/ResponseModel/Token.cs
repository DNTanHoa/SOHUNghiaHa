using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOHU.API.ResponseModel
{
    public class Token
    {
        public DateTime CurrentDatetime { get; set; }
        public string Key { get; set; }
        public int ExpireMinute { get; set; }
    }
}
