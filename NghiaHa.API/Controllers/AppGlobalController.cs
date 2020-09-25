using Microsoft.AspNetCore.Mvc;
using SOHU.Data.Helpers;

namespace NghiaHa.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppGlobalController : BaseController
    {
        [HttpGet]
        public ActionResult<string> GetYearFinanceToList()
        {
            var data = YearFinance.GetAllToList();
            return ObjectToJson(data);
        }

        [HttpGet]
        public ActionResult<string> GetMonthFinanceToList()
        {
            var data = MonthFinance.GetAllToList();
            return ObjectToJson(data);
        }
    }
}
