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
    public class InvoiceController : BaseController
    {
        private readonly IInvoiceRepository _invoiceResposistory;

        public InvoiceController(IInvoiceRepository invoiceResposistory)
        {
            _invoiceResposistory = invoiceResposistory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int ID)
        {
            var model = _invoiceResposistory.GetByID(ID) ?? new Invoice();
            return View(model);
        }

        public IActionResult GetAllToList()
        {
            return Json(_invoiceResposistory.GetAllToList());
        }

        public IActionResult GetByID(int ID)
        {
            return Json(_invoiceResposistory.GetByID(ID));
        }

        public IActionResult Create(Invoice model)
        {
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = _invoiceResposistory.Create(model);
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

        public IActionResult Update(Invoice model)
        {
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _invoiceResposistory.Update(model.Id, model);
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
            int result = _invoiceResposistory.Delete(ID);
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

        public IActionResult SaveChange(Invoice model)
        {
            string note = AppGlobal.InitString;
            int result = 0;
            if (model.Id > 0)
            {
                model.Initialization(InitType.Update, RequestUserID);
                result = _invoiceResposistory.Update(model.Id, model);
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
                result = _invoiceResposistory.Create(model);
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
    }
}