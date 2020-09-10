using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace SOHU.MVC.Controllers
{
    public class ProductConfigController : BaseController
    {
        private readonly IProductConfigRepository _productConfigResposistory;

        public ProductConfigController(IProductConfigRepository productConfigResposistory)
        {
            _productConfigResposistory = productConfigResposistory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int ID)
        {
            var model = _productConfigResposistory.GetByID(ID) ?? new ProductConfig();
            return View(model);
        }

        public IActionResult GetAllToList()
        {
            return Json(_productConfigResposistory.GetAllToList());
        }

        public IActionResult GetByID(int ID)
        {
            return Json(_productConfigResposistory.GetByID(ID));
        }

        public IActionResult Create(ProductConfig model)
        {
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = _productConfigResposistory.Create(model);
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

        public IActionResult Update(ProductConfig model)
        {
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _productConfigResposistory.Update(model.Id, model);
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
            int result = _productConfigResposistory.Delete(ID);
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

        public IActionResult SaveChange(ProductConfig model)
        {
            string note = AppGlobal.InitString;
            int result = 0;
            if (model.Id > 0)
            {
                model.Initialization(InitType.Update, RequestUserID);
                result = _productConfigResposistory.Update(model.Id, model);
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
                result = _productConfigResposistory.Create(model);
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

        public IActionResult ProductInfor(int ProductID)
        {
            var model = _productConfigResposistory.GetDataTransfersByProductIDToList(ProductID);
            return PartialView("~/Views/ProductConfig/_ProductInfor.cshtml", model);
        }
    }
}