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
            return _context.Invoice.Where(item => item.CategoryID == categoryID && item.InvoiceCreated.Value.Year == year && item.InvoiceCreated.Value.Month == month).OrderByDescending(item => item.InvoiceCreated).ToList();
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
        public void InitializationByIDAndParentID(int ID, int parentID)
        {
            if ((ID > 0) && (parentID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@ID",ID),
                new SqlParameter("@ParentID",parentID)
                    };
                SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sprocInvoiceInitializationByIDAndParentID", parameters);
            }
        }
        public void InitializationByIDAndCategoryID(int ID, int categoryID)
        {
            if ((ID > 0) && (categoryID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@ID",ID),
                new SqlParameter("@CategoryID",categoryID)
                    };
                SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sprocInvoiceInitializationByIDAndCategoryID", parameters);
            }
        }
        public List<Invoice> GetInvoiceInputByProductIDToList(int productID)
        {
            List<Invoice> list = new List<Invoice>();
            if (productID > 0)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@ProductID",productID),
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocInvoiceInputSelectByProductID", parameters);
                list = SQLHelper.ToList<Invoice>(dt);
            }
            return list;
        }
        public List<Invoice> GetInvoiceOutputByProductIDToList(int productID)
        {
            List<Invoice> list = new List<Invoice>();
            if (productID > 0)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@ProductID",productID),
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocInvoiceOutputSelectByProductID", parameters);
                list = SQLHelper.ToList<Invoice>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].ID = int.Parse(dt.Rows[i]["ID"].ToString());
                }
            }
            return list;
        }
    }
}
