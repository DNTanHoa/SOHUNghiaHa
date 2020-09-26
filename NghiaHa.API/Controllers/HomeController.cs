using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NghiaHa.API.RequestModel;
using NghiaHa.API.ResponseModel;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;
using SOHU.Data.Results;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NghiaHa.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IConfigRepository _configResposistory;
        public HomeController(ILogger<HomeController> logger, IConfigRepository configResposistory, IMembershipRepository membershipRepository)
        {
            _logger = logger;
            _membershipRepository = membershipRepository;
            _configResposistory = configResposistory;
        }

        [HttpPost]
        public ActionResult<string> Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_membershipRepository.IsValid(model.Account, model.Password))
                {
                    var cookie = new CookieOptions();
                    cookie.Expires = AppGlobal.InitDateTime.AddDays(30);
                    var member = _membershipRepository.GetByAccount(model.Account);
                    //Response.Cookies.Append("UserID", member.ID.ToString(), cookie);
                    //Response.Cookies.Append("RememberPassword", model.RememberPassword.ToString(), cookie);
                    //Response.Cookies.Append("Password", MD5Helper.EncryptDataMD5(model.Password, AppGlobal.MD5Key), cookie);
                    //Response.Cookies.Append("IsLogin", "True", cookie);

                    RouteResult = new SuccessResult();
                }
                else
                {
                    RouteResult = new ErrorResult(ErrorType.LoginError, AppGlobal.Loading);
                }
            }
            else
            {
                RouteResult = new ErrorResult(ErrorType.LoginError, AppGlobal.Loading);
            }

            return ObjectToJson(new BaseResponseModel(null, RouteResult));
        }

        [HttpGet]
        public ActionResult<string> GetMenuLeft()
        {            
            List<Config> listProductCategory = _configResposistory.GetByGroupNameAndCodeToList(AppGlobal.CRM, AppGlobal.ProductCategory);
            return ObjectToJson(new BaseResponseModel(listProductCategory));
        }
    }
}
