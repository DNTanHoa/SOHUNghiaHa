using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Euronailsupply.Controllers;
using Euronailsupply.Models;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;
using SOHU.Data.DataTransferObject;

namespace Euronailsupply.Controllers
{
    public class InvoiceController : BaseController
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;
        private readonly IMembershipRepository _membershipRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository, IInvoiceDetailRepository invoiceDetailRepository, IMembershipRepository membershipRepository)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceDetailRepository = invoiceDetailRepository;
            _membershipRepository = membershipRepository;
        }
        private void Initialization(Invoice model)
        {
            if (!string.IsNullOrEmpty(model.InvoiceCode))
            {
                model.InvoiceCode = model.InvoiceCode.Trim();
            }
        }
        public IActionResult RetailStore()
        {
            Invoice model = new Invoice();
            model.InvoiceCode = AppGlobal.DateTimeCode;
            model.InvoiceCreated = DateTime.Now;
            model.Tax = 0;
            model.TotalNoTax = 0;
            model.TotalTax = 0;
            model.Total = 0;
            model.TotalPaid = 0;
            model.TotalDebt = 0;
            model.CategoryID = AppGlobal.InvoiceExportID;
            model.ParentID = AppGlobal.InvoiceExportID;
            model.BuyID = AppGlobal.GuestID;
            model.SellID = AppGlobal.SellID;
            model.Active = false;
            model.Initialization(InitType.Insert, RequestUserID);
            _invoiceRepository.Create(model);
            return View(model);
        }
        public IActionResult RetailStorePreview(int ID)
        {
            Invoice model = new Invoice();
            if (ID > 0)
            {
                model = _invoiceRepository.GetByID(ID);
                List<InvoiceDetailDataTransfer> list = _invoiceDetailRepository.GetDataTransferByInvoiceIDToList(ID);
                StringBuilder txt = new StringBuilder();
                foreach (InvoiceDetailDataTransfer item in list)
                {
                    txt.AppendLine(@"<tr>");
                    txt.AppendLine(@"<td style='text-align:left;'>" + item.ProductTitle + "</td>");
                    txt.AppendLine(@"<td style='text-align:right;'>" + item.Quantity.Value.ToString("N2") + "</td>");
                    txt.AppendLine(@"<td style='text-align:right;'>" + item.UnitPrice.Value.ToString("N2") + "</td>");
                    txt.AppendLine(@"<td style='text-align:right;'><b>" + item.Total.Value.ToString("N2") + "</b></td>");
                    txt.AppendLine(@"</tr>");
                }
                model.HopDong = txt.ToString();
                model.Active = true;
                model.Initialization(InitType.Update, RequestUserID);
                _invoiceRepository.Update(model.ID, model);
            }
            model.DateCreated = DateTime.Now;
            if (model.Total == null)
            {
                model.Total = 0;
            }
            return View(model);
        }
        public IActionResult Retail()
        {
            BaseViewModel viewModel = new BaseViewModel();
            viewModel.YearFinance = DateTime.Now.Year;
            viewModel.MonthFinance = DateTime.Now.Month;
            return View(viewModel);
        }
        public IActionResult ShoppingCart()
        {
            BaseViewModel viewModel = new BaseViewModel();
            viewModel.YearFinance = DateTime.Now.Year;
            viewModel.MonthFinance = DateTime.Now.Month;
            return View(viewModel);
        }
        public IActionResult Warehouse()
        {
            BaseViewModel viewModel = new BaseViewModel();
            viewModel.YearFinance = DateTime.Now.Year;
            viewModel.MonthFinance = DateTime.Now.Month;
            return View(viewModel);
        }
        public IActionResult RetailDetail(int ID)
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
            model.CategoryID = AppGlobal.InvoiceExportID;
            model.ParentID = AppGlobal.InvoiceExportID;
            return View(model);
        }
        public IActionResult ShoppingCartDetail(int ID)
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
            model.CategoryID = AppGlobal.ShoppingCartID;
            model.ParentID = AppGlobal.InvoiceExportID;
            return View(model);
        }
        public IActionResult WarehouseDetail(int ID)
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
            model.CategoryID = AppGlobal.InvoiceImportID;
            model.ParentID = AppGlobal.InvoiceImportID;
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
        public IActionResult GetByShoppingCartAndYearAndMonthToList([DataSourceRequest] DataSourceRequest request, int year, int month)
        {
            return Json(_invoiceRepository.GetByCategoryIDAndYearAndMonthToList(AppGlobal.ShoppingCartID, year, month).ToDataSourceResult(request));
        }
        public IActionResult GetByInvoiceExportAndYearAndMonthToList([DataSourceRequest] DataSourceRequest request, int year, int month)
        {
            return Json(_invoiceRepository.GetByCategoryIDAndYearAndMonthToList(AppGlobal.InvoiceExportID, year, month).ToDataSourceResult(request));
        }
        public IActionResult GetByInvoiceImportAndYearAndMonthToList([DataSourceRequest] DataSourceRequest request, int year, int month)
        {
            return Json(_invoiceRepository.GetByCategoryIDAndYearAndMonthToList(AppGlobal.InvoiceImportID, year, month).ToDataSourceResult(request));
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
        public IActionResult SaveWarehouse(Invoice model)
        {
            model.SellName = _membershipRepository.GetByID(model.SellID.Value).FullName;
            model.BuyID = AppGlobal.EuronailsupplyID;
            model.Active = true;
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
            return RedirectToAction("InvoiceInputDetail", new { ID = model.ID });
        }
        [AcceptVerbs("Post")]
        public IActionResult SaveRetail(Invoice model)
        {
            model.BuyName = _membershipRepository.GetByID(model.BuyID.Value).FullName;
            model.SellID = AppGlobal.EuronailsupplyID;
            model.Active = true;
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
            return RedirectToAction("RetailDetail", new { ID = model.ID });
        }
        [AcceptVerbs("Post")]
        public IActionResult SaveShoppingCart(Invoice model)
        {
            model.SellID = AppGlobal.EuronailsupplyID;
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
            return RedirectToAction("ShoppingCartDetail", new { ID = model.ID });
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
    }
}
