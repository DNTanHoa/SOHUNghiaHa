using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<InvoicePayment> GetByInvoiceIDToList(int invoiceID)
        {
            return _context.InvoicePayment.Where(item => item.InvoiceID == invoiceID).OrderBy(item => item.PaymentCreated).ToList();
        }
    }
}
