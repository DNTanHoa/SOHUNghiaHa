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
    public class BaoCaoThongKeController : BaseController
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IProductRepository _productRepository;

        public BaoCaoThongKeController(IInvoiceRepository invoiceRepository, IProductRepository productRepository)
        {
            _invoiceRepository = invoiceRepository;
            _productRepository = productRepository;
        }
        public IActionResult ProjectCongNo()
        {
            BaoCaoThongKeViewModel model = new BaoCaoThongKeViewModel();
            List<Invoice> list = _invoiceRepository.GetProjectCongNoByCategoryIDToList(AppGlobal.DuAnID);
            if (list.Count > 0)
            {
                int no = 0;
                StringBuilder txt = new StringBuilder();
                txt.AppendLine(@"<table class='border' style='width: 100%; font-size:16px; line-height:30px;'>");
                txt.AppendLine(@"<thead>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>STT</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Dự án</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Tổng cộng</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Thuế</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Thành tiền</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Thanh toán</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Dư nợ</a></th>");
                txt.AppendLine(@"</thead>");
                txt.AppendLine(@"<tbody>");
                foreach (Invoice item in list)
                {
                    no = no + 1;
                    if (no % 2 == 0)
                    {
                        txt.AppendLine(@"<tr style='background-color:#ffffff;'>");
                    }
                    else
                    {
                        txt.AppendLine(@"<tr style='background-color:#f1f1f1;'>");
                    }
                    txt.AppendLine(@"<td style='text-align:center;'>" + no + "</td>");
                    if (no > 1)
                    {
                        txt.AppendLine(@"<td style='text-align:left;'>");
                        txt.AppendLine(@"<a title='" + item.InvoiceName + "' href='/Project/Detail?ID=" + item.ID + "' target='_blank'> Dự án: " + item.InvoiceName + "</a>");
                        txt.AppendLine(@"<br/>");
                        txt.AppendLine(@"Khách hàng: <b>" + item.BuyName + "</b>");
                        txt.AppendLine(@"<br/>");
                        txt.AppendLine(@"Liên hệ: <b>" + item.ManageCode + "</b>");
                        txt.AppendLine(@"<br/>");
                        txt.AppendLine(@"Điện thoại: <b>" + item.BuyPhone + "</b>");
                        txt.AppendLine(@"</td>");
                    }
                    else
                    {
                        txt.AppendLine(@"<td style='text-align:left;'>");
                        txt.AppendLine(@"");
                        txt.AppendLine(@"</td>");
                    }

                    txt.AppendLine(@"<td style='text-align:right;'>" + item.TotalNoTax.Value.ToString("N0") + "</td>");
                    if (no > 1)
                    {
                        txt.AppendLine(@"<td style='text-align:right;'>" + item.TotalTax.Value.ToString("N0") + " (" + item.Tax.Value.ToString("N0") + "%)</td>");
                    }
                    else
                    {
                        txt.AppendLine(@"<td style='text-align:right;'>" + item.TotalTax.Value.ToString("N0") + "</td>");
                    }
                    txt.AppendLine(@"<td style='text-align:right;'><b>" + item.Total.Value.ToString("N0") + "<b></td>");
                    txt.AppendLine(@"<td style='text-align:right; color: green;'><b>" + item.TotalPaid.Value.ToString("N0") + "</b></td>");
                    txt.AppendLine(@"<td style='text-align:right; color: red;'><b>" + item.TotalDebt.Value.ToString("N0") + "</b></td>");
                    txt.AppendLine(@"</tr>");
                }
                txt.AppendLine(@"</tbody>");
                txt.AppendLine(@"</table>");
                model.ProjectCongNo = txt.ToString();
            }
            return View(model);
        }
        public IActionResult HoaDonNhapHangCongNo()
        {
            BaoCaoThongKeViewModel model = new BaoCaoThongKeViewModel();
            List<Invoice> list = _invoiceRepository.GetInvoiceNhapHangCongNoByCategoryIDToList(AppGlobal.InvoiceInputID);
            if (list.Count > 0)
            {
                int no = 0;
                StringBuilder txt = new StringBuilder();
                txt.AppendLine(@"<table class='border' style='width: 100%; font-size:16px; line-height:30px;'>");
                txt.AppendLine(@"<thead>");                
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Hóa đơn / Nhà cung cấp</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Tổng cộng</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Thuế</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Thành tiền</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Thanh toán</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Dư nợ</a></th>");
                txt.AppendLine(@"</thead>");
                txt.AppendLine(@"<tbody>");
                foreach (Invoice item in list)
                {
                    if (!string.IsNullOrEmpty(item.InvoiceName))
                    {
                        if (item.InvoiceName.Contains(@"SUM") == true)
                        {
                            txt.AppendLine(@"<tr style='background-color:#f1f1f1;'>");
                            txt.AppendLine(@"<td style='text-align:left;'>");                            
                            txt.AppendLine(@"<b>" + item.SellName + "</b>");
                            txt.AppendLine(@"</td>");
                            txt.AppendLine(@"<td style='text-align:right;'>" + item.TotalNoTax.Value.ToString("N0") + "</td>");
                            txt.AppendLine(@"<td style='text-align:right;'>" + item.TotalTax.Value.ToString("N0") + "</td>");
                            txt.AppendLine(@"<td style='text-align:right;'><b>" + item.Total.Value.ToString("N0") + "<b></td>");
                            txt.AppendLine(@"<td style='text-align:right; color: green;'><b>" + item.TotalPaid.Value.ToString("N0") + "</b></td>");
                            txt.AppendLine(@"<td style='text-align:right; color: red;'><b>" + item.TotalDebt.Value.ToString("N0") + "</b></td>");
                        }
                    }
                    else
                    {
                        txt.AppendLine(@"<tr style='background-color:#ffffff;'>");
                        txt.AppendLine(@"<td style='text-align:left;'>");
                        txt.AppendLine(@"<a title='" + item.SoHoaDon + "' href='/Invoice/InvoiceInputDetail?ID=" + item.ID + "' target='_blank'> Số hóa đơn: " + item.SoHoaDon + "</a>");                        
                        txt.AppendLine(@"</td>");
                        txt.AppendLine(@"<td style='text-align:right;'>" + item.TotalNoTax.Value.ToString("N0") + "</td>");
                        txt.AppendLine(@"<td style='text-align:right;'>" + item.TotalTax.Value.ToString("N0") + " (" + item.Tax.Value.ToString("N0") + "%)</td>");
                        txt.AppendLine(@"<td style='text-align:right;'><b>" + item.Total.Value.ToString("N0") + "<b></td>");
                        txt.AppendLine(@"<td style='text-align:right; color: green;'><b>" + item.TotalPaid.Value.ToString("N0") + "</b></td>");
                        txt.AppendLine(@"<td style='text-align:right; color: red;'><b>" + item.TotalDebt.Value.ToString("N0") + "</b></td>");
                    }                   
                    txt.AppendLine(@"</tr>");
                }
                txt.AppendLine(@"</tbody>");
                txt.AppendLine(@"</table>");
                model.ProjectCongNo = txt.ToString();
            }
            return View(model);
        }
        public IActionResult HoaDonXuatHangCongNo()
        {
            BaoCaoThongKeViewModel model = new BaoCaoThongKeViewModel();
            List<Invoice> list = _invoiceRepository.GetInvoiceXuatHangCongNoByCategoryIDToList(AppGlobal.InvoiceOutputID);
            if (list.Count > 0)
            {
                int no = 0;
                StringBuilder txt = new StringBuilder();
                txt.AppendLine(@"<table class='border' style='width: 100%; font-size:16px; line-height:30px;'>");
                txt.AppendLine(@"<thead>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>STT</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Hóa đơn</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Tổng cộng</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Thuế</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Thành tiền</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Thanh toán</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Dư nợ</a></th>");
                txt.AppendLine(@"</thead>");
                txt.AppendLine(@"<tbody>");
                foreach (Invoice item in list)
                {
                    no = no + 1;
                    if (no % 2 == 0)
                    {
                        txt.AppendLine(@"<tr style='background-color:#ffffff;'>");
                    }
                    else
                    {
                        txt.AppendLine(@"<tr style='background-color:#f1f1f1;'>");
                    }
                    txt.AppendLine(@"<td style='text-align:center;'>" + no + "</td>");
                    if (no > 1)
                    {
                        txt.AppendLine(@"<td style='text-align:left;'>");
                        txt.AppendLine(@"<a title='" + item.SoHoaDon + "' href='/Invoice/InvoiceOutputDetail?ID=" + item.ID + "' target='_blank'> Số hóa đơn: " + item.SoHoaDon + "</a>");
                        txt.AppendLine(@"<br/>");
                        txt.AppendLine(@"Nhà cung cấp: <b>" + item.BuyName + "</b>");
                        txt.AppendLine(@"</td>");
                    }
                    else
                    {
                        txt.AppendLine(@"<td style='text-align:left;'>");
                        txt.AppendLine(@"");
                        txt.AppendLine(@"</td>");
                    }

                    txt.AppendLine(@"<td style='text-align:right;'>" + item.TotalNoTax.Value.ToString("N0") + "</td>");
                    if (no > 1)
                    {
                        txt.AppendLine(@"<td style='text-align:right;'>" + item.TotalTax.Value.ToString("N0") + " (" + item.Tax.Value.ToString("N0") + "%)</td>");
                    }
                    else
                    {
                        txt.AppendLine(@"<td style='text-align:right;'>" + item.TotalTax.Value.ToString("N0") + "</td>");
                    }
                    txt.AppendLine(@"<td style='text-align:right;'><b>" + item.Total.Value.ToString("N0") + "<b></td>");
                    txt.AppendLine(@"<td style='text-align:right; color: green;'><b>" + item.TotalPaid.Value.ToString("N0") + "</b></td>");
                    txt.AppendLine(@"<td style='text-align:right; color: red;'><b>" + item.TotalDebt.Value.ToString("N0") + "</b></td>");
                    txt.AppendLine(@"</tr>");
                }
                txt.AppendLine(@"</tbody>");
                txt.AppendLine(@"</table>");
                model.ProjectCongNo = txt.ToString();
            }
            return View(model);
        }
        public IActionResult ProductTonKho()
        {
            BaoCaoThongKeViewModel model = new BaoCaoThongKeViewModel();
            List<Product> list = _productRepository.GetProductTonKhoToList();
            if (list.Count > 0)
            {
                int no = 0;
                StringBuilder txt = new StringBuilder();
                txt.AppendLine(@"<table class='border' style='width: 100%; font-size:16px; line-height:30px;'>");
                txt.AppendLine(@"<thead>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'></a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'></a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'></a></th>");
                txt.AppendLine(@"<th style='text-align:center;' colspan='3'><a style='cursor:pointer;'>Tồn kho</a></th>");
                txt.AppendLine(@"<th style='text-align:center;' colspan='3'><a style='cursor:pointer;'>Hóa đơn</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'></a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'></a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'></a></th>");
                txt.AppendLine(@"</thead>");
                txt.AppendLine(@"<thead>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>STT</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Vật tư</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Đầu kỳ</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Nhập</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Xuất</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Tồn</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Nhập</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Xuất</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Tồn</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Nhập hàng</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Xuất hàng</a></th>");
                txt.AppendLine(@"<th style='text-align:center;'><a style='cursor:pointer;'>Dự án</a></th>");
                txt.AppendLine(@"</thead>");
                txt.AppendLine(@"<tbody>");
                foreach (Product item in list)
                {
                    List<Invoice> listInvoice = _invoiceRepository.GetInvoiceTonKhoSelectByProductIDToList(item.ID);
                    no = no + 1;
                    if (no % 2 == 0)
                    {
                        txt.AppendLine(@"<tr style='background-color:#ffffff;'>");
                    }
                    else
                    {
                        txt.AppendLine(@"<tr style='background-color:#f1f1f1;'>");
                    }
                    txt.AppendLine(@"<td style='text-align:center;'>" + no + "</td>");
                    txt.AppendLine(@"<td style='text-align:left;'>");
                    txt.AppendLine(@"<a style='color: #000000;' title='" + item.Title + "' href='/Product/Detail?ID=" + item.ID + "' target='_blank'>" + item.Title + "</a>");
                    txt.AppendLine(@"</td>");
                    txt.AppendLine(@"<td style='text-align:right; color: red;'>" + item.QuantityInStockRoot.Value.ToString("N0") + "</td>");
                    txt.AppendLine(@"<td style='text-align:right; color: #000000;'>" + item.QuantityInput.Value.ToString("N0") + "</td>");
                    txt.AppendLine(@"<td style='text-align:right; color: #000000;'>" + item.QuantityOutput.Value.ToString("N0") + "</td>");
                    txt.AppendLine(@"<td style='text-align:right; color: red;'>" + item.QuantityInStock.Value.ToString("N0") + "</td>");
                    txt.AppendLine(@"<td style='text-align:right; color: blue;'>" + item.QuantityInput001.Value.ToString("N0") + "</td>");
                    txt.AppendLine(@"<td style='text-align:right; color: blue;'>" + item.QuantityOutput001.Value.ToString("N0") + "</td>");
                    txt.AppendLine(@"<td style='text-align:right; color: red;'>" + item.QuantityInStock001.Value.ToString("N0") + "</td>");
                    string invoiceInputString = "";
                    string invoiceOutputString = "";
                    string thiCongString = "";
                    foreach (Invoice invoice in listInvoice)
                    {
                        if (invoice.CategoryID == AppGlobal.InvoiceInputID)
                        {
                            invoiceInputString = invoiceInputString + "<a style='color: #000000;' title='" + invoice.SoHoaDon + "' href='/Invoice/InvoiceInputDetail?ID=" + invoice.ID + "' target='_blank'>" + invoice.SoHoaDon + "</a><br/>";
                        }
                    }
                    foreach (Invoice invoice in listInvoice)
                    {
                        if (invoice.CategoryID == AppGlobal.InvoiceOutputID)
                        {
                            invoiceOutputString = invoiceOutputString + "<a style='color: #000000;' title='" + invoice.SoHoaDon + "' href='/Invoice/InvoiceOutputDetail?ID=" + invoice.ID + "' target='_blank'>" + invoice.SoHoaDon + "</a><br/>";
                        }
                    }
                    foreach (Invoice invoice in listInvoice)
                    {
                        if (invoice.CategoryID == AppGlobal.ThiCongID)
                        {
                            thiCongString = thiCongString + "<a style='color: #000000;' title='" + invoice.SoHoaDon + "' href='/Project/Detail?ID=" + invoice.ID + "' target='_blank'>" + invoice.SoHoaDon + "</a><br/>";
                        }
                    }
                    txt.AppendLine(@"<td style='text-align:left;'>" + invoiceInputString + "</td>");
                    txt.AppendLine(@"<td style='text-align:left;'>" + invoiceOutputString + "</td>");
                    txt.AppendLine(@"<td style='text-align:left;'>" + thiCongString + "</td>");
                    txt.AppendLine(@"</tr>");
                }
                txt.AppendLine(@"</tbody>");
                txt.AppendLine(@"</table>");
                model.ProjectCongNo = txt.ToString();
            }
            return View(model);
        }
    }
}