using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using NghiaHa.CRM.Controllers;
using NghiaHa.CRM.Web.Models;
using SOHU.Data.DataTransferObject;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace NghiaHa.CRM.Web.Controllers
{
    public class InvoiceDetailController : BaseController
    {
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;
        private readonly IInvoiceRepository _invoiceRepository;

        private readonly IProductRepository _productRepository;
        public InvoiceDetailController(IInvoiceRepository invoiceRepository, IInvoiceDetailRepository invoiceDetailRepository, IProductRepository productRepository)
        {
            _invoiceDetailRepository = invoiceDetailRepository;
            _invoiceRepository = invoiceRepository;
            _productRepository = productRepository;
        }
        private void InitializationDataTransfer(InvoiceDetailDataTransfer model)
        {
            model.ProductId = model.Product.Id;
            model.UnitId = model.Unit.Id;
            model.Total = model.UnitPrice * model.Quantity;
        }
        public IActionResult GetDataTransferByInvoiceIDToList([DataSourceRequest] DataSourceRequest request, int invoiceID)
        {
            return Json(_invoiceDetailRepository.GetDataTransferByInvoiceIDToList(invoiceID).ToDataSourceResult(request));
        }
        public IActionResult GetByInvoiceIDToList([DataSourceRequest] DataSourceRequest request, int invoiceID)
        {
            return Json(_invoiceDetailRepository.GetByInvoiceIDToList(invoiceID).ToDataSourceResult(request));
        }
        public IActionResult Create(InvoiceDetailDataTransfer model, int invoiceID)
        {
            model.InvoiceId = invoiceID;
            InitializationDataTransfer(model);
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;

            if ((model.ProductId > 0) && (model.UnitId > 0))
            {
                result = _invoiceDetailRepository.Create(model);
            }

            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
                _invoiceRepository.InitializationByID(model.InvoiceId.Value);
                _productRepository.InitializationByID(model.ProductId.Value);
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult Update(InvoiceDetailDataTransfer model)
        {
            InitializationDataTransfer(model);
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _invoiceDetailRepository.Update(model.Id, model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
                _invoiceRepository.InitializationByID(model.InvoiceId.Value);
                _productRepository.InitializationByID(model.ProductId.Value);
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.EditFail;
            }

            return Json(note);
        }
        public IActionResult Delete(int ID)
        {
            InvoiceDetail invoiceDetail = _invoiceDetailRepository.GetByID(ID);
            int? invoiceID = 0;
            int? productId = 0;
            if (invoiceDetail != null)
            {
                invoiceID = invoiceDetail.InvoiceId.Value;
                productId = invoiceDetail.ProductId.Value;
            }
            string note = AppGlobal.InitString;
            int result = _invoiceDetailRepository.Delete(ID);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.DeleteSuccess;
                _invoiceRepository.InitializationByID(invoiceID.Value);
                _productRepository.InitializationByID(productId.Value);
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.DeleteFail;
            }
            return Json(note);
        }
    }
}
