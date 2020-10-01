using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Mobile.Services
{
    public class LoginResponeModel
    {
        public JObject Result { get; set; }

        public JObject Data { get; set; }
    }
}
