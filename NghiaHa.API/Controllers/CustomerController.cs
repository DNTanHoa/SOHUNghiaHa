using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NghiaHa.API.ResponseModel;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;
using SOHU.Data.Results;
using System.Net;

namespace NghiaHa.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerController : BaseController
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> Detail(int ID)
        {
            var model = customerRepository.GetByID(ID);
            return new BaseResponeModel(model);
        }

        [HttpPost]
        public ActionResult<BaseResponeModel> Create(Customer model)
        {
            model.Initialization(InitType.Insert, RequestUserID);
            int result = customerRepository.Create(model);

            if (result > 0)
            {
                RouteResult = new SuccessResult(AppGlobal.CreateSuccess);
            }
            else
            {
                RouteResult = new ErrorResult(ErrorType.InsertError, AppGlobal.CreateFail);
            }

            return new BaseResponeModel(null, RouteResult);
        }

        [HttpPut]
        public ActionResult<BaseResponeModel> Update(Customer model)
        {
            model.Initialization(InitType.Insert, RequestUserID);
            var result = customerRepository.Update(model.ID, model);

            if (result > 0)
            {
                RouteResult = new SuccessResult(AppGlobal.EditSuccess);
            }
            else
            {
                RouteResult = new ErrorResult(ErrorType.EditError, AppGlobal.EditFail);
            }

            return new BaseResponeModel(null, RouteResult);
        }

        [HttpDelete]
        public ActionResult<BaseResponeModel> Delete(int ID)
        {
            string note = AppGlobal.InitString;
            var result = customerRepository.Delete(ID);

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

        [HttpGet]
        public ActionResult<BaseResponeModel> GetAllToList()
        {
            return new BaseResponeModel(customerRepository.GetAllToList());
        }
    }
}
