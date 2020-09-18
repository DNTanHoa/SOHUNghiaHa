using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NghiaHa.CRM.Models;
using SOHU.Data.Helpers;
using SOHU.Data.Repositories;

namespace NghiaHa.CRM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMembershipRepository membershipRepository;

        public HomeController(ILogger<HomeController> logger,
                              IMembershipRepository membershipRepository)
        {
            _logger = logger;
            this.membershipRepository = membershipRepository;
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
            if(ModelState.IsValid)
            {
                if(membershipRepository.IsValid(model.Account, model.Password))
                {
                    var cookie = new CookieOptions();
                    cookie.Expires = AppGlobal.InitDateTime.AddDays(30);
                    var member = membershipRepository.GetByAccount(model.Account);
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
    }
}
