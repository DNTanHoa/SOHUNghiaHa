using Microsoft.AspNetCore.Mvc;
using SOHU.Data.ModelExtensions;
using NghiaHa.API.ResponseModel;
using SOHU.Data.DataTransferObject;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;
using SOHU.Data.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace NghiaHa.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ConfigController : BaseController
    {
        private readonly IConfigRepository _configResposistory;
        public ConfigController(IConfigRepository configResposistory)
        {
            _configResposistory = configResposistory;
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetByCRMAndProductCategoryToTree()
        {
            var data = _configResposistory.GetByCRMAndProductCategoryToTree();
            return new BaseResponeModel(data);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetDataTransferByParentIDToList(int parentID)
        {
            var data = _configResposistory.GetDataTransferByParentIDToList(parentID);
            return new BaseResponeModel(data);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetUnitToList()
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(AppGlobal.CRM, AppGlobal.Unit);
            return new BaseResponeModel(data);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetProductCategoryToList()
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(AppGlobal.CRM, AppGlobal.ProductCategory);
            return new BaseResponeModel(data);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetCustomerCategoryToList()
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(AppGlobal.CRM, AppGlobal.CustomerCategory);
            return new BaseResponeModel(data);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetInvoiceCategoryToList()
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(AppGlobal.CRM, AppGlobal.InvoiceCategory);
            return new BaseResponeModel(data);
        }

        [HttpPost]
        public ActionResult<BaseResponeModel> CreateInvoiceCategory(Config model)
        {
            model.TrimModel();
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.InvoiceCategory;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;

            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }

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

        [HttpPost]
        public ActionResult<BaseResponeModel> CreateCustomerCategory(Config model)
        {
            model.TrimModel();
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.CustomerCategory;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;

            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }

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

        [HttpPost]
        public ActionResult<BaseResponeModel> CreateProductCategory(ConfigDataTransfer model)
        {
            model.TrimModel();
            model.ParentID = model.Parent.ID;
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.ProductCategory;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;

            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }

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

        [HttpPost]
        public ActionResult<BaseResponeModel> CreateUnit(Config model)
        {
            model.TrimModel();
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.Unit;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;

            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }

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
        public ActionResult<BaseResponeModel> UpdateDataTransfer(ConfigDataTransfer model)
        {
            model.TrimModel();
            model.ParentID = model.Parent.ID;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _configResposistory.Update(model.ID, model);

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

        [HttpPut]
        public ActionResult<BaseResponeModel> Update(Config model)
        {
            model.TrimModel();
            model.Initialization(InitType.Update, RequestUserID);
            int result = _configResposistory.Update(model.ID, model);

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
            int result = _configResposistory.Delete(ID);

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
    }
}