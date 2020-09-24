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
        public List<Invoice> GetInvoiceInputByProductIDToList(int productID);
        public List<Invoice> GetInvoiceOutputByProductIDToList(int productID);
        public void InitializationByID(int ID);
        public void InitializationByIDAndParentID(int ID, int parentID);
        public void InitializationByIDAndCategoryID(int ID, int categoryID);
    }
}
