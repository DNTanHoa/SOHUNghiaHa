using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Repositories
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        public bool IsValidBySoHoaDon(string soHoaDon);
        public bool IsValidByInvoiceCode(string invoiceCode);
        public List<Invoice> GetByCategoryIDAndYearAndMonthToList(int categoryID, int year, int month);
        public List<Invoice> GetByCategoryIDAndYearAndMonthAndActiveToList(int categoryID, int year, int month, bool active);
        public List<Invoice> GetInvoiceInputByProductIDToList(int productID);
        public List<Invoice> GetInvoiceOutputByProductIDToList(int productID);
        public void InitializationByID(int ID);
        public void InitializationByIDAndParentID(int ID, int parentID);
        public void InitializationByIDAndCategoryID(int ID, int categoryID);
        public List<Invoice> GetByCategoryIDAndYearAndMonthAndSellIDAndSearchStringToList(int categoryID, int year, int month, int sellID, string searchString);
        public Invoice GetByCategoryIDAndYearAndMonthAndSellIDAndSearchStringToSUM(int categoryID, int year, int month, int sellID, string searchString);
        public List<Invoice> GetByCategoryIDAndDatePublishBeginAndDatePublishEndAndIsChaoGiaAndIsThiCongAndIsHoanThanhAndIsXuatHoaDonAndMembershipIDToList(int categoryID, DateTime datePublishBegin, DateTime datePublishEnd, bool isChaoGia, bool isThiCong, bool isHoanThanh, bool isXuatHoaDon, int membershipID);

    }
}
