using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;
using SOHU.Data.Enum;
using Euronailsupply.CRM.Controllers;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace Euronailsupply.CRM.Web.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int ID)
        {
            var model = customerRepository.GetByID(ID);
            return View(model);
        }

        public IActionResult Create(Customer model)
        {
            model.Initialization(InitType.Insert, RequestUserID);
            string note = AppGlobal.InitString;
            var result = customerRepository.Create(model);
            if(result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }

        public IActionResult Update(Customer model)
        {
            model.Initialization(InitType.Insert, RequestUserID);
            string note = AppGlobal.InitString;
            var result = customerRepository.Update(model.ID, model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.EditFail;
            }
            return Json(note);
        }

        public IActionResult Delete(int ID)
        {
            string note = AppGlobal.InitString;
            var result = customerRepository.Delete(ID);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.DeleteSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.DeleteFail;
            }
            return Json(note);
        }

        public IActionResult GetAllToList([DataSourceRequest]DataSourceRequest request)
        {
            return Json(customerRepository.GetAllToList().ToDataSourceResult(request));
        }
    }
}
