using System;
using System.Text;
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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace NghiaHa.CRM.Web.Controllers
{
    public class InvoiceController : BaseController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoicePropertyRepository _invoicePropertyRepository;
        private readonly IMembershipRepository _membershipRepository;
        public InvoiceController(IWebHostEnvironment hostingEnvironment, IInvoiceRepository invoiceRepository, IInvoicePropertyRepository invoicePropertyRepository, IMembershipRepository membershipRepository)
        {
            _invoiceRepository = invoiceRepository;
            _invoicePropertyRepository = invoicePropertyRepository;
            _membershipRepository = membershipRepository;
            _hostingEnvironment = hostingEnvironment;
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
        public IActionResult InvoiceOutputDetailFiles(int ID)
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
        public IActionResult BanLeDetailFiles(int ID)
        {
            InvoiceProperty model = new InvoiceProperty();
            if (ID > 0)
            {
                Invoice invoice = _invoiceRepository.GetByID(ID);
                if (invoice != null)
                {
                    model.Title = invoice.BuyName;
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
        public IActionResult BanLe()
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
            model.IsXuatHoaDon = false;
            model.IsNhanHoaDon = false;
            if (ID > 0)
            {
                model = _invoiceRepository.GetByID(ID);
                List<InvoiceProperty> listInvoiceProperty = _invoicePropertyRepository.GetByInvoiceIDToList(ID);
                StringBuilder txt = new StringBuilder();
                foreach (InvoiceProperty item in listInvoiceProperty)
                {
                    switch (item.Note)
                    {
                        case ".pdf":
                            txt.AppendLine("<iframe src='" + AppGlobal.Domain + "Images/Project/" + item.FileName + "' width='100%' height='1000px'></iframe>");
                            break;
                        case ".png":
                        case ".jpg":
                        case ".jpeg":
                            txt.AppendLine("<img src='" + AppGlobal.Domain + "Images/Project/" + item.FileName + "' class='img-thumbnail' />");
                            break;
                    }
                }
                model.Note = txt.ToString();
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
            model.IsXuatHoaDon = false;
            model.IsNhanHoaDon = false;
            if (ID > 0)
            {
                model = _invoiceRepository.GetByID(ID);
                List<InvoiceProperty> listInvoiceProperty = _invoicePropertyRepository.GetByInvoiceIDToList(ID);
                StringBuilder txt = new StringBuilder();
                foreach (InvoiceProperty item in listInvoiceProperty)
                {
                    switch (item.Note)
                    {
                        case ".pdf":
                            txt.AppendLine("<iframe src='" + AppGlobal.Domain + "Images/Project/" + item.FileName + "' width='100%' height='1000px'></iframe>");
                            break;
                        case ".png":
                        case ".jpg":
                        case ".jpeg":
                            txt.AppendLine("<img src='" + AppGlobal.Domain + "Images/Project/" + item.FileName + "' class='img-thumbnail' />");
                            break;
                    }
                }
                model.Note = txt.ToString();
            }
            model.CategoryID = AppGlobal.InvoiceOutputID;
            return View(model);
        }
        public IActionResult BanLeDetail(int ID)
        {
            Invoice model = new Invoice();
            model.InvoiceCreated = DateTime.Now;
            model.Tax = AppGlobal.Tax;
            model.TotalNoTax = 0;
            model.TotalTax = 0;
            model.Total = 0;
            model.TotalPaid = 0;
            model.TotalDebt = 0;
            model.IsXuatHoaDon = false;
            model.IsNhanHoaDon = false;
            if (ID > 0)
            {
                model = _invoiceRepository.GetByID(ID);
                List<InvoiceProperty> listInvoiceProperty = _invoicePropertyRepository.GetByInvoiceIDToList(ID);
                StringBuilder txt = new StringBuilder();
                foreach (InvoiceProperty item in listInvoiceProperty)
                {
                    switch (item.Note)
                    {
                        case ".pdf":
                            txt.AppendLine("<iframe src='" + AppGlobal.Domain + "Images/Project/" + item.FileName + "' width='100%' height='1000px'></iframe>");
                            break;
                        case ".png":
                        case ".jpg":
                        case ".jpeg":
                            txt.AppendLine("<img src='" + AppGlobal.Domain + "Images/Project/" + item.FileName + "' class='img-thumbnail' />");
                            break;
                    }
                }
                model.Note = txt.ToString();
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
        public IActionResult GetHoaDonByInvoiceOutputAndYearAndSellIDAndSearchStringToList([DataSourceRequest] DataSourceRequest request, int year, int sellID, string searchString)
        {
            return Json(_invoiceRepository.GetHoaDonByCategoryIDAndYearAndSellIDAndSearchStringToList(AppGlobal.InvoiceOutputID, year, sellID, searchString).ToDataSourceResult(request));
        }
        public IActionResult GetHoaDonByInvoiceInputAndYearAndSellIDAndSearchStringToList([DataSourceRequest] DataSourceRequest request, int year, int sellID, string searchString)
        {
            return Json(_invoiceRepository.GetHoaDonByCategoryIDAndYearAndSellIDAndSearchStringToList(AppGlobal.InvoiceInputID, year, sellID, searchString).ToDataSourceResult(request));
        }
        public IActionResult GetBanLeByInvoiceOutputAndYearAndSellIDToList([DataSourceRequest] DataSourceRequest request, int year, int sellID)
        {
            return Json(_invoiceRepository.GetBanLeByCategoryIDAndYearAndSellIDToList(AppGlobal.InvoiceOutputID, year, sellID).ToDataSourceResult(request));
        }
        public IActionResult GetBanLeByCategoryIDAndYearAndSellIDToSUM(int year, int sellID)
        {
            return Json(_invoiceRepository.GetBanLeByCategoryIDAndYearAndSellIDToSUM(AppGlobal.InvoiceOutputID, year, sellID));
        }
        public IActionResult GetByInvoiceInputAndYearAndMonthAndSellIDAndSearchStringToSUM(int year, int month, int sellID, string searchString)
        {
            return Json(_invoiceRepository.GetByCategoryIDAndYearAndMonthAndSellIDAndSearchStringToSUM(AppGlobal.InvoiceInputID, year, month, sellID, searchString));
        }
        public IActionResult GetByInvoiceOutputAndYearAndMonthAndSellIDAndSearchStringToSUM(int year, int month, int sellID, string searchString)
        {
            return Json(_invoiceRepository.GetByCategoryIDAndYearAndMonthAndSellIDAndSearchStringToSUM(AppGlobal.InvoiceOutputID, year, month, sellID, searchString));
        }
        public IActionResult GetHoaDonByInvoiceOutputAndYearAndSellIDAndSearchStringToSUM(int year, int sellID, string searchString)
        {
            return Json(_invoiceRepository.GetHoaDonByCategoryIDAndYearAndSellIDAndSearchStringToSUM(AppGlobal.InvoiceOutputID, year, sellID, searchString));
        }
        public IActionResult GetHoaDonByInvoiceInputAndYearAndSellIDAndSearchStringToSUM(int year, int sellID, string searchString)
        {
            return Json(_invoiceRepository.GetHoaDonByCategoryIDAndYearAndSellIDAndSearchStringToSUM(AppGlobal.InvoiceInputID, year, sellID, searchString));
        }
        public IActionResult GetInvoiceInputByProductIDToList([DataSourceRequest] DataSourceRequest request, int productID)
        {
            return Json(_invoiceRepository.GetInvoiceInputByProductIDToList(productID).ToDataSourceResult(request));
        }
        public IActionResult GetInvoiceOutputByProductIDToList([DataSourceRequest] DataSourceRequest request, int productID)
        {
            return Json(_invoiceRepository.GetInvoiceOutputByProductIDToList(productID).ToDataSourceResult(request));
        }
        public List<Invoice> GetSUMSQLByInvoiceInputAndYearAndMonthAndSellIDAndSearchStringToListToJSON(int year, int month, int sellID, string searchString)
        {
            List<Invoice> list = _invoiceRepository.GetSUMSQLByCategoryIDAndYearAndMonthAndSellIDAndSearchStringToList(AppGlobal.InvoiceInputID, year, month, sellID, searchString);
            return list;
        }
        public List<Invoice> GetSUMSQLByInvoiceOutputAndYearAndMonthAndSellIDAndSearchStringToListToJSON(int year, int month, int sellID, string searchString)
        {
            List<Invoice> list = _invoiceRepository.GetSUMSQLByCategoryIDAndYearAndMonthAndSellIDAndSearchStringToList(AppGlobal.InvoiceOutputID, year, month, sellID, searchString);
            return list;
        }
        public List<Invoice> GetSUMSQLBanLeByInvoiceOutputAndYearAndMonthAndSellIDAndSearchStringToListToJSON(int year, int month, int sellID, string searchString)
        {
            List<Invoice> list = _invoiceRepository.GetSUMSQLBanLeByCategoryIDAndYearAndMonthAndSellIDAndSearchStringToList(AppGlobal.InvoiceOutputID, year, month, sellID, searchString);
            return list;
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
                _invoiceRepository.InitializationByID(model.ID);
            }
            else
            {
                Initialization(model);
                model.Initialization(InitType.Insert, RequestUserID);
                _invoiceRepository.Create(model);
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
                _invoiceRepository.InitializationByID(model.ID);
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
        public IActionResult SaveBanLe(Invoice model)
        {
            model.BuyName = _membershipRepository.GetByID(model.BuyID.Value).FullName;
            model.SellID = AppGlobal.NghiaHaID;
            if (model.ID > 0)
            {
                Initialization(model);
                model.Initialization(InitType.Update, RequestUserID);
                _invoiceRepository.Update(model.ID, model);
                _invoiceRepository.InitializationByID(model.ID);
            }
            else
            {
                Initialization(model);
                model.Initialization(InitType.Insert, RequestUserID);
                _invoiceRepository.Create(model);
            }
            return RedirectToAction("BanLeDetail", new { ID = model.ID });
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
        [AcceptVerbs("Post")]
        public IActionResult SaveInvoiceInputFiles(InvoiceProperty model)
        {
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    List<InvoiceProperty> list = new List<InvoiceProperty>();
                    for (int i = 0; i < Request.Form.Files.Count; i++)
                    {
                        var file = Request.Form.Files[i];
                        if (file != null)
                        {
                            string fileExtension = Path.GetExtension(file.FileName);
                            string fileName = Path.GetFileNameWithoutExtension(file.FileName);

                            fileName = AppGlobal.SetName(fileName);
                            fileName = fileName + "-" + AppGlobal.DateTimeCode + fileExtension;
                            var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, AppGlobal.URLImagesCustomer, fileName);
                            using (var stream = new FileStream(physicalPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                                InvoiceProperty membershipPermission = new InvoiceProperty();
                                membershipPermission.Initialization(InitType.Insert, RequestUserID);
                                membershipPermission.Code = "File";
                                membershipPermission.InvoiceID = model.InvoiceID;
                                membershipPermission.Title = model.Title;
                                membershipPermission.FileName = fileName;
                                membershipPermission.Note = fileExtension;
                                list.Add(membershipPermission);
                            }
                        }
                    }
                    _invoicePropertyRepository.Range(list);
                }
            }
            catch (Exception e)
            {
                string mes = e.Message;
            }
            return RedirectToAction("DetailFiles", "Invoice", new { ID = model.InvoiceID });
        }
        [AcceptVerbs("Post")]
        public IActionResult SaveInvoiceOutputFiles(InvoiceProperty model)
        {
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    List<InvoiceProperty> list = new List<InvoiceProperty>();
                    for (int i = 0; i < Request.Form.Files.Count; i++)
                    {
                        var file = Request.Form.Files[i];
                        if (file != null)
                        {
                            string fileExtension = Path.GetExtension(file.FileName);
                            string fileName = Path.GetFileNameWithoutExtension(file.FileName);

                            fileName = AppGlobal.SetName(fileName);
                            fileName = fileName + "-" + AppGlobal.DateTimeCode + fileExtension;
                            var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, AppGlobal.URLImagesCustomer, fileName);
                            using (var stream = new FileStream(physicalPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                                InvoiceProperty membershipPermission = new InvoiceProperty();
                                membershipPermission.Initialization(InitType.Insert, RequestUserID);
                                membershipPermission.Code = "File";
                                membershipPermission.InvoiceID = model.InvoiceID;
                                membershipPermission.Title = model.Title;
                                membershipPermission.FileName = fileName;
                                membershipPermission.Note = fileExtension;
                                list.Add(membershipPermission);
                            }
                        }
                    }
                    _invoicePropertyRepository.Range(list);
                }
            }
            catch (Exception e)
            {
                string mes = e.Message;
            }
            return RedirectToAction("InvoiceOutputDetailFiles", "Invoice", new { ID = model.InvoiceID });
        }
        [AcceptVerbs("Post")]
        public IActionResult SaveBanLeDetailFiles(InvoiceProperty model)
        {
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    List<InvoiceProperty> list = new List<InvoiceProperty>();
                    for (int i = 0; i < Request.Form.Files.Count; i++)
                    {
                        var file = Request.Form.Files[i];
                        if (file != null)
                        {
                            string fileExtension = Path.GetExtension(file.FileName);
                            string fileName = Path.GetFileNameWithoutExtension(file.FileName);

                            fileName = AppGlobal.SetName(fileName);
                            fileName = fileName + "-" + AppGlobal.DateTimeCode + fileExtension;
                            var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, AppGlobal.URLImagesCustomer, fileName);
                            using (var stream = new FileStream(physicalPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                                InvoiceProperty membershipPermission = new InvoiceProperty();
                                membershipPermission.Initialization(InitType.Insert, RequestUserID);
                                membershipPermission.Code = "File";
                                membershipPermission.InvoiceID = model.InvoiceID;
                                membershipPermission.Title = model.Title;
                                membershipPermission.FileName = fileName;
                                membershipPermission.Note = fileExtension;
                                list.Add(membershipPermission);
                            }
                        }
                    }
                    _invoicePropertyRepository.Range(list);
                }
            }
            catch (Exception e)
            {
                string mes = e.Message;
            }
            return RedirectToAction("BanLeDetailFiles", "Invoice", new { ID = model.InvoiceID });
        }
    }
}
