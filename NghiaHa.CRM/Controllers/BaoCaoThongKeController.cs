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

        public BaoCaoThongKeController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
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
    }
}