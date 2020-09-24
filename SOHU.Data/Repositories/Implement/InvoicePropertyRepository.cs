using SOHU.Data.Helpers;
using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SOHU.Data.Repositories
{
    public class InvoicePropertyRepository : Repository<InvoiceProperty>, IInvoicePropertyRepository
    {
        private readonly SOHUContext _context;

        public InvoicePropertyRepository(SOHUContext context) : base(context)
        {
            _context = context;
        }
        public List<InvoiceProperty> GetByInvoiceIDToList(int invoiceID)
        {
            return _context.InvoiceProperty.Where(item => item.InvoiceID == invoiceID).ToList();
        }
    }
}
