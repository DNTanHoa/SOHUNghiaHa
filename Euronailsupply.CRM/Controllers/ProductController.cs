using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Euronailsupply.CRM.Controllers;
using Euronailsupply.CRM.Web.Models;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace Euronailsupply.CRM.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IProductRepository _productRepository;

        public ProductController(IHostingEnvironment hostingEnvironment, IProductRepository productRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _productRepository = productRepository;
        }
        private void Initialization(Product model)
        {
            if (!string.IsNullOrEmpty(model.Title))
            {
                model.Title = model.Title.Trim();
            }
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index01(int ID)
        {
            BaseViewModel model = new BaseViewModel();
            return View(model);
        }
        public IActionResult Detail(int ID)
        {
            Product model = new Product();
            model.ContentMain = "Tính theo thực tế";
            model.Discount = 0;
            if (ID > 0)
            {
                model = _productRepository.GetByID(ID);
            }
            return View(model);
        }
        public ActionResult GetAllToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _productRepository.GetAllToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetAllOrderByTitleToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _productRepository.GetAllOrderByTitleToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetByCategoryIDToList([DataSourceRequest] DataSourceRequest request, int categoryID)
        {
            var data = _productRepository.GetByCategoryIDToList(categoryID);
            return Json(data.ToDataSourceResult(request));
        }
        public IActionResult Save(Product model)
        {
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];
                if (file != null)
                {
                    string fileExtension = Path.GetExtension(file.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    fileName = AppGlobal.SetName(fileName);
                    fileName = fileName + "-" + AppGlobal.DateTimeCode + fileExtension;
                    var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/Product", fileName);
                    using (var stream = new FileStream(physicalPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        model.Image = fileName;
                    }
                }
            }
            if (model.ID > 0)
            {
                Initialization(model);
                model.Initialization(InitType.Update, RequestUserID);
                _productRepository.Update(model.ID, model);
            }
            else
            {
                Initialization(model);
                model.Initialization(InitType.Insert, RequestUserID);
                if (_productRepository.IsValidByTitle(model.Title) == true)
                {
                    _productRepository.Create(model);
                }
            }
            return RedirectToAction("Detail", new { ID = model.ID });
        }
        public IActionResult Delete(int ID)
        {
            string note = AppGlobal.InitString;
            int result = _productRepository.Delete(ID);
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
