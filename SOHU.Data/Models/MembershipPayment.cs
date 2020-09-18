using System;
using System.Collections.Generic;

namespace SOHU.Data.Models
{
    public partial class MembershipPayment : BaseModel
    {
       
        public int? MembershipID { get; set; }
        public int? BankID { get; set; }
        public string AccountNumber { get; set; }
    }
}
