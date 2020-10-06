using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace Euronailsupply.Controllers
{
    public class MembershipController : BaseController
    {
        private readonly IMembershipRepository _membershipRepository;

        public MembershipController(IMembershipRepository membershipRepository)
        {
            _membershipRepository = membershipRepository;
        }


        private void Initialization(Membership model, int action)
        {
            
            if (!string.IsNullOrEmpty(model.FullName))
            {
                model.FullName = model.FullName.Trim();
            }
            if (!string.IsNullOrEmpty(model.Phone))
            {
                model.Phone = model.Phone.Trim();
            }
            if (!string.IsNullOrEmpty(model.Address))
            {
                model.Address = model.Address.Trim();
            }                        
            if (!string.IsNullOrEmpty(model.Email))
            {
                model.Email = model.Email.Trim();
            }
            if (string.IsNullOrEmpty(model.Account))
            {
                model.Account = model.Phone;
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                model.Password = "0";
            }
            switch (action)
            {
                case 0:
                    model.InitDefaultValue();
                    model.EncryptPassword();
                    break;
                case 1:
                    Membership model001 = _membershipRepository.GetByID(model.ID);
                    if (model001 != null)
                    {
                        if (model.Password != model001.Password)
                        {
                            model.InitDefaultValue();
                            model.EncryptPassword();
                        }
                    }
                    break;
            }
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Customer()
        {
            return View();
        }
        public IActionResult Employee()
        {
            return View();
        }
        public IActionResult Supplier()
        {
            return View();
        }
        public IActionResult CustomerDetail(int ID)
        {
            Membership model = new Membership();
            if (ID > 0)
            {
                model = _membershipRepository.GetByID(ID);
            }
            model.ParentID = AppGlobal.CustomerParentID;
            return View(model);
        }
        public IActionResult SupplierDetail(int ID)
        {
            Membership model = new Membership();
            if (ID > 0)
            {
                model = _membershipRepository.GetByID(ID);
            }
            model.ParentID = AppGlobal.SupplierParentID;
            return View(model);
        }
        public IActionResult EmployeeDetail(int ID)
        {
            Membership model = new Membership();
            if (ID > 0)
            {
                model = _membershipRepository.GetByID(ID);
            }
            model.ParentID = AppGlobal.EmployeeParentID;
            return View(model);
        }
        public ActionResult HeaderInfor()
        {
            var member = _membershipRepository.GetByID(RequestUserID);
            return PartialView("~/Views/Membership/_HeaderInfor.cshtml", member);
        }

        public ActionResult SidebarInfor()
        {
            var member = _membershipRepository.GetByID(RequestUserID);
            return PartialView("~/Views/Membership/_SidebarInfor.cshtml", member);
        }
        public ActionResult GetByCustomerParentIDToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _membershipRepository.GetByParentIDToList(AppGlobal.CustomerParentID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetBySupplierParentIDToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _membershipRepository.GetByParentIDToList(AppGlobal.SupplierParentID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetByEmployeeParentIDToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _membershipRepository.GetByParentIDToList(AppGlobal.EmployeeParentID);
            return Json(data.ToDataSourceResult(request));
        }
        public IActionResult SaveCustomer(Membership model)
        {
            if (model.ID > 0)
            {
                Initialization(model, 1);
                model.Initialization(InitType.Update, RequestUserID);
                _membershipRepository.Update(model.ID, model);
            }
            else
            {
                Initialization(model, 0);
                model.Initialization(InitType.Insert, RequestUserID);
                _membershipRepository.Create(model);
                //bool check = false;
                //if ((model.ParentID == AppGlobal.CustomerParentID) || (model.ParentID == AppGlobal.SupplierParentID))
                //{                    
                //    check = _membershipRepository.IsValidByTaxCode(model.TaxCode);
                //}
                //if (model.ParentID == AppGlobal.EmployeeParentID)
                //{
                //    check = _membershipRepository.IsValidByCitizenIdentification(model.CitizenIdentification);
                //}
                //if (check == true)
                //{
                //    Initialization(model, 0);
                //    model.Initialization(InitType.Insert, RequestUserID);
                //    _membershipRepository.Create(model);
                //}
            }
            return RedirectToAction("CustomerDetail", new { ID = model.ID });
        }
        public IActionResult SaveSupplier(Membership model)
        {
            if (model.ID > 0)
            {
                Initialization(model, 1);
                model.Initialization(InitType.Update, RequestUserID);
                _membershipRepository.Update(model.ID, model);
            }
            else
            {
                Initialization(model, 0);
                model.Initialization(InitType.Insert, RequestUserID);
                _membershipRepository.Create(model);
                //bool check = false;
                //if ((model.ParentID == AppGlobal.CustomerParentID) || (model.ParentID == AppGlobal.SupplierParentID))
                //{
                //    check = _membershipRepository.IsValidByTaxCode(model.TaxCode);
                //}
                //if (model.ParentID == AppGlobal.EmployeeParentID)
                //{
                //    check = _membershipRepository.IsValidByCitizenIdentification(model.CitizenIdentification);
                //}
                //if (check == true)
                //{
                    
                //}
            }
            return RedirectToAction("SupplierDetail", new { ID = model.ID });
        }
        public IActionResult Delete(int ID)
        {
            string note = AppGlobal.InitString;
            int result = _membershipRepository.Delete(ID);
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
        public IActionResult SaveEmployee(Membership model)
        {
            if (model.ID > 0)
            {
                Initialization(model, 1);
                model.Initialization(InitType.Update, RequestUserID);
                _membershipRepository.Update(model.ID, model);
            }
            else
            {
                Initialization(model, 0);
                model.Initialization(InitType.Insert, RequestUserID);
                _membershipRepository.Create(model);
                //bool check = false;
                //if ((model.ParentID == AppGlobal.CustomerParentID) || (model.ParentID == AppGlobal.SupplierParentID))
                //{
                //    check = _membershipRepository.IsValidByTaxCode(model.TaxCode);
                //}
                //if (model.ParentID == AppGlobal.EmployeeParentID)
                //{
                //    check = _membershipRepository.IsValidByCitizenIdentification(model.CitizenIdentification);
                //}
                //if (check == true)
                //{

                //}
            }
            return RedirectToAction("EmployeeDetail", new { ID = model.ID });
        }
    }
}
