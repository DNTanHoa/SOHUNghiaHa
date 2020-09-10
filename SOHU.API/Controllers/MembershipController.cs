using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SOHU.API.RequestModel;
using SOHU.API.ResponseModel;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace SOHU.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MembershipController : BaseController
    {
        private readonly IMembershipRepository _membershipRepository;

        public MembershipController(IMembershipRepository membershipRepository)
        {
            _membershipRepository = membershipRepository;
        }

        [HttpPost]
        public ActionResult<string> CheckUser(MembershipLoginRequestModel model)
        {
            string result = AppGlobal.InitString;
            if (_membershipRepository.IsValid(model.Account, model.Password))
            {
                var member = _membershipRepository.GetByAccount(model.Account);
                Token token = new Token
                {
                    CurrentDatetime = DateTime.Now,
                    Key = TokenHelper.GenerateString(member),
                    ExpireMinute = AppGlobal.TokenExpireMinute,
                };

                return ObjectToJson(token);
            }
            return Content(result);
        }

        [HttpGet]
        public ActionResult<string> GetDetail(string userName)
        {
            var user = _membershipRepository.GetByAccount(userName) ?? new Membership();

            return ObjectToJson(user);
        }

        [HttpPost]
        public ActionResult<string> SaveChange(Membership model)
        {
            Result routeResult;
            int result = 0;

            if (model.Id > 0)
            {
                model.Initialization(InitType.Update, RequestUserID);

                //concat first + lastname
                _membershipRepository.InitBeforeSave(model, InitType.Update);

                result = _membershipRepository.Update(model.Id, model);

                if (result > 0)
                {
                    routeResult = new Result()
                        .setResultType(ResultType.Success)
                        .setMessage(AppGlobal.EditSuccess);
                }
                else
                {
                    routeResult = new Result()
                        .setResultType(ResultType.Error)
                        .setErrorType(ErrorType.EditError)
                        .setMessage(AppGlobal.EditFail);
                }
            }
            else
            {
                model.Initialization(InitType.Insert, RequestUserID);

                //ConcatFullname, InitDefaultValue, EncryptPassword;
                _membershipRepository.InitBeforeSave(model, InitType.Insert);

                result = _membershipRepository.Create(model);
                if (result > 0)
                {
                    routeResult = new Result()
                        .setResultType(ResultType.Success)
                        .setMessage(AppGlobal.CreateSuccess);
                }
                else
                {
                    routeResult = new Result()
                        .setResultType(ResultType.Error)
                        .setErrorType(ErrorType.InsertError)
                        .setMessage(AppGlobal.CreateFail);
                }
            }

            return ObjectToJson(routeResult);
        }

        

    }
}
