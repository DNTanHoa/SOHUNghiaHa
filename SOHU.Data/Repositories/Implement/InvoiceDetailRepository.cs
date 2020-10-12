using SOHU.Data.DataTransferObject;
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
    public class InvoiceDetailRepository : Repository<InvoiceDetail>, IInvoiceDetailRepository
    {
        private readonly SOHUContext _context;

        public InvoiceDetailRepository(SOHUContext context) : base(context)
        {
            _context = context;
        }
        public InvoiceDetail GetByInvoiceIDAndProductID(int invoiceID, int productID)
        {
            return _context.InvoiceDetail.FirstOrDefault(item => item.InvoiceID == invoiceID && item.ProductID == productID);
        }
        public List<InvoiceDetail> GetByInvoiceIDToList(int invoiceID)
        {
            return _context.InvoiceDetail.Where(item => item.InvoiceID == invoiceID).OrderBy(item => item.ID).ToList();
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
                    list[i].Product.ID = list[i].ProductID;
                    list[i].Product.TextName = list[i].ProductTitle;
                    list[i].Unit = new ModelTemplate();
                    list[i].Unit.ID = list[i].UnitID;
                    list[i].Unit.TextName = list[i].UnitName;
                }
            }

            return list;
        }
        public List<InvoiceDetailDataTransfer> GetProjectNhanSuByInvoiceIDAndParentIDToList(int invoiceID, int parentID)
        {
            List<InvoiceDetailDataTransfer> list = new List<InvoiceDetailDataTransfer>();
            if ((invoiceID > 0) && (parentID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@InvoiceID",invoiceID),
                new SqlParameter("@ParentID",parentID)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocProjectNhanSuByInvoiceIDAndParentID", parameters);
                list = SQLHelper.ToList<InvoiceDetailDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Employee = new ModelTemplate();
                    list[i].Employee.ID = list[i].EmployeeID;
                    list[i].Employee.TextName = list[i].FullName;
                }
            }
            return list;
        }
        public List<InvoiceDetailDataTransfer> GetProjectNhanSuByInvoiceIDAndCategoryIDToList(int invoiceID, int categoryID)
        {
            List<InvoiceDetailDataTransfer> list = new List<InvoiceDetailDataTransfer>();
            if ((invoiceID > 0) && (categoryID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@InvoiceID",invoiceID),
                new SqlParameter("@CategoryID",categoryID)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocProjectNhanSuByInvoiceIDAndCategoryID", parameters);
                list = SQLHelper.ToList<InvoiceDetailDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Employee = new ModelTemplate();
                    list[i].Employee.ID = list[i].EmployeeID;
                    list[i].Employee.TextName = list[i].FullName;
                }
            }
            return list;
        }
        public List<InvoiceDetailDataTransfer> GetProjectDuToanByInvoiceIDAndParentIDToList(int invoiceID, int parentID)
        {
            List<InvoiceDetailDataTransfer> list = new List<InvoiceDetailDataTransfer>();
            if ((invoiceID > 0) && (parentID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@InvoiceID",invoiceID),
                new SqlParameter("@ParentID",parentID)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocProjectDuToanByInvoiceIDAndParentID", parameters);
                list = SQLHelper.ToList<InvoiceDetailDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Product = new ModelTemplate();
                    list[i].Product.ID = list[i].ProductID;
                    list[i].Product.TextName = list[i].ProductTitle;
                    list[i].Unit = new ModelTemplate();
                    list[i].Unit.ID = list[i].UnitID;
                    list[i].Unit.TextName = list[i].UnitName;
                }
            }
            return list;
        }
        public List<InvoiceDetailDataTransfer> GetProjectDuToanByInvoiceIDAndCategoryIDToList(int invoiceID, int categoryID)
        {
            List<InvoiceDetailDataTransfer> list = new List<InvoiceDetailDataTransfer>();
            if ((invoiceID > 0) && (categoryID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@InvoiceID",invoiceID),
                new SqlParameter("@CategoryID",categoryID)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocProjectDuToanByInvoiceIDAndCategoryID", parameters);
                list = SQLHelper.ToList<InvoiceDetailDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Product = new ModelTemplate();
                    list[i].Product.ID = list[i].ProductID;
                    list[i].Product.TextName = list[i].ProductTitle;
                    list[i].Unit = new ModelTemplate();
                    list[i].Unit.ID = list[i].UnitID;
                    list[i].Unit.TextName = list[i].UnitName;
                }
            }
            return list;
        }
        public List<InvoiceDetailDataTransfer> GetProjectDuToanFullNameByInvoiceIDAndCategoryIDToList(int invoiceID, int categoryID)
        {
            List<InvoiceDetailDataTransfer> list = new List<InvoiceDetailDataTransfer>();
            if ((invoiceID > 0) && (categoryID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@InvoiceID",invoiceID),
                new SqlParameter("@CategoryID",categoryID)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocProjectDuToanFullNameByInvoiceIDAndCategoryID", parameters);
                list = SQLHelper.ToList<InvoiceDetailDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Parent = new ModelTemplate();
                    list[i].Parent.ParentID = list[i].ID;
                    list[i].Parent.TextName = list[i].ParentName;
                }
            }
            return list;
        }
        public List<InvoiceDetailDataTransfer> GetProjectThiCongByInvoiceIDAndCategoryIDToList(int invoiceID, int categoryID)
        {
            List<InvoiceDetailDataTransfer> list = new List<InvoiceDetailDataTransfer>();
            if ((invoiceID > 0) && (categoryID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@InvoiceID",invoiceID),
                new SqlParameter("@CategoryID",categoryID)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocProjectThiCongByInvoiceIDAndCategoryID", parameters);
                list = SQLHelper.ToList<InvoiceDetailDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Parent = new ModelTemplate();
                    list[i].Parent.ID = list[i].ID;
                    list[i].Parent.TextName = list[i].ParentName;
                    list[i].Employee = new ModelTemplate();
                    list[i].Employee.ID = list[i].EmployeeID;
                    list[i].Employee.TextName = list[i].FullName;
                    list[i].Product = new ModelTemplate();
                    list[i].Product.ID = list[i].ProductID;
                    list[i].Product.TextName = list[i].ProductTitle;
                    list[i].Unit = new ModelTemplate();
                    list[i].Unit.ID = list[i].UnitID;
                    list[i].Unit.TextName = list[i].UnitName;
                }
            }
            return list;
        }
        public List<InvoiceDetailDataTransfer> GetProjectChaoGiaByInvoiceIDAndCategoryIDToList(int invoiceID, int categoryID)
        {
            List<InvoiceDetailDataTransfer> list = new List<InvoiceDetailDataTransfer>();
            if ((invoiceID > 0) && (categoryID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@InvoiceID",invoiceID),
                new SqlParameter("@CategoryID",categoryID)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocProjectChaoGiaByInvoiceIDAndCategoryID", parameters);
                list = SQLHelper.ToList<InvoiceDetailDataTransfer>(dt);
            }
            return list;
        }
        public InvoiceDetail GetByCategoryIDAndManufacturingCode(int categoryID, string manufacturingCode)
        {
            SqlParameter[] parameters =
                    {
                new SqlParameter("@CategoryID",categoryID),
                new SqlParameter("@ManufacturingCode",manufacturingCode),
                };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocInvoiceDetailSelectByCategoryIDAndManufacturingCode", parameters);
            return SQLHelper.ToList<InvoiceDetail>(dt).FirstOrDefault();
        }
        public void DeleteByProductIsNull()
        {
            SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sprocInvoiceDetailDeleteByProductIsNull");
        }
        public string UpdateItemsByInvoiceIDAndEmployeeID(int invoiceID, int employeeID)
        {
            SqlParameter[] parameters =
                    {
                new SqlParameter("@InvoiceID",invoiceID),
                new SqlParameter("@EmployeeID",employeeID),
                };            
            return SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sprocInvoiceDetailUpdateItemsByInvoiceIDAndEmployeeID", parameters);
        }
    }
}
