using System;
using System.Net;
using TransCelerate.SDR.Core.ErrorModels;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class SuccessResponseHelper
    {
        /// <summary>
        /// Response Helper for successful validation
        /// </summary>
        /// <returns>
        /// A <see cref="ErrorModel"/> When there is an Unauthorized Access      
        /// </returns>>
        public static ErrorModel ValidationSuccess(string message)
        {
            ErrorModel errorModel = new()
            {
                StatusCode = ((int)HttpStatusCode.OK).ToString(),
                Message = message
            };
            return errorModel;
        }
    }
}
