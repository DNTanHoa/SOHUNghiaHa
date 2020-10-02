using Microsoft.CodeAnalysis.CSharp.Syntax;
using SOHU.Data.Extensions;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SOHU.Data.Repositories
{
    public class InvoiceForAPIRepository : Repository<Invoice>, IInvoiceRepository
    {
        private readonly SOHUContext _context;

        public InvoiceForAPIRepository(SOHUContext context) : base(context)
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
            List<Invoice> ListInvoice = _context.Invoice.Where(item => item.CategoryID == categoryID
                                                                   && item.InvoiceCreated.Value.Year == year
                                                                   && item.InvoiceCreated.Value.Month == month)
                                                        .OrderByDescending(item => item.InvoiceCreated)
                                                        .Do(item => item.HopDong = null)
                                                        .ToList();

            return ListInvoice;
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
            List<Invoice> ListInvoice = new List<Invoice>();
            if (productID > 0)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@ProductID",productID),
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocInvoiceInputSelectByProductID", parameters);
                ListInvoice = SQLHelper.ToList<Invoice>(dt);
            }

            ListInvoice.ForEach(item => item.HopDong = null);

            return ListInvoice;
        }
        public List<Invoice> GetInvoiceOutputByProductIDToList(int productID)
        {
            List<Invoice> ListInvoice = new List<Invoice>();
            if (productID > 0)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@ProductID",productID),
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocInvoiceOutputSelectByProductID", parameters);
                ListInvoice = SQLHelper.ToList<Invoice>(dt);
                for (int i = 0; i < ListInvoice.Count; i++)
                {
                    ListInvoice[i].ID = int.Parse(dt.Rows[i]["ID"].ToString());
                }
            }

            ListInvoice.ForEach(item => item.HopDong = null);

            return ListInvoice;
        }
    }
}
