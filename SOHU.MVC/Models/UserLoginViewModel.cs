using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SOHU.MVC.Models
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Username can not be null")]
        public string Account { get; set; }

        [Required(ErrorMessage = "Password can not be null")]
        public string Password { get; set; }

        public bool IsRemember { get; set; }

        public string ReturnUrl { get; set; }
    }
}
