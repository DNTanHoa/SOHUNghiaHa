using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Results
{
    public class SuccessResult : BaseResult
    {
        /// <summary>
        /// Initialize SuccessResult
        /// </summary>
        /// <param name="Message">>Message success, default is message in AppGlobal.Success</param>
        public SuccessResult(string Message = null)
        {
            if (string.IsNullOrWhiteSpace(Message))
            {
                Message = AppGlobal.Success;
            }

            base.Message = Message;
            base.ResultType = ResultType.Success;
        }
    }
}
