using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Repositories
{
    public class InvoicePaymentRepository : Repository<InvoicePayment>, IInvoicePaymentRepository
    {
        private readonly SOHUContext _context;

        public InvoicePaymentRepository(SOHUContext context) : base(context)
        {
            _context = context;
        }
    }
}
