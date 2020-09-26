using Microsoft.AspNetCore.Mvc;
using NghiaHa.API.ResponseModel;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.ModelExtensions;
using SOHU.Data.Models;
using SOHU.Data.Repositories;
using SOHU.Data.Results;

namespace NghiaHa.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoicePaymentController : BaseController
    {
        private readonly IInvoicePaymentRepository _invoicePaymentRepository;
        private readonly IInvoiceRepository _invoiceRepository;
       
        public InvoicePaymentController(IInvoiceRepository invoiceRepository, IInvoicePaymentRepository invoicePaymentRepository)
        {
            _invoicePaymentRepository = invoicePaymentRepository;
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet]
        public ActionResult<string> GetByInvoiceIDToList(int invoiceID)
        {
            var data = _invoicePaymentRepository.GetByInvoiceIDToList(invoiceID);
            return ObjectToJson(new BaseResponseModel(data));
        }

        [HttpPost]
        public ActionResult<string> Create(InvoicePayment model, int invoiceID)
        {
            model.InvoiceID = invoiceID;
            model.TrimModel();
            model.Initialization(InitType.Insert, RequestUserID);

            int result = _invoicePaymentRepository.Create(model);

            if (result > 0)
            {
                RouteResult = new SuccessResult(AppGlobal.CreateSuccess);
                _invoiceRepository.InitializationByID(model.InvoiceID.Value);
            }
            else
            {
                RouteResult = new ErrorResult(ErrorType.InsertError, AppGlobal.CreateFail);
            }

            return ObjectToJson(new BaseResponseModel(null, RouteResult));
        }

        [HttpPut]
        public ActionResult<string> Update(InvoicePayment model)
        {
            model.TrimModel();
            model.Initialization(InitType.Update, RequestUserID);

            int result = _invoicePaymentRepository.Update(model.ID, model);

            if (result > 0)
            {
                RouteResult = new SuccessResult(AppGlobal.EditSuccess);
                _invoiceRepository.InitializationByID(model.InvoiceID.Value);
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
            int invoiceID = _invoicePaymentRepository.GetByID(ID).InvoiceID.Value;
            string note = AppGlobal.InitString;

            int result = _invoicePaymentRepository.Delete(ID);

            if (result > 0)
            {
                RouteResult = new SuccessResult(AppGlobal.DeleteSuccess);
                _invoiceRepository.InitializationByID(invoiceID);
            }
            else
            {
                RouteResult = new ErrorResult(ErrorType.DeleteError, AppGlobal.DeleteFail);
            }

            return ObjectToJson(new BaseResponseModel(null, RouteResult));
        }
    }
}
