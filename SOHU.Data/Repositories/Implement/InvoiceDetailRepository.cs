﻿using SOHU.Data.DataTransferObject;
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
        public bool IsValidByManufacturingCode(string manufacturingCode)
        {
            return _context.Set<InvoiceDetail>().FirstOrDefault(item => item.ManufacturingCode.Equals(manufacturingCode)) == null ? true : false;
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
        public List<InvoiceDetailDataTransfer> GetProjectThiCongByInvoiceIDAndCategoryIDSUMToList(int invoiceID, int categoryID)
        {
            List<InvoiceDetailDataTransfer> list = new List<InvoiceDetailDataTransfer>();
            if ((invoiceID > 0) && (categoryID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@InvoiceID",invoiceID),
                new SqlParameter("@CategoryID",categoryID)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocProjectThiCongByInvoiceIDAndCategoryIDSUM", parameters);
                list = SQLHelper.ToList<InvoiceDetailDataTransfer>(dt);
            }
            return list;
        }
        public List<InvoiceDetailDataTransfer> GetProjectThiCongByInvoiceIDAndCategoryIDSUMProductToList(int invoiceID, int categoryID)
        {
            List<InvoiceDetailDataTransfer> list = new List<InvoiceDetailDataTransfer>();
            if ((invoiceID > 0) && (categoryID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@InvoiceID",invoiceID),
                new SqlParameter("@CategoryID",categoryID)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocProjectThiCongByInvoiceIDAndCategoryIDSUMProduct", parameters);
                list = SQLHelper.ToList<InvoiceDetailDataTransfer>(dt);
            }
            return list;
        }
        public List<InvoiceDetailDataTransfer> GetProjectThiCongByInvoiceIDAndCategoryIDSUMProductNotManufacturingCodeToList(int invoiceID, int categoryID)
        {
            List<InvoiceDetailDataTransfer> list = new List<InvoiceDetailDataTransfer>();
            if ((invoiceID > 0) && (categoryID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@InvoiceID",invoiceID),
                new SqlParameter("@CategoryID",categoryID)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocProjectThiCongByInvoiceIDAndCategoryIDSUMProductNotManufacturingCode", parameters);
                list = SQLHelper.ToList<InvoiceDetailDataTransfer>(dt);
            }
            return list;
        }
        public List<InvoiceDetailDataTransfer> GetProjectThiCongByInvoiceIDAndCategoryIDAndDateTrackToList(int invoiceID, int categoryID, DateTime dateTrack)
        {
            List<InvoiceDetailDataTransfer> list = new List<InvoiceDetailDataTransfer>();
            if ((invoiceID > 0) && (categoryID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@InvoiceID",invoiceID),
                new SqlParameter("@CategoryID",categoryID),
                new SqlParameter("@DateTrack",dateTrack),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocProjectThiCongByInvoiceIDAndCategoryIDAndDateTrack", parameters);
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
        public List<InvoiceDetailDataTransfer> GetInputByProductIDToList(int productID)
        {
            List<InvoiceDetailDataTransfer> list = new List<InvoiceDetailDataTransfer>();
            if (productID > 0)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@ProductID",productID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocInvoiceDetailInputSelectByProductID", parameters);
                list = SQLHelper.ToList<InvoiceDetailDataTransfer>(dt);
            }
            return list;
        }
        public List<InvoiceDetailDataTransfer> GetByProductIDAndCategoryIDToList(int productID, int categoryID)
        {
            List<InvoiceDetailDataTransfer> list = new List<InvoiceDetailDataTransfer>();
            if (productID > 0)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@ProductID",productID),
                new SqlParameter("@CategoryID",categoryID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocInvoiceDetailSelectByProductIDAndCategoryID", parameters);
                list = SQLHelper.ToList<InvoiceDetailDataTransfer>(dt);
            }
            return list;
        }
        public List<InvoiceDetailDataTransfer> GetOutputByProductIDToList(int productID)
        {
            List<InvoiceDetailDataTransfer> list = new List<InvoiceDetailDataTransfer>();
            if (productID > 0)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@ProductID",productID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocInvoiceDetailOutputSelectByProductID", parameters);
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
        public InvoiceDetail GetByInvoiceIDAndProductIDAndCategoryID(int invoiceID, int productID, int categoryID)
        {
            SqlParameter[] parameters =
                    {
                new SqlParameter("@InvoiceID",invoiceID),
                new SqlParameter("@ProductID",productID),
                new SqlParameter("@CategoryID",categoryID),
                };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocInvoiceDetailSelectByInvoiceIDAndProductIDAndCategoryID", parameters);
            return SQLHelper.ToList<InvoiceDetail>(dt).FirstOrDefault();
        }
        public string DeleteIDAndCategoryID(int ID, int categoryID)
        {
            SqlParameter[] parameters =
                    {
                new SqlParameter("@ID",ID),
                new SqlParameter("@CategoryID",categoryID),
                };
            return SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sprocInvoiceDetailDeleteIDAndCategoryID", parameters);
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
        public string UpdateItemsByProductIDAndInvoiceIDAndCategoryIDAndQuantityAndTotalAndManufacturingCode(int productID, int invoiceID, int categoryID, decimal quantity, decimal unitPrice, decimal total, string manufacturingCode)
        {
            SqlParameter[] parameters =
                    {
                new SqlParameter("@ProductID",productID),
                new SqlParameter("@InvoiceID",invoiceID),
                new SqlParameter("@CategoryID",categoryID),
                new SqlParameter("@Quantity",quantity),
                new SqlParameter("@UnitPrice",unitPrice),
                new SqlParameter("@Total",total),
                new SqlParameter("@ManufacturingCode",manufacturingCode),
                };
            return SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sprocInvoiceDetailUpdateItemsByProductIDAndInvoiceIDAndCategoryIDAndQuantityAndTotalAndManufacturingCode", parameters);
        }
        public string UpdateItemsByInvoiceIDAndEmployeeIDAndCategoryIDAndDateTrack(int invoiceID, int employeeID, int categoryID, DateTime dateTrack)
        {
            SqlParameter[] parameters =
                    {
                new SqlParameter("@InvoiceID",invoiceID),
                new SqlParameter("@EmployeeID",employeeID),
                new SqlParameter("@CategoryID",categoryID),
                new SqlParameter("@DateTrack",dateTrack),
                };
            return SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sprocInvoiceDetailUpdateItemsByInvoiceIDAndEmployeeIDAndCategoryIDAndDateTrack", parameters);
        }
        public string UpdateSingleItemByProductCodeAndManufacturingCode(string productCode, string manufacturingCode)
        {
            SqlParameter[] parameters =
                    {            
                new SqlParameter("@ProductCode",productCode),
                new SqlParameter("@ManufacturingCode",manufacturingCode),
                };
            return SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sprocInvoiceDetailUpdateSingleItemByProductCodeAndManufacturingCode", parameters);
        }
        public string InitializationUnitPrice()
        {
            return SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sprocInvoiceDetailInitializationUnitPrice");
        }
    }
}
