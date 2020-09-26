using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Hosting;
using System.IO;
using NghiaHa.CRM.Controllers;
using SOHU.Data.Helpers;

namespace NghiaHa.CRM.Web.Controllers
{
    public class AppGlobalController : BaseController
    {
        public AppGlobalController() : base()
        {            
        }
        public ActionResult GetYearFinanceToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = YearFinance.GetAllToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetMonthFinanceToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = MonthFinance.GetAllToList();
            return Json(data.ToDataSourceResult(request));
        }
    }
}
