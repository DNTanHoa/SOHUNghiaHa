using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Models
{
    public class Role : BaseModel
    {
        public long MenuID { get; set; }

        public long MembershipID { get; set; }

        public bool IsAllow { get; set; }
    }
}
