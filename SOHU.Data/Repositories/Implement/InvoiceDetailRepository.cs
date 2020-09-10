using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Repositories
{
    public class InvoiceDetailRepository : Repository<InvoiceDetail>, IInvoiceDetailRepository
    {
        private readonly SOHUContext _context;

        public InvoiceDetailRepository(SOHUContext context) : base(context)
        {
            _context = context;
        }


    }
}
