using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NghiaHa.API.ResponseModel;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.ModelExtensions;
using SOHU.Data.Models;
using SOHU.Data.Repositories;
using SOHU.Data.Results;
using System;
using System.Collections.Generic;
using System.Net;

namespace NghiaHa.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InvoiceController : BaseController
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMembershipRepository _membershipRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository, IMembershipRepository membershipRepository)
        {
            _invoiceRepository = invoiceRepository;
            _membershipRepository = membershipRepository;
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> Detail(int ID)
        {
            var model = _invoiceRepository.GetByID(ID);
            return new BaseResponeModel(model);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> InvoiceInput()
        {
            BaseViewModel viewModel = new BaseViewModel();
            viewModel.YearFinance = DateTime.Now.Year;
            viewModel.MonthFinance = DateTime.Now.Month;

            return new BaseResponeModel(viewModel);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> InvoiceInputDetail(int ID)
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

            return new BaseResponeModel(model);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> InvoiceInputDetailWindow(int ID)
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

            return new BaseResponeModel(model);
        }

        [HttpDelete]
        public ActionResult<BaseResponeModel> Delete(int ID)
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

            return new BaseResponeModel(null, RouteResult);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetByID(int ID)
        {
            return new BaseResponeModel(_invoiceRepository.GetByID(ID));
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetAllToList()
        {
            return new BaseResponeModel(_invoiceRepository.GetAllToList());
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetByDuAnAndYearAndMonthToList([FromBody]int year, int month)
        {
            List<Invoice> Invoices = _invoiceRepository.GetByCategoryIDAndYearAndMonthToList(AppGlobal.DuAnID, year, month);
            return new BaseResponeModel(Invoices);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetByInvoiceInputAndYearAndMonthToList(int year, int month)
        {
            List<Invoice> Invoices = _invoiceRepository.GetByCategoryIDAndYearAndMonthToList(AppGlobal.InvoiceInputID, year, month);
            return new BaseResponeModel(Invoices);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetInvoiceInputByProductIDToList(int productID)
        {
            List<Invoice> Invoices = _invoiceRepository.GetInvoiceInputByProductIDToList(productID);
            return new BaseResponeModel(Invoices);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetInvoiceOutputByProductIDToList(int productID)
        {
            List<Invoice> Invoices = _invoiceRepository.GetInvoiceOutputByProductIDToList(productID);
            return new BaseResponeModel(Invoices);
        }

        [HttpPost]
        public ActionResult<BaseResponeModel> SaveInvoiceInput(Invoice model)
        {
            int result;

            model.SellName = _membershipRepository.GetByID(model.SellID.Value).FullName;
            model.BuyID = AppGlobal.NghiaHaID;
            if (model.ID > 0)
            {
                model.TrimInvoiceCode();
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
                model.TrimInvoiceCode();
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

            return new BaseResponeModel(new { ID = model.ID }, RouteResult);
        }

        [HttpPost]
        public ActionResult<BaseResponeModel> SaveInvoiceInputWindow(Invoice model)
        {
            int result;

            model.SellName = _membershipRepository.GetByID(model.SellID.Value).FullName;
            
            if (model.ID > 0)
            {
                model.TrimInvoiceCode();
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
                model.TrimInvoiceCode();
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

            return new BaseResponeModel(new { ID = model.ID }, RouteResult);
        }
    }
}
