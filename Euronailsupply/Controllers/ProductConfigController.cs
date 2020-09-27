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
using Euronailsupply.Controllers;
using Euronailsupply.Models;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace Euronailsupply.Controllers
{
    
    public class ProductConfigController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IProductConfigRepository _productConfigRepository;
        public ProductConfigController(IHostingEnvironment hostingEnvironment, IProductConfigRepository productConfigRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _productConfigRepository = productConfigRepository;
        }
        private void Initialization(Product model)
        {
            if (!string.IsNullOrEmpty(model.Title))
            {
                model.Title = model.Title.Trim();
            }
        }
        public ActionResult GetAttachedFilesByProductIDToList([DataSourceRequest] DataSourceRequest request, int productID)
        {
            var data = _productConfigRepository.GetAttachedFilesByProductIDToList(productID);
            return Json(data.ToDataSourceResult(request));
        }
        public IActionResult Delete(int ID)
        {
            string note = AppGlobal.InitString;
            int result = _productConfigRepository.Delete(ID);
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
        [AcceptVerbs("Post")]
        public IActionResult SaveFiles(Product model)
        {
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    List<ProductConfig> list = new List<ProductConfig>();
                    for (int i = 0; i < Request.Form.Files.Count; i++)
                    {
                        var file = Request.Form.Files[i];
                        if (file != null)
                        {
                            string fileExtension = Path.GetExtension(file.FileName);
                            string fileName = Path.GetFileNameWithoutExtension(file.FileName);

                            fileName = AppGlobal.SetName(fileName);
                            fileName = fileName + "-" + AppGlobal.DateTimeCode + fileExtension;
                            var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, AppGlobal.URLImagesProduct, fileName);
                            using (var stream = new FileStream(physicalPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                                ProductConfig productConfig = new ProductConfig();
                                productConfig.Initialization(InitType.Insert, RequestUserID);                                
                                productConfig.ProductID = model.ID;
                                productConfig.Title = model.Title;
                                productConfig.FileName = fileName;
                                productConfig.Note = fileExtension;
                                list.Add(productConfig);
                            }
                        }
                    }
                    _productConfigRepository.Range(list);
                }
            }
            catch (Exception e)
            {

            }
            return RedirectToAction("Files", "Product", new { ID = model.ID });
        }
    }
}
