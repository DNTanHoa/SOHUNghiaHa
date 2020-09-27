using System;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Results;

namespace NghiaHa.API.ResponseModel
{
    public class BaseResponseModel
    {
        /// <summary>
        /// Initialize Response model
        /// </summary>
        /// <param name="Data">Data response</param>
        /// <param name="Result">Result respones, Success if null</param>
        public BaseResponseModel(Object Data, BaseResult Result = null)
        {
            this.Data = Data;

            BaseResult result = Result;

            if (result == null)
            {
                //default result is success
                result = new SuccessResult();
            }

            this.Result = result;
        }

        public BaseResult Result { get; set; }
        public Object Data { get; set; }
    }
}
