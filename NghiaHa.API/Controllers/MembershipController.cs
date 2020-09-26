using Microsoft.AspNetCore.Mvc;
using NghiaHa.API.ResponseModel;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;
using SOHU.Data.Results;

namespace NghiaHa.API.Controllers
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

        /// <summary>
        /// CCode ngu vler
        /// </summary>
        /// <param name="model"></param>
        private void Initialization(Membership model)
        {
            if (!string.IsNullOrEmpty(model.FullName))
            {
                model.FullName = model.FullName.Trim();
            }
            if (!string.IsNullOrEmpty(model.Phone))
            {
                model.Phone = model.Phone.Trim();
            }
            if (!string.IsNullOrEmpty(model.Address))
            {
                model.Address = model.Address.Trim();
            }
            if (!string.IsNullOrEmpty(model.TaxCode))
            {
                model.TaxCode = model.TaxCode.Trim();
            }
            if (!string.IsNullOrEmpty(model.CitizenIdentification))
            {
                model.CitizenIdentification = model.CitizenIdentification.Trim();
            }
            if (!string.IsNullOrEmpty(model.Email))
            {
                model.Email = model.Email.Trim();
            }
        }

        [HttpGet]
        public ActionResult<string> CustomerDetail(int ID)
        {
            Membership model = new Membership();

            if (ID > 0)
            {
                model = _membershipRepository.GetByID(ID);
            }
            model.ParentID = AppGlobal.CustomerParentID;

            return ObjectToJson(new BaseResponseModel(model));
        }

        [HttpGet]
        public ActionResult<string> SupplierDetail(int ID)
        {
            Membership model = new Membership();

            if (ID > 0)
            {
                model = _membershipRepository.GetByID(ID);
            }
            model.ParentID = AppGlobal.SupplierParentID;

            return ObjectToJson(new BaseResponseModel(model));
        }

        [HttpGet]
        public ActionResult<string> EmployeeDetail(int ID)
        {
            Membership model = new Membership();

            if (ID > 0)
            {
                model = _membershipRepository.GetByID(ID);
            }
            model.ParentID = AppGlobal.EmployeeParentID;

            return ObjectToJson(new BaseResponseModel(model));
        }

        [HttpGet]
        public ActionResult<string> HeaderInfor()
        {
            var member = _membershipRepository.GetByID(RequestUserID);
            return ObjectToJson(new BaseResponseModel(member));
        }

        [HttpGet]
        public ActionResult<string> SidebarInfor()
        {
            var member = _membershipRepository.GetByID(RequestUserID);
            return ObjectToJson(new BaseResponseModel(member));
        }

        [HttpGet]
        public ActionResult<string> GetByCustomerParentIDToList()
        {
            var data = _membershipRepository.GetByParentIDToList(AppGlobal.CustomerParentID);
            return ObjectToJson(new BaseResponseModel(data));
        }

        [HttpGet]
        public ActionResult<string> GetBySupplierParentIDToList()
        {
            var data = _membershipRepository.GetByParentIDToList(AppGlobal.SupplierParentID);
            return ObjectToJson(new BaseResponseModel(data));
        }

        [HttpGet]
        public ActionResult<string> GetByEmployeeParentIDToList()
        {
            var data = _membershipRepository.GetByParentIDToList(AppGlobal.EmployeeParentID);
            return ObjectToJson(new BaseResponseModel(data));
        }

        [HttpPost]
        public ActionResult<string> SaveCustomer(Membership model)
        {
            int result;

            if (model.ID > 0)
            {
                Initialization(model);
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
                    Initialization(model);
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

            return ObjectToJson(new BaseResponseModel(new { ID = model.ID }, RouteResult));
        }

        [HttpPost]
        public ActionResult<string> SaveSupplier(Membership model)
        {
            int result;

            if (model.ID > 0)
            {
                Initialization(model);
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
                    Initialization(model);
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

            return ObjectToJson(new BaseResponseModel(new { ID = model.ID }, RouteResult));
        }

        [HttpDelete]
        public ActionResult<string> Delete(int ID)
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

            return ObjectToJson(new BaseResponseModel(null, RouteResult));
        }

        [HttpPost]
        public ActionResult<string> SaveEmployee(Membership model)
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
                    Initialization(model);
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
                    Initialization(model);
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

            return ObjectToJson(new BaseResponseModel(null, RouteResult));
        }
    }
}
