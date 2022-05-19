using System;
using System.Net;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    /// <summary>
    /// Response Helper for Errors
    /// </summary>
    public static class ErrorResponseHelper
    {
        /// <summary>
        /// Resposne Helper When there is an exception
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>
        /// A <see cref="ErrorModel"/> When there is an exception      
        /// </returns>>
        public static ErrorModel ErrorResponseModel(Exception exception)
        {
            string statusCode;
            if (exception is UnauthorizedAccessException) statusCode = ((int)HttpStatusCode.Forbidden).ToString();
            else if (exception is TimeoutException) statusCode = ((int)HttpStatusCode.GatewayTimeout).ToString();
            else statusCode = ((int)HttpStatusCode.BadRequest).ToString();

            ErrorModel errorModel = new ErrorModel
            {
                statusCode = statusCode,
                message = Constants.ErrorMessages.GenericError
            };
            return errorModel;
        }

        /// <summary>
        /// Resposne Helper When there is an Unauthorized Access
        /// </summary>
        /// <returns>
        /// A <see cref="ErrorModel"/> When there is an Unauthorized Access      
        /// </returns>>
        public static ErrorModel UnAuthorizedAccess()
        {
            ErrorModel errorModel = new ErrorModel
            {
                statusCode = ((int)HttpStatusCode.Unauthorized).ToString(),
                message = "Access Denied"
            };
            return errorModel;
        }

        /// <summary>
        /// Resposne Helper When the resource is Not Found
        /// </summary>
        /// <param name="message">Message for error response</param>
        /// <returns>
        /// A <see cref="ErrorModel"/> When the resource is Not Found
        /// </returns>>
        public static ErrorModel NotFound(string message = null)
        {
            ErrorModel errorModel = new ErrorModel
            {
                statusCode = ((int)HttpStatusCode.NotFound).ToString(),
                message = message ?? "The requested page is not found"
            };
            return errorModel;
        }

        /// <summary>
        /// Resposne Helper When there is Gateway Error
        /// </summary>
        /// <returns>
        /// A <see cref="ErrorModel"/> When there is Gateway Error
        /// </returns>>
        public static ErrorModel GatewayError()
        {
            ErrorModel errorModel = new ErrorModel
            {
                statusCode = ((int)HttpStatusCode.InternalServerError).ToString(),
                message = "Internal Server Error"
            };
            return errorModel;
        }

        /// <summary>
        /// Resposne Helper When there is Bad Request
        /// </summary>
        /// <param name="message">Message for error response</param>
        /// <returns>
        /// A <see cref="ErrorModel"/> When there is Bad Request
        /// </returns>>
        public static ErrorModel BadRequest(string message = null)
        {
            ErrorModel errorModel = new ErrorModel
            {
                statusCode = ((int)HttpStatusCode.BadRequest).ToString(),
                message = message ?? "Invalid Input"
            };
            return errorModel;
        }

        /// <summary>
        /// Resposne Helper When there is Conformance Error or Invalid Inpt
        /// </summary>
        /// <param name="validationProblemDetails">Object for holding validation errors</param>
        /// <param name="message">Message for error response</param>
        /// <returns>
        /// A <see cref="ValidationErrorModel"/> When there is Conformance Error or Invalid Inpt
        /// </returns>>
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

        /// <summary>
        /// Resposne Helper When specific method for an API is not called. Ex: When a GET method is called with a POST request.
        /// </summary>
        /// <param name="message">Message for error response</param>
        /// <returns>
        /// A <see cref="ErrorModel"/> When specific method for an API is not called
        /// </returns>>
        public static ErrorModel MethodNotAllowed(string message = null)
        {
            ErrorModel errorModel = new ErrorModel
            {
                statusCode = ((int)HttpStatusCode.MethodNotAllowed).ToString(),
                message = message ?? "Method Not Allowed"
            };
            return errorModel;
        }
        
        public static ErrorModel InternalServerError(string message = null)
        {
            ErrorModel errorModel = new ErrorModel
            {
                statusCode = ((int)HttpStatusCode.InternalServerError).ToString(),
                message = message ?? "Internal Server Error"
            };
            return errorModel;
        }
    }
}
