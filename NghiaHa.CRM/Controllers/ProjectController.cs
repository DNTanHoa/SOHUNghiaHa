using System;
using System.Text;
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
        private readonly IProductRepository _productRepository;
        public ProjectController(IHostingEnvironment hostingEnvironment, IProductRepository productRepository, IInvoiceRepository invoiceRepository, IInvoiceDetailRepository invoiceDetailRepository, IInvoicePropertyRepository invoicePropertyRepository, IMembershipRepository membershipRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _invoiceRepository = invoiceRepository;
            _invoiceDetailRepository = invoiceDetailRepository;
            _invoicePropertyRepository = invoicePropertyRepository;
            _membershipRepository = membershipRepository;
            _productRepository = productRepository;
        }
        private void Initialization(Invoice model)
        {
            if (!string.IsNullOrEmpty(model.InvoiceCode))
            {
                model.InvoiceCode = model.InvoiceCode.Trim();
            }
            if (!string.IsNullOrEmpty(model.InvoiceName))
            {
                model.InvoiceName = model.InvoiceName.Trim();
            }
            if (!string.IsNullOrEmpty(model.BuyPhone))
            {
                model.BuyPhone = model.BuyPhone.Trim();
            }
            if (!string.IsNullOrEmpty(model.BuyAddress))
            {
                model.BuyAddress = model.BuyAddress.Trim();
            }
            if (!string.IsNullOrEmpty(model.HangMuc))
            {
                model.HangMuc = model.HangMuc.Trim();
            }
            if (!string.IsNullOrEmpty(model.HopDongTitle))
            {
                model.HopDongTitle = model.HopDongTitle.Trim();
            }
            if (!string.IsNullOrEmpty(model.HopDongTitleSub))
            {
                model.HopDongTitleSub = model.HopDongTitleSub.Trim();
            }
        }
        private void InitializationInvoiceDetailDataTransfer(InvoiceDetailDataTransfer model)
        {
            if (model.Product != null)
            {
                model.ProductID = model.Product.ID;
            }
            if (model.Unit != null)
            {
                model.UnitID = model.Unit.ID;
            }
            model.Total = model.UnitPrice * model.Quantity;
        }
        public IActionResult Index()
        {
            BaseViewModel viewModel = new BaseViewModel();
            viewModel.YearFinance = DateTime.Now.Year;
            viewModel.MonthFinance = DateTime.Now.Month;
            return View(viewModel);
        }
        public IActionResult DetailChamCong(int ID)
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
                if (string.IsNullOrEmpty(model.HopDong))
                {
                    string hopDong = "";
                    var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, "html", "HopDong.html");
                    using (var stream = new FileStream(physicalPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            hopDong = reader.ReadToEnd();
                        }
                    }
                    DateTime now = DateTime.Now;
                    hopDong = hopDong.Replace(@"[Day]", now.Day.ToString());
                    hopDong = hopDong.Replace(@"[Month]", now.Month.ToString());
                    hopDong = hopDong.Replace(@"[Year]", now.Year.ToString());
                    hopDong = hopDong.Replace(@"[InvoiceName]", model.InvoiceName);
                    hopDong = hopDong.Replace(@"[HopDongTitle]", model.HopDongTitle);
                    hopDong = hopDong.Replace(@"[HopDongTitleSub]", model.HopDongTitleSub);
                    hopDong = hopDong.Replace(@"[InvoiceCode]", model.InvoiceCode);
                    hopDong = hopDong.Replace(@"[HangMuc]", model.HangMuc);
                    Membership buyer = _membershipRepository.GetByID(model.BuyID.Value);
                    if (buyer != null)
                    {
                        hopDong = hopDong.Replace(@"[BuyName]", buyer.FullName);
                        hopDong = hopDong.Replace(@"[BuyAddress]", buyer.Address);
                        hopDong = hopDong.Replace(@"[BuyFullName]", buyer.ContactFullName);
                        hopDong = hopDong.Replace(@"[BuyPosition]", buyer.ContactPosition);
                        hopDong = hopDong.Replace(@"[BuyBankAccount]", buyer.BankAccount);
                        hopDong = hopDong.Replace(@"[BuyBankName]", buyer.BankName);
                        hopDong = hopDong.Replace(@"[BuyTaxCode]", buyer.TaxCode);
                        hopDong = hopDong.Replace(@"[BuyPhone]", buyer.Phone);
                    }
                    Membership seller = _membershipRepository.GetByID(model.SellID.Value);
                    if (buyer != null)
                    {
                        hopDong = hopDong.Replace(@"[SellName]", seller.FullName);
                        hopDong = hopDong.Replace(@"[SellAddress]", seller.Address);
                        hopDong = hopDong.Replace(@"[SellFullName]", seller.ContactFullName);
                        hopDong = hopDong.Replace(@"[SellPosition]", seller.ContactPosition);
                        hopDong = hopDong.Replace(@"[SellBankAccount]", seller.BankAccount);
                        hopDong = hopDong.Replace(@"[SellBankName]", seller.BankName);
                        hopDong = hopDong.Replace(@"[SellTaxCode]", seller.TaxCode);
                        hopDong = hopDong.Replace(@"[SellPhone]", seller.Phone);
                    }
                    model.HopDong = hopDong;
                }
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
                //if (string.IsNullOrEmpty(model.ChaoGia))
                //{
                string chaoGia = "";
                var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, "html", "ChaoGia.html");
                using (var stream = new FileStream(physicalPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        chaoGia = reader.ReadToEnd();
                    }
                }
                DateTime now = DateTime.Now;
                chaoGia = chaoGia.Replace(@"[Day]", now.Day.ToString());
                chaoGia = chaoGia.Replace(@"[Month]", now.Month.ToString());
                chaoGia = chaoGia.Replace(@"[Year]", now.Year.ToString());
                Membership buyer = _membershipRepository.GetByID(model.BuyID.Value);
                if (buyer != null)
                {
                    chaoGia = chaoGia.Replace(@"[BuyName]", buyer.FullName);
                    chaoGia = chaoGia.Replace(@"[BuyAddress]", buyer.Address);
                }
                Membership seller = _membershipRepository.GetByID(AppGlobal.NghiaHaID);
                if (buyer != null)
                {
                    chaoGia = chaoGia.Replace(@"[SellName]", seller.FullName);
                    chaoGia = chaoGia.Replace(@"[SellAddress]", seller.Address);
                    chaoGia = chaoGia.Replace(@"[SellEmail]", seller.Email);
                    chaoGia = chaoGia.Replace(@"[SellTaxCode]", seller.TaxCode);
                    chaoGia = chaoGia.Replace(@"[SellPhone]", seller.Phone);
                }

                List<InvoiceDetailDataTransfer> list = _invoiceDetailRepository.GetProjectChaoGiaByInvoiceIDAndCategoryIDToList(model.ID, AppGlobal.ChaoGiaID);
                if (list.Count > 0)
                {
                    StringBuilder txt = new StringBuilder();
                    txt.AppendLine(@"<table class='border' style='width: 100%; font-size:14px; line-height:20px;'>");
                    int no = 0;
                    txt.AppendLine(@"<thead>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>No</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Hàng hóa</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Thông số kỹ thuật</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Đơn vị tính</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Số lượng</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Đơn giá</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Thành tiền</a></th>");
                    txt.AppendLine(@"</thead>");
                    txt.AppendLine(@"<tbody>");
                    foreach (InvoiceDetailDataTransfer item in list)
                    {
                        no = no + 1;
                        txt.AppendLine(@"<tr>");
                        txt.AppendLine(@"<td style='text-align:center;'>" + no + "</td>");
                        txt.AppendLine(@"<td style='text-align:center;'>");
                        txt.AppendLine(@"<img src='" + item.ImageURLFull + "' width='200px' height='200px' />");
                        txt.AppendLine(@"<br/>");
                        txt.AppendLine(@"<b>" + item.ProductTitle + "</b>");
                        txt.AppendLine(@"</td>");
                        txt.AppendLine(@"<td style='text-align:left;'>" + item.ContentMain + "</td>");
                        txt.AppendLine(@"<td style='text-align:center;'>" + item.UnitName + "</td>");
                        txt.AppendLine(@"<td style='text-align:center;'>" + item.Quantity.Value.ToString("N0").Replace(@",", @".") + "</td>");
                        txt.AppendLine(@"<td style='text-align:center;'>" + item.UnitPrice.Value.ToString("N0").Replace(@",", @".") + "</td>");
                        txt.AppendLine(@"<td style='text-align:center;'>" + item.Total.Value.ToString("N0").Replace(@",", @".") + "</td>");
                        txt.AppendLine(@"</tr>");
                    }
                    txt.AppendLine(@"</tbody>");
                    txt.AppendLine(@"</table>");
                    chaoGia = chaoGia.Replace(@"[ChaoGia]", txt.ToString());
                }

                model.ChaoGia = chaoGia;
                //}
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
        public ActionResult GetProjectDuToanFullNameByInvoiceIDAndDuToanToList([DataSourceRequest] DataSourceRequest request, int invoiceID)
        {
            var data = _invoiceDetailRepository.GetProjectDuToanFullNameByInvoiceIDAndCategoryIDToList(invoiceID, AppGlobal.DuToanID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetInvoicePropertyByInvoiceIDToList([DataSourceRequest] DataSourceRequest request, int invoiceID)
        {
            var data = _invoicePropertyRepository.GetByInvoiceIDToList(invoiceID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetProjectNhanSuByInvoiceIDAndNhanSuToList([DataSourceRequest] DataSourceRequest request, int invoiceID)
        {
            var data = _invoiceDetailRepository.GetProjectNhanSuByInvoiceIDAndParentIDToList(invoiceID, AppGlobal.NhanSuID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetProjectChamCongByInvoiceIDAndChamCongToList([DataSourceRequest] DataSourceRequest request, int invoiceID)
        {
            var data = _invoiceDetailRepository.GetProjectNhanSuByInvoiceIDAndParentIDToList(invoiceID, AppGlobal.ChamCongID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetProjectDuToanByInvoiceIDAndThiCongToList([DataSourceRequest] DataSourceRequest request, int invoiceID)
        {
            var data = _invoiceDetailRepository.GetProjectDuToanByInvoiceIDAndParentIDToList(invoiceID, AppGlobal.ThiCongID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetProjectNhanSuByInvoiceIDAndChamCongToList([DataSourceRequest] DataSourceRequest request, int invoiceID)
        {
            var data = _invoiceDetailRepository.GetProjectNhanSuByInvoiceIDAndCategoryIDToList(invoiceID, AppGlobal.ChamCongID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetProjectDuToanByInvoiceIDAndDuToanToList([DataSourceRequest] DataSourceRequest request, int invoiceID)
        {
            var data = _invoiceDetailRepository.GetProjectDuToanByInvoiceIDAndCategoryIDToList(invoiceID, AppGlobal.DuToanID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetProjectDuToanByInvoiceIDAndChaoGiaToList([DataSourceRequest] DataSourceRequest request, int invoiceID)
        {
            var data = _invoiceDetailRepository.GetProjectDuToanByInvoiceIDAndCategoryIDToList(invoiceID, AppGlobal.ChaoGiaID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetProjectThiCongByInvoiceIDAndThiCongToList([DataSourceRequest] DataSourceRequest request, int invoiceID)
        {
            var data = _invoiceDetailRepository.GetProjectThiCongByInvoiceIDAndCategoryIDToList(invoiceID, AppGlobal.ThiCongID);
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
        public IActionResult DeleteProjectThiCong(int ID)
        {
            InvoiceDetail invoiceDetail = _invoiceDetailRepository.GetByID(ID);
            int invoiceID = 0;
            int productID = 0;
            int categoryID = 0;
            if (invoiceDetail != null)
            {
                invoiceID = invoiceDetail.InvoiceID.Value;
                productID = invoiceDetail.ProductID.Value;
                categoryID = invoiceDetail.CategoryID.Value;
            }
            string note = AppGlobal.InitString;
            int result = _invoiceDetailRepository.Delete(ID);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.DeleteSuccess;
                _invoiceRepository.InitializationByIDAndCategoryID(invoiceID, categoryID);
                _productRepository.InitializationByIDAndCategoryID(productID, categoryID);
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
            model.SellID = AppGlobal.NghiaHaID;
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
                invoice.InvoiceCode = model.InvoiceCode;
                invoice.HangMuc = model.HangMuc;
                invoice.HopDongTitle = model.HopDongTitle;
                invoice.HopDongTitleSub = model.HopDongTitleSub;
                if (!string.IsNullOrEmpty(invoice.HopDongTitleSub))
                {
                    invoice.HopDongTitleSub = model.HopDongTitle;
                }
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
            model.CategoryID = AppGlobal.DuToanID;
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
                    baogia.CategoryID = AppGlobal.ChaoGiaID;
                    _invoiceDetailRepository.Create(baogia);
                }
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateProjectThiCong(InvoiceDetailDataTransfer model, int invoiceID)
        {
            model.CategoryID = AppGlobal.ThiCongID;
            model.InvoiceID = invoiceID;
            model.ParentID = model.Parent.ID;
            model.ProductID = model.Product.ID;
            model.EmployeeID = model.Employee.ID;
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
                _invoiceRepository.InitializationByIDAndCategoryID(model.InvoiceID.Value, model.CategoryID.Value);
                _productRepository.InitializationByIDAndCategoryID(model.ProductID.Value, model.CategoryID.Value);
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateProjectChaoGia(InvoiceDetailDataTransfer model, int invoiceID)
        {
            model.CategoryID = AppGlobal.ChaoGiaID;
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
            model.CategoryID = AppGlobal.NhanSuID;
            model.InvoiceID = invoiceID;
            model.EmployeeID = model.Employee.ID;
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
        public IActionResult CreateProjectChamCong(InvoiceDetailDataTransfer model, int invoiceID)
        {
            model.CategoryID = AppGlobal.ChamCongID;
            model.InvoiceID = invoiceID;
            model.EmployeeID = model.Employee.ID;
            model.ProductID = AppGlobal.NhanCongID;
            model.UnitID = AppGlobal.LanID;
            model.Quantity = model.Shift01 + model.Shift02 + model.Shift03;
            model.UnitPrice = 0;
            InitializationInvoiceDetailDataTransfer(model);
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
        public IActionResult UpdateProjectThiCong(InvoiceDetailDataTransfer model)
        {
            string note = AppGlobal.InitString;
            InitializationInvoiceDetailDataTransfer(model);
            model.Initialization(InitType.Update, RequestUserID);
            int result = _invoiceDetailRepository.Update(model.ID, model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
                _invoiceRepository.InitializationByIDAndCategoryID(model.InvoiceID.Value, model.CategoryID.Value);
                _productRepository.InitializationByIDAndCategoryID(model.ProductID.Value, model.CategoryID.Value);
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.EditFail;
            }
            return Json(note);
        }
        public IActionResult UpdateProjectNhanSu(InvoiceDetailDataTransfer model)
        {
            model.EmployeeID = model.Employee.ID;
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
        public IActionResult UpdateProjectChamCong(InvoiceDetailDataTransfer model)
        {
            model.EmployeeID = model.Employee.ID;
            model.Quantity = model.Shift01 + model.Shift02 + model.Shift03;
            InitializationInvoiceDetailDataTransfer(model);
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