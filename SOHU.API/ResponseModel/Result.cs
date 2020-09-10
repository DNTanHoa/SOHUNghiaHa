using SOHU.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOHU.API.ResponseModel
{
    public class Result
    {
        public ResultType resultType;

        public ErrorType errorType;

        public string message;

        public Result setResultType(ResultType resultType)
        {
            this.resultType = resultType;
            return this;
        }

        public Result setErrorType(ErrorType errorType)
        {
            this.errorType = errorType;
            return this;
        }

        public Result setMessage(string message)
        {
            this.message = message;
            return this;
        }
    }
}
