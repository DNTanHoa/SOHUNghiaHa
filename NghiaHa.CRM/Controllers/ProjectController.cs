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
        public ProjectController(IHostingEnvironment hostingEnvironment, IInvoiceRepository invoiceRepository, IInvoicePropertyRepository invoicePropertyRepository, IMembershipRepository membershipRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _invoiceRepository = invoiceRepository;
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
        public IActionResult Index()
        {
            BaseViewModel viewModel = new BaseViewModel();
            viewModel.YearFinance = DateTime.Now.Year;
            viewModel.MonthFinance = DateTime.Now.Month;
            return View(viewModel);
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
            model.CategoryId = AppGlobal.DuAnID;
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
            model.CategoryId = AppGlobal.DuAnID;
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
            model.CategoryId = AppGlobal.DuAnID;
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
            model.CategoryId = AppGlobal.DuAnID;
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
            model.CategoryId = AppGlobal.DuAnID;
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
            model.CategoryId = AppGlobal.DuAnID;
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
            model.CategoryId = AppGlobal.DuAnID;
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
            model.CategoryId = AppGlobal.DuAnID;
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
            model.CategoryId = AppGlobal.DuAnID;
            return View(model);
        }
        public ActionResult GetInvoicePropertyByInvoiceIDToList([DataSourceRequest] DataSourceRequest request, int invoiceID)
        {
            var data = _invoicePropertyRepository.GetByInvoiceIDToList(invoiceID);
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
        [AcceptVerbs("Post")]
        public IActionResult SaveProject(Invoice model)
        {
            Membership membership = _membershipRepository.GetByID(model.BuyId.Value);
            model.BuyName = membership.FullName;
            if (string.IsNullOrEmpty(model.BuyPhone))
            {
                model.BuyPhone = membership.Phone;
            }
            if (string.IsNullOrEmpty(model.BuyAddress))
            {
                model.BuyAddress = membership.Address;
            }
            if (model.Id > 0)
            {
                Initialization(model);
                model.Initialization(InitType.Update, RequestUserID);
                _invoiceRepository.Update(model.Id, model);
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
            return RedirectToAction("Detail", new { ID = model.Id });
        }
        [AcceptVerbs("Post")]
        public IActionResult SaveHopDong(Invoice model)
        {
            if (model.Id > 0)
            {
                Initialization(model);
                model.Initialization(InitType.Update, RequestUserID);
                _invoiceRepository.Update(model.Id, model);
            }
            return RedirectToAction("DetailHopDong", new { ID = model.Id });
        }
        [AcceptVerbs("Post")]
        public IActionResult SaveChaoGia(Invoice model)
        {
            if (model.Id > 0)
            {
                Initialization(model);
                model.Initialization(InitType.Update, RequestUserID);
                _invoiceRepository.Update(model.Id, model);
            }
            return RedirectToAction("DetailChaoGia", new { ID = model.Id });
        }
        [AcceptVerbs("Post")]
        public IActionResult SaveNghiemThu(Invoice model)
        {
            if (model.Id > 0)
            {
                Initialization(model);
                model.Initialization(InitType.Update, RequestUserID);
                _invoiceRepository.Update(model.Id, model);
            }
            return RedirectToAction("DetailNghiemThu", new { ID = model.Id });
        }
        [AcceptVerbs("Post")]
        public IActionResult SaveThanhLy(Invoice model)
        {
            if (model.Id > 0)
            {
                Initialization(model);
                model.Initialization(InitType.Update, RequestUserID);
                _invoiceRepository.Update(model.Id, model);
            }
            return RedirectToAction("DetailThanhLy", new { ID = model.Id });
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