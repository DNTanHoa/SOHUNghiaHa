using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SOHU.Data.Repositories;

namespace SOHU.MVC.Controllers
{
    public class CartDetailController : BaseController
    {
        private readonly ICartDetailRepository _cartDetailRepository;

        public CartDetailController(ICartDetailRepository cartDetailRepository)
        {
            _cartDetailRepository = cartDetailRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TopView()
        {
            var model = _cartDetailRepository.GetDataTransferByCartIDToList(CartID);
            return PartialView(model);
        }
    }
}
