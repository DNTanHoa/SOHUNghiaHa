using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SOHU.Data.Helpers;

namespace Euronailsupply.CRM.Controllers
{
    public class BaseController : Controller, IActionFilter
    {
        public int RequestUserID
        {
            get
            {
                int.TryParse(Request.Cookies["UserID"]?.ToString(), out int result);
                return result;
            }
        }

        public bool IsLogin
        {
            get
            {
                bool.TryParse(Request.Cookies["UserID"]?.ToString(), out bool result);
                return result;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {         
            base.OnActionExecuting(context);
        }
    }
}
