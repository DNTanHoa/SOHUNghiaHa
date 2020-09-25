using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NghiaHa.API.ResponseModel;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;
using System.IO;

namespace NghiaHa.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
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

        [HttpGet]
        public ActionResult<string> Index01(int ID)
        {
            BaseViewModel model = new BaseViewModel();
            return ObjectToJson(model);
        }

        [HttpGet]
        public ActionResult<string> Detail(int ID)
        {
            Product model = new Product();
            model.ContentMain = "Tính theo thực tế";
            model.Discount = 0;
            if (ID > 0)
            {
                model = _productRepository.GetByID(ID);
            }
            return ObjectToJson(model);
        }

        [HttpGet]
        public ActionResult<string> GetAllToList()
        {
            var data = _productRepository.GetAllToList();
            return ObjectToJson(data);
        }

        [HttpGet]
        public ActionResult<string> GetAllOrderByTitleToList()
        {
            var data = _productRepository.GetAllOrderByTitleToList();
            return ObjectToJson(data);
        }

        [HttpGet]
        public ActionResult<string> GetByCategoryIDToList(int categoryID)
        {
            var data = _productRepository.GetByCategoryIDToList(categoryID);
            return ObjectToJson(data);
        }

        [HttpPost]
        public ActionResult<string> Save(Product model)
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

        [HttpDelete]
        public ActionResult<string> Delete(int ID)
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
            return ObjectToJson(note);
        }
    }
}
