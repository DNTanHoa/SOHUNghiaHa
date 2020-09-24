using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using SOHU.Data.DataTransferObject;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace SOHU.MVC.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductRepository _productResposistory;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ProductController(IProductRepository productResposistory, IHostingEnvironment hostingEnvironment)
        {
            _productResposistory = productResposistory;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            ModelTemplate model = new ModelTemplate();
            return View(model);
        }

        public IActionResult Detail(int ID)
        {
            var model = _productResposistory.GetByID(ID) ?? new Product();
            return View(model);
        }
        public IActionResult DetailCompact(int ID)
        {
            Product product=_productResposistory.GetByID(ID) ?? new Product();
            ProductDataTransfer model = product.MapTo<ProductDataTransfer>();
            return View(model);
        }
        public IActionResult GetAllToList()
        {
            return Json(_productResposistory.GetAllToList());
        }
        public IActionResult GetByCategoryIDToList(int categoryID)
        {
            List<Product> list = new List<Product>();
            if (categoryID > 0)
            {
                list = _productResposistory.GetByCategoryIDToList(categoryID);
            }
            return Json(list);
        }
        public IActionResult GetByID(int ID)
        {
            return Json(_productResposistory.GetByID(ID));
        }

        public IActionResult Create(Product model)
        {
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = _productResposistory.Create(model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }

        public IActionResult Update(Product model)
        {
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _productResposistory.Update(model.ID, model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            }
            else
            {
                note = AppGlobal.Success + " - " + AppGlobal.EditFail;
            }
            return Json(note);
        }

        public IActionResult Delete(int ID)
        {
            string note = AppGlobal.InitString;
            int result = _productResposistory.Delete(ID);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.DeleteSuccess;
            }
            else
            {
                note = AppGlobal.Success + " - " + AppGlobal.DeleteFail;
            }
            return Json(note);
        }
        [AcceptVerbs("Post")]
        public IActionResult SaveChange(ProductDataTransfer model)
        {
            string note = AppGlobal.InitString;
            int result = 0;
            if (!string.IsNullOrEmpty(model.Title))
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
                        var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, AppGlobal.URLImagesProduct, fileName);
                        using (var stream = new FileStream(physicalPath, FileMode.Create))
                        {
                            file.CopyToAsync(stream);
                            model.Image = AppGlobal.Domain + AppGlobal.URLImagesProduct + "/" + fileName;
                        }
                    }
                }
                if (string.IsNullOrEmpty(model.MetaTitle))
                {
                    model.MetaTitle = AppGlobal.SetName(model.Title);
                }
                if (string.IsNullOrEmpty(model.Urlcode))
                {
                    model.Urlcode = model.MetaTitle;
                }
                if (string.IsNullOrEmpty(model.MetaKeyword))
                {
                    model.MetaKeyword = model.Title;
                }
                if (string.IsNullOrEmpty(model.MetaDescription))
                {
                    model.MetaDescription = model.Title;
                }
                if (string.IsNullOrEmpty(model.Tags))
                {
                    model.Tags = model.Title;
                }
                if (string.IsNullOrEmpty(model.Description))
                {
                    model.Description = model.Title;
                }
                if (string.IsNullOrEmpty(model.Image))
                {
                    if (!string.IsNullOrEmpty(model.ContentMain))
                    {
                        model.Image = AppGlobal.SetImagesFileName(model.ContentMain);
                    }
                }

                if (model.ID > 0)
                {
                    model.Initialization(InitType.Update, RequestUserID);
                    result = _productResposistory.Update(model.ID, model);
                    if (result > 0)
                    {
                        note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
                    }
                    else
                    {
                        note = AppGlobal.Success + " - " + AppGlobal.EditFail;
                    }
                }
                else
                {
                    model.Initialization(InitType.Insert, RequestUserID);
                    result = _productResposistory.Create(model);
                    if (result > 0)
                    {
                        note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
                    }
                    else
                    {
                        note = AppGlobal.Success + " - " + AppGlobal.CreateFail;
                    }
                }
            }
            //return Json(note);
            return RedirectToAction("DetailCompact", new { ID = model.ID });
        }

        public IActionResult NewProducts(int PageSize)
        {
            var model = _productResposistory.GetAllToList().OrderBy(item => item.DateCreated).Take(PageSize);
            return PartialView("~/Views/Product/_List.cshtml", model);
        }

        public IActionResult TopProducts(int PageSize)
        {
            var model = _productResposistory.GetAllToList().OrderBy(item => item.DateCreated).Take(PageSize);
            return PartialView("~/Views/Product/_List.cshtml", model);
        }

        public IActionResult SaleProducts(int PageSize)
        {
            var model = _productResposistory.GetAllToList().OrderBy(item => item.DateCreated).Take(PageSize);
            return PartialView("~/Views/Product/_List.cshtml", model);
        }
    }
}