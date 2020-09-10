using System;
using System.Collections.Generic;

namespace SOHU.Data.Models
{
    public partial class MembershipPayment : BaseModel
    {
       
        public int? MembershipId { get; set; }
        public int? BankId { get; set; }
        public string AccountNumber { get; set; }
    }
}
