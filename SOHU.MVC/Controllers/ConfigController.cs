using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SOHU.Data.Enum;
using SOHU.Data.Extensions;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace SOHU.MVC.Controllers
{
    public class ConfigController : BaseController
    {
        private readonly IConfigRepository _configResposistory;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ConfigController(IConfigRepository configResposistory, IHostingEnvironment hostingEnvironment)
        {
            _configResposistory = configResposistory;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Banner()
        {
            return View();
        }
        public IActionResult MenuCMS()
        {
            return View();
        }
        public IActionResult MenuWebsite()
        {
            return View();
        }
        public IActionResult ProductCategory()
        {
            return View();
        }
        public IActionResult Detail(int ID)
        {
            var model = _configResposistory.GetByID(ID) ?? new Config();
            return View(model);
        }
        public IActionResult MenuCMSDetail(int ID)
        {
            var model = _configResposistory.GetByID(ID) ?? new Config();
            model.Code = AppGlobal.MenuLeftCode;
            return View(model);
        }
        public IActionResult BannerDetail(int ID)
        {
            var model = _configResposistory.GetByID(ID) ?? new Config();
            model.Code = AppGlobal.CarouselCode;
            return View(model);
        }
        public IActionResult MenuWebsiteDetail(int ID)
        {
            var model = _configResposistory.GetByID(ID) ?? new Config();
            model.Code = AppGlobal.TopMenuCode;
            return View(model);
        }
        public IActionResult ProductCategoryDetail(int ID)
        {
            var model = _configResposistory.GetByID(ID) ?? new Config();
            model.Code = AppGlobal.CategoryCode;
            return View(model);
        }
        public IActionResult GetAllToList()
        {
            return Json(_configResposistory.GetAllToList());
        }
        public IActionResult GetMenuCMSToList()
        {
            return Json(_configResposistory.GetByCodeToList(AppGlobal.MenuLeftCode).OrderBy(item => item.SortOrder).ToList());
        }
        public IActionResult GetMenuWebsiteToList()
        {
            return Json(_configResposistory.GetByCodeToList(AppGlobal.TopMenuCode).OrderBy(item => item.SortOrder).ToList());
        }
        public IActionResult GetProductCategoryToList()
        {
            return Json(_configResposistory.GetByCodeToList(AppGlobal.CategoryCode).OrderBy(item => item.SortOrder).ToList());
        }
        public IActionResult GetBannerToList()
        {
            return Json(_configResposistory.GetByCodeToList(AppGlobal.CarouselCode).OrderBy(item => item.SortOrder).ToList());
        }
        public IActionResult GetByID(int ID)
        {
            return Json(_configResposistory.GetByID(ID));
        }

        public IActionResult Create(Config model)
        {
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = _configResposistory.Create(model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }

        public IActionResult Update(Config model)
        {
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _configResposistory.Update(model.Id, model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            }
            else
            {
                note = AppGlobal.Success + " - " + AppGlobal.EditFail;
            }
            return Json(note);
        }

        public IActionResult Delete(int ID)
        {
            string note = AppGlobal.InitString;
            int result = _configResposistory.Delete(ID);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.DeleteSuccess;
            }
            else
            {
                note = AppGlobal.Success + " - " + AppGlobal.DeleteFail;
            }
            return Json(note);
        }

        public IActionResult SaveChangeBanner(Config model)
        {
            string note = AppGlobal.InitString;
            int result = 0;
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];
                if (file != null)
                {
                    string fileExtension = Path.GetExtension(file.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    fileName = AppGlobal.SetName(fileName);
                    fileName = fileName + "-" + AppGlobal.DateTimeCode + fileExtension;
                    var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, AppGlobal.URLImages, fileName);
                    using (var stream = new FileStream(physicalPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        model.CodenameSub = fileName;
                    }
                }
            }
            if (model.Id > 0)
            {
                model.Initialization(InitType.Update, RequestUserID);
                result = _configResposistory.Update(model.Id, model);
                if (result > 0)
                {
                    note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
                }
                else
                {
                    note = AppGlobal.Success + " - " + AppGlobal.EditFail;
                }
            }
            else
            {
                model.Initialization(InitType.Insert, RequestUserID);
                result = _configResposistory.Create(model);
                if (result > 0)
                {
                    note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
                }
                else
                {
                    note = AppGlobal.Success + " - " + AppGlobal.CreateFail;
                }
            }            
            return RedirectToAction("BannerDetail", new { ID = model.Id });
        }

        public IActionResult SaveChange(Config model)
        {
            string note = AppGlobal.InitString;
            int result = 0;
           
            if (model.Id > 0)
            {
                model.Initialization(InitType.Update, RequestUserID);
                result = _configResposistory.Update(model.Id, model);
                if (result > 0)
                {
                    note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
                }
                else
                {
                    note = AppGlobal.Success + " - " + AppGlobal.EditFail;
                }
            }
            else
            {
                model.Initialization(InitType.Insert, RequestUserID);
                result = _configResposistory.Create(model);
                if (result > 0)
                {
                    note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
                }
                else
                {
                    note = AppGlobal.Success + " - " + AppGlobal.CreateFail;
                }
            }
            return Json(note);            
        }

        public ActionResult GetTreeMenuDataTransferByCodeToList(string Code)
        {
            var data = _configResposistory.GetByCodeToList(Code);
            var result = data.GenerateTree(item => item.Id, item => item.ParentId);
            return Json(data.GenerateTree(item => item.Id, Item => Item.ParentId));
        }

        public IActionResult GetByCodeToList(string Code)
        {
            return Json(_configResposistory.GetByCodeToList(Code));
        }

        public IActionResult MenuLeft()
        {
            var model = _configResposistory.GetByCodeToList(AppGlobal.MenuLeftCode).OrderBy(item => item.SortOrder).ToList();
            return PartialView("~/Views/Config/_MenuLeft.cshtml", model);
        }
    }
}