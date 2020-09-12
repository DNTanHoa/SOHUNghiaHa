using SOHU.Data.DataTransferObject;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public List<InvoiceDetailDataTransfer> GetDataTransferByInvoiceIDToList(int invoiceID)
        {
            List<InvoiceDetailDataTransfer> list = new List<InvoiceDetailDataTransfer>();
            if (invoiceID > 0)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@InvoiceID",invoiceID),
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocInvoiceDetailSelectByInvoiceID", parameters);
                list = SQLHelper.ToList<InvoiceDetailDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Product = new ModelTemplate();
                    list[i].Product.Id = list[i].ProductId;
                    list[i].Product.TextName = list[i].ProductTitle;
                    list[i].Unit = new ModelTemplate();
                    list[i].Unit.Id = list[i].UnitId;
                    list[i].Unit.TextName = list[i].UnitName;
                }
            }

            return list;
        }
        public void DeleteByProductIsNull()
        {
            SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sprocInvoiceDetailDeleteByProductIsNull");
        }
    }
}
