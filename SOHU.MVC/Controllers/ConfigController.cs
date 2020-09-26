using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public ConfigController(IConfigRepository configResposistory)
        {
            _configResposistory = configResposistory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int ID)
        {
            var model = _configResposistory.GetByID(ID) ?? new Config();
            return View(model);
        }

        public IActionResult GetAllToList()
        {
            return Json(_configResposistory.GetAllToList());
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
    }
}