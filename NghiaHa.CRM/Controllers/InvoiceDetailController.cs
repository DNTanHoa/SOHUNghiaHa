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
            model.ProductID = model.Product.ID;
            model.UnitID = model.Unit.ID;
            model.Total = model.UnitPrice * model.Quantity;
            model.Total01 = model.UnitPrice * model.Quantity01;
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
                    InvoiceDetail model = new InvoiceDetail();
                    model.DateTrack = DateTime.Now;
                    model.CategoryID = AppGlobal.ThiCongID;
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
        public IActionResult CreateByInvoiceIDAndManufacturingCode(int invoiceID, string manufacturingCode)
        {
            string note = AppGlobal.InitString;
            if (!string.IsNullOrEmpty(manufacturingCode))
            {
                InvoiceDetail invoiceDetail = _invoiceDetailRepository.GetByCategoryIDAndManufacturingCode(AppGlobal.InvoiceInputID, manufacturingCode);
                if (invoiceDetail != null)
                {
                    int result = 0;
                    InvoiceDetail model = new InvoiceDetail();
                    model.DateTrack = DateTime.Now;
                    model.CategoryID = AppGlobal.ThiCongID;
                    model.InvoiceID = invoiceID;
                    model.ProductID = invoiceDetail.ProductID;
                    model.UnitPrice = _productRepository.GetByID(invoiceDetail.ProductID.Value).Price;
                    model.ProductCode = invoiceDetail.ProductCode;
                    model.ManufacturingCode = manufacturingCode;
                    model.Quantity = 1;
                    model.UnitID = AppGlobal.UnitID;
                    model.Total = model.UnitPrice * model.Quantity;
                    model.Initialization(InitType.Insert, RequestUserID);
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
                }
            }
            return Json(_invoiceRepository.GetByID(invoiceID));
        }
        public IActionResult CreateByInvoiceIDAndManufacturingCodeAndQuantityAndEmployeeID(int invoiceID, string manufacturingCode, int quantity, int employeeID)
        {
            string note = AppGlobal.InitString;
            if (!string.IsNullOrEmpty(manufacturingCode))
            {
                InvoiceDetail invoiceDetail = _invoiceDetailRepository.GetByCategoryIDAndManufacturingCode(AppGlobal.InvoiceInputID, manufacturingCode);
                if (invoiceDetail != null)
                {
                    int result = 0;
                    InvoiceDetail model = new InvoiceDetail();
                    model.DateTrack = DateTime.Now;
                    model.CategoryID = AppGlobal.ThiCongID;
                    model.InvoiceID = invoiceID;
                    model.ProductID = invoiceDetail.ProductID;
                    model.UnitPrice = _productRepository.GetByID(invoiceDetail.ProductID.Value).Price;
                    model.ProductCode = invoiceDetail.ProductCode;
                    model.ManufacturingCode = manufacturingCode;
                    model.Quantity = quantity;
                    model.UnitID = AppGlobal.UnitID;
                    model.Total = model.UnitPrice * model.Quantity;
                    model.EmployeeID = employeeID;
                    model.Initialization(InitType.Insert, RequestUserID);
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
                }
            }
            return Json(_invoiceRepository.GetByID(invoiceID));
        }
        public IActionResult CreateByInvoiceIDAndProductCodeAndManufacturingCode(int invoiceID, string productCode, string manufacturingCode)
        {
            string note = AppGlobal.InitString;
            if (!string.IsNullOrEmpty(productCode))
            {
                Product product = _productRepository.GetByMetaTitle(productCode);
                if (product != null)
                {
                    int result = 0;
                    InvoiceDetail model = new InvoiceDetail();
                    model.DateTrack = DateTime.Now;
                    model.CategoryID = AppGlobal.ThiCongID;
                    model.InvoiceID = invoiceID;
                    model.ProductID = product.ID;
                    model.UnitPrice = product.Price;
                    model.ProductCode = productCode;
                    model.ManufacturingCode = manufacturingCode;
                    model.Quantity = 1;
                    model.UnitID = AppGlobal.UnitID;
                    model.Total = model.UnitPrice * model.Quantity;
                    model.Initialization(InitType.Insert, RequestUserID);
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
                }
            }
            return Json(_invoiceRepository.GetByID(invoiceID));
        }
        public IActionResult CreateByInvoiceIDAndProductCodeAndManufacturingCodeAndQuantity(int invoiceID, string productCode, string manufacturingCode, int quantity)
        {
            string note = AppGlobal.InitString;
            if (!string.IsNullOrEmpty(productCode))
            {
                Product product = _productRepository.GetByMetaTitle(productCode);
                if (product != null)
                {
                    int result = 0;
                    InvoiceDetail model = new InvoiceDetail();
                    model.DateTrack = DateTime.Now;
                    model.CategoryID = AppGlobal.ThiCongID;
                    model.InvoiceID = invoiceID;
                    model.ProductID = product.ID;
                    model.UnitPrice = product.Price;
                    model.ProductCode = productCode;
                    model.ManufacturingCode = manufacturingCode;
                    model.Quantity = quantity;
                    model.UnitID = AppGlobal.UnitID;
                    model.Total = model.UnitPrice * model.Quantity;
                    model.Initialization(InitType.Insert, RequestUserID);
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
                }
            }
            return Json(_invoiceRepository.GetByID(invoiceID));
        }

        public IActionResult UpdateItemsByInvoiceIDAndEmployeeID(int invoiceID, int employeeID)
        {
            string note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            _invoiceDetailRepository.UpdateItemsByInvoiceIDAndEmployeeID(invoiceID, employeeID);
            return Json(note);
        }
        public IActionResult Create(InvoiceDetailDataTransfer model, int invoiceID)
        {
            model.InvoiceID = invoiceID;
            InitializationDataTransfer(model);
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;

            if ((model.ProductID > 0) && (model.UnitID > 0))
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
