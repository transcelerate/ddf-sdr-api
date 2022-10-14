using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Utilities.Common;
using Newtonsoft.Json;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using TransCelerate.SDR.Core.Utilities;

namespace TransCelerate.SDR.Core.Filters
{
    /// <summary>
    /// This class is an action filter which will be executed before and after an action is performed
    /// </summary>
    public class ActionFilter : IAsyncActionFilter
    {
        private readonly ILogHelper _logger;
        public ActionFilter(ILogHelper logger)
        {
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                _logger.LogInformation($"Started Action Filter : {nameof(ActionFilter)}; Method : {nameof(OnActionExecutionAsync)};");
                
                string request = string.Empty;
                string response = string.Empty;
                int statusCode = 0;

                // execute any code before the action executes                             

                var result = await next();
                // execute any code after the action executes                

                if(context!=null)
                {
                    //Getting Request Body                
                    var input = context.ActionArguments;
                    if (input != null)
                        request = JsonConvert.SerializeObject(input);

                    //Getting Response Body                
                    var objResult = result.Result as ObjectResult;
                    if (objResult != null && objResult.Value != null)
                    {
                        statusCode = objResult.StatusCode ?? 0;
                        response = JsonConvert.SerializeObject(objResult.Value);
                    }
                    var AuthToken = context?.HttpContext?.Request?.Headers["Authorization"];
                    context?.HttpContext?.Response?.Headers?.Add("Controller", "True");
                    //Logging Request and Response parameters
                    _logger.LogInformation($"Status Code: {statusCode}; UserName : {context.HttpContext.User?.FindFirst(ClaimTypes.Email)?.Value}; UserRole : {context.HttpContext.User?.FindFirst(ClaimTypes.Role)?.Value} URL: {context.HttpContext.Request?.Path}; AuthToken: {AuthToken}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception occured in the Action Filter. Exception: {ex.Message}");
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Action Filter : {nameof(ActionFilter)}; Method : {nameof(OnActionExecutionAsync)};");
            }
        }
    }
}
