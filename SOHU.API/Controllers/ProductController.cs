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
    public class ProductController : BaseController
    {
        private readonly IProductRepository _productResposistory;

        public ProductController(IProductRepository productResposistory)
        {
            _productResposistory = productResposistory;
        }

        [HttpGet]
        public ActionResult<string> Detail(int ID)
        {
            var model = _productResposistory.GetByID(ID) ?? new Product();
            return ObjectToJson(model);
        }

        [HttpGet]
        public ActionResult<string> GetAllToList()
        {
            return ObjectToJson(_productResposistory.GetAllToList());
        }

        [HttpGet]
        public ActionResult<string> GetByID(int ID)
        {
            return ObjectToJson(_productResposistory.GetByID(ID));
        }

        [HttpPost]
        public ActionResult<string> Create(Product model)
        {
            Result routeResult;

            model.Initialization(InitType.Insert, RequestUserID);
            int result = _productResposistory.Create(model);

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
        public ActionResult<string> Update(Product model)
        {
            Result routeResult;

            model.Initialization(InitType.Update, RequestUserID);
            int result = _productResposistory.Update(model.Id, model);
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

            int result = _productResposistory.Delete(ID);

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
        public ActionResult<string> SaveChange(Product model)
        {
            Result routeResult;
            int result = 0;

            if (model.Id > 0)
            {
                model.Initialization(InitType.Update, RequestUserID);
                result = _productResposistory.Update(model.Id, model);

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
                result = _productResposistory.Create(model);

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
        public ActionResult<string> NewProducts(int PageSize)
        {
            var model = _productResposistory.GetAllToList().OrderBy(item => item.DateCreated).Take(PageSize);
            return ObjectToJson(model);
        }

        [HttpGet]
        public ActionResult<string> TopProducts(int PageSize)
        {
            var model = _productResposistory.GetAllToList().OrderBy(item => item.DateCreated).Take(PageSize);
            return ObjectToJson(model);
        }

        [HttpGet]
        public ActionResult<string> SaleProducts(int PageSize)
        {
            var model = _productResposistory.GetAllToList().OrderBy(item => item.DateCreated).Take(PageSize);
            return ObjectToJson(model);
        }
    }
}