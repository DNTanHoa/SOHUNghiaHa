using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NghiaHa.API.ResponseModel;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.ModelExtensions;
using SOHU.Data.Models;
using SOHU.Data.Repositories;
using SOHU.Data.Results;
using System.IO;

namespace NghiaHa.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IProductRepository _productRepository;

        public ProductController(IHostingEnvironment hostingEnvironment, IProductRepository productRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> Index01(int ID)
        {
            BaseViewModel model = new BaseViewModel();
            return new BaseResponeModel(model);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> Detail(int ID)
        {
            Product model = new Product();
            model.ContentMain = "Tính theo thực tế";
            model.Discount = 0;

            if (ID > 0)
            {
                model = _productRepository.GetByID(ID);
            }

            return new BaseResponeModel(model);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetAllToList()
        {
            var data = _productRepository.GetAllToList();
            return new BaseResponeModel(data);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetAllOrderByTitleToList()
        {
            var data = _productRepository.GetAllOrderByTitleToList();
            return new BaseResponeModel(data);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetByCategoryIDToList(int categoryID)
        {
            var data = _productRepository.GetByCategoryIDToList(categoryID);
            return new BaseResponeModel(data);
        }

        [HttpPost]
        public ActionResult<BaseResponeModel> Save(Product model)
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

            model.TrimTitle();

            if (model.ID > 0)
            {
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

            return new BaseResponeModel(new { ID = model.ID }, RouteResult);
        }

        [HttpDelete]
        public ActionResult<BaseResponeModel> Delete(int ID)
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

            return new BaseResponeModel(null, RouteResult);
        }
    }
}
