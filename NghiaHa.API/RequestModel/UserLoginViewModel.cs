﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NghiaHa.API.RequestModel
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        public string Account { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get; set; }

        public bool RememberPassword { get; set; }

    }
}
