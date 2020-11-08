﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using NghiaHa.CRM.Controllers;
using NghiaHa.CRM.Web.Models;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace NghiaHa.CRM.Web.Controllers
{
    public class InvoiceController : BaseController
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMembershipRepository _membershipRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository, IMembershipRepository membershipRepository)
        {
            _invoiceRepository = invoiceRepository;
            _membershipRepository = membershipRepository;
        }
        private void Initialization(Invoice model)
        {
            if (!string.IsNullOrEmpty(model.InvoiceCode))
            {
                model.InvoiceCode = model.InvoiceCode.Trim();
            }
            if (!string.IsNullOrEmpty(model.SoHoaDon))
            {
                model.SoHoaDon = model.SoHoaDon.Trim();
            }
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int ID, int CategoryID)
        {
            var model = _invoiceRepository.GetByID(ID);
            return View(model);
        }
        public IActionResult DetailFiles(int ID)
        {
            InvoiceProperty model = new InvoiceProperty();
            if (ID > 0)
            {
                Invoice invoice = _invoiceRepository.GetByID(ID);
                if (invoice != null)
                {
                    model.Title = invoice.SoHoaDon;
                    model.InvoiceID = ID;
                }
            }
            return View(model);
        }
        public IActionResult InvoiceInput()
        {
            BaseViewModel viewModel = new BaseViewModel();
            viewModel.YearFinance = DateTime.Now.Year;
            viewModel.MonthFinance = DateTime.Now.Month;
            return View(viewModel);
        }
        public IActionResult InvoiceOutput()
        {
            BaseViewModel viewModel = new BaseViewModel();
            viewModel.YearFinance = DateTime.Now.Year;
            viewModel.MonthFinance = DateTime.Now.Month;
            return View(viewModel);
        }
        public IActionResult InvoiceInputDetail(int ID)
        {
            Invoice model = new Invoice();
            model.InvoiceCreated = DateTime.Now;
            model.Tax = AppGlobal.Tax;
            model.TotalNoTax = 0;
            model.TotalTax = 0;
            model.Total = 0;
            model.TotalPaid = 0;
            model.TotalDebt = 0;
            if (ID > 0)
            {
                model = _invoiceRepository.GetByID(ID);
            }
            model.CategoryID = AppGlobal.InvoiceInputID;
            return View(model);
        }
        public IActionResult InvoiceOutputDetail(int ID)
        {
            Invoice model = new Invoice();
            model.InvoiceCreated = DateTime.Now;
            model.Tax = AppGlobal.Tax;
            model.TotalNoTax = 0;
            model.TotalTax = 0;
            model.Total = 0;
            model.TotalPaid = 0;
            model.TotalDebt = 0;
            if (ID > 0)
            {
                model = _invoiceRepository.GetByID(ID);
            }
            model.CategoryID = AppGlobal.InvoiceOutputID;
            return View(model);
        }
        public IActionResult InvoiceInputDetailBarcode(int ID)
        {
            Invoice model = new Invoice();
            if (ID > 0)
            {
                model = _invoiceRepository.GetByID(ID);
            }
            model.ManageCode = "";
            model.MakeCode = "";
            model.TotalDiscount = 1;
            return View(model);
        }
        public IActionResult InvoiceInputDetailWindow(int ID)
        {
            Invoice model = new Invoice();
            model.InvoiceCreated = DateTime.Now;
            model.Tax = AppGlobal.Tax;
            model.TotalNoTax = 0;
            model.TotalTax = 0;
            model.Total = 0;
            model.TotalPaid = 0;
            model.TotalDebt = 0;
            if (ID > 0)
            {
                model = _invoiceRepository.GetByID(ID);
            }
            model.CategoryID = AppGlobal.InvoiceInputID;
            return View(model);
        }
        public IActionResult Delete(int ID)
        {
            string note = AppGlobal.InitString;
            int result = _invoiceRepository.Delete(ID);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.DeleteSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.DeleteFail;
            }
            return Json(note);
        }
        public IActionResult GetByID(int ID)
        {
            return Json(_invoiceRepository.GetByID(ID));
        }
        public IActionResult GetAllToList([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_invoiceRepository.GetAllToList().ToDataSourceResult(request));
        }
        
        public IActionResult GetByDuAnAndYearAndMonthToList([DataSourceRequest] DataSourceRequest request, int year, int month)
        {
            return Json(_invoiceRepository.GetByCategoryIDAndYearAndMonthToList(AppGlobal.DuAnID, year, month).ToDataSourceResult(request));
        }
        public IActionResult GetByInvoiceInputAndYearAndMonthToList([DataSourceRequest] DataSourceRequest request, int year, int month)
        {
            return Json(_invoiceRepository.GetByCategoryIDAndYearAndMonthToList(AppGlobal.InvoiceInputID, year, month).ToDataSourceResult(request));
        }
        public IActionResult GetByInvoiceInputAndYearAndMonthAndSellIDAndSearchStringToList([DataSourceRequest] DataSourceRequest request, int year, int month, int sellID, string searchString)
        {
            return Json(_invoiceRepository.GetByCategoryIDAndYearAndMonthAndSellIDAndSearchStringToList(AppGlobal.InvoiceInputID, year, month, sellID, searchString).ToDataSourceResult(request));
        }
        public IActionResult GetByInvoiceOutputAndYearAndMonthAndSellIDAndSearchStringToList([DataSourceRequest] DataSourceRequest request, int year, int month, int sellID, string searchString)
        {
            return Json(_invoiceRepository.GetByCategoryIDAndYearAndMonthAndSellIDAndSearchStringToList(AppGlobal.InvoiceOutputID, year, month, sellID, searchString).ToDataSourceResult(request));
        }
        public IActionResult GetByInvoiceInputAndYearAndMonthAndSellIDAndSearchStringToSUM(int year, int month, int sellID, string searchString)
        {
            return Json(_invoiceRepository.GetByCategoryIDAndYearAndMonthAndSellIDAndSearchStringToSUM(AppGlobal.InvoiceInputID, year, month, sellID, searchString));
        }
        public IActionResult GetByInvoiceOutputAndYearAndMonthAndSellIDAndSearchStringToSUM(int year, int month, int sellID, string searchString)
        {
            return Json(_invoiceRepository.GetByCategoryIDAndYearAndMonthAndSellIDAndSearchStringToSUM(AppGlobal.InvoiceOutputID, year, month, sellID, searchString));
        }
        public IActionResult GetInvoiceInputByProductIDToList([DataSourceRequest] DataSourceRequest request, int productID)
        {
            return Json(_invoiceRepository.GetInvoiceInputByProductIDToList(productID).ToDataSourceResult(request));
        }
        public IActionResult GetInvoiceOutputByProductIDToList([DataSourceRequest] DataSourceRequest request, int productID)
        {
            return Json(_invoiceRepository.GetInvoiceOutputByProductIDToList(productID).ToDataSourceResult(request));
        }
        [AcceptVerbs("Post")]
        public IActionResult SaveInvoiceInput(Invoice model)
        {
            model.SellName = _membershipRepository.GetByID(model.SellID.Value).FullName;
            model.BuyID = AppGlobal.NghiaHaID;
            if (model.ID > 0)
            {
                Initialization(model);
                model.Initialization(InitType.Update, RequestUserID);
                _invoiceRepository.Update(model.ID, model);
            }
            else
            {
                Initialization(model);
                model.Initialization(InitType.Insert, RequestUserID);
                if (_invoiceRepository.IsValidBySoHoaDon(model.SoHoaDon) == true)
                {
                    _invoiceRepository.Create(model);
                }
            }
            return RedirectToAction("InvoiceInputDetail", new { ID = model.ID });
        }
        [AcceptVerbs("Post")]
        public IActionResult SaveInvoiceOutput(Invoice model)
        {
            model.BuyName = _membershipRepository.GetByID(model.BuyID.Value).FullName;
            model.SellID = AppGlobal.NghiaHaID;
            if (model.ID > 0)
            {
                Initialization(model);
                model.Initialization(InitType.Update, RequestUserID);
                _invoiceRepository.Update(model.ID, model);
            }
            else
            {
                Initialization(model);
                model.Initialization(InitType.Insert, RequestUserID);
                if (_invoiceRepository.IsValidBySoHoaDon(model.SoHoaDon) == true)
                {
                    _invoiceRepository.Create(model);
                }
            }
            return RedirectToAction("InvoiceOutputDetail", new { ID = model.ID });
        }
        [AcceptVerbs("Post")]
        public IActionResult SaveInvoiceInputWindow(Invoice model)
        {
            model.SellName = _membershipRepository.GetByID(model.SellID.Value).FullName;
            if (model.ID > 0)
            {
                Initialization(model);
                model.Initialization(InitType.Update, RequestUserID);
                _invoiceRepository.Update(model.ID, model);
            }
            else
            {
                Initialization(model);
                model.Initialization(InitType.Insert, RequestUserID);
                if (_invoiceRepository.IsValidByInvoiceCode(model.InvoiceCode) == true)
                {
                    _invoiceRepository.Create(model);
                }
            }
            return RedirectToAction("InvoiceInputWindow", new { ID = model.ID });
        }
        public IActionResult DongBoHoaDon()
        {
            foreach (Invoice model in _invoiceRepository.GetAllToList())
            {
                _invoiceRepository.InitializationByID(model.ID);
            }
            string note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            return Json(note);
        }
    }
}
