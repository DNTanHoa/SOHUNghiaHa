using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SOHU.MVC.Models;

namespace SOHU.MVC.Controllers
{
    public class BaseController : Controller
    {
        public int RequestUserID
        {
            get
            {
                int.TryParse(Request.Cookies["UserID"]?.ToString(), out int result);
                return result;
            }
        }

        public int CartID
        {
            get
            {
                int.TryParse(Request.Cookies["CartID"]?.ToString(), out int result);
                return result;
            }
        }

        public IActionResult ConfirmDialog(ConfirmObjectViewModel model)
        {
            return PartialView("~/Views/Shared/_ConfirmDialog.cshtml", model);
        }
    }
}