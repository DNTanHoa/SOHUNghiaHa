﻿using SOHU.Data.DataTransferObject;
using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Repositories
{
    public interface IInvoiceDetailRepository : IRepository<InvoiceDetail>
    {
        public InvoiceDetail GetByCategoryIDAndManufacturingCode(int categoryID, string manufacturingCode);
        public InvoiceDetail GetByInvoiceIDAndProductID(int invoiceID, int productID);
        public List<InvoiceDetail> GetByInvoiceIDToList(int invoiceID);
        public List<InvoiceDetailDataTransfer> GetDataTransferByInvoiceIDToList(int invoiceID);
        public void DeleteByProductIsNull();
        public List<InvoiceDetailDataTransfer> GetProjectChaoGiaByInvoiceIDAndCategoryIDToList(int invoiceID, int categoryID);
        public List<InvoiceDetailDataTransfer> GetProjectNhanSuByInvoiceIDAndParentIDToList(int invoiceID, int parentID);
        public List<InvoiceDetailDataTransfer> GetProjectDuToanByInvoiceIDAndParentIDToList(int invoiceID, int parentID);
        public List<InvoiceDetailDataTransfer> GetProjectNhanSuByInvoiceIDAndCategoryIDToList(int invoiceID, int categoryID);
        public List<InvoiceDetailDataTransfer> GetProjectDuToanByInvoiceIDAndCategoryIDToList(int invoiceID, int categoryID);
        public List<InvoiceDetailDataTransfer> GetProjectThiCongByInvoiceIDAndCategoryIDToList(int invoiceID, int categoryID);
        public List<InvoiceDetailDataTransfer> GetProjectThiCongByInvoiceIDAndCategoryIDAndDateTrackToList(int invoiceID, int categoryID, DateTime dateTrack);
        public List<InvoiceDetailDataTransfer> GetProjectDuToanFullNameByInvoiceIDAndCategoryIDToList(int invoiceID, int categoryID);
        public string UpdateItemsByInvoiceIDAndEmployeeID(int invoiceID, int employeeID);
        public string UpdateItemsByInvoiceIDAndEmployeeIDAndCategoryIDAndDateTrack(int invoiceID, int employeeID, int categoryID, DateTime dateTrack);
        public bool IsValidByManufacturingCode(string manufacturingCode);
        public string InitializationUnitPrice();
        public List<InvoiceDetailDataTransfer> GetInputByProductIDToList(int productID);
        public List<InvoiceDetailDataTransfer> GetOutputByProductIDToList(int productID);
        public List<InvoiceDetailDataTransfer> GetByProductIDAndCategoryIDToList(int productID, int categoryID);
        public List<InvoiceDetailDataTransfer> GetProjectThiCongByInvoiceIDAndCategoryIDSUMToList(int invoiceID, int categoryID);
        public List<InvoiceDetailDataTransfer> GetProjectThiCongByInvoiceIDAndCategoryIDSUMProductToList(int invoiceID, int categoryID);
        public string UpdateItemsByProductIDAndInvoiceIDAndCategoryIDAndQuantityAndTotalAndManufacturingCode(int productID, int invoiceID, int categoryID, decimal quantity, decimal unitPrice, decimal total, string manufacturingCode);

    }
}
