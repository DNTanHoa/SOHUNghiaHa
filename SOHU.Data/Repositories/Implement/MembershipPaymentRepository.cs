using SOHU.Data.Helpers;
using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOHU.Data.Repositories
{
    public class MembershipPaymentRepository : Repository<MembershipPayment>, IMembershipPaymentRepository
    {
        private readonly SOHUContext _context;

        public MembershipPaymentRepository(SOHUContext context) : base(context)
        {
            _context = context;
        }

        
    }
}
