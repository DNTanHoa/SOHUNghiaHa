using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SOHU.MVC.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MenuLeft()
        {
            return PartialView("~/Views/Menu/_MenuLeft.cshtml");
        }
    }
}