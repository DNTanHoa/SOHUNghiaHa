using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;
using SOHU.MVC.Models;

namespace SOHU.MVC.Controllers
{
    public class MembershipController : BaseController
    {
        private readonly IMembershipRepository _resposistory;

        public MembershipController(IMembershipRepository resposistory)
        {
            _resposistory = resposistory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int ID)
        {
            var model = _resposistory.GetByID(ID) ?? new Membership();
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult GetAllToList()
        {
            return Json(_resposistory.GetAllToList());
        }

        public IActionResult ValidUser(UserLoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                if(_resposistory.IsValid(model.Account, model.Password))
                {
                    var cookie = new CookieOptions();
                    cookie.Expires = AppGlobal.InitDateTime.AddDays(30);
                    Response.Cookies.Append("Account", model.Account, cookie);
                    Response.Cookies.Append("IsRemember", model.IsRemember.ToString(), cookie);
                    Response.Cookies.Append("Password", MD5Helper.EncryptDataMD5(model.Password, AppGlobal.MD5Key), cookie);
                    return Json(AppGlobal.Success + " - " + AppGlobal.RedirectDefault);
                }
                return Json(AppGlobal.Fail + " - " + AppGlobal.LoginFail);
            }   
            else
            {
                return Json(AppGlobal.Fail + " - " + AppGlobal.LoginFail);
            }    
        }

        public IActionResult SaveChange(Membership model)
        {
            string note = AppGlobal.InitString;
            int result = 0;
            if (model.Id > 0)
            {
                model.Initialization(InitType.Update, RequestUserID);
                model.ConcatFullname();
                result = _resposistory.Update(model.Id, model);
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
                model.ConcatFullname();
                model.InitDefaultValue();
                model.EncryptPassword();
                result = _resposistory.Create(model);
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