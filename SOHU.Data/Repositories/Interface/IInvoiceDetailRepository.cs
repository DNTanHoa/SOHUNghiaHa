﻿using SOHU.Data.DataTransferObject;
using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Repositories
{
    public interface IInvoiceDetailRepository : IRepository<InvoiceDetail>
    {
        public List<InvoiceDetail> GetByInvoiceIDToList(int invoiceID);
        public List<InvoiceDetailDataTransfer> GetDataTransferByInvoiceIDToList(int invoiceID);
        public void DeleteByProductIsNull();
    }
}
