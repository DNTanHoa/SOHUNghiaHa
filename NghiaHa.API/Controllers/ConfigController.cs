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

        [HttpGet]
        public ActionResult<string> GetByCRMAndProductCategoryToTree()
        {
            var data = _configResposistory.GetByCRMAndProductCategoryToTree();
            return ObjectToJson(data);
        }

        [HttpGet]
        public ActionResult<string> GetDataTransferByParentIDToList(int parentID)
        {
            var data = _configResposistory.GetDataTransferByParentIDToList(parentID);
            return ObjectToJson(data);
        }

        [HttpGet]
        public ActionResult<string> GetUnitToList()
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(AppGlobal.CRM, AppGlobal.Unit);
            return ObjectToJson(data);
        }

        [HttpGet]
        public ActionResult<string> GetProductCategoryToList()
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(AppGlobal.CRM, AppGlobal.ProductCategory);
            return ObjectToJson(data);
        }

        [HttpGet]
        public ActionResult<string> GetCustomerCategoryToList()
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(AppGlobal.CRM, AppGlobal.CustomerCategory);
            return ObjectToJson(data);
        }

        [HttpGet]
        public ActionResult<string> GetInvoiceCategoryToList()
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(AppGlobal.CRM, AppGlobal.InvoiceCategory);
            return ObjectToJson(data);
        }

        [HttpPost]
        public ActionResult<string> CreateInvoiceCategory(Config model)
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
            return ObjectToJson(note);
        }

        [HttpPost]
        public ActionResult<string> CreateCustomerCategory(Config model)
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
            return ObjectToJson(note);
        }

        [HttpPost]
        public ActionResult<string> CreateProductCategory(ConfigDataTransfer model)
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
            return ObjectToJson(note);
        }

        [HttpPost]
        public ActionResult<string> CreateUnit(Config model)
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
            return ObjectToJson(note);
        }

        [HttpPut]
        public ActionResult<string> UpdateDataTransfer(ConfigDataTransfer model)
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
            return ObjectToJson(note);
        }

        [HttpPut]
        public ActionResult<string> Update(Config model)
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
            return ObjectToJson(note);
        }

        [HttpDelete]
        public ActionResult<string> Delete(int ID)
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
            return ObjectToJson(note);
        }
    }
}