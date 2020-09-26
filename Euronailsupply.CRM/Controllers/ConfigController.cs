using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Euronailsupply.CRM.Controllers;
using SOHU.Data.DataTransferObject;
using SOHU.Data.Enum;
using SOHU.Data.Extensions;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace Euronailsupply.CRM.Web.Controllers
{
    public class ConfigController : BaseController
    {
        private readonly IConfigRepository _configResposistory;
        public ConfigController(IConfigRepository configResposistory)
        {
            _configResposistory = configResposistory;
        }
        private void Initialization(Config model)
        {
            if (!string.IsNullOrEmpty(model.CodeName))
            {
                model.CodeName = model.CodeName.Trim();
            }
            if (!string.IsNullOrEmpty(model.Note))
            {
                model.Note = model.Note.Trim();
            }
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Unit()
        {
            return View();
        }
        public IActionResult ProductCategory()
        {
            return View();
        }
        public IActionResult CustomerCategory()
        {
            return View();
        }
        public IActionResult InvoiceCategory()
        {
            return View();
        }
        public ActionResult GetByCRMAndProductCategoryToTree([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByCRMAndProductCategoryToTree();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferByParentIDToList([DataSourceRequest] DataSourceRequest request, int parentID)
        {
            var data = _configResposistory.GetDataTransferByParentIDToList(parentID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetUnitToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(AppGlobal.CRM, AppGlobal.Unit);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetProductCategoryToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(AppGlobal.CRM, AppGlobal.ProductCategory);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCustomerCategoryToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(AppGlobal.CRM, AppGlobal.CustomerCategory);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetInvoiceCategoryToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(AppGlobal.CRM, AppGlobal.InvoiceCategory);
            return Json(data.ToDataSourceResult(request));
        }
        public IActionResult CreateInvoiceCategory(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.InvoiceCategory;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateCustomerCategory(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.CustomerCategory;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateProductCategory(ConfigDataTransfer model)
        {
            Initialization(model);
            model.ParentID = model.Parent.ID;
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.ProductCategory;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateUnit(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.Unit;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult UpdateDataTransfer(ConfigDataTransfer model)
        {
            Initialization(model);
            model.ParentID = model.Parent.ID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _configResposistory.Update(model.ID, model);
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
        public IActionResult Update(Config model)
        {
            Initialization(model);
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _configResposistory.Update(model.ID, model);
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
            int result = _configResposistory.Delete(ID);
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
    }
}