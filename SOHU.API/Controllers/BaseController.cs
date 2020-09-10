using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SOHU.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase//, IActionFilter
    {
        /// <summary>
        /// Not Implement
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check user has allow
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }

        public int RequestUserID
        {
            get
            {
                int.TryParse(this.RequestUserClaims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value, out int UserId);
                
                return UserId;
            }
        }

        public IEnumerable<Claim> RequestUserClaims
        {
            get
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                    return identity.Claims;

                return null;
            }
        }

        public ActionResult<string> ObjectToJson(object obj)
        {
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
        }
    }
}
