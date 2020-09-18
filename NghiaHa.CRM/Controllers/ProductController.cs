using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using NghiaHa.CRM.Controllers;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace NghiaHa.CRM.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
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

        public IActionResult Detail(int ID)
        {
            Product model = new Product();
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
        public ActionResult GetByCategoryIDToList([DataSourceRequest] DataSourceRequest request, int categoryID)
        {
            var data = _productRepository.GetByCategoryIDToList(categoryID);
            return Json(data.ToDataSourceResult(request));
        }
        public IActionResult Save(Product model)
        {
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
    }
}
