using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NghiaHa.API.ResponseModel;
using SOHU.Data.Models;
using SOHU.Data.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace NghiaHa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected BaseResult RouteResult;

        public int RequestUserID
        {
            get
            {
                int.TryParse(this.RequestUserClaims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value, out int UserId);

                return UserId;
            }
        }

        public bool IsLogin
        {
            get
            {
                bool.TryParse(this.RequestUserClaims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value, out bool result);

                return result;
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
    }
}
