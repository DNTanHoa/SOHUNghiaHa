using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Mobile.Services
{
    public class TokenResult
    {
        public string Token { get; set; }

        public DateTime TokenExpireDate { get; set; }

        public object LoginUser { get; set; }
    }
}
