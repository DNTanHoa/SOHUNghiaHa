using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Repositories
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        public bool IsValidByInvoiceCode(string invoiceCode);
        public List<Invoice> GetByCategoryIDAndYearAndMonthToList(int categoryID, int year, int month);
        public void InitializationByID(int ID);
    }
}
