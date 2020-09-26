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
    public class ProductController : BaseController
    {
        private readonly IProductRepository _productResposistory;

        public ProductController(IProductRepository productResposistory)
        {
            _productResposistory = productResposistory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int ID)
        {
            var model = _productResposistory.GetByID(ID) ?? new Product();
            return View(model);
        }

        public IActionResult GetAllToList()
        {
            return Json(_productResposistory.GetAllToList());
        }

        public IActionResult GetByID(int ID)
        {
            return Json(_productResposistory.GetByID(ID));
        }

        public IActionResult Create(Product model)
        {
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = _productResposistory.Create(model);
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

        public IActionResult Update(Product model)
        {
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _productResposistory.Update(model.Id, model);
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
            int result = _productResposistory.Delete(ID);
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

        public IActionResult SaveChange(Product model)
        {
            string note = AppGlobal.InitString;
            int result = 0;
            if (model.Id > 0)
            {
                model.Initialization(InitType.Update, RequestUserID);
                result = _productResposistory.Update(model.Id, model);
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
                result = _productResposistory.Create(model);
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

        public IActionResult NewProducts(int PageSize)
        {
            var model = _productResposistory.GetAllToList().OrderBy(item => item.DateCreated).Take(PageSize);
            return PartialView("~/Views/Product/_List.cshtml", model);
        }

        public IActionResult TopProducts(int PageSize)
        {
            var model = _productResposistory.GetAllToList().OrderBy(item => item.DateCreated).Take(PageSize);
            return PartialView("~/Views/Product/_List.cshtml", model);
        }

        public IActionResult SaleProducts(int PageSize)
        {
            var model = _productResposistory.GetAllToList().OrderBy(item => item.DateCreated).Take(PageSize);
            return PartialView("~/Views/Product/_List.cshtml", model);
        }
    }
}