using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NghiaHa.API.ResponseModel;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;
using SOHU.Data.Results;
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
            return ObjectToJson(new BaseResponseModel(model));
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

            return ObjectToJson(new BaseResponseModel(model));
        }

        [HttpGet]
        public ActionResult<string> GetAllToList()
        {
            var data = _productRepository.GetAllToList();
            return ObjectToJson(new BaseResponseModel(data));
        }

        [HttpGet]
        public ActionResult<string> GetAllOrderByTitleToList()
        {
            var data = _productRepository.GetAllOrderByTitleToList();
            return ObjectToJson(new BaseResponseModel(data));
        }

        [HttpGet]
        public ActionResult<string> GetByCategoryIDToList(int categoryID)
        {
            var data = _productRepository.GetByCategoryIDToList(categoryID);
            return ObjectToJson(new BaseResponseModel(data));
        }

        [HttpPost]
        public ActionResult<string> Save(Product model)
        {
            int result;

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

                result = _productRepository.Update(model.ID, model);

                if (result > 0)
                {
                    RouteResult = new SuccessResult(AppGlobal.EditSuccess);
                }
                else
                {
                    RouteResult = new ErrorResult(ErrorType.EditError, AppGlobal.EditFail);
                }
            }
            else
            {
                Initialization(model);
                model.Initialization(InitType.Insert, RequestUserID);
                if (_productRepository.IsValidByTitle(model.Title) == true)
                {
                    result = _productRepository.Create(model);

                    if (result > 0)
                    {
                        RouteResult = new SuccessResult(AppGlobal.CreateSuccess);
                    }
                    else
                    {
                        RouteResult = new ErrorResult(ErrorType.InsertError, AppGlobal.CreateFail);
                    }
                }
            }

            return ObjectToJson(new BaseResponseModel(new { ID = model.ID }, RouteResult));
        }

        [HttpDelete]
        public ActionResult<string> Delete(int ID)
        {
            int result = _productRepository.Delete(ID);
            if (result > 0)
            {
                RouteResult = new SuccessResult(AppGlobal.DeleteSuccess);
            }
            else
            {
                RouteResult = new ErrorResult(ErrorType.DeleteError, AppGlobal.DeleteFail);
            }

            return ObjectToJson(new BaseResponseModel(null, RouteResult));
        }
    }
}
