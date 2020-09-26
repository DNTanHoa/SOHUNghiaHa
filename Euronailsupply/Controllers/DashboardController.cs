using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NghiaHa.CRM.Web.Models;

namespace NghiaHa.CRM.Controllers
{
    public class DashboardController : Controller
    {
        public DashboardController()
        {

        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel();
            return View(model);
        }
    }
}
