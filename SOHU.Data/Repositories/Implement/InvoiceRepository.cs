﻿using SOHU.Data.Helpers;
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
        public bool IsValidBySoHoaDon(string soHoaDon)
        {
            Invoice item = _context.Set<Invoice>().FirstOrDefault(item => item.SoHoaDon.Equals(soHoaDon));
            return item == null ? true : false;
        }
        public List<Invoice> GetByCategoryIDAndYearAndMonthToList(int categoryID, int year, int month)
        {
            return _context.Invoice.Where(item => item.CategoryID == categoryID && item.InvoiceCreated.Value.Year == year && item.InvoiceCreated.Value.Month == month).OrderByDescending(item => item.InvoiceCreated).ToList();
        }
        public List<Invoice> GetByCategoryIDAndDatePublishBeginAndDatePublishEndAndIsChaoGiaAndIsThiCongAndIsHoanThanhAndIsXuatHoaDonAndMembershipIDToList(int categoryID, DateTime datePublishBegin, DateTime datePublishEnd, bool isChaoGia, bool isThiCong, bool isHoanThanh, bool isXuatHoaDon, int membershipID)
        {
            DateTime datePublishBegin001 = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
            DateTime datePublishEnd001 = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
            List<Invoice> list = new List<Invoice>();
            if (membershipID > 0)
            {
                list = _context.Invoice.Where(item => item.CategoryID == categoryID && item.InvoiceCreated >= datePublishBegin001 && item.InvoiceCreated <= datePublishEnd001 && (item.IsChaoGia == isChaoGia || item.IsThiCong == isThiCong || item.IsHoanThanh == isHoanThanh || item.IsXuatHoaDon == isXuatHoaDon) && (item.SellID == membershipID)).OrderByDescending(item => item.InvoiceCreated).ToList();
            }
            else
            {
                list = GetByCategoryIDAndDatePublishBeginAndDatePublishEndAndIsChaoGiaAndIsThiCongAndIsHoanThanhAndIsXuatHoaDonToList(categoryID, datePublishBegin001, datePublishEnd001, isChaoGia, isThiCong, isHoanThanh, isXuatHoaDon);
            }
            return list;
        }
        public List<Invoice> GetByCategoryIDAndDatePublishBeginAndDatePublishEndAndIsChaoGiaAndIsThiCongAndIsHoanThanhAndIsXuatHoaDonToList(int categoryID, DateTime datePublishBegin, DateTime datePublishEnd, bool isChaoGia, bool isThiCong, bool isHoanThanh, bool isXuatHoaDon)
        {
            List<Invoice> list = new List<Invoice>();
            list = _context.Invoice.Where(item => item.CategoryID == categoryID && item.InvoiceCreated >= datePublishBegin && item.InvoiceCreated <= datePublishEnd && (item.IsChaoGia == isChaoGia || item.IsThiCong == isThiCong || item.IsHoanThanh == isHoanThanh || item.IsXuatHoaDon == isXuatHoaDon)).OrderByDescending(item => item.InvoiceCreated).ToList();
            return list;
        }

        public List<Invoice> GetByCategoryIDAndYearAndMonthAndSellIDToList(int categoryID, int year, int month, int sellID)
        {
            return _context.Invoice.Where(item => item.CategoryID == categoryID && item.InvoiceCreated.Value.Year == year && item.InvoiceCreated.Value.Month == month && (item.SellID.Value == sellID || item.BuyID.Value == sellID)).OrderByDescending(item => item.InvoiceCreated).ToList();
        }
        public List<Invoice> GetByCategoryIDAndSearchStringToList(int categoryID, string searchString)
        {
            return _context.Invoice.Where(item => item.CategoryID == categoryID && item.SoHoaDon.Contains(searchString)).OrderByDescending(item => item.InvoiceCreated).ToList();
        }
        public List<Invoice> GetByCategoryIDAndYearAndMonthAndSellIDAndSearchStringToList(int categoryID, int year, int month, int sellID, string searchString)
        {
            List<Invoice> list = new List<Invoice>();
            if (!string.IsNullOrEmpty(searchString))
            {
                list = GetByCategoryIDAndSearchStringToList(categoryID, searchString);
            }
            else
            {
                if (sellID > 0)
                {
                    list = GetByCategoryIDAndYearAndMonthAndSellIDToList(categoryID, year, month, sellID);
                }
                else
                {
                    list = GetByCategoryIDAndYearAndMonthToList(categoryID, year, month);
                }
            }
            return list;
        }
        public Invoice GetByCategoryIDAndYearAndMonthAndSellIDAndSearchStringToSUM(int categoryID, int year, int month, int sellID, string searchString)
        {
            Invoice invoice = new Invoice();
            List<Invoice> list = new List<Invoice>();
            if (!string.IsNullOrEmpty(searchString))
            {
                list = GetByCategoryIDAndSearchStringToList(categoryID, searchString);
            }
            else
            {
                if (sellID > 0)
                {
                    list = GetByCategoryIDAndYearAndMonthAndSellIDToList(categoryID, year, month, sellID);
                }
                else
                {
                    list = GetByCategoryIDAndYearAndMonthToList(categoryID, year, month);
                }
            }
            invoice.Total = 0;
            invoice.TotalPaid = 0;
            invoice.TotalDebt = 0;
            foreach (Invoice item in list)
            {
                if (item.Total != null)
                {
                    invoice.Total = invoice.Total + item.Total;
                }
                if (item.TotalPaid != null)
                {
                    invoice.TotalPaid = invoice.TotalPaid + item.TotalPaid;
                }
                if (item.TotalDebt != null)
                {
                    invoice.TotalDebt = invoice.TotalDebt + item.TotalDebt;
                }
            }
            return invoice;
        }
        public List<Invoice> GetByCategoryIDAndYearAndMonthAndActiveToList(int categoryID, int year, int month, bool active)
        {
            return _context.Invoice.Where(item => item.CategoryID == categoryID && item.InvoiceCreated.Value.Year == year && item.InvoiceCreated.Value.Month == month && item.Active == active).OrderByDescending(item => item.InvoiceCreated).ToList();
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
        public List<Invoice> GetProjectCongNoByCategoryIDToList(int categoryID)
        {
            List<Invoice> list = new List<Invoice>();
            if (categoryID > 0)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@CategoryID",categoryID),
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sprocProjectCongNoByCategoryID", parameters);
                list = SQLHelper.ToList<Invoice>(dt);
            }
            return list;
        }
    }
}
