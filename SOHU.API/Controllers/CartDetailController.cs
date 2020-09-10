using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SOHU.Data.Repositories;

namespace SOHU.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartDetailController : BaseController
    {
        private readonly ICartDetailRepository _cartDetailRepository;

        public CartDetailController(ICartDetailRepository cartDetailRepository)
        {
            _cartDetailRepository = cartDetailRepository;
        }

        [HttpGet]
        public ActionResult<string> TopView(int CartID)
        {
            var model = _cartDetailRepository.GetDataTransferByCartIDToList(CartID);
            return ObjectToJson(model);
        }
    }
}
