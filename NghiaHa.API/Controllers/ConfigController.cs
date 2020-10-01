using Microsoft.AspNetCore.Mvc;
using SOHU.Data.ModelExtensions;
using NghiaHa.API.ResponseModel;
using SOHU.Data.DataTransferObject;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;
using SOHU.Data.Results;

namespace NghiaHa.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConfigController : BaseController
    {
        private readonly IConfigRepository _configResposistory;
        public ConfigController(IConfigRepository configResposistory)
        {
            _configResposistory = configResposistory;
        }

        [HttpGet]
        public ActionResult<string> GetByCRMAndProductCategoryToTree()
        {
            var data = _configResposistory.GetByCRMAndProductCategoryToTree();
            return ObjectToJson(new BaseResponseModel(data));
        }

        [HttpGet]
        public ActionResult<string> GetDataTransferByParentIDToList(int parentID)
        {
            var data = _configResposistory.GetDataTransferByParentIDToList(parentID);
            return ObjectToJson(new BaseResponseModel(data));
        }

        [HttpGet]
        public ActionResult<BaseResponseModel> GetUnitToList()
        {            
            return new BaseResponseModel(_configResposistory.GetByGroupNameAndCodeToList(AppGlobal.CRM, AppGlobal.Unit));
        }

        [HttpGet]
        public ActionResult<BaseResponseModel> GetProductCategoryToList()
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(AppGlobal.CRM, AppGlobal.ProductCategory);
            return new BaseResponseModel(data);
        }

        [HttpGet]
        public ActionResult<string> GetCustomerCategoryToList()
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(AppGlobal.CRM, AppGlobal.CustomerCategory);
            return ObjectToJson(new BaseResponseModel(data));
        }

        [HttpGet]
        public ActionResult<string> GetInvoiceCategoryToList()
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(AppGlobal.CRM, AppGlobal.InvoiceCategory);
            return ObjectToJson(new BaseResponseModel(data));
        }

        [HttpPost]
        public ActionResult<string> CreateInvoiceCategory(Config model)
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

            return ObjectToJson(new BaseResponseModel(null, RouteResult));
        }

        [HttpPost]
        public ActionResult<string> CreateCustomerCategory(Config model)
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

            return ObjectToJson(new BaseResponseModel(null, RouteResult));
        }

        [HttpPost]
        public ActionResult<string> CreateProductCategory(ConfigDataTransfer model)
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

            return ObjectToJson(new BaseResponseModel(null, RouteResult));
        }

        [HttpPost]
        public ActionResult<string> CreateUnit(Config model)
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

            return ObjectToJson(new BaseResponseModel(null, RouteResult));
        }

        [HttpPut]
        public ActionResult<string> UpdateDataTransfer(ConfigDataTransfer model)
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

            return ObjectToJson(new BaseResponseModel(null, RouteResult));
        }

        [HttpPut]
        public ActionResult<string> Update(Config model)
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

            return ObjectToJson(new BaseResponseModel(null, RouteResult));
        }

        [HttpDelete]
        public ActionResult<string> Delete(int ID)
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

            return ObjectToJson(new BaseResponseModel(null, RouteResult));
        }
    }
}