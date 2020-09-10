using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SOHU.API.ResponseModel;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace SOHU.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartController : BaseController
    {
        private readonly ICartRepository _cartRepository;

        private readonly IProductRepository _productRepository;

        private readonly ICartDetailRepository _cartDetailRepository;

        public CartController(ICartRepository cartRepository,
                              IProductRepository productRepository,
                              ICartDetailRepository cartDetailRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _cartDetailRepository = cartDetailRepository;
        }

        [HttpPost]
        public ActionResult<string> AddProduct(int ProductID, int CartID, int Quantity = 1)
        {
            Result routeResult;

            var product = _productRepository.GetByID(ProductID);
            var item = product.MapTo<CartDetail>();
            item.Id = 0;
            item.Quantity = Quantity;
            item.ProductId = ProductID;
            item.Initialization(InitType.Insert, 0);

            if (CartID == 0)
            {
                var cart = new Cart()
                {
                    ManageCode = AppGlobal.DateTimeCode,
                    CartCode = AppGlobal.DateTimeCode
                };
                cart.Initialization(InitType.Insert, 0);
                _cartRepository.Create(cart, out cart);
                //Response.Cookies.Append("CartID", cart.Id.ToString());
                item.CartId = cart.Id;
            }
            else
            {
                item.CartId = CartID;
            }

            int result = _cartDetailRepository.Create(item);

            if (result > 0)
            {
                routeResult = new Result()
                        .setResultType(ResultType.Success)
                        .setMessage(AppGlobal.CreateSuccess);
            }
            else
            {
                routeResult = new Result()
                        .setResultType(ResultType.Error)
                        .setErrorType(ErrorType.InsertError)
                        .setMessage(AppGlobal.CreateFail);
            }

            return ObjectToJson(routeResult);
        }
    }
}
