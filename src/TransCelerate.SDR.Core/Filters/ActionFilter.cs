﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
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

                string response = string.Empty;
                int statusCode = 0;

                // execute any code before the action executes                             

                var result = await next();

                // execute any code after the action executes                

                if (context != null)
                {
                    //Getting Response Body                
                    if (result.Result is ObjectResult objResult && objResult.Value != null)
                    {
                        statusCode = objResult.StatusCode ?? 0;
                    }
                    context?.HttpContext?.Response?.Headers?.Add("Controller", "True");

                    //Logging Request and Response parameters
                    _logger.LogInformation($"Status Code: {statusCode}; URL: {context.HttpContext.Request?.Path}");
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