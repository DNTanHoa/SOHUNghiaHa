using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NghiaHa.API.RequestModel;
using NghiaHa.API.ResponseModel;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.ModelExtensions;
using SOHU.Data.Models;
using SOHU.Data.Providers;
using SOHU.Data.Repositories;
using SOHU.Data.Results;
using System;
using System.Linq;

namespace NghiaHa.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MembershipController : BaseController
    {
        private readonly IMembershipRepository _membershipRepository;

        public MembershipController(IMembershipRepository membershipRepository)
        {
            _membershipRepository = membershipRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<BaseResponeModel> Login(UserLoginViewModel model)
        {
            var LoginResponeModel = new LoginResponeModel();
            BaseResult Result;

            if (ModelState.IsValid)
            {
                if (_membershipRepository.IsValid(model.Account, model.Password))
                {
                    Result = new SuccessResult();
                    LoginResponeModel.TokenExpireDate = DateTime.Now.AddDays(1);
                    LoginResponeModel.Token = TokenProvider.GenerateTokenString(model.ToDictionaryStringString());
                }
                else
                {
                    Result = new ErrorResult(ErrorType.LoginError,AppGlobal.LoginFail);
                }
            }
            else
            {
                string message = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                Result = new ErrorResult(ErrorType.LoginError, message);
            }

            return new BaseResponeModel(LoginResponeModel, Result);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> CustomerDetail(int ID)
        {
            Membership model = new Membership();

            if (ID > 0)
            {
                model = _membershipRepository.GetByID(ID);
            }
            model.ParentID = AppGlobal.CustomerParentID;

            return new BaseResponeModel(model);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> SupplierDetail(int ID)
        {
            Membership model = new Membership();

            if (ID > 0)
            {
                model = _membershipRepository.GetByID(ID);
            }
            model.ParentID = AppGlobal.SupplierParentID;

            return new BaseResponeModel(model);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> EmployeeDetail(int ID)
        {
            Membership model = new Membership();

            if (ID > 0)
            {
                model = _membershipRepository.GetByID(ID);
            }
            model.ParentID = AppGlobal.EmployeeParentID;

            return new BaseResponeModel(model);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> HeaderInfor()
        {
            var member = _membershipRepository.GetByID(RequestUserID);
            return new BaseResponeModel(member);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> SidebarInfor()
        {
            var member = _membershipRepository.GetByID(RequestUserID);
            return new BaseResponeModel(member);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetByCustomerParentIDToList()
        {
            var data = _membershipRepository.GetByParentIDToList(AppGlobal.CustomerParentID);
            return new BaseResponeModel(data);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetBySupplierParentIDToList()
        {
            var data = _membershipRepository.GetByParentIDToList(AppGlobal.SupplierParentID);
            return new BaseResponeModel(data);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetByEmployeeParentIDToList()
        {
            var data = _membershipRepository.GetByParentIDToList(AppGlobal.EmployeeParentID);
            return new BaseResponeModel(data);
        }

        [HttpPost]
        public ActionResult<BaseResponeModel> SaveCustomer(Membership model)
        {
            int result;

            if (model.ID > 0)
            {
                model.TrimModel();
                model.Initialization(InitType.Update, RequestUserID);

                result = _membershipRepository.Update(model.ID, model);

                if (result > 0)
                {
                    RouteResult = new SuccessResult(AppGlobal.EditSuccess);
                }
                else
                {
                    RouteResult = new ErrorResult(ErrorType.EditError, AppGlobal.EditFail);
                }
            }
            else
            {
                bool check = false;
                if ((model.ParentID == AppGlobal.CustomerParentID) || (model.ParentID == AppGlobal.SupplierParentID))
                {
                    check = _membershipRepository.IsValidByTaxCode(model.TaxCode);
                }

                if (model.ParentID == AppGlobal.EmployeeParentID)
                {
                    check = _membershipRepository.IsValidByCitizenIdentification(model.CitizenIdentification);
                }

                if (check == true)
                {
                    model.TrimModel();
                    model.SetDefaultValue(); //set default username, password
                    model.Initialization(InitType.Insert, RequestUserID);

                    result = _membershipRepository.Create(model);

                    if (result > 0)
                    {
                        RouteResult = new SuccessResult(AppGlobal.CreateSuccess);
                    }
                    else
                    {
                        RouteResult = new ErrorResult(ErrorType.InsertError, AppGlobal.CreateFail);
                    }
                }
            }

            return new BaseResponeModel(new { ID = model.ID }, RouteResult);
        }

        [HttpPost]
        public ActionResult<BaseResponeModel> SaveSupplier(Membership model)
        {
            int result;

            if (model.ID > 0)
            {
                model.TrimModel();
                model.Initialization(InitType.Update, RequestUserID);

                result = _membershipRepository.Update(model.ID, model);

                if (result > 0)
                {
                    RouteResult = new SuccessResult(AppGlobal.EditSuccess);
                }
                else
                {
                    RouteResult = new ErrorResult(ErrorType.EditError, AppGlobal.EditFail);
                }
            }
            else
            {
                bool check = false;
                if ((model.ParentID == AppGlobal.CustomerParentID) || (model.ParentID == AppGlobal.SupplierParentID))
                {
                    check = _membershipRepository.IsValidByTaxCode(model.TaxCode);
                }
                if (model.ParentID == AppGlobal.EmployeeParentID)
                {
                    check = _membershipRepository.IsValidByCitizenIdentification(model.CitizenIdentification);
                }
                if (check == true)
                {
                    model.TrimModel();
                    model.Initialization(InitType.Insert, RequestUserID);

                    result = _membershipRepository.Create(model);

                    if (result > 0)
                    {
                        RouteResult = new SuccessResult(AppGlobal.CreateSuccess);
                    }
                    else
                    {
                        RouteResult = new ErrorResult(ErrorType.InsertError, AppGlobal.CreateFail);
                    }
                }
            }

            return new BaseResponeModel(new { ID = model.ID }, RouteResult);
        }

        [HttpDelete]
        public ActionResult<BaseResponeModel> Delete(int ID)
        {
            int result = _membershipRepository.Delete(ID);

            if (result > 0)
            {
                RouteResult = new SuccessResult(AppGlobal.DeleteSuccess);
            }
            else
            {
                RouteResult = new ErrorResult(ErrorType.DeleteError, AppGlobal.DeleteFail);
            }

            return new BaseResponeModel(null, RouteResult);
        }

        [HttpPost]
        public ActionResult<BaseResponeModel> SaveEmployee(Membership model)
        {
            bool check = false;

            if ((model.ParentID == AppGlobal.CustomerParentID) || (model.ParentID == AppGlobal.SupplierParentID))
            {
                check = _membershipRepository.IsValidByTaxCode(model.TaxCode);
            }

            if (model.ParentID == AppGlobal.EmployeeParentID)
            {
                check = _membershipRepository.IsValidByPhone(model.Phone);
            }

            if (check == true)
            {
                if (model.ID > 0)
                {
                    model.TrimModel();
                    model.Initialization(InitType.Update, RequestUserID);

                    int result = _membershipRepository.Update(model.ID, model);

                    if (result > 0)
                    {
                        RouteResult = new SuccessResult(AppGlobal.EditSuccess);
                    }
                    else
                    {
                        RouteResult = new ErrorResult(ErrorType.EditError, AppGlobal.EditFail);
                    }
                }
                else
                {
                    model.TrimModel();
                    model.SetDefaultValue();//set default username, password
                    model.Initialization(InitType.Insert, RequestUserID);

                    int result = _membershipRepository.Create(model);

                    if (result > 0)
                    {
                        RouteResult = new SuccessResult(AppGlobal.CreateSuccess);
                    }
                    else
                    {
                        RouteResult = new ErrorResult(ErrorType.InsertError, AppGlobal.CreateFail);
                    }
                }
            }

            return new BaseResponeModel(null, RouteResult);
        }
    }
}
