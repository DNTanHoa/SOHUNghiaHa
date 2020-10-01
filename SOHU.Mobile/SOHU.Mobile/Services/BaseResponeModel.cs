using Android.Runtime;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Mobile.Services
{
    public class BaseResponeModel
    {
        public object Result { get; set; }

        public JArray Data { get; set; }
    }
}
