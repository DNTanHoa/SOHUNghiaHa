using SOHU.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Results
{
    public class BaseResult
    {
        public ResultType ResultType { get; set; }

        public string Message { get; set; }
    }
}
