using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NghiaHa.API.ResponseModel;
using SOHU.Data.Helpers;
using System.Net;

namespace NghiaHa.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AppGlobalController : BaseController
    {
        [HttpGet]
        public ActionResult<BaseResponeModel> GetYearFinanceToList()
        {
            var data = YearFinance.GetAllToList();
            return new BaseResponeModel(data);
        }

        [HttpGet]
        public ActionResult<BaseResponeModel> GetMonthFinanceToList()
        {
            var data = MonthFinance.GetAllToList();
            return new BaseResponeModel(data);
        }
    }
}
