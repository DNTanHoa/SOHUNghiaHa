using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Euronailsupply.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Euronailsupply.Controllers
{
    public class HomeController : BaseController
    {   
        public HomeController()
        {          
        }
        public IActionResult Index()
        {
            return View();
        }               
    }
}
