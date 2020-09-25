using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NghiaHa.API.RequestModel
{
    public class MembershipLoginRequestModel
    {
        public string Account { get; set; }

        public string Password { get; set; }
    }
}
