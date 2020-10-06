using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Euronailsupply.Controllers;
using Euronailsupply.Models;
using SOHU.Data.DataTransferObject;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace Euronailsupply.Controllers
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
            model.ProductID = model.Product.ID;
            model.UnitID = model.Unit.ID;
            if (model.UnitID == null)
            {
                model.UnitID = AppGlobal.UnitID;
            }
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
        public IActionResult CreateByInvoiceIDAndProductMetaTitle(int invoiceID, string productMetaTitle)
        {
            string note = AppGlobal.InitString;
            if (!string.IsNullOrEmpty(productMetaTitle))
            {
                Product product = _productRepository.GetByMetaTitle(productMetaTitle);
                if (product != null)
                {
                    int result = 0;
                    InvoiceDetail model = _invoiceDetailRepository.GetByInvoiceIDAndProductID(invoiceID, product.ID);
                    if (model == null)
                    {
                        model = new InvoiceDetail();
                        model.InvoiceID = invoiceID;
                        model.ProductID = product.ID;
                        model.UnitPrice = product.Price;
                        model.ManufacturingCode = product.MetaTitle;
                        model.Quantity = 1;
                        model.UnitID = AppGlobal.UnitID;
                        model.Total = model.UnitPrice * model.Quantity;
                        model.Initialization(InitType.Insert, RequestUserID);

                        if ((model.ProductID > 0) && (model.InvoiceID > 0))
                        {
                            result = _invoiceDetailRepository.Create(model);
                        }
                    }
                    else
                    {
                        model.UnitPrice = product.Price;
                        model.Quantity = model.Quantity + 1;
                        model.Total = model.UnitPrice * model.Quantity;
                        model.Initialization(InitType.Update, RequestUserID);
                        result = _invoiceDetailRepository.Update(model.ID, model);
                    }
                    if (result > 0)
                    {
                        note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
                        _invoiceRepository.InitializationByID(model.InvoiceID.Value);
                        _productRepository.InitializationByID(model.ProductID.Value);
                    }
                    else
                    {
                        note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
                    }
                }
            }
            return Json(_invoiceRepository.GetByID(invoiceID));
        }
        public IActionResult Create(InvoiceDetailDataTransfer model, int invoiceID)
        {
            model.InvoiceID = invoiceID;
            InitializationDataTransfer(model);
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if ((model.ProductID > 0) && (model.InvoiceID > 0))
            {
                result = _invoiceDetailRepository.Create(model);
            }

            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
                _invoiceRepository.InitializationByID(model.InvoiceID.Value);
                _productRepository.InitializationByID(model.ProductID.Value);
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
            int result = _invoiceDetailRepository.Update(model.ID, model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
                _invoiceRepository.InitializationByID(model.InvoiceID.Value);
                _productRepository.InitializationByID(model.ProductID.Value);
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
                invoiceID = invoiceDetail.InvoiceID.Value;
                productId = invoiceDetail.ProductID.Value;
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
