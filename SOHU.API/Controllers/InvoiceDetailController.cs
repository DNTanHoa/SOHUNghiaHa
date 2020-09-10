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
    public class InvoiceDetailDetailController : BaseController
    {
        private readonly IInvoiceDetailRepository _invoiceDetailResposistory;

        public InvoiceDetailDetailController(IInvoiceDetailRepository invoiceDetailResposistory)
        {
            _invoiceDetailResposistory = invoiceDetailResposistory;
        }

        [HttpGet]
        public ActionResult<string> Detail(int ID)
        {
            var model = _invoiceDetailResposistory.GetByID(ID) ?? new InvoiceDetail();
            return ObjectToJson(model);
        }

        [HttpGet]
        public ActionResult<string> GetAllToList()
        {
            return ObjectToJson(_invoiceDetailResposistory.GetAllToList());
        }

        [HttpGet]
        public ActionResult<string> GetByID(int ID)
        {
            return ObjectToJson(_invoiceDetailResposistory.GetByID(ID));
        }

        [HttpPost]
        public ActionResult<string> Create(InvoiceDetail model)
        {
            Result routeResult;

            model.Initialization(InitType.Insert, RequestUserID);
            int result = _invoiceDetailResposistory.Create(model);

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
        public ActionResult<string> Update(InvoiceDetail model)
        {
            Result routeResult;

            model.Initialization(InitType.Update, RequestUserID);
            int result = _invoiceDetailResposistory.Update(model.Id, model);

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

            int result = _invoiceDetailResposistory.Delete(ID);

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
        public ActionResult<string> SaveChange(InvoiceDetail model)
        {
            Result routeResult;
            int result = 0;

            if (model.Id > 0)
            {
                model.Initialization(InitType.Update, RequestUserID);
                result = _invoiceDetailResposistory.Update(model.Id, model);

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
                result = _invoiceDetailResposistory.Create(model);

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