using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Repositories
{
    public interface IInvoicePropertyRepository : IRepository<InvoiceProperty>
    {
        public List<InvoiceProperty> GetByInvoiceIDToList(int invoiceID);
    }
}
