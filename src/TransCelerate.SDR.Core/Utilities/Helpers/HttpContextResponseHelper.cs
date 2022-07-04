using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    /// <summary>
    /// This class is used to format the error messages in the response
    /// </summary>
    public static class HttpContextResponseHelper
    {
        /// <summary>
        /// This method is used to format the error messages in the response
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <param name="response">Response string</param>
        /// <returns></returns>
        public static async Task<string> Response(HttpContext context,string response)
        {
            if(!String.IsNullOrWhiteSpace(context.Response.Headers["Content-Type"]))
            {
                context.Response.Headers.Add("Content-Type", "application/json");
            }          
            else if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                response = JsonConvert.SerializeObject(ErrorResponseHelper.UnAuthorizedAccess());                
            }
            if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                response = JsonConvert.SerializeObject(ErrorResponseHelper.Forbidden(Constants.ErrorMessages.Forbidden));
            }
            else if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
            {
                response = JsonConvert.SerializeObject(ErrorResponseHelper.NotFound());                
            }
            else if (context.Response.StatusCode == (int)HttpStatusCode.MethodNotAllowed)
            {
                response = JsonConvert.SerializeObject(ErrorResponseHelper.MethodNotAllowed());                
            }
            await context.Response.WriteAsync(response);
            return response;
        }
    }
}
