using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Euronailsupply.CRM.Models;
using SOHU.Data.Helpers;
using SOHU.Data.Repositories;
using SOHU.Data.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Euronailsupply.CRM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IConfigRepository _configResposistory;
        public HomeController(ILogger<HomeController> logger, IConfigRepository configResposistory, IMembershipRepository membershipRepository)
        {
            _logger = logger;
            _membershipRepository = membershipRepository;
            _configResposistory = configResposistory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_membershipRepository.IsValid(model.Account, model.Password))
                {
                    var cookie = new CookieOptions();
                    cookie.Expires = AppGlobal.InitDateTime.AddDays(30);
                    var member = _membershipRepository.GetByAccount(model.Account);
                    Response.Cookies.Append("UserID", member.ID.ToString(), cookie);
                    Response.Cookies.Append("RememberPassword", model.RememberPassword.ToString(), cookie);
                    Response.Cookies.Append("Password", MD5Helper.EncryptDataMD5(model.Password, AppGlobal.MD5Key), cookie);
                    Response.Cookies.Append("IsLogin", "True", cookie);
                    return Json(AppGlobal.Success + " - " + AppGlobal.RedirectDefault);
                }
                else
                {
                    return Json(AppGlobal.Error + "-" + AppGlobal.LoginFail);
                }
            }
            else
            {
                return Json(AppGlobal.Error + "-" + AppGlobal.LoginFail);
            }
        }
        public string InitializationMenuLeft()
        {            
            StringBuilder txt = new StringBuilder();            
            txt.AppendLine(@"<li class='nav-item has-treeview'>");
            txt.AppendLine(@"<a class='nav-link' href='#'>");
            txt.AppendLine(@"<i class='nav-icon fas fa-file-signature'></i>");
            txt.AppendLine(@"<p>");
            txt.AppendLine(@"Dự án");
            txt.AppendLine(@"<i class='right fas fa-angle-left'></i>");
            txt.AppendLine(@"</p>");
            txt.AppendLine(@"</a>");
            txt.AppendLine(@"<ul class='nav nav-treeview'>");
            txt.AppendLine(@"<li class='nav-item'>");
            txt.AppendLine(@"<a class='nav-link' href='/Project/Index'>");
            txt.AppendLine(@"<i class='nav-icon far fa-circle'></i>");
            txt.AppendLine(@"<p>");
            txt.AppendLine(@"Danh sách");
            txt.AppendLine(@"</p>");
            txt.AppendLine(@"</a>");
            txt.AppendLine(@"</li>");
            txt.AppendLine(@"</ul>");
            txt.AppendLine(@"</li>");
            txt.AppendLine(@"<li class='nav-item has-treeview'>");
            txt.AppendLine(@"<a class='nav-link' href='#'>");
            txt.AppendLine(@"<i class='nav-icon fas fa-file-invoice-dollar'></i>");
            txt.AppendLine(@"<p>");
            txt.AppendLine(@"Hóa đơn - Chứng từ");
            txt.AppendLine(@"<i class='right fas fa-angle-left'></i>");
            txt.AppendLine(@"</p>");
            txt.AppendLine(@"</a>");
            txt.AppendLine(@"<ul class='nav nav-treeview'>");
            txt.AppendLine(@"<li class='nav-item'>");
            txt.AppendLine(@"<a class='nav-link' href='/Invoice/InvoiceInput'>");
            txt.AppendLine(@"<i class='nav-icon far fa-circle'></i>");
            txt.AppendLine(@"<p>");
            txt.AppendLine(@"Hóa đơn nhập");
            txt.AppendLine(@"</p>");
            txt.AppendLine(@"</a>");
            txt.AppendLine(@"</li>");
            txt.AppendLine(@"</ul>");
            txt.AppendLine(@"</li>");
            txt.AppendLine(@"<li class='nav-item has-treeview'>");
            txt.AppendLine(@"<a class='nav-link' href='#'>");
            txt.AppendLine(@"<i class='nav-icon fas fa-user-friends'></i>");
            txt.AppendLine(@"<p>");
            txt.AppendLine(@"Thành viên");
            txt.AppendLine(@"<i class='right fas fa-angle-left'></i>");
            txt.AppendLine(@"</p>");
            txt.AppendLine(@"</a>");
            txt.AppendLine(@"<ul class='nav nav-treeview'>");
            txt.AppendLine(@"<li class='nav-item'>");
            txt.AppendLine(@"<a class='nav-link' href='/Membership/Customer'>");
            txt.AppendLine(@"<i class='nav-icon far fa-circle'></i>");
            txt.AppendLine(@"<p>");
            txt.AppendLine(@"Khách hàng");
            txt.AppendLine(@"</p>");
            txt.AppendLine(@"</a>");
            txt.AppendLine(@"</li>");
            txt.AppendLine(@"<li class='nav-item'>");
            txt.AppendLine(@"<a class='nav-link' href='/Membership/Supplier'>");
            txt.AppendLine(@"<i class='nav-icon far fa-circle'></i>");
            txt.AppendLine(@"<p>");
            txt.AppendLine(@"Nhà cung cấp");
            txt.AppendLine(@"</p>");
            txt.AppendLine(@"</a>");
            txt.AppendLine(@"</li>");
            txt.AppendLine(@"<li class='nav-item'>");
            txt.AppendLine(@"<a class='nav-link' href='/Membership/Employee'>");
            txt.AppendLine(@"<i class='nav-icon far fa-circle'></i>");
            txt.AppendLine(@"<p>");
            txt.AppendLine(@"Nhân viên");
            txt.AppendLine(@"</p>");
            txt.AppendLine(@"</a>");
            txt.AppendLine(@"</li>");
            txt.AppendLine(@"</ul>");
            txt.AppendLine(@"</li>");
            txt.AppendLine(@"<li class='nav-item has-treeview'>");
            txt.AppendLine(@"<a class='nav-link' href='#'>");
            txt.AppendLine(@"<i class='nav-icon fas fa-box-open'></i>");
            txt.AppendLine(@"<p>");
            txt.AppendLine(@"Vật tư - Thiết bị");
            txt.AppendLine(@"<i class='right fas fa-angle-left'></i>");
            txt.AppendLine(@"</p>");
            txt.AppendLine(@"</a>");
            txt.AppendLine(@"<ul class='nav nav-treeview'>");
            List<Config> listProductCategory = _configResposistory.GetByGroupNameAndCodeToList(AppGlobal.CRM, AppGlobal.ProductCategory);
            foreach (Config item in listProductCategory)
            {
                txt.AppendLine(@"<li class='nav-item'>");
                txt.AppendLine(@"<a class='nav-link' href='/Product/Index01/" + item.ID + "'>");
                txt.AppendLine(@"<i class='nav-icon far fa-circle'></i>");
                txt.AppendLine(@"<p>");
                txt.AppendLine(@"" + item.CodeName);
                txt.AppendLine(@"</p>");
                txt.AppendLine(@"</a>");
                txt.AppendLine(@"</li>");
            }
            txt.AppendLine(@"</ul>");
            txt.AppendLine(@"</li>");
            txt.AppendLine(@"<li class='nav-item has-treeview'>");
            txt.AppendLine(@"<a class='nav-link' href='#'>");
            txt.AppendLine(@"<i class='nav-icon fas fa-book'></i>");
            txt.AppendLine(@"<p>");
            txt.AppendLine(@"Hệ thống");
            txt.AppendLine(@"<i class='right fas fa-angle-left'></i>");
            txt.AppendLine(@"</p>");
            txt.AppendLine(@"</a>");
            txt.AppendLine(@"<ul class='nav nav-treeview'>");
            txt.AppendLine(@"<li class='nav-item'>");
            txt.AppendLine(@"<a class='nav-link' href='/Config/InvoiceCategory'>");
            txt.AppendLine(@"<i class='nav-icon far fa-circle'></i>");
            txt.AppendLine(@"<p>");
            txt.AppendLine(@"Danh mục chứng từ");
            txt.AppendLine(@"</p>");
            txt.AppendLine(@"</a>");
            txt.AppendLine(@"</li>");
            txt.AppendLine(@"<li class='nav-item'>");
            txt.AppendLine(@"<a class='nav-link' asp-action='CustomerCategory' asp-controller='Config'>");
            txt.AppendLine(@"<i class='nav-icon far fa-circle'></i>");
            txt.AppendLine(@"<p>");
            txt.AppendLine(@"Danh mục thành viên");
            txt.AppendLine(@"</p>");
            txt.AppendLine(@"</a>");
            txt.AppendLine(@"</li>");
            txt.AppendLine(@"<li class='nav-item'>");
            txt.AppendLine(@"<a class='nav-link' href='/Config/ProductCategory'>");
            txt.AppendLine(@"<i class='nav-icon far fa-circle'></i>");
            txt.AppendLine(@"<p>");
            txt.AppendLine(@"Danh mục vật tư");
            txt.AppendLine(@"</p>");
            txt.AppendLine(@"</a>");
            txt.AppendLine(@"</li>");
            txt.AppendLine(@"<li class='nav-item'>");
            txt.AppendLine(@"<a class='nav-link' href='/Config/Unit'>");
            txt.AppendLine(@"<i class='nav-icon far fa-circle'></i>");
            txt.AppendLine(@"<p>");
            txt.AppendLine(@"Đơn vị tính");
            txt.AppendLine(@"</p>");
            txt.AppendLine(@"</a>");
            txt.AppendLine(@"</li>");
            txt.AppendLine(@"</ul>");
            txt.AppendLine(@"</li>");
            return txt.ToString();
        }
    }
}
