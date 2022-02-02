using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TransCelerate.SDR.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransCelerate.SDR.Core.ErrorModels;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class ErrorResponseHelper
    {        
        public static ErrorModel ErrorResponseModel(Exception exception)
        {
            string statusCode;
            if (exception is UnauthorizedAccessException) statusCode = ((int)HttpStatusCode.Forbidden).ToString();
            else if (exception is TimeoutException) statusCode = ((int)HttpStatusCode.GatewayTimeout).ToString();
            else statusCode = ((int)HttpStatusCode.BadRequest).ToString();

            ErrorModel errorModel = new ErrorModel
            {
                statusCode = statusCode,
                message = "An Error Occured"                
            };
            return errorModel;
        }

        public static ErrorModel UnAuthorizedAccess()
        {
            ErrorModel errorModel = new ErrorModel
            {
                statusCode = ((int)HttpStatusCode.Unauthorized).ToString(),
                message = "Access Denied"
            };
            return errorModel;
        }
        public static ErrorModel NotFound(string message = null)
        {
            ErrorModel errorModel = new ErrorModel
            {
                statusCode = ((int)HttpStatusCode.NotFound).ToString(),
                message = message ?? "The requested page is not found"
            };
            return errorModel;
        }
        public static ErrorModel GatewayError()
        {
            ErrorModel errorModel = new ErrorModel
            {
                statusCode = ((int)HttpStatusCode.InternalServerError).ToString(),
                message = "Internal Server Error"
            };
            return errorModel;
        }

        public static ErrorModel BadRequest(string message = null)
        {
            ErrorModel errorModel = new ErrorModel
            {
                statusCode = ((int)HttpStatusCode.BadRequest).ToString(),
                message = message ?? "Invalid Input"
            };
            return errorModel;
        }

        public static ValidationErrorModel BadRequest(Object validationProblemDetails, string message = null)
        {            
            ValidationErrorModel errorModel = new ValidationErrorModel
            {
                statusCode = ((int)HttpStatusCode.BadRequest).ToString(),
                message = message ?? "Conformance Error",
                error = validationProblemDetails
            };
            return errorModel;
        } 
        public static ValidationErrorModel BadRequest()
        {            
            ValidationErrorModel errorModel = new ValidationErrorModel
            {
                statusCode = ((int)HttpStatusCode.BadRequest).ToString(),
                message = "API Specification Error",
                error = "JSON data is not compliant to API Specifications"
            };
            return errorModel;
        }
        public static ErrorModel MethodNotAllowed(string detail = null)
        {
            ErrorModel errorModel = new ErrorModel
            {
                statusCode = ((int)HttpStatusCode.MethodNotAllowed).ToString(),
                message = detail ?? "Method Not Allowed"
            };
            return errorModel;
        }
    }
}
