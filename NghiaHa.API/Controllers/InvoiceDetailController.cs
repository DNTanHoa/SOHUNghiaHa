using Microsoft.AspNetCore.Mvc;
using NghiaHa.API.ResponseModel;
using SOHU.Data.DataTransferObject;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;
using SOHU.Data.Results;
using System.Collections.Generic;

namespace NghiaHa.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceDetailController : BaseController
    {
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;
        private readonly IInvoiceRepository _invoiceRepository;

        private readonly IProductRepository _productRepository;
        public InvoiceDetailController(IInvoiceRepository invoiceRepository, IInvoiceDetailRepository invoiceDetailRepository, IProductRepository productRepository)
        {
            _invoiceDetailRepository = invoiceDetailRepository;
            _invoiceRepository = invoiceRepository;
            _productRepository = productRepository;
        }
        private void InitializationDataTransfer(InvoiceDetailDataTransfer model)
        {
            model.ProductID = model.Product.ID;
            model.UnitID = model.Unit.ID;
            model.Total = model.UnitPrice * model.Quantity;
            model.Total01 = model.UnitPrice * model.Quantity01;
        }

        [HttpGet]
        public ActionResult<string> GetDataTransferByInvoiceIDToList(int invoiceID)
        {
            var data = _invoiceDetailRepository.GetDataTransferByInvoiceIDToList(invoiceID);
            return ObjectToJson(new BaseResponseModel(data));
        }

        [HttpGet]
        public ActionResult<string> GetByInvoiceIDToList(int invoiceID)
        {
            var data = _invoiceDetailRepository.GetByInvoiceIDToList(invoiceID);
            return ObjectToJson(new BaseResponseModel(data));
        }

        [HttpPost]
        public ActionResult<string> Create(InvoiceDetailDataTransfer model, int invoiceID)
        {
            model.InvoiceID = invoiceID;
            InitializationDataTransfer(model);
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;

            if ((model.ProductID > 0) && (model.UnitID > 0))
            {
                result = _invoiceDetailRepository.Create(model);
            }

            if (result > 0)
            {
                RouteResult = new SuccessResult(AppGlobal.CreateSuccess);

                _invoiceRepository.InitializationByID(model.InvoiceID.Value);
                _productRepository.InitializationByID(model.ProductID.Value);
            }
            else
            {
                RouteResult = new ErrorResult(ErrorType.InsertError, AppGlobal.CreateFail);
            }

            return ObjectToJson(new BaseResponseModel(null, RouteResult));
        }

        [HttpPut]
        public ActionResult<string> Update(InvoiceDetailDataTransfer model)
        {
            InitializationDataTransfer(model);
            model.Initialization(InitType.Update, RequestUserID);
            int result = _invoiceDetailRepository.Update(model.ID, model);
            if (result > 0)
            {
                RouteResult = new SuccessResult(AppGlobal.EditSuccess);
                _invoiceRepository.InitializationByID(model.InvoiceID.Value);
                _productRepository.InitializationByID(model.ProductID.Value);
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
            InvoiceDetail invoiceDetail = _invoiceDetailRepository.GetByID(ID);
            int? invoiceID = 0;
            int? productId = 0;

            if (invoiceDetail != null)
            {
                invoiceID = invoiceDetail.InvoiceID.Value;
                productId = invoiceDetail.ProductID.Value;
            }

            int result = _invoiceDetailRepository.Delete(ID);

            if (result > 0)
            {
                RouteResult = new SuccessResult(AppGlobal.DeleteSuccess);
                _invoiceRepository.InitializationByID(invoiceID.Value);
                _productRepository.InitializationByID(productId.Value);
            }
            else
            {
                RouteResult = new ErrorResult(ErrorType.DeleteError, AppGlobal.DeleteFail);
            }

            return ObjectToJson(new BaseResponseModel(null, RouteResult));
        }
    }
}
