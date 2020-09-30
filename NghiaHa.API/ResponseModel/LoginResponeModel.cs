using SOHU.Data.Models;
using System;

namespace NghiaHa.API.ResponseModel
{
    public class LoginResponeModel
    {
        public DateTime? TokenExpireDate { get; set; }

        public string Token { get; set; }

        public Membership LoginUser { get; set; }
    }
}
