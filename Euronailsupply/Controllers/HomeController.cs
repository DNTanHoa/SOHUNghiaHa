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
using SOHU.Data.Models;
using SOHU.Data.Repositories;
using SOHU.Data.Helpers;

namespace Euronailsupply.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IConfigRepository _configResposistory;
        public HomeController(IConfigRepository configResposistory)
        {
            _configResposistory = configResposistory;
        }
        public IActionResult Index()
        {
            return View();
        }
        public string MenuTopSub(int parentID)
        {
            string menuTop = "";
            List<Config> list = _configResposistory.GetMenuTopByParentIDToList(parentID);
            if (list.Count > 0)
            {
                StringBuilder txt = new StringBuilder();
                txt.AppendLine(@"<ul class='nav nav-pills'>");
                foreach (Config item in list)
                {
                    string url = AppGlobal.Domain + item.CodenameSub + "-" + item.ID;
                    if (_configResposistory.GetMenuTopByParentIDToList(item.ID).Count > 0)
                    {
                        txt.AppendLine(@"<li class='dropdown'>");
                        txt.AppendLine(@"<a href='#' class='nav-link dropdown-toggle' style='font-size:14px; color:#000000;'>" + item.CodeName + "</a>");
                        txt.AppendLine(MenuTopSub(item.ID));
                        txt.AppendLine(@"</li>");
                    }
                    else
                    {
                        txt.AppendLine(@"<li>");
                        txt.AppendLine(@"<a href='"+ url + "' class='nav-link' style='font-size:14px; color:#000000;'>" + item.CodeName + "</a>");
                        txt.AppendLine(@"</li>");
                    }

                }
                txt.AppendLine(@"</ul>");
                menuTop = txt.ToString();
            }
            return menuTop;
        }
        public string MenuTop()
        {
            string menuTop = MenuTopSub(40);
            return menuTop;
        }
    }
}
