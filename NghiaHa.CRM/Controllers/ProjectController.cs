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
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IInvoicePropertyRepository _invoicePropertyRepository;
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;
        private readonly IProductRepository _productRepository;
        public ProjectController(IWebHostEnvironment hostingEnvironment, IProductRepository productRepository, IInvoiceRepository invoiceRepository, IInvoiceDetailRepository invoiceDetailRepository, IInvoicePropertyRepository invoicePropertyRepository, IMembershipRepository membershipRepository)
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
            Invoice invoice = _invoiceRepository.GetByID(model.InvoiceID.Value);
            if (model.UnitPrice == null)
            {
                model.UnitPrice = 0;
            }
            if (model.Quantity == null)
            {
                model.Quantity = 0;
            }
            if (model.Discount == null)
            {
                model.Discount = 0;
            }
            if (model.Quantity01 == null)
            {
                model.Quantity01 = model.Quantity;
            }
            if (model.Unit != null)
            {
                model.UnitID = model.Unit.ID;
            }
            if (model.Product != null)
            {
                model.ProductID = model.Product.ID;
                Product product = _productRepository.GetByID(model.ProductID.Value);
                if (string.IsNullOrEmpty(model.ProductCode))
                {
                    model.ProductCode = product.MetaTitle;
                }
                if (model.UnitID == null)
                {
                    model.UnitID = product.PriceUnitID;
                }
            }
            model.Tax = 0;
            if (invoice != null)
            {
                model.Tax = invoice.Tax;
            }
            model.TotalNoTax = model.UnitPrice * model.Quantity;
            model.TotalDiscount = model.TotalNoTax * model.Discount / 100;
            model.TotalTax = (model.TotalNoTax - model.TotalDiscount) * model.Tax / 100;
            model.Total = model.TotalNoTax - model.TotalDiscount + model.TotalTax;
            if (model.Total01 == null)
            {
                model.Total01 = model.Total;
            }
        }
        public IActionResult Index()
        {
            DateTime now = DateTime.Now;
            BaseViewModel viewModel = new BaseViewModel();
            viewModel.DatePublishBegin = new DateTime(now.Year, now.Month, 1);
            viewModel.DatePublishEnd = now;
            return View(viewModel);
        }
        public IActionResult DetailChamCong(int ID)
        {
            Invoice model = new Invoice();
            model.InvoiceCreated = DateTime.Now;
            model.DateBegin = DateTime.Now;
            model.DateEnd = DateTime.Now;
            model.NgayXuatHoaDon = DateTime.Now;
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
        public IActionResult DetailThiCongBarcode(int ID)
        {
            Invoice model = new Invoice();
            if (ID > 0)
            {
                model = _invoiceRepository.GetByID(ID);
            }
            model.ManageCode = "";
            model.TotalDiscount = 1;
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
            model.IsChaoGia = false;
            model.IsHopDong = false;
            model.IsThiCong = false;
            model.IsNghiemThu = false;
            model.IsThanhLy = false;
            model.IsHoanThanh = false;
            model.IsXuatHoaDon = false;
            model.IsNhanHoaDon = false;
            if (ID > 0)
            {
                model = _invoiceRepository.GetByID(ID);
            }
            model.CategoryID = AppGlobal.DuAnID;
            return View(model);
        }
        public IActionResult DetailPrint(int ID)
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
            model.DateBegin = DateTime.Now;
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
                if (model != null)
                {
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
                        if (seller != null)
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
                        _invoiceRepository.Update(model.ID, model);
                    }
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
                chaoGia = chaoGia.Replace(@"[InvoiceName]", model.InvoiceName);
                chaoGia = chaoGia.Replace(@"[BuyAddress]", model.BuyAddress);
                Membership buyer = _membershipRepository.GetByID(model.BuyID.Value);
                if (buyer != null)
                {
                    chaoGia = chaoGia.Replace(@"[BuyName]", buyer.FullName);
                    if (string.IsNullOrEmpty(model.BuyAddress))
                    {
                        chaoGia = chaoGia.Replace(@"[BuyAddress]", buyer.Address);
                    }
                }
                Membership seller = _membershipRepository.GetByID(AppGlobal.NghiaHaID);
                if (seller != null)
                {
                    chaoGia = chaoGia.Replace(@"[SellFullName]", seller.FullName);
                    chaoGia = chaoGia.Replace(@"[SellAddress]", seller.Address);
                    chaoGia = chaoGia.Replace(@"[SellEmail]", seller.Email);
                    chaoGia = chaoGia.Replace(@"[SellTaxCode]", seller.TaxCode);
                    chaoGia = chaoGia.Replace(@"[SellPhone]", seller.Phone);
                }
                chaoGia = chaoGia.Replace(@"[ManageCode]", model.ManageCode);
                chaoGia = chaoGia.Replace(@"[BuyPhone]", model.BuyPhone);
                List<InvoiceDetailDataTransfer> list = _invoiceDetailRepository.GetProjectChaoGiaByInvoiceIDAndCategoryIDToList(model.ID, AppGlobal.ChaoGiaID);
                if (list.Count > 0)
                {
                    int no = 0;
                    decimal totalDiscount = 0;
                    decimal total = 0;
                    decimal totalNoTax = 0;
                    decimal totalTax = 0;
                    decimal totalNoTaxAndDiscount = 0;
                    StringBuilder txt = new StringBuilder();
                    txt.AppendLine(@"<table class='border' style='width: 100%; font-size:24px; line-height:24px;'>");

                    txt.AppendLine(@"<thead>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer; color: blue;'>Hàng hóa</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer; color: blue;'>Thông số kỹ thuật</a></th>");                    
                    txt.AppendLine(@"<th style='text-align:center; width: 150px;'><a style='cursor:pointer; color: blue;'>Số lượng</a></th>");
                    txt.AppendLine(@"<th style='text-align:center; width: 150px;'><a style='cursor:pointer; color: blue;'>Đơn giá</a></th>");                    
                    txt.AppendLine(@"<th style='text-align:center; width: 150px;'><a style='cursor:pointer; color: blue;'>Tổng cộng</a></th>");
                    txt.AppendLine(@"</thead>");
                    txt.AppendLine(@"<tbody>");
                    foreach (InvoiceDetailDataTransfer item in list)
                    {
                        if (item.TotalDiscount != null)
                        {
                            totalDiscount = totalDiscount + item.TotalDiscount.Value;
                        }
                        if (item.TotalNoTax != null)
                        {
                            totalNoTax = totalNoTax + item.TotalNoTax.Value;
                        }
                        decimal totalNoTaxItem = 0;
                        decimal discountItem = 0;
                        decimal totalDiscountItem = 0;
                        decimal totalItem = 0;
                        if (item.TotalNoTax != null)
                        {
                            totalNoTaxItem = item.TotalNoTax.Value;
                        }
                        if (item.Discount != null)
                        {
                            discountItem = item.Discount.Value;
                        }
                        if (item.TotalDiscount != null)
                        {
                            totalDiscountItem = item.TotalDiscount.Value;
                        }
                        if (item.Total != null)
                        {
                            totalItem = item.Total.Value;
                        }
                        no = no + 1;
                        txt.AppendLine(@"<tr>");
                        txt.AppendLine(@"<td style='text-align:center;'>");
                        if (!string.IsNullOrEmpty(item.Image))
                        {
                            txt.AppendLine(@"<img src='" + item.ImageURLFull + "' width='150px' height='100px' />");
                        }
                        txt.AppendLine(@"<br/>");
                        txt.AppendLine(@"<div style='color: blue; font-weight:bold;'>" + item.ProductTitle + "</div>");
                        txt.AppendLine(@"<div style='color: blue; font-weight:bold;'>[" + item.MetaTitle + "]</div>");
                        txt.AppendLine(@"</td>");
                        txt.AppendLine(@"<td style='text-align:left;'>" + item.ContentMain + "</td>");                       
                        txt.AppendLine(@"<td style='text-align:right;'>" + item.Quantity.Value.ToString("N0").Replace(@",", @".") + " (" + item.UnitName + ")</td>");
                        txt.AppendLine(@"<td style='text-align:right;'>" + item.UnitPrice.Value.ToString("N0").Replace(@",", @".") + "</td>");                        
                        txt.AppendLine(@"<td style='text-align:right;'>");
                        txt.AppendLine(@"<b>" + item.TotalNoTax.Value.ToString("N0").Replace(@",", @".") + "</b>");
                        if (item.Discount.Value > 0)
                        {
                            txt.AppendLine(@"<br/>");
                            txt.AppendLine(@"<b>Giảm " + item.Discount.Value.ToString("N0").Replace(@",", @".") + "%</b>");
                        }
                        txt.AppendLine(@"</td>");
                        txt.AppendLine(@"</tr>");
                    }
                    totalNoTaxAndDiscount = totalNoTax - totalDiscount;
                    totalTax = totalNoTaxAndDiscount * model.Tax.Value / 100;
                    total = totalNoTaxAndDiscount + totalTax;
                    txt.AppendLine(@"<tr>");                 
                    txt.AppendLine(@"<td colspan='4'><div style='text-align:left; color: red; font-weight:bold; font-size:30px;'>Tổng cộng</div></td>");                   
                    txt.AppendLine(@"<td><div style='text-align:right; color: red; font-weight:bold; font-size:24px;'>" + totalNoTax.ToString("N0").Replace(@",", @".") + "</div></td>");
                    txt.AppendLine(@"</tr>");
                    txt.AppendLine(@"<tr>");                   
                    txt.AppendLine(@"<td colspan='4'><div style='text-align:left; color: red; font-weight:bold; font-size:24px;'>Giảm</div></td>");                  
                    txt.AppendLine(@"<td><div style='text-align:right; color: red; font-weight:bold; font-size:24px;'>" + totalDiscount.ToString("N0").Replace(@",", @".") + "</div></td>");
                    txt.AppendLine(@"</tr>");
                    txt.AppendLine(@"<tr>");                   
                    txt.AppendLine(@"<td colspan='4'><div style='text-align:left; color: red; font-weight:bold; font-size:24px;'>VAT (" + model.Tax.Value.ToString("N0").Replace(@",", @".") + " %)</div></td>");                  
                    txt.AppendLine(@"<td><div style='text-align:right; color: red; font-weight:bold; font-size:24px;'>" + totalTax.ToString("N0").Replace(@",", @".") + "</div></td>");
                    txt.AppendLine(@"</tr>");
                    txt.AppendLine(@"<tr>");                  
                    txt.AppendLine(@"<td colspan='4'><div style='text-align:left; color: red; font-weight:bold; font-size:24px;'>Thành tiền</div></td>");                  
                    txt.AppendLine(@"<td><div style='text-align:right; color: red; font-weight:bold; font-size:24px;'>" + total.ToString("N0").Replace(@",", @".") + "</div></td>");
                    txt.AppendLine(@"</tr>");
                    txt.AppendLine(@"</tbody>");
                    txt.AppendLine(@"</table>");
                    chaoGia = chaoGia.Replace(@"[ChaoGia]", txt.ToString());
                }
                model.ChaoGia = chaoGia;
                _invoiceRepository.Update(model.ID, model);
                //}
            }
            model.CategoryID = AppGlobal.DuAnID;
            return View(model);
        }
        public IActionResult PrintPreviewPhieuXuatKho(int ID, string dateTrackString)
        {
            DateTime dateTrack = DateTime.Now;
            if (!string.IsNullOrEmpty(dateTrackString))
            {
                try
                {
                    dateTrack = new DateTime(int.Parse(dateTrackString.Split('_')[0]), int.Parse(dateTrackString.Split('_')[1]), int.Parse(dateTrackString.Split('_')[2]));
                }
                catch
                {
                }
            }
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

                string chaoGia = "";
                var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, "html", "PhieuXuatKho.html");
                using (var stream = new FileStream(physicalPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        chaoGia = reader.ReadToEnd();
                    }
                }
                DateTime now = DateTime.Now;
                chaoGia = chaoGia.Replace(@"[DatePrint]", now.ToString("dd/MM/yyyy HH:mm:ss"));
                chaoGia = chaoGia.Replace(@"[ProjectName]", model.InvoiceName);
                chaoGia = chaoGia.Replace(@"[BuyName]", model.BuyName);
                chaoGia = chaoGia.Replace(@"[ManageCode]", model.ManageCode);
                chaoGia = chaoGia.Replace(@"[BuyPhone]", model.BuyPhone);
                chaoGia = chaoGia.Replace(@"[BuyAddress]", model.BuyAddress);
                chaoGia = chaoGia.Replace(@"[DateExport]", dateTrack.ToString("dd/MM/yyyy"));
                Membership seller = _membershipRepository.GetByID(AppGlobal.NghiaHaID);
                if (seller != null)
                {
                    chaoGia = chaoGia.Replace(@"[SellFullName]", seller.FullName);
                    chaoGia = chaoGia.Replace(@"[SellName]", seller.FullName);
                    chaoGia = chaoGia.Replace(@"[SellAddress]", seller.Address);
                    chaoGia = chaoGia.Replace(@"[SellEmail]", seller.Email);
                    chaoGia = chaoGia.Replace(@"[SellTaxCode]", seller.TaxCode);
                    chaoGia = chaoGia.Replace(@"[SellPhone]", seller.Phone);
                }
                List<InvoiceDetailDataTransfer> list = _invoiceDetailRepository.GetProjectThiCongByInvoiceIDAndCategoryIDAndDateTrackToList(model.ID, AppGlobal.ThiCongID, dateTrack);
                if (list.Count > 0)
                {
                    int no = 0;
                    StringBuilder txt = new StringBuilder();
                    txt.AppendLine(@"<table class='border' style='width: 100%; font-size:14px; line-height:20px;'>");
                    txt.AppendLine(@"<thead>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>No</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Hàng hóa</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Đơn vị tính</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Số lượng</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Mã sản xuất</a></th>");
                    txt.AppendLine(@"<th style='text-align:center; width:200px;'><a style='cursor:pointer;'>Mã vạch</a></th>");
                    txt.AppendLine(@"</thead>");
                    txt.AppendLine(@"<tbody>");
                    foreach (InvoiceDetailDataTransfer item in list)
                    {
                        no = no + 1;
                        txt.AppendLine(@"<tr>");
                        txt.AppendLine(@"<td style='text-align:center;'>" + no + "</td>");
                        txt.AppendLine(@"<td style='text-align:left;'>");
                        txt.AppendLine(@"<b>" + item.ProductTitle + "</b>");
                        txt.AppendLine(@"</td>");
                        txt.AppendLine(@"<td style='text-align:center;'>" + item.UnitName + "</td>");
                        txt.AppendLine(@"<td style='text-align:right;'>" + item.Quantity.Value.ToString("N0").Replace(@",", @".") + "</td>");
                        txt.AppendLine(@"<td style='text-align:right;'><b>" + item.ManufacturingCode + "</b></td>");
                        txt.AppendLine(@"<td style='text-align:right;'><b>" + item.MetaTitle + "</b></td>");
                        txt.AppendLine(@"</tr>");
                    }
                    txt.AppendLine(@"</tbody>");
                    txt.AppendLine(@"</table>");
                    chaoGia = chaoGia.Replace(@"[Detail]", txt.ToString());
                }
                model.Note = chaoGia;
            }
            model.CategoryID = AppGlobal.DuAnID;
            return View(model);
        }
        public IActionResult PrintPreviewBangThongKeVatTuThiCong(int ID)
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

                string chaoGia = "";
                var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, "html", "BangThongKeVatTuThiCong.html");
                using (var stream = new FileStream(physicalPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        chaoGia = reader.ReadToEnd();
                    }
                }
                DateTime now = DateTime.Now;
                chaoGia = chaoGia.Replace(@"[DatePrint]", now.ToString("dd/MM/yyyy HH:mm:ss"));
                chaoGia = chaoGia.Replace(@"[ProjectName]", model.InvoiceName);
                chaoGia = chaoGia.Replace(@"[BuyName]", model.BuyName);
                chaoGia = chaoGia.Replace(@"[ManageCode]", model.ManageCode);
                chaoGia = chaoGia.Replace(@"[BuyPhone]", model.BuyPhone);
                chaoGia = chaoGia.Replace(@"[BuyAddress]", model.BuyAddress);
                chaoGia = chaoGia.Replace(@"[DateExport]", now.ToString("dd/MM/yyyy"));
                Membership seller = _membershipRepository.GetByID(AppGlobal.NghiaHaID);
                if (seller != null)
                {
                    chaoGia = chaoGia.Replace(@"[SellFullName]", seller.FullName);
                    chaoGia = chaoGia.Replace(@"[SellName]", seller.FullName);
                    chaoGia = chaoGia.Replace(@"[SellAddress]", seller.Address);
                    chaoGia = chaoGia.Replace(@"[SellEmail]", seller.Email);
                    chaoGia = chaoGia.Replace(@"[SellTaxCode]", seller.TaxCode);
                    chaoGia = chaoGia.Replace(@"[SellPhone]", seller.Phone);
                }


                List<InvoiceDetailDataTransfer> list = _invoiceDetailRepository.GetProjectThiCongByInvoiceIDAndCategoryIDToList(model.ID, AppGlobal.ThiCongID).OrderByDescending(item => item.DateTrack).ToList();
                if (list.Count > 0)
                {
                    int no = 0;
                    StringBuilder txt = new StringBuilder();
                    txt.AppendLine(@"<table class='border' style='width: 100%; font-size:14px; line-height:20px;'>");
                    txt.AppendLine(@"<thead>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>No</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Hàng hóa</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Số lượng</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Đơn vị tính</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Mã sản xuất</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Mã vạch</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Ngày xuất</a></th>");
                    txt.AppendLine(@"</thead>");
                    txt.AppendLine(@"<tbody>");
                    foreach (InvoiceDetailDataTransfer item in list)
                    {
                        no = no + 1;
                        txt.AppendLine(@"<tr>");
                        txt.AppendLine(@"<td style='text-align:center;'>" + no + "</td>");
                        txt.AppendLine(@"<td style='text-align:left;'>");
                        txt.AppendLine(@"<b>" + item.ProductTitle + "</b>");
                        txt.AppendLine(@"</td>");
                        txt.AppendLine(@"<td style='text-align:right;'><b>" + item.Quantity.Value.ToString("N0").Replace(@",", @".") + "</b></td>");
                        txt.AppendLine(@"<td style='text-align:center;'>" + item.UnitName + "</td>");
                        txt.AppendLine(@"<td style='text-align:right;'>" + item.ManufacturingCode + "</td>");
                        txt.AppendLine(@"<td style='text-align:right;'>" + item.MetaTitle + "</td>");
                        txt.AppendLine(@"<td style='text-align:right;'>" + item.DateTrack.Value.ToString("dd/MM/yyyy") + "</td>");
                        txt.AppendLine(@"</tr>");
                    }
                    txt.AppendLine(@"</tbody>");
                    txt.AppendLine(@"</table>");
                    chaoGia = chaoGia.Replace(@"[Detail]", txt.ToString());
                }
                model.Note = chaoGia;
            }
            model.CategoryID = AppGlobal.DuAnID;
            return View(model);
        }

        public IActionResult PrintPreviewBangThongKeVatTuThiCongSUM(int ID)
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

                string chaoGia = "";
                var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, "html", "BangThongKeVatTuThiCong.html");
                using (var stream = new FileStream(physicalPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        chaoGia = reader.ReadToEnd();
                    }
                }
                DateTime now = DateTime.Now;
                chaoGia = chaoGia.Replace(@"[DatePrint]", now.ToString("dd/MM/yyyy HH:mm:ss"));
                chaoGia = chaoGia.Replace(@"[ProjectName]", model.InvoiceName);
                chaoGia = chaoGia.Replace(@"[BuyName]", model.BuyName);
                chaoGia = chaoGia.Replace(@"[ManageCode]", model.ManageCode);
                chaoGia = chaoGia.Replace(@"[BuyPhone]", model.BuyPhone);
                chaoGia = chaoGia.Replace(@"[BuyAddress]", model.BuyAddress);
                chaoGia = chaoGia.Replace(@"[DateExport]", now.ToString("dd/MM/yyyy"));
                Membership seller = _membershipRepository.GetByID(AppGlobal.NghiaHaID);
                if (seller != null)
                {
                    chaoGia = chaoGia.Replace(@"[SellFullName]", seller.FullName);
                    chaoGia = chaoGia.Replace(@"[SellName]", seller.FullName);
                    chaoGia = chaoGia.Replace(@"[SellAddress]", seller.Address);
                    chaoGia = chaoGia.Replace(@"[SellEmail]", seller.Email);
                    chaoGia = chaoGia.Replace(@"[SellTaxCode]", seller.TaxCode);
                    chaoGia = chaoGia.Replace(@"[SellPhone]", seller.Phone);
                }


                List<InvoiceDetailDataTransfer> list = _invoiceDetailRepository.GetProjectThiCongByInvoiceIDAndCategoryIDSUMToList(model.ID, AppGlobal.ThiCongID);
                if (list.Count > 0)
                {
                    int no = 0;
                    StringBuilder txt = new StringBuilder();
                    txt.AppendLine(@"<table class='border' style='width: 100%; font-size:14px; line-height:20px;'>");
                    txt.AppendLine(@"<thead>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>No</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Hàng hóa</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Số lượng</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Đơn vị tính</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Mã sản xuất</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Mã vạch</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Ngày xuất</a></th>");
                    txt.AppendLine(@"</thead>");
                    txt.AppendLine(@"<tbody>");
                    foreach (InvoiceDetailDataTransfer item in list)
                    {
                        no = no + 1;
                        if (item.DateTrack.Value.Year > 2019)
                        {
                            txt.AppendLine(@"<tr>");
                        }
                        else
                        {
                            txt.AppendLine(@"<tr style='background-color:#f1f1f1;'>");

                        }
                        txt.AppendLine(@"<td style='text-align:center;'>" + no + "</td>");
                        txt.AppendLine(@"<td style='text-align:left;'>");
                        txt.AppendLine(@"<b>" + item.ProductTitle + "</b>");
                        txt.AppendLine(@"</td>");
                        txt.AppendLine(@"<td style='text-align:right;'><b>" + item.Quantity.Value.ToString("N0").Replace(@",", @".") + "</b></td>");
                        txt.AppendLine(@"<td style='text-align:center;'>" + item.UnitName + "</td>");
                        txt.AppendLine(@"<td style='text-align:right;'>" + item.ManufacturingCode + "</td>");
                        txt.AppendLine(@"<td style='text-align:right;'>" + item.ProductCode + "</td>");
                        if (item.DateTrack.Value.Year > 2019)
                        {
                            txt.AppendLine(@"<td style='text-align:right;'>" + item.DateTrack.Value.ToString("dd/MM/yyyy") + "</td>");
                        }
                        else
                        {
                            txt.AppendLine(@"<td style='text-align:right;'></td>");
                        }
                        txt.AppendLine(@"</tr>");
                    }
                    txt.AppendLine(@"</tbody>");
                    txt.AppendLine(@"</table>");
                    chaoGia = chaoGia.Replace(@"[Detail]", txt.ToString());
                }
                model.Note = chaoGia;
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
                //if (string.IsNullOrEmpty(model.NghiemThu))
                //{
                string nghiemThu = "";
                var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, "html", "NghiemThu.html");
                using (var stream = new FileStream(physicalPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        nghiemThu = reader.ReadToEnd();
                    }
                }
                DateTime now = DateTime.Now;
                nghiemThu = nghiemThu.Replace(@"[InvoiceCode]", model.InvoiceCode);
                nghiemThu = nghiemThu.Replace(@"[InvoiceDay]", model.InvoiceCreated.Value.Day.ToString());
                nghiemThu = nghiemThu.Replace(@"[InvoiceMonth]", model.InvoiceCreated.Value.Month.ToString());
                nghiemThu = nghiemThu.Replace(@"[InvoiceYear]", model.InvoiceCreated.Value.Year.ToString());
                nghiemThu = nghiemThu.Replace(@"[Day]", now.Day.ToString());
                nghiemThu = nghiemThu.Replace(@"[Month]", now.Month.ToString());
                nghiemThu = nghiemThu.Replace(@"[Year]", now.Year.ToString());
                Membership buyer = _membershipRepository.GetByID(model.BuyID.Value);
                if (buyer != null)
                {
                    nghiemThu = nghiemThu.Replace(@"[BuyName]", buyer.FullName);
                    nghiemThu = nghiemThu.Replace(@"[BuyAddress]", buyer.Address);
                    nghiemThu = nghiemThu.Replace(@"[BuyFullName]", buyer.ContactFullName);
                    nghiemThu = nghiemThu.Replace(@"[BuyPosition]", buyer.ContactPosition);
                    nghiemThu = nghiemThu.Replace(@"[BuyBankAccount]", buyer.BankAccount);
                    nghiemThu = nghiemThu.Replace(@"[BuyBankName]", buyer.BankName);
                    nghiemThu = nghiemThu.Replace(@"[BuyTaxCode]", buyer.TaxCode);
                    nghiemThu = nghiemThu.Replace(@"[BuyPhone]", buyer.Phone);
                }
                Membership seller = _membershipRepository.GetByID(model.SellID.Value);
                if (seller != null)
                {
                    nghiemThu = nghiemThu.Replace(@"[SellName]", seller.FullName);
                    nghiemThu = nghiemThu.Replace(@"[SellAddress]", seller.Address);
                    nghiemThu = nghiemThu.Replace(@"[SellFullName]", seller.ContactFullName);
                    nghiemThu = nghiemThu.Replace(@"[SellPosition]", seller.ContactPosition);
                    nghiemThu = nghiemThu.Replace(@"[SellBankAccount]", seller.BankAccount);
                    nghiemThu = nghiemThu.Replace(@"[SellBankName]", seller.BankName);
                    nghiemThu = nghiemThu.Replace(@"[SellTaxCode]", seller.TaxCode);
                    nghiemThu = nghiemThu.Replace(@"[SellPhone]", seller.Phone);
                }

                List<InvoiceDetailDataTransfer> list = _invoiceDetailRepository.GetProjectThiCongByInvoiceIDAndCategoryIDSUMProductToList(model.ID, AppGlobal.ThiCongID);
                if (list.Count > 0)
                {
                    int no = 0;
                    decimal totalDiscount = 0;
                    decimal total = 0;
                    decimal totalNoTax = 0;
                    StringBuilder txt = new StringBuilder();
                    txt.AppendLine(@"<table class='border' style='width: 100%; font-size:14px; line-height:20px;'>");
                    txt.AppendLine(@"<thead>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>No</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Hàng hóa</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Số lượng</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Đơn giá</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Thành tiền</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Đơn vị tính</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Mã sản suất</a></th>");
                    txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Mã vạch</a></th>");
                    txt.AppendLine(@"</thead>");
                    txt.AppendLine(@"<tbody>");
                    foreach (InvoiceDetailDataTransfer item in list)
                    {                        
                        no = no + 1;
                        txt.AppendLine(@"<tr>");
                        txt.AppendLine(@"<td style='text-align:center;'>" + no + "</td>");
                        txt.AppendLine(@"<td style='text-align:left;'>");
                        txt.AppendLine(@"<b>" + item.ProductTitle + "</b>");
                        txt.AppendLine(@"</td>");                        
                        txt.AppendLine(@"<td style='text-align:right;'>" + item.Quantity.Value.ToString("N0").Replace(@",", @".") + "</td>");
                        txt.AppendLine(@"<td style='text-align:right;'>" + item.UnitPrice.Value.ToString("N0").Replace(@",", @".") + "</td>");
                        txt.AppendLine(@"<td style='text-align:right;'><b>" + item.Total.Value.ToString("N0").Replace(@",", @".") + "</b></td>");
                        txt.AppendLine(@"<td style='text-align:center;'>" + item.UnitName + "</td>");
                        txt.AppendLine(@"<td style='text-align:right;'>" + item.ManufacturingCode + "</td>");
                        txt.AppendLine(@"<td style='text-align:right;'>" + item.ProductCode + "</td>");
                        txt.AppendLine(@"</tr>");
                    }
                    txt.AppendLine(@"<tr>");
                    txt.AppendLine(@"<td style='text-align:center;'></td>");
                    txt.AppendLine(@"<td style='text-align:left;'><b>Tổng cộng (đồng)</b></td>");
                    txt.AppendLine(@"<td style='text-align:center;'></td>");
                    txt.AppendLine(@"<td style='text-align:right;'></td>");                    
                    txt.AppendLine(@"<td style='text-align:right;'><b>" + model.TotalNoTax.Value.ToString("N0").Replace(@",", @".") + "</b></td>");
                    txt.AppendLine(@"<td style='text-align:right;'></td>");
                    txt.AppendLine(@"<td style='text-align:right;'></td>");
                    txt.AppendLine(@"<td style='text-align:right;'></td>");
                    txt.AppendLine(@"</tr>");
                    txt.AppendLine(@"<tr>");
                    txt.AppendLine(@"<td style='text-align:center;'></td>");
                    txt.AppendLine(@"<td style='text-align:left;'><b>Thuế VAT (đồng) (" + model.Tax.Value.ToString("N0") + "%)</b></td>");
                    txt.AppendLine(@"<td style='text-align:center;'></td>");
                    txt.AppendLine(@"<td style='text-align:right;'></td>");
                    txt.AppendLine(@"<td style='text-align:right;'><b>" + model.TotalTax.Value.ToString("N0").Replace(@",", @".") + "</b></td>");
                    txt.AppendLine(@"<td style='text-align:right;'></td>");
                    txt.AppendLine(@"<td style='text-align:right;'></td>");
                    txt.AppendLine(@"<td style='text-align:right;'></td>");
                    txt.AppendLine(@"</tr>");
                    txt.AppendLine(@"<tr>");
                    txt.AppendLine(@"<td style='text-align:center;'></td>");
                    txt.AppendLine(@"<td style='text-align:left;'><b>Thành tiền (đồng)</b></td>");
                    txt.AppendLine(@"<td style='text-align:center;'></td>");
                    txt.AppendLine(@"<td style='text-align:right;'></td>");
                    txt.AppendLine(@"<td style='text-align:right;'><div style='font-weight: bold; color: blue;'>" + model.Total.Value.ToString("N0").Replace(@",", @".") + "</div></td>");
                    txt.AppendLine(@"<td style='text-align:right;'></td>");
                    txt.AppendLine(@"<td style='text-align:right;'></td>");
                    txt.AppendLine(@"<td style='text-align:right;'></td>");
                    txt.AppendLine(@"</tr>");
                    txt.AppendLine(@"<tr>");
                    txt.AppendLine(@"<td style='text-align:center;'></td>");
                    txt.AppendLine(@"<td style='text-align:left;'><b>Thanh toán (đồng)</b></td>");
                    txt.AppendLine(@"<td style='text-align:center;'></td>");
                    txt.AppendLine(@"<td style='text-align:right;'></td>");
                    txt.AppendLine(@"<td style='text-align:right;'><div style='font-weight: bold; color: green;'>" + model.TotalPaid.Value.ToString("N0").Replace(@",", @".") + "</div></td>");
                    txt.AppendLine(@"<td style='text-align:right;'></td>");
                    txt.AppendLine(@"<td style='text-align:right;'></td>");
                    txt.AppendLine(@"<td style='text-align:right;'></td>");
                    txt.AppendLine(@"</tr>");
                    txt.AppendLine(@"<tr>");
                    txt.AppendLine(@"<td style='text-align:center;'></td>");
                    txt.AppendLine(@"<td style='text-align:left;'><b>Dư nợ (đồng)</b></td>");
                    txt.AppendLine(@"<td style='text-align:center;'></td>");
                    txt.AppendLine(@"<td style='text-align:right;'></td>");
                    txt.AppendLine(@"<td style='text-align:right;'><div style='font-weight: bold; color: red;'>" + model.TotalDebt.Value.ToString("N0").Replace(@",", @".") + "</div></td>");
                    txt.AppendLine(@"<td style='text-align:right;'></td>");
                    txt.AppendLine(@"<td style='text-align:right;'></td>");
                    txt.AppendLine(@"<td style='text-align:right;'></td>");
                    txt.AppendLine(@"</tr>");
                    txt.AppendLine(@"</tbody>");
                    txt.AppendLine(@"</table>");
                    nghiemThu = nghiemThu.Replace(@"[ThiCong]", txt.ToString());
                }
                model.NghiemThu = nghiemThu;
                _invoiceRepository.Update(model.ID, model);
            }
            //}
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
                if (string.IsNullOrEmpty(model.ThanhLy))
                {
                    string thanhLy = "";
                    var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, "html", "ThanhLy.html");
                    using (var stream = new FileStream(physicalPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            thanhLy = reader.ReadToEnd();
                        }
                    }
                    DateTime now = DateTime.Now;
                    thanhLy = thanhLy.Replace(@"[InvoiceTotal]", model.Total.Value.ToString("N0").Replace(@",", @"."));
                    thanhLy = thanhLy.Replace(@"[InvoiceTotalString]", AppGlobal.ConvertDecimalToString(model.Total.Value));
                    thanhLy = thanhLy.Replace(@"[InvoiceDay]", model.InvoiceCreated.Value.Day.ToString());
                    thanhLy = thanhLy.Replace(@"[InvoiceMonth]", model.InvoiceCreated.Value.Month.ToString());
                    thanhLy = thanhLy.Replace(@"[InvoiceYear]", model.InvoiceCreated.Value.Year.ToString());
                    thanhLy = thanhLy.Replace(@"[Day]", now.Day.ToString());
                    thanhLy = thanhLy.Replace(@"[Month]", now.Month.ToString());
                    thanhLy = thanhLy.Replace(@"[Year]", now.Year.ToString());
                    thanhLy = thanhLy.Replace(@"[InvoiceName]", model.InvoiceName);
                    thanhLy = thanhLy.Replace(@"[HopDongTitle]", model.HopDongTitle);
                    thanhLy = thanhLy.Replace(@"[HopDongTitleSub]", model.HopDongTitleSub);
                    thanhLy = thanhLy.Replace(@"[InvoiceCode]", model.InvoiceCode);
                    thanhLy = thanhLy.Replace(@"[HangMuc]", model.HangMuc);
                    Membership buyer = _membershipRepository.GetByID(model.BuyID.Value);
                    if (buyer != null)
                    {
                        thanhLy = thanhLy.Replace(@"[BuyName]", buyer.FullName);
                        thanhLy = thanhLy.Replace(@"[BuyAddress]", buyer.Address);
                        thanhLy = thanhLy.Replace(@"[BuyFullName]", buyer.ContactFullName);
                        thanhLy = thanhLy.Replace(@"[BuyPosition]", buyer.ContactPosition);
                        thanhLy = thanhLy.Replace(@"[BuyBankAccount]", buyer.BankAccount);
                        thanhLy = thanhLy.Replace(@"[BuyBankName]", buyer.BankName);
                        thanhLy = thanhLy.Replace(@"[BuyTaxCode]", buyer.TaxCode);
                        thanhLy = thanhLy.Replace(@"[BuyPhone]", buyer.Phone);
                    }
                    Membership seller = _membershipRepository.GetByID(model.SellID.Value);
                    if (buyer != null)
                    {
                        thanhLy = thanhLy.Replace(@"[SellName]", seller.FullName);
                        thanhLy = thanhLy.Replace(@"[SellAddress]", seller.Address);
                        thanhLy = thanhLy.Replace(@"[SellFullName]", seller.ContactFullName);
                        thanhLy = thanhLy.Replace(@"[SellPosition]", seller.ContactPosition);
                        thanhLy = thanhLy.Replace(@"[SellBankAccount]", seller.BankAccount);
                        thanhLy = thanhLy.Replace(@"[SellBankName]", seller.BankName);
                        thanhLy = thanhLy.Replace(@"[SellTaxCode]", seller.TaxCode);
                        thanhLy = thanhLy.Replace(@"[SellPhone]", seller.Phone);
                    }
                    model.ThanhLy = thanhLy;
                    _invoiceRepository.Update(model.ID, model);
                }
            }
            model.CategoryID = AppGlobal.DuAnID;
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
                    model.ID = ID;
                    model.Title = invoice.InvoiceName;
                    model.InvoiceID = ID;
                }
            }
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
        public IActionResult GetByDuAnIDAndDatePublishBeginAndDatePublishEndAndIsChaoGiaAndIsThiCongAndIsHoanThanhAndIsXuatHoaDonAndMembershipIDToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, bool isChaoGia, bool isThiCong, bool isHoanThanh, bool isXuatHoaDon, int membershipID)
        {
            return Json(_invoiceRepository.GetByCategoryIDAndDatePublishBeginAndDatePublishEndAndIsChaoGiaAndIsThiCongAndIsHoanThanhAndIsXuatHoaDonAndMembershipIDToList(AppGlobal.DuAnID, datePublishBegin, datePublishEnd, isChaoGia, isThiCong, isHoanThanh, isXuatHoaDon, membershipID).ToDataSourceResult(request));
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
        public ActionResult GetProjectThiCongByInvoiceIDAndCategoryIDAndDateTrackToList([DataSourceRequest] DataSourceRequest request, int invoiceID)
        {
            var data = _invoiceDetailRepository.GetProjectThiCongByInvoiceIDAndCategoryIDAndDateTrackToList(invoiceID, AppGlobal.ThiCongID, DateTime.Now);
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
            if (membership != null)
            {
                model.BuyName = membership.FullName;
                if (string.IsNullOrEmpty(model.BuyPhone))
                {
                    model.BuyPhone = membership.Phone;
                }
                if (string.IsNullOrEmpty(model.BuyAddress))
                {
                    model.BuyAddress = membership.Address;
                }
                if (string.IsNullOrEmpty(model.ManageCode))
                {
                    model.ManageCode = membership.ContactFullName;
                }
            }
            if (model.ID > 0)
            {
                Initialization(model);
                model.Initialization(InitType.Update, RequestUserID);
                _invoiceRepository.Update(model.ID, model);
                _invoiceRepository.InitializationByIDAndCategoryID(model.ID, model.CategoryID.Value);
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
                if (string.IsNullOrEmpty(invoice.InvoiceCode))
                {
                    invoice.InvoiceCode = "" + invoice.ID + "-" + DateTime.Now.Year + "/HĐKT";
                }
                invoice.HangMuc = model.HangMuc;
                invoice.HopDongTitle = model.HopDongTitle;
                invoice.HopDongTitleSub = model.HopDongTitleSub;
                if (string.IsNullOrEmpty(invoice.HopDongTitleSub))
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
                string mes = e.Message;
            }
            return RedirectToAction("DetailFiles", "Project", new { ID = model.InvoiceID });
        }
    }
}