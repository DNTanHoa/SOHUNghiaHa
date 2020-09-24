using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace SOHU.MVC.Controllers
{
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddProduct(int ProductID, int Quantity = 1)
        {
            var product = _productRepository.GetByID(ProductID);
            var item = product.MapTo<CartDetail>();
            item.ID = 0;
            item.Quantity = Quantity;
            item.ProductID = ProductID;
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
                Response.Cookies.Append("CartID", cart.ID.ToString());
                item.CartID = cart.ID;
            }
            else
            {
                item.CartID = CartID;
            }
            _cartDetailRepository.Create(item);
            return Ok();
        }
    }
}
