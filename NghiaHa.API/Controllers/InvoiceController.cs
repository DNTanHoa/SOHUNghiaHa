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
using SOHU.Data.Results;

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
            return ObjectToJson(new BaseResponseModel(model));
        }

        [HttpGet]
        public ActionResult<string> InvoiceInput()
        {
            BaseViewModel viewModel = new BaseViewModel();
            viewModel.YearFinance = DateTime.Now.Year;
            viewModel.MonthFinance = DateTime.Now.Month;

            return ObjectToJson(new BaseResponseModel(viewModel));
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

            return ObjectToJson(new BaseResponseModel(model));
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

            return ObjectToJson(new BaseResponseModel(model));
        }

        [HttpDelete]
        public ActionResult<string> Delete(int ID)
        {
            int result = _invoiceRepository.Delete(ID);

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

        [HttpGet]
        public ActionResult<string> GetByID(int ID)
        {
            return ObjectToJson(new BaseResponseModel(_invoiceRepository.GetByID(ID)));
        }

        [HttpGet]
        public ActionResult<string> GetAllToList()
        {
            return ObjectToJson(new BaseResponseModel(_invoiceRepository.GetAllToList()));
        }

        [HttpGet]
        public ActionResult<string> GetByDuAnAndYearAndMonthToList(int year, int month)
        {
            List<Invoice> Invoices = _invoiceRepository.GetByCategoryIDAndYearAndMonthToList(AppGlobal.DuAnID, year, month);
            return ObjectToJson(new BaseResponseModel(Invoices));
        }

        [HttpGet]
        public ActionResult<string> GetByInvoiceInputAndYearAndMonthToList(int year, int month)
        {
            List<Invoice> Invoices = _invoiceRepository.GetByCategoryIDAndYearAndMonthToList(AppGlobal.InvoiceInputID, year, month);
            return ObjectToJson(new BaseResponseModel(Invoices));
        }

        [HttpGet]
        public ActionResult<string> GetInvoiceInputByProductIDToList(int productID)
        {
            List<Invoice> Invoices = _invoiceRepository.GetInvoiceInputByProductIDToList(productID);
            return ObjectToJson(new BaseResponseModel(Invoices));
        }

        [HttpGet]
        public ActionResult<string> GetInvoiceOutputByProductIDToList(int productID)
        {
            List<Invoice> Invoices = _invoiceRepository.GetInvoiceOutputByProductIDToList(productID);
            return ObjectToJson(new BaseResponseModel(Invoices));
        }

        [HttpPost]
        public ActionResult<string> SaveInvoiceInput(Invoice model)
        {
            int result;

            model.SellName = _membershipRepository.GetByID(model.SellID.Value).FullName;
            model.BuyID = AppGlobal.NghiaHaID;
            if (model.ID > 0)
            {
                Initialization(model);
                model.Initialization(InitType.Update, RequestUserID);

                result = _invoiceRepository.Update(model.ID, model);

                if (result > 0)
                {
                    RouteResult = new SuccessResult(AppGlobal.EditSuccess);
                }
                else
                {
                    RouteResult = new ErrorResult(ErrorType.EditError, AppGlobal.EditFail);
                }
            }
            else
            {
                Initialization(model);
                model.Initialization(InitType.Insert, RequestUserID);
                if (_invoiceRepository.IsValidByInvoiceCode(model.InvoiceCode) == true)
                {
                    result = _invoiceRepository.Create(model);

                    if (result > 0)
                    {
                        RouteResult = new SuccessResult(AppGlobal.CreateSuccess);
                    }
                    else
                    {
                        RouteResult = new ErrorResult(ErrorType.InsertError, AppGlobal.CreateFail);
                    }
                }
            }

            return ObjectToJson(new BaseResponseModel(new { ID = model.ID }, RouteResult));
        }

        [HttpPost]
        public ActionResult<string> SaveInvoiceInputWindow(Invoice model)
        {
            int result;

            model.SellName = _membershipRepository.GetByID(model.SellID.Value).FullName;
            
            if (model.ID > 0)
            {
                Initialization(model);
                model.Initialization(InitType.Update, RequestUserID);
                result = _invoiceRepository.Update(model.ID, model);

                if (result > 0)
                {
                    RouteResult = new SuccessResult(AppGlobal.EditSuccess);
                }
                else
                {
                    RouteResult = new ErrorResult(ErrorType.EditError, AppGlobal.EditFail);
                }
            }
            else
            {
                Initialization(model);
                model.Initialization(InitType.Insert, RequestUserID);
                if (_invoiceRepository.IsValidByInvoiceCode(model.InvoiceCode) == true)
                {
                    result = _invoiceRepository.Create(model);

                    if (result > 0)
                    {
                        RouteResult = new SuccessResult(AppGlobal.CreateSuccess);
                    }
                    else
                    {
                        RouteResult = new ErrorResult(ErrorType.InsertError, AppGlobal.CreateFail);
                    }
                }
            }

            return ObjectToJson(new BaseResponseModel(new { ID = model.ID }, RouteResult));
        }
    }
}
