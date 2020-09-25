using Microsoft.AspNetCore.Mvc;
using SOHU.Data.DataTransferObject;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

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
            return ObjectToJson(_invoiceDetailRepository.GetDataTransferByInvoiceIDToList(invoiceID));
        }

        [HttpGet]
        public ActionResult<string> GetByInvoiceIDToList(int invoiceID)
        {
            return ObjectToJson(_invoiceDetailRepository.GetByInvoiceIDToList(invoiceID));
        }

        [HttpPost]
        public ActionResult<string> Create(InvoiceDetailDataTransfer model, int invoiceID)
        {
            model.InvoiceID = invoiceID;
            InitializationDataTransfer(model);
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;

            if ((model.ProductID > 0) && (model.UnitID > 0))
            {
                result = _invoiceDetailRepository.Create(model);
            }

            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
                _invoiceRepository.InitializationByID(model.InvoiceID.Value);
                _productRepository.InitializationByID(model.ProductID.Value);
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return ObjectToJson(note);
        }

        [HttpPut]
        public ActionResult<string> Update(InvoiceDetailDataTransfer model)
        {
            InitializationDataTransfer(model);
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _invoiceDetailRepository.Update(model.ID, model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
                _invoiceRepository.InitializationByID(model.InvoiceID.Value);
                _productRepository.InitializationByID(model.ProductID.Value);
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.EditFail;
            }

            return ObjectToJson(note);
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
            string note = AppGlobal.InitString;
            int result = _invoiceDetailRepository.Delete(ID);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.DeleteSuccess;
                _invoiceRepository.InitializationByID(invoiceID.Value);
                _productRepository.InitializationByID(productId.Value);
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.DeleteFail;
            }
            return ObjectToJson(note);
        }
    }
}
