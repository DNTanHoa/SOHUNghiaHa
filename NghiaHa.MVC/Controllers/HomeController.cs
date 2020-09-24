using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NghiaHa.MVC.Models;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace NghiaHa.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productResposistory;
        public HomeController(ILogger<HomeController> logger, IProductRepository productResposistory)
        {
            _logger = logger;
            _productResposistory = productResposistory;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            Product model = _productResposistory.GetByID(AppGlobal.AboutID);
            if (model == null)
            {
                model = new Product();
            }
            return View(model);
        }
        public IActionResult ProductDetail(int ProductID)
        {
            Product model = _productResposistory.GetByID(ProductID);
            if (model == null)
            {
                model = new Product();
            }
            return View(model);
        }
        public IActionResult Contact()
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
    }
}
