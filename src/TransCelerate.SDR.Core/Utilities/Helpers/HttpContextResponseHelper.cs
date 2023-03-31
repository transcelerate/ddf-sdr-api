using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
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
        public static async Task<string> Response(HttpContext context, string response)
        {
            var jsonSettings = new JsonSerializerSettings 
            { 
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() 
            };
            if (String.IsNullOrWhiteSpace(context.Response.Headers["Content-Type"]))
            {
                context.Response.Headers.Add("Content-Type", "application/json");
            }
            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                response = JsonConvert.SerializeObject(ErrorResponseHelper.UnAuthorizedAccess(), jsonSettings);
            }
            else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                response = JsonConvert.SerializeObject(ErrorResponseHelper.Forbidden(Constants.ErrorMessages.Forbidden), jsonSettings);
            }
            else if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
            {
                response = JsonConvert.SerializeObject(ErrorResponseHelper.NotFound(), jsonSettings);
            }
            else if (context.Response.StatusCode == (int)HttpStatusCode.MethodNotAllowed)
            {
                response = JsonConvert.SerializeObject(ErrorResponseHelper.MethodNotAllowed(), jsonSettings);
            }
            await context.Response.WriteAsync(response);
            return response;
        }
    }
}
