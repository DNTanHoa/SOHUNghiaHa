using Microsoft.AspNetCore.Mvc;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

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
        private void Initialization(InvoicePayment model)
        {
            if (!string.IsNullOrEmpty(model.FullName))
            {
                model.FullName = model.FullName.Trim();
            }
            if (!string.IsNullOrEmpty(model.Phone))
            {
                model.Phone = model.Phone.Trim();
            }
        }

        [HttpGet]
        public ActionResult<string> GetByInvoiceIDToList(int invoiceID)
        {
            return ObjectToJson(_invoicePaymentRepository.GetByInvoiceIDToList(invoiceID));
        }

        [HttpPost]
        public ActionResult<string> Create(InvoicePayment model, int invoiceID)
        {
            model.InvoiceID = invoiceID;
            Initialization(model);
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            result = _invoicePaymentRepository.Create(model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
                _invoiceRepository.InitializationByID(model.InvoiceID.Value);
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return ObjectToJson(note);
        }

        [HttpPut]
        public ActionResult<string> Update(InvoicePayment model)
        {
            Initialization(model);
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _invoicePaymentRepository.Update(model.ID, model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
                _invoiceRepository.InitializationByID(model.InvoiceID.Value);
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
            int invoiceID = _invoicePaymentRepository.GetByID(ID).InvoiceID.Value;
            string note = AppGlobal.InitString;
            int result = _invoicePaymentRepository.Delete(ID);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.DeleteSuccess;
                _invoiceRepository.InitializationByID(invoiceID);
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.DeleteFail;
            }
            return ObjectToJson(note);
        }
    }
}
