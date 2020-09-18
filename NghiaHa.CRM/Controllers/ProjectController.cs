using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NghiaHa.CRM.Controllers;
using NghiaHa.CRM.Web.Models;
using SOHU.Data.DataTransferObject;
using SOHU.Data.Enum;
using SOHU.Data.Extensions;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace NghiaHa.CRM.Web.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IInvoicePropertyRepository _invoicePropertyRepository;
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;
        public ProjectController(IHostingEnvironment hostingEnvironment, IInvoiceRepository invoiceRepository, IInvoiceDetailRepository invoiceDetailRepository, IInvoicePropertyRepository invoicePropertyRepository, IMembershipRepository membershipRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _invoiceRepository = invoiceRepository;
            _invoiceDetailRepository = invoiceDetailRepository;
            _invoicePropertyRepository = invoicePropertyRepository;
            _membershipRepository = membershipRepository;
        }
        private void Initialization(Invoice model)
        {
            if (!string.IsNullOrEmpty(model.InvoiceCode))
            {
                model.InvoiceCode = model.InvoiceCode.Trim();
            }
        }
        private void InitializationInvoiceDetailDataTransfer(InvoiceDetailDataTransfer model)
        {
            model.ProductID = model.Product.ID;
            model.UnitID = model.Unit.ID;
            model.Total = model.UnitPrice * model.Quantity;
        }
        public IActionResult Index()
        {
            BaseViewModel viewModel = new BaseViewModel();
            viewModel.YearFinance = DateTime.Now.Year;
            viewModel.MonthFinance = DateTime.Now.Month;
            return View(viewModel);
        }
        public IActionResult DetailDuToan(int ID)
        {
            Invoice model = new Invoice();
            model.InvoiceCreated = DateTime.Now;
            model.DateBegin = DateTime.Now;
            model.DateEnd = DateTime.Now;
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
            model.CategoryID = AppGlobal.DuAnID;
            return View(model);
        }
        public IActionResult DetailNhanSu(int ID)
        {
            Invoice model = new Invoice();
            model.InvoiceCreated = DateTime.Now;
            model.DateBegin = DateTime.Now;
            model.DateEnd = DateTime.Now;
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
            model.CategoryID = AppGlobal.DuAnID;
            return View(model);
        }
        public IActionResult Detail(int ID)
        {
            Invoice model = new Invoice();
            model.InvoiceCreated = DateTime.Now;
            model.DateBegin = DateTime.Now;
            model.DateEnd = DateTime.Now;
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
            model.CategoryID = AppGlobal.DuAnID;
            return View(model);
        }
        public IActionResult DetailHopDong(int ID)
        {
            Invoice model = new Invoice();
            model.InvoiceCreated = DateTime.Now;
            model.DateBegin = DateTime.Now;
            model.DateEnd = DateTime.Now;
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
            model.CategoryID = AppGlobal.DuAnID;
            return View(model);
        }
        public IActionResult DetailChaoGia(int ID)
        {
            Invoice model = new Invoice();
            model.InvoiceCreated = DateTime.Now;
            model.DateBegin = DateTime.Now;
            model.DateEnd = DateTime.Now;
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
            model.CategoryID = AppGlobal.DuAnID;
            return View(model);
        }
        public IActionResult DetailNghiemThu(int ID)
        {
            Invoice model = new Invoice();
            model.InvoiceCreated = DateTime.Now;
            model.DateBegin = DateTime.Now;
            model.DateEnd = DateTime.Now;
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
            model.CategoryID = AppGlobal.DuAnID;
            return View(model);
        }
        public IActionResult DetailThanhLy(int ID)
        {
            Invoice model = new Invoice();
            model.InvoiceCreated = DateTime.Now;
            model.DateBegin = DateTime.Now;
            model.DateEnd = DateTime.Now;
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
            model.CategoryID = AppGlobal.DuAnID;
            return View(model);
        }
        public IActionResult DetailFiles(int ID)
        {
            InvoiceProperty model = new InvoiceProperty();
            model.InvoiceID = ID;
            return View(model);
        }
        public IActionResult PrintPreviewHopDong(int ID)
        {
            Invoice model = new Invoice();
            model.InvoiceCreated = DateTime.Now;
            model.DateBegin = DateTime.Now;
            model.DateEnd = DateTime.Now;
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
            model.CategoryID = AppGlobal.DuAnID;
            return View(model);
        }
        public IActionResult PrintPreviewThanhLy(int ID)
        {
            Invoice model = new Invoice();
            model.InvoiceCreated = DateTime.Now;
            model.DateBegin = DateTime.Now;
            model.DateEnd = DateTime.Now;
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
            model.CategoryID = AppGlobal.DuAnID;
            return View(model);
        }
        public IActionResult PrintPreviewNghiemThu(int ID)
        {
            Invoice model = new Invoice();
            model.InvoiceCreated = DateTime.Now;
            model.DateBegin = DateTime.Now;
            model.DateEnd = DateTime.Now;
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
            model.CategoryID = AppGlobal.DuAnID;
            return View(model);
        }
        public IActionResult PrintPreviewChaoGia(int ID)
        {
            Invoice model = new Invoice();
            model.InvoiceCreated = DateTime.Now;
            model.DateBegin = DateTime.Now;
            model.DateEnd = DateTime.Now;
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
            model.CategoryID = AppGlobal.DuAnID;
            return View(model);
        }
        public ActionResult GetInvoicePropertyByInvoiceIDToList([DataSourceRequest] DataSourceRequest request, int invoiceID)
        {
            var data = _invoicePropertyRepository.GetByInvoiceIDToList(invoiceID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetProjectNhanSuByInvoiceIDParentIDToList([DataSourceRequest] DataSourceRequest request, int invoiceID)
        {
            var data = _invoiceDetailRepository.GetProjectNhanSuByInvoiceIDAndParentIDToList(invoiceID, AppGlobal.NhanSuID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetProjectDuToanByInvoiceIDAndDuToanToList([DataSourceRequest] DataSourceRequest request, int invoiceID)
        {
            var data = _invoiceDetailRepository.GetProjectDuToanByInvoiceIDAndParentIDToList(invoiceID, AppGlobal.DuToanID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetProjectDuToanByInvoiceIDAndChaoGiaToList([DataSourceRequest] DataSourceRequest request, int invoiceID)
        {
            var data = _invoiceDetailRepository.GetProjectDuToanByInvoiceIDAndParentIDToList(invoiceID, AppGlobal.ChaoGiaID);
            return Json(data.ToDataSourceResult(request));
        }
        public IActionResult DeleteInvoiceProperty(int ID)
        {
            string note = AppGlobal.InitString;
            int result = _invoicePropertyRepository.Delete(ID);
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
        public IActionResult DeleteDetail(int ID)
        {
            string note = AppGlobal.InitString;
            int result = _invoiceDetailRepository.Delete(ID);
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
        [AcceptVerbs("Post")]
        public IActionResult SaveProject(Invoice model)
        {
            Membership membership = _membershipRepository.GetByID(model.BuyID.Value);
            model.BuyName = membership.FullName;
            if (string.IsNullOrEmpty(model.BuyPhone))
            {
                model.BuyPhone = membership.Phone;
            }
            if (string.IsNullOrEmpty(model.BuyAddress))
            {
                model.BuyAddress = membership.Address;
            }
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
                _invoiceRepository.Create(model);
            }
            return RedirectToAction("Detail", new { ID = model.ID });
        }
        [AcceptVerbs("Post")]
        public IActionResult SaveHopDong(Invoice model)
        {
            if (model.ID > 0)
            {
                Invoice invoice = _invoiceRepository.GetByID(model.ID);
                invoice.HopDong = model.HopDong;
                Initialization(invoice);
                invoice.Initialization(InitType.Update, RequestUserID);
                _invoiceRepository.Update(invoice.ID, invoice);
            }
            return RedirectToAction("DetailHopDong", new { ID = model.ID });
        }
        [AcceptVerbs("Post")]
        public IActionResult SaveChaoGia(Invoice model)
        {
            if (model.ID > 0)
            {
                Invoice invoice = _invoiceRepository.GetByID(model.ID);
                invoice.ChaoGia = model.ChaoGia;
                Initialization(invoice);
                invoice.Initialization(InitType.Update, RequestUserID);
                _invoiceRepository.Update(invoice.ID, invoice);
            }
            return RedirectToAction("DetailChaoGia", new { ID = model.ID });
        }
        [AcceptVerbs("Post")]
        public IActionResult SaveNghiemThu(Invoice model)
        {
            if (model.ID > 0)
            {
                Invoice invoice = _invoiceRepository.GetByID(model.ID);
                invoice.NghiemThu = model.NghiemThu;
                Initialization(invoice);
                invoice.Initialization(InitType.Update, RequestUserID);
                _invoiceRepository.Update(invoice.ID, invoice);
            }
            return RedirectToAction("DetailNghiemThu", new { ID = model.ID });
        }
        [AcceptVerbs("Post")]
        public IActionResult SaveThanhLy(Invoice model)
        {
            if (model.ID > 0)
            {
                Invoice invoice = _invoiceRepository.GetByID(model.ID);
                invoice.ThanhLy = model.ThanhLy;
                Initialization(invoice);
                invoice.Initialization(InitType.Update, RequestUserID);
                _invoiceRepository.Update(invoice.ID, invoice);
            }
            return RedirectToAction("DetailThanhLy", new { ID = model.ID });
        }
        public IActionResult CreateProjectDuToan(InvoiceDetailDataTransfer model, int invoiceID)
        {
            model.ParentID = AppGlobal.DuToanID;
            model.InvoiceID = invoiceID;
            InitializationInvoiceDetailDataTransfer(model);
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
                if (model.IsChaoGia == true)
                {
                    InvoiceDetailDataTransfer baogia = model;
                    baogia.ID = 0;
                    baogia.ParentID = AppGlobal.ChaoGiaID;
                    _invoiceDetailRepository.Create(baogia);
                }
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateProjectChaoGia(InvoiceDetailDataTransfer model, int invoiceID)
        {
            model.ParentID = AppGlobal.ChaoGiaID;
            model.InvoiceID = invoiceID;
            InitializationInvoiceDetailDataTransfer(model);
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
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateProjectNhanSu(InvoiceDetailDataTransfer model, int invoiceID)
        {
            model.ParentID = AppGlobal.NhanSuID;
            model.InvoiceID = invoiceID;
            model.CurrencyID = model.Employee.ID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            result = _invoiceDetailRepository.Create(model);

            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult UpdateProjectDuToan(InvoiceDetailDataTransfer model)
        {
            string note = AppGlobal.InitString;
            InitializationInvoiceDetailDataTransfer(model);
            model.Initialization(InitType.Update, RequestUserID);
            int result = _invoiceDetailRepository.Update(model.ID, model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.EditFail;
            }
            return Json(note);
        }
        public IActionResult UpdateProjectNhanSu(InvoiceDetailDataTransfer model)
        {
            model.CurrencyID = model.Employee.ID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _invoiceDetailRepository.Update(model.ID, model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.EditFail;
            }
            return Json(note);
        }
        [AcceptVerbs("Post")]
        public IActionResult SaveFiles(InvoiceProperty model)
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

            }
            return RedirectToAction("DetailFiles", "Project", new { ID = model.InvoiceID });
        }
    }
}