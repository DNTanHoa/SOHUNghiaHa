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
    public class MembershipPaymentController : BaseController
    {
        private readonly IMembershipPaymentRepository _menbershipPaymentResposistory;

        public MembershipPaymentController(IMembershipPaymentRepository menbershipPaymentResposistory)
        {
            _menbershipPaymentResposistory = menbershipPaymentResposistory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int ID)
        {
            var model = _menbershipPaymentResposistory.GetByID(ID) ?? new MembershipPayment();
            return View(model);
        }

        public IActionResult GetAllToList()
        {
            return Json(_menbershipPaymentResposistory.GetAllToList());
        }

        public IActionResult GetByID(int ID)
        {
            return Json(_menbershipPaymentResposistory.GetByID(ID));
        }

        public IActionResult Create(MembershipPayment model)
        {
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = _menbershipPaymentResposistory.Create(model);
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

        public IActionResult Update(MembershipPayment model)
        {
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _menbershipPaymentResposistory.Update(model.Id, model);
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
            int result = _menbershipPaymentResposistory.Delete(ID);
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

        public IActionResult SaveChange(MembershipPayment model)
        {
            string note = AppGlobal.InitString;
            int result = 0;
            if (model.Id > 0)
            {
                model.Initialization(InitType.Update, RequestUserID);
                result = _menbershipPaymentResposistory.Update(model.Id, model);
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
                result = _menbershipPaymentResposistory.Create(model);
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