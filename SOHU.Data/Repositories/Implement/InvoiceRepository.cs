using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Repositories
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        private readonly SOHUContext _context;

        public InvoiceRepository(SOHUContext context) : base(context)
        {
            _context = context;
        }


    }
}
