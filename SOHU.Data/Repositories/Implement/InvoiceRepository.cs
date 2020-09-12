using SOHU.Data.Helpers;
using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
        public bool IsValidByInvoiceCode(string invoiceCode)
        {
            Invoice item = _context.Set<Invoice>().FirstOrDefault(item => item.InvoiceCode.Equals(invoiceCode));
            return item == null ? true : false;
        }
        public List<Invoice> GetByCategoryIDAndYearAndMonthToList(int categoryID, int year, int month)
        {
            return _context.Invoice.Where(item => item.CategoryId == categoryID && item.InvoiceCreated.Value.Year == year && item.InvoiceCreated.Value.Month == month).OrderByDescending(item => item.InvoiceCreated).ToList();
        }
        public void InitializationByID(int ID)
        {
            if (ID > 0)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@ID",ID),
            };
                SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sprocInvoiceInitializationByID", parameters);
            }
        }
    }
}
