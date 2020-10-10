﻿using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using NghiaHa.CRM.Controllers;
using NghiaHa.CRM.Web.Models;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace NghiaHa.CRM.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IProductRepository _productRepository;

        public ProductController(IWebHostEnvironment hostingEnvironment, IProductRepository productRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _productRepository = productRepository;
        }
        private void InitializationBarcode(Product model)
        {
            var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/Product/Barcode");
            Ean13.CreateEAN13Image_Product(model, physicalPath);
        }
        private void Initialization(Product model)
        {
            if (!string.IsNullOrEmpty(model.Title))
            {
                model.Title = model.Title.Trim();
            }
            if (!string.IsNullOrEmpty(model.Author))
            {
                model.Author = model.Author.Trim();
            }
            if (!string.IsNullOrEmpty(model.MetaTitle))
            {
                model.MetaTitle = model.MetaTitle.Trim();
            }
            if (model.Price == null)
            {
                model.Price = 0;
            }
            if (model.Discount == null)
            {
                model.Discount = 0;
            }
        }
        public IActionResult BarcodePreview()
        {
            BaseViewModel model = new BaseViewModel();
            string listProductID = Request.Cookies["ListProductID"];
            if (!string.IsNullOrEmpty(listProductID))
            {
                StringBuilder txt = new StringBuilder();
                txt.AppendLine(@"<table>");
                int i = 0;
                foreach (string IDString in listProductID.Split(';'))
                {
                    if (!string.IsNullOrEmpty(IDString))
                    {
                        if (i % 2 == 0)
                        {
                            txt.AppendLine(@"<tr>");
                        }
                        try
                        {
                            int ID = int.Parse(IDString);
                            txt.AppendLine(@"<td>");
                            if (i % 2 == 0)
                            {
                                txt.AppendLine(@"<div style='width: 160px; height: 90px; padding: 9px; border-right-color:#000000; border-right-style:dotted; border-right-width:1px; border-bottom-color:#000000; border-bottom-style:dotted; border-bottom-width:1px;'>");
                            }
                            else
                            {
                                txt.AppendLine(@"<div style='width:160px; height:90px; padding:9px; border-bottom-color:#000000; border-bottom-style:dotted; border-bottom-width:1px;'>");
                            }
                            txt.AppendLine(@"<img src='http://crm.nghiaha.vn/images/Product/Barcode/" + _productRepository.GetByID(ID).ImageThumbnail + "' width='100%' height='100%' />");
                            txt.AppendLine(@"</div>");
                            txt.AppendLine(@"</td>");
                        }
                        catch
                        {

                        }
                        if ((i % 2 == 1) || (i == listProductID.Split(';').Length - 1))
                        {
                            txt.AppendLine(@"</tr>");
                        }
                        i = i + 1;
                    }
                }
                txt.AppendLine(@"</table>");
                model.Content = txt.ToString();
            }
            Response.Cookies.Append("ListProductID", "");
            return View(model);
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
            model.ID = ID;
            model.ContentMain = "Tính theo thực tế";
            model.Discount = 0;
            if (ID > 0)
            {
                model = _productRepository.GetByID(ID);
            }
            model.ParentID = model.ID;
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
        public IActionResult TaoMaVach()
        {
            foreach (Product model in _productRepository.GetAllToList())
            {
                if (string.IsNullOrEmpty(model.ImageThumbnail))
                {
                    model.MetaTitle = "";
                    model.MetaDescription = "";
                    InitializationBarcode(model);
                }
                if (model.ID > 0)
                {
                    model.Initialization(InitType.Update, RequestUserID);
                    _productRepository.Update(model.ID, model);
                }
            }
            string note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            return Json(note);
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
            if (model.PriceUnitID > 0)
            {
                string listProductID = Request.Cookies["ListProductID"];
                listProductID = listProductID + ";" + model.ID;
                Response.Cookies.Append("ListProductID", listProductID);
            }
            model.PriceUnitID = null;
            if (string.IsNullOrEmpty(model.ImageThumbnail))
            {
                model.MetaTitle = "";
                model.MetaDescription = "";
                InitializationBarcode(model);
            }
            if (model.ID > 0)
            {
                Initialization(model);
                if (string.IsNullOrEmpty(model.Image))
                {
                    model.Image = _productRepository.GetByID(model.ID).Image;
                }
                model.Initialization(InitType.Update, RequestUserID);
                _productRepository.Update(model.ID, model);
            }
            else
            {
                Initialization(model);
                model.Initialization(InitType.Insert, RequestUserID);
                _productRepository.Create(model);
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
