using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SOHU.API.ResponseModel;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace SOHU.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductConfigController : BaseController
    {
        private readonly IProductConfigRepository _productConfigResposistory;

        public ProductConfigController(IProductConfigRepository productConfigResposistory)
        {
            _productConfigResposistory = productConfigResposistory;
        }

        [HttpGet]
        public ActionResult<string> Detail(int ID)
        {
            var model = _productConfigResposistory.GetByID(ID) ?? new ProductConfig();
            return ObjectToJson(model);
        }

        [HttpGet]
        public ActionResult<string> GetAllToList()
        {
            return ObjectToJson(_productConfigResposistory.GetAllToList());
        }

        [HttpGet]
        public ActionResult<string> GetByID(int ID)
        {
            return ObjectToJson(_productConfigResposistory.GetByID(ID));
        }

        [HttpPost]
        public ActionResult<string> Create(ProductConfig model)
        {
            Result routeResult;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = _productConfigResposistory.Create(model);
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

            return ObjectToJson(routeResult);
        }

        [HttpPut]
        public ActionResult<string> Update(ProductConfig model)
        {
            Result routeResult;

            model.Initialization(InitType.Update, RequestUserID);
            int result = _productConfigResposistory.Update(model.Id, model);
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

            return ObjectToJson(routeResult);
        }

        [HttpDelete]
        public ActionResult<string> Delete(int ID)
        {
            Result routeResult;

            int result = _productConfigResposistory.Delete(ID);
            if (result > 0)
            {
                routeResult = new Result()
                        .setResultType(ResultType.Success)
                        .setMessage(AppGlobal.DeleteSuccess);
            }
            else
            {
                routeResult = new Result()
                        .setResultType(ResultType.Error)
                        .setErrorType(ErrorType.DeleteError)
                        .setMessage(AppGlobal.DeleteFail);
            }

            return ObjectToJson(routeResult);
        }

        [HttpPost]
        public ActionResult<string> SaveChange(ProductConfig model)
        {
            Result routeResult;
            int result = 0;

            if (model.Id > 0)
            {
                model.Initialization(InitType.Update, RequestUserID);
                result = _productConfigResposistory.Update(model.Id, model);

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
                result = _productConfigResposistory.Create(model);

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

        [HttpGet]
        public ActionResult<string> ProductInfor(int ProductID)
        {
            var model = _productConfigResposistory.GetDataTransfersByProductIDToList(ProductID);
            return ObjectToJson(model);
        }
    }
}