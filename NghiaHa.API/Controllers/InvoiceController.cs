using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NghiaHa.API.Controllers;
using NghiaHa.API.ResponseModel;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace NghiaHa.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceController : BaseController
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMembershipRepository _membershipRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository, IMembershipRepository membershipRepository)
        {
            _invoiceRepository = invoiceRepository;
            _membershipRepository = membershipRepository;
        }
        private void Initialization(Invoice model)
        {
            if (!string.IsNullOrEmpty(model.InvoiceCode))
            {
                model.InvoiceCode = model.InvoiceCode.Trim();
            }
        }

        [HttpGet]
        public ActionResult<string> Detail(int ID)
        {
            var model = _invoiceRepository.GetByID(ID);
            return ObjectToJson(model);
        }

        [HttpGet]
        public ActionResult<string> InvoiceInput()
        {
            BaseViewModel viewModel = new BaseViewModel();
            viewModel.YearFinance = DateTime.Now.Year;
            viewModel.MonthFinance = DateTime.Now.Month;
            return ObjectToJson(viewModel);
        }

        [HttpGet]
        public ActionResult<string> InvoiceInputDetail(int ID)
        {
            Invoice model = new Invoice();
            model.InvoiceCreated = DateTime.Now;
            model.Tax = AppGlobal.Tax;
            model.TotalNoTax = 0;
            model.TotalTax = 0;
            model.Total = 0;
            model.TotalPaid = 0;
            model.TotalDebt = 0;

            if (ID > 0)
            {
                model = _invoiceRepository.GetByID(ID);
            }
            model.CategoryID = AppGlobal.InvoiceInputID;
            return ObjectToJson(model);
        }

        [HttpGet]
        public ActionResult<string> InvoiceInputDetailWindow(int ID)
        {
            Invoice model = new Invoice();
            model.InvoiceCreated = DateTime.Now;
            model.Tax = AppGlobal.Tax;
            model.TotalNoTax = 0;
            model.TotalTax = 0;
            model.Total = 0;
            model.TotalPaid = 0;
            model.TotalDebt = 0;
            if (ID > 0)
            {
                model = _invoiceRepository.GetByID(ID);
            }
            model.CategoryID = AppGlobal.InvoiceInputID;
            return ObjectToJson(model);
        }

        [HttpDelete]
        public ActionResult<string> Delete(int ID)
        {
            string note = AppGlobal.InitString;
            int result = _invoiceRepository.Delete(ID);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.DeleteSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.DeleteFail;
            }
            return ObjectToJson(note);
        }

        [HttpGet]
        public ActionResult<string> GetByID(int ID)
        {
            return ObjectToJson(_invoiceRepository.GetByID(ID));
        }

        [HttpGet]
        public ActionResult<string> GetAllToList()
        {
            return ObjectToJson(_invoiceRepository.GetAllToList());
        }

        [HttpGet]
        public ActionResult<string> GetByDuAnAndYearAndMonthToList(int year, int month)
        {
            return ObjectToJson(_invoiceRepository.GetByCategoryIDAndYearAndMonthToList(AppGlobal.DuAnID, year, month));
        }

        [HttpGet]
        public ActionResult<string> GetByInvoiceInputAndYearAndMonthToList(int year, int month)
        {
            return ObjectToJson(_invoiceRepository.GetByCategoryIDAndYearAndMonthToList(AppGlobal.InvoiceInputID, year, month));
        }

        [HttpGet]
        public ActionResult<string> GetInvoiceInputByProductIDToList(int productID)
        {
            return ObjectToJson(_invoiceRepository.GetInvoiceInputByProductIDToList(productID));
        }

        [HttpGet]
        public ActionResult<string> GetInvoiceOutputByProductIDToList(int productID)
        {
            return ObjectToJson(_invoiceRepository.GetInvoiceOutputByProductIDToList(productID));
        }

        [HttpPost]
        public ActionResult<string> SaveInvoiceInput(Invoice model)
        {
            model.SellName = _membershipRepository.GetByID(model.SellID.Value).FullName;
            model.BuyID = AppGlobal.NghiaHaID;
            if (model.ID > 0)
            {
                Initialization(model);
                model.Initialization(InitType.Update, RequestUserID);
                _invoiceRepository.Update(model.ID, model);
            }
            else
            {
                Initialization(model);
                model.Initialization(InitType.Insert, RequestUserID);
                if (_invoiceRepository.IsValidByInvoiceCode(model.InvoiceCode) == true)
                {
                    _invoiceRepository.Create(model);
                }
            }
            return RedirectToAction("InvoiceInputDetail", new { ID = model.ID });
        }

        [HttpPost]
        public ActionResult<string> SaveInvoiceInputWindow(Invoice model)
        {
            model.SellName = _membershipRepository.GetByID(model.SellID.Value).FullName;
            if (model.ID > 0)
            {
                Initialization(model);
                model.Initialization(InitType.Update, RequestUserID);
                _invoiceRepository.Update(model.ID, model);
            }
            else
            {
                Initialization(model);
                model.Initialization(InitType.Insert, RequestUserID);
                if (_invoiceRepository.IsValidByInvoiceCode(model.InvoiceCode) == true)
                {
                    _invoiceRepository.Create(model);
                }
            }
            return RedirectToAction("InvoiceInputWindow", new { ID = model.ID });
        }
    }
}
