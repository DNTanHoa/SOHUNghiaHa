﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NghiaHa.API.ResponseModel;
using SOHU.Data.DataTransferObject;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.ModelExtensions;
using SOHU.Data.Models;
using SOHU.Data.Repositories;
using SOHU.Data.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NghiaHa.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProjectController : BaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IInvoicePropertyRepository _invoicePropertyRepository;
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;
        private readonly IProductRepository _productRepository;
        public ProjectController(IWebHostEnvironment webHostEnvironment, IProductRepository productRepository, IInvoiceRepository invoiceRepository, IInvoiceDetailRepository invoiceDetailRepository, IInvoicePropertyRepository invoicePropertyRepository, IMembershipRepository membershipRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _invoiceRepository = invoiceRepository;
            _invoiceDetailRepository = invoiceDetailRepository;
            _invoicePropertyRepository = invoicePropertyRepository;
            _membershipRepository = membershipRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> Index()
        {
            BaseViewModel viewModel = new BaseViewModel();
            viewModel.YearFinance = DateTime.Now.Year;
            viewModel.MonthFinance = DateTime.Now.Month;

            return new BaseResponeModel(viewModel);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> DetailChamCong(int ID)
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
            return new BaseResponeModel(model);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> DetailDuToan(int ID)
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

            return new BaseResponeModel(model);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> DetailNhanSu(int ID)
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
            
            return new BaseResponeModel(model);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> Detail(int ID)
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

            return new BaseResponeModel(model);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> DetailHopDong(int ID)
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
                    var physicalPath = Path.Combine(_webHostEnvironment.WebRootPath, "html", "HopDong.html");
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

            return new BaseResponeModel(model);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> DetailChaoGia(int ID)
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

                if (string.IsNullOrEmpty(model.ChaoGia))
                {
                    string chaoGia = "";
                    var physicalPath = Path.Combine(_webHostEnvironment.WebRootPath, "html", "ChaoGia.html");
                    
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
                        int no = 0;
                        decimal totalDiscount = 0;
                        decimal total = 0;
                        decimal totalNoTax = 0;
                        StringBuilder txt = new StringBuilder();
                        txt.AppendLine(@"<table class='border' style='width: 100%; font-size:14px; line-height:20px;'>");

                        txt.AppendLine(@"<thead>");
                        txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>No</a></th>");
                        txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Hàng hóa</a></th>");
                        txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Thông số kỹ thuật</a></th>");
                        txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Đơn vị tính</a></th>");
                        txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Số lượng</a></th>");
                        txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Đơn giá</a></th>");
                        txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Tổng cộng</a></th>");
                        txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Chiết khấu (%)</a></th>");
                        txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Chiết khấu</a></th>");
                        txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Thành tiền</a></th>");
                        txt.AppendLine(@"</thead>");
                        txt.AppendLine(@"<tbody>");
                        
                        foreach (InvoiceDetailDataTransfer item in list)
                        {
                            totalDiscount = totalDiscount + item.TotalDiscount.Value;
                            total = total + item.Total.Value;
                            totalNoTax = totalNoTax + item.TotalNoTax.Value;
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
                            txt.AppendLine(@"<td style='text-align:right;'>" + item.Quantity.Value.ToString("N0").Replace(@",", @".") + "</td>");
                            txt.AppendLine(@"<td style='text-align:right;'>" + item.UnitPrice.Value.ToString("N0").Replace(@",", @".") + "</td>");
                            txt.AppendLine(@"<td style='text-align:right;'><b>" + item.TotalNoTax.Value.ToString("N0").Replace(@",", @".") + "</b></td>");
                            txt.AppendLine(@"<td style='text-align:right;'><b>" + item.Discount.Value.ToString("N0").Replace(@",", @".") + "</b></td>");
                            txt.AppendLine(@"<td style='text-align:right;'><b>" + item.TotalDiscount.Value.ToString("N0").Replace(@",", @".") + "</b></td>");
                            txt.AppendLine(@"<td style='text-align:right;'><b>" + item.Total.Value.ToString("N0").Replace(@",", @".") + "</b></td>");
                            txt.AppendLine(@"</tr>");
                        }

                        txt.AppendLine(@"<tr>");
                        txt.AppendLine(@"<td style='text-align:center;'></td>");
                        txt.AppendLine(@"<td style='text-align:center;'>");
                        txt.AppendLine(@"</td>");
                        txt.AppendLine(@"<td style='text-align:left;'>Tổng cộng</td>");
                        txt.AppendLine(@"<td style='text-align:center;'></td>");
                        txt.AppendLine(@"<td style='text-align:right;'></td>");
                        txt.AppendLine(@"<td style='text-align:right;'></td>");
                        txt.AppendLine(@"<td style='text-align:right;'><b>" + totalNoTax.ToString("N0").Replace(@",", @".") + "</b></td>");
                        txt.AppendLine(@"<td style='text-align:right;'></td>");
                        txt.AppendLine(@"<td style='text-align:right;'><b>" + totalDiscount.ToString("N0").Replace(@",", @".") + "</b></td>");
                        txt.AppendLine(@"<td style='text-align:right;'><b>" + total.ToString("N0").Replace(@",", @".") + "</b></td>");
                        txt.AppendLine(@"</tr>");
                        txt.AppendLine(@"</tbody>");
                        txt.AppendLine(@"</table>");

                        chaoGia = chaoGia.Replace(@"[ChaoGia]", txt.ToString());
                    }

                    model.ChaoGia = chaoGia;
                }
            }

            model.CategoryID = AppGlobal.DuAnID;
            return new BaseResponeModel(model);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> DetailNghiemThu(int ID)
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

                if (string.IsNullOrEmpty(model.NghiemThu))
                {
                    string nghiemThu = "";
                    var physicalPath = Path.Combine(_webHostEnvironment.WebRootPath, "html", "NghiemThu.html");
                    
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
                    
                    if (buyer != null)
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

                    List<InvoiceDetailDataTransfer> list = _invoiceDetailRepository.GetProjectThiCongByInvoiceIDAndCategoryIDToList(model.ID, AppGlobal.ThiCongID);
                    
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
                        txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Đơn vị tính</a></th>");
                        txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Số lượng</a></th>");
                        txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Đơn giá</a></th>");
                        txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Thành tiền</a></th>");
                        txt.AppendLine(@"</thead>");
                        txt.AppendLine(@"<tbody>");
                        foreach (InvoiceDetailDataTransfer item in list)
                        {
                            totalDiscount = totalDiscount + item.TotalDiscount.Value;
                            total = total + item.Total.Value;
                            totalNoTax = totalNoTax + item.TotalNoTax.Value;
                            no = no + 1;
                            txt.AppendLine(@"<tr>");
                            txt.AppendLine(@"<td style='text-align:center;'>" + no + "</td>");
                            txt.AppendLine(@"<td style='text-align:center;'>");
                            txt.AppendLine(@"<b>" + item.ProductTitle + "</b>");
                            txt.AppendLine(@"</td>");
                            txt.AppendLine(@"<td style='text-align:center;'>" + item.UnitName + "</td>");
                            txt.AppendLine(@"<td style='text-align:right;'>" + item.Quantity.Value.ToString("N0").Replace(@",", @".") + "</td>");
                            txt.AppendLine(@"<td style='text-align:right;'>" + item.UnitPrice.Value.ToString("N0").Replace(@",", @".") + "</td>");
                            txt.AppendLine(@"<td style='text-align:right;'><b>" + item.Total.Value.ToString("N0").Replace(@",", @".") + "</b></td>");
                            txt.AppendLine(@"</tr>");
                        }
                        txt.AppendLine(@"<tr>");
                        txt.AppendLine(@"<td style='text-align:center;'></td>");
                        txt.AppendLine(@"<td style='text-align:left;'>Tổng cộng</td>");
                        txt.AppendLine(@"<td style='text-align:center;'></td>");
                        txt.AppendLine(@"<td style='text-align:right;'></td>");
                        txt.AppendLine(@"<td style='text-align:right;'></td>");
                        txt.AppendLine(@"<td style='text-align:right;'><b>" + total.ToString("N0").Replace(@",", @".") + "</b></td>");
                        txt.AppendLine(@"</tr>");
                        txt.AppendLine(@"</tbody>");
                        txt.AppendLine(@"</table>");
                        nghiemThu = nghiemThu.Replace(@"[ThiCong]", txt.ToString());
                    }                    
                    model.NghiemThu = nghiemThu;
                }
            }

            model.CategoryID = AppGlobal.DuAnID;

            return new BaseResponeModel(model);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> DetailThanhLy(int ID)
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
                    var physicalPath = Path.Combine(_webHostEnvironment.WebRootPath, "html", "ThanhLy.html");
                    
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
                }
            }

            model.CategoryID = AppGlobal.DuAnID;

            return new BaseResponeModel(model);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> DetailFiles(int ID)
        {
            InvoiceProperty model = new InvoiceProperty();
            model.InvoiceID = ID;

            return new BaseResponeModel(model);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> PrintPreviewHopDong(int ID)
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

            return new BaseResponeModel(model);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> PrintPreviewThanhLy(int ID)
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

            return new BaseResponeModel(model);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> PrintPreviewNghiemThu(int ID)
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

            return new BaseResponeModel(model);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> PrintPreviewChaoGia(int ID)
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

            return new BaseResponeModel(model);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetProjectDuToanFullNameByInvoiceIDAndDuToanToList(int invoiceID)
        {
            var data = _invoiceDetailRepository.GetProjectDuToanFullNameByInvoiceIDAndCategoryIDToList(invoiceID, AppGlobal.DuToanID);
            return new BaseResponeModel(data);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetInvoicePropertyByInvoiceIDToList(int invoiceID)
        {
            var data = _invoicePropertyRepository.GetByInvoiceIDToList(invoiceID);
            return new BaseResponeModel(data);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetProjectNhanSuByInvoiceIDAndNhanSuToList(int invoiceID)
        {
            var data = _invoiceDetailRepository.GetProjectNhanSuByInvoiceIDAndParentIDToList(invoiceID, AppGlobal.NhanSuID);
            return new BaseResponeModel(data);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetProjectChamCongByInvoiceIDAndChamCongToList(int invoiceID)
        {
            var data = _invoiceDetailRepository.GetProjectNhanSuByInvoiceIDAndParentIDToList(invoiceID, AppGlobal.ChamCongID);
            return new BaseResponeModel(data);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetProjectDuToanByInvoiceIDAndThiCongToList(int invoiceID)
        {
            var data = _invoiceDetailRepository.GetProjectDuToanByInvoiceIDAndParentIDToList(invoiceID, AppGlobal.ThiCongID);
            return new BaseResponeModel(data);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetProjectNhanSuByInvoiceIDAndChamCongToList(int invoiceID)
        {
            var data = _invoiceDetailRepository.GetProjectNhanSuByInvoiceIDAndCategoryIDToList(invoiceID, AppGlobal.ChamCongID);
            return new BaseResponeModel(data);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetProjectDuToanByInvoiceIDAndDuToanToList(int invoiceID)
        {
            var data = _invoiceDetailRepository.GetProjectDuToanByInvoiceIDAndCategoryIDToList(invoiceID, AppGlobal.DuToanID);
            return new BaseResponeModel(data);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetProjectDuToanByInvoiceIDAndChaoGiaToList(int invoiceID)
        {
            var data = _invoiceDetailRepository.GetProjectDuToanByInvoiceIDAndCategoryIDToList(invoiceID, AppGlobal.ChaoGiaID);
            return new BaseResponeModel(data);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetProjectThiCongByInvoiceIDAndThiCongToList(int invoiceID)
        {
            var data = _invoiceDetailRepository.GetProjectThiCongByInvoiceIDAndCategoryIDToList(invoiceID, AppGlobal.ThiCongID);
            return new BaseResponeModel(data);
        }

        [HttpDelete]
        public ActionResult<BaseResponeModel> DeleteInvoiceProperty(int ID)
        {
            int result = _invoicePropertyRepository.Delete(ID);
            if (result > 0)
            {
                RouteResult = new SuccessResult(AppGlobal.DeleteSuccess);
            }
            else
            {
                RouteResult = new ErrorResult(ErrorType.DeleteError, AppGlobal.DeleteFail);
            }

            return new BaseResponeModel(null, RouteResult);
        }

        [HttpDelete]
        public ActionResult<BaseResponeModel> DeleteProjectThiCong(int ID)
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

            int result = _invoiceDetailRepository.Delete(ID);

            if (result > 0)
            {
                RouteResult = new SuccessResult(AppGlobal.DeleteSuccess);
                _invoiceRepository.InitializationByIDAndCategoryID(invoiceID, categoryID);
                _productRepository.InitializationByIDAndCategoryID(productID, categoryID);
            }
            else
            {
                RouteResult = new ErrorResult(ErrorType.DeleteError, AppGlobal.DeleteFail);
            }

            return new BaseResponeModel(null, RouteResult);
        }

        [HttpDelete]
        public ActionResult<BaseResponeModel> DeleteDetail(int ID)
        {
            int result = _invoiceDetailRepository.Delete(ID);

            if (result > 0)
            {
                RouteResult = new SuccessResult(AppGlobal.DeleteSuccess);
            }
            else
            {
                RouteResult = new ErrorResult(ErrorType.DeleteError, AppGlobal.DeleteFail);
            }

            return new BaseResponeModel(null, RouteResult);
        }


        [HttpPost]
        public ActionResult<BaseResponeModel> SaveProject(Invoice model)
        {
            int result;

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
                model.TrimModel();
                model.Initialization(InitType.Update, RequestUserID);

                result = _invoiceRepository.Update(model.ID, model);

                if (result > 0)
                {
                    RouteResult = new SuccessResult(AppGlobal.EditSuccess);
                }
                else
                {
                    RouteResult = new ErrorResult(ErrorType.EditError, AppGlobal.EditFail);
                }
            }
            else
            {
                model.TrimModel();
                model.Initialization(InitType.Insert, RequestUserID);

                result = _invoiceRepository.Create(model);

                if (result > 0)
                {
                    RouteResult = new SuccessResult(AppGlobal.CreateSuccess);
                }
                else
                {
                    RouteResult = new ErrorResult(ErrorType.InsertError, AppGlobal.CreateFail);
                }
            }

            return new BaseResponeModel(new { ID = model.ID }, RouteResult);
        }


        [HttpPost]
        public ActionResult<BaseResponeModel> SaveHopDong(Invoice model)
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

                model.TrimModel();
                invoice.Initialization(InitType.Update, RequestUserID);

                int result = _invoiceRepository.Update(invoice.ID, invoice);

                if (result > 0)
                {
                    RouteResult = new SuccessResult(AppGlobal.EditSuccess);
                }
                else
                {
                    RouteResult = new ErrorResult(ErrorType.EditError, AppGlobal.EditFail);
                }
            }

            return new BaseResponeModel(new { ID = model.ID }, RouteResult);
        }


        [HttpPost]
        public ActionResult<BaseResponeModel> SaveChaoGia(Invoice model)
        {
            if (model.ID > 0)
            {
                Invoice invoice = _invoiceRepository.GetByID(model.ID);
                invoice.ChaoGia = model.ChaoGia;
                invoice.TrimModel();
                invoice.Initialization(InitType.Update, RequestUserID);

                int result = _invoiceRepository.Update(invoice.ID, invoice);

                if (result > 0)
                {
                    RouteResult = new SuccessResult(AppGlobal.EditSuccess);
                }
                else
                {
                    RouteResult = new ErrorResult(ErrorType.EditError, AppGlobal.EditFail);
                }
            }

            return new BaseResponeModel(new { ID = model.ID }, RouteResult);
        }


        [HttpPost]
        public ActionResult<BaseResponeModel> SaveNghiemThu(Invoice model)
        {
            if (model.ID > 0)
            {
                Invoice invoice = _invoiceRepository.GetByID(model.ID);
                invoice.NghiemThu = model.NghiemThu;
                invoice.TrimModel();
                invoice.Initialization(InitType.Update, RequestUserID);

                int result = _invoiceRepository.Update(invoice.ID, invoice);

                if (result > 0)
                {
                    RouteResult = new SuccessResult(AppGlobal.EditSuccess);
                }
                else
                {
                    RouteResult = new ErrorResult(ErrorType.EditError, AppGlobal.EditFail);
                }
            }

            return new BaseResponeModel(new { ID = model.ID }, RouteResult);
        }


        [HttpPost]
        public ActionResult<BaseResponeModel> SaveThanhLy(Invoice model)
        {
            if (model.ID > 0)
            {
                Invoice invoice = _invoiceRepository.GetByID(model.ID);
                invoice.ThanhLy = model.ThanhLy;
                invoice.TrimModel();
                invoice.Initialization(InitType.Update, RequestUserID);
                
                int result = _invoiceRepository.Update(invoice.ID, invoice);

                if (result > 0)
                {
                    RouteResult = new SuccessResult(AppGlobal.EditSuccess);
                }
                else
                {
                    RouteResult = new ErrorResult(ErrorType.EditError, AppGlobal.EditFail);
                }
            }

            return new BaseResponeModel(new { ID = model.ID }, RouteResult);
        }

        [HttpPost]
        public ActionResult<BaseResponeModel> CreateProjectDuToan(InvoiceDetailDataTransfer model, int invoiceID)
        {
            model.CategoryID = AppGlobal.DuToanID;
            model.InvoiceID = invoiceID;
            model.InitializationForProject();
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;

            if ((model.ProductID > 0) && (model.UnitID > 0))
            {
                result = _invoiceDetailRepository.Create(model);
            }

            if (result > 0)
            {
                RouteResult = new SuccessResult(AppGlobal.CreateSuccess);

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
                RouteResult = new ErrorResult(ErrorType.InsertError, AppGlobal.CreateFail);
            }

            return new BaseResponeModel(null, RouteResult);
        }

        [HttpPost]
        public ActionResult<BaseResponeModel> CreateProjectThiCong(InvoiceDetailDataTransfer model, int invoiceID)
        {
            model.CategoryID = AppGlobal.ThiCongID;
            model.InvoiceID = invoiceID;
            model.ParentID = model.Parent.ID;
            model.ProductID = model.Product.ID;
            model.EmployeeID = model.Employee.ID;
            model.InitializationForProject();
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            
            if ((model.ProductID > 0) && (model.UnitID > 0))
            {
                result = _invoiceDetailRepository.Create(model);
            }

            if (result > 0)
            {
                RouteResult = new SuccessResult(AppGlobal.CreateSuccess);
                _invoiceRepository.InitializationByIDAndCategoryID(model.InvoiceID.Value, model.CategoryID.Value);
                _productRepository.InitializationByIDAndCategoryID(model.ProductID.Value, model.CategoryID.Value);
            }
            else
            {
                RouteResult = new ErrorResult(ErrorType.InsertError, AppGlobal.CreateFail);
            }

            return new BaseResponeModel(null, RouteResult);
        }

        [HttpPost]
        public ActionResult<BaseResponeModel> CreateProjectChaoGia(InvoiceDetailDataTransfer model, int invoiceID)
        {
            model.CategoryID = AppGlobal.ChaoGiaID;
            model.InvoiceID = invoiceID;
            model.InitializationForProject();
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;

            if ((model.ProductID > 0) && (model.UnitID > 0))
            {
                result = _invoiceDetailRepository.Create(model);
            }

            if(result > 0)
            {
                RouteResult = new SuccessResult(AppGlobal.CreateSuccess);
            }
            else
            {
                RouteResult = new ErrorResult(ErrorType.InsertError, AppGlobal.CreateFail);
            }

            return new BaseResponeModel(null, RouteResult);
        }

        [HttpPost]
        public ActionResult<BaseResponeModel> CreateProjectNhanSu(InvoiceDetailDataTransfer model, int invoiceID)
        {
            model.CategoryID = AppGlobal.NhanSuID;
            model.InvoiceID = invoiceID;
            model.EmployeeID = model.Employee.ID;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            result = _invoiceDetailRepository.Create(model);

            if (result > 0)
            {
                RouteResult = new SuccessResult(AppGlobal.CreateSuccess);
            }
            else
            {
                RouteResult = new ErrorResult(ErrorType.InsertError, AppGlobal.CreateFail);
            }

            return new BaseResponeModel(null, RouteResult);
        }

        [HttpPost]
        public ActionResult<BaseResponeModel> CreateProjectChamCong(InvoiceDetailDataTransfer model, int invoiceID)
        {
            model.CategoryID = AppGlobal.ChamCongID;
            model.InvoiceID = invoiceID;
            model.EmployeeID = model.Employee.ID;
            model.ProductID = AppGlobal.NhanCongID;
            model.UnitID = AppGlobal.LanID;
            model.Quantity = model.Shift01 + model.Shift02 + model.Shift03;
            model.UnitPrice = 0;

            model.InitializationForProject();
            model.Initialization(InitType.Insert, RequestUserID);

            int result = _invoiceDetailRepository.Create(model);

            if (result > 0)
            {
                RouteResult = new SuccessResult(AppGlobal.CreateSuccess);
            }
            else
            {
                RouteResult = new ErrorResult(ErrorType.InsertError, AppGlobal.CreateFail);
            }

            return new BaseResponeModel(null, RouteResult);
        }

        [HttpPut]
        public ActionResult<BaseResponeModel> UpdateProjectDuToan(InvoiceDetailDataTransfer model)
        {
            model.InitializationForProject();
            model.Initialization(InitType.Update, RequestUserID);

            int result = _invoiceDetailRepository.Update(model.ID, model);

            if (result > 0)
            {
                RouteResult = new SuccessResult(AppGlobal.EditSuccess);
            }
            else
            {
                RouteResult = new ErrorResult(ErrorType.EditError, AppGlobal.EditFail);
            }

            return new BaseResponeModel(null, RouteResult);
        }

        [HttpPut]
        public ActionResult<BaseResponeModel> UpdateProjectThiCong(InvoiceDetailDataTransfer model)
        {
            model.InitializationForProject();
            model.Initialization(InitType.Update, RequestUserID);

            int result = _invoiceDetailRepository.Update(model.ID, model);

            if (result > 0)
            {
                RouteResult = new SuccessResult(AppGlobal.EditSuccess);
                _invoiceRepository.InitializationByIDAndCategoryID(model.InvoiceID.Value, model.CategoryID.Value);
                _productRepository.InitializationByIDAndCategoryID(model.ProductID.Value, model.CategoryID.Value);
            }
            else
            {
                RouteResult = new ErrorResult(ErrorType.EditError, AppGlobal.EditFail);
            }

            return new BaseResponeModel(null, RouteResult);
        }

        [HttpPut]
        public ActionResult<BaseResponeModel> UpdateProjectNhanSu(InvoiceDetailDataTransfer model)
        {
            model.EmployeeID = model.Employee.ID;
            model.Initialization(InitType.Update, RequestUserID);

            int result = _invoiceDetailRepository.Update(model.ID, model);

            if (result > 0)
            {
                RouteResult = new SuccessResult(AppGlobal.EditSuccess);
            }
            else
            {
                RouteResult = new ErrorResult(ErrorType.EditError, AppGlobal.EditFail);
            }

            return new BaseResponeModel(null, RouteResult);
        }

        [HttpPut]
        public ActionResult<BaseResponeModel> UpdateProjectChamCong(InvoiceDetailDataTransfer model)
        {
            model.EmployeeID = model.Employee.ID;
            model.Quantity = model.Shift01 + model.Shift02 + model.Shift03;
            model.InitializationForProject();
            model.Initialization(InitType.Update, RequestUserID);

            int result = _invoiceDetailRepository.Update(model.ID, model);

            if (result > 0)
            {
                RouteResult = new SuccessResult(AppGlobal.EditSuccess);
            }
            else
            {
                RouteResult = new ErrorResult(ErrorType.EditError, AppGlobal.EditFail);
            }

            return new BaseResponeModel(null, RouteResult);
        }

        [HttpPost]
        public ActionResult<BaseResponeModel> SaveFiles(InvoiceProperty model)
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
                            var physicalPath = Path.Combine(_webHostEnvironment.WebRootPath, AppGlobal.URLImagesCustomer, fileName);
                            
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

            return new BaseResponeModel(new { ID = model.InvoiceID }, RouteResult);
        }
    }
}