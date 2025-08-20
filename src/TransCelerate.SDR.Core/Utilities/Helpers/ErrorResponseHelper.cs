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
            else statusCode = ((int)HttpStatusCode.BadRequest).ToString();

            ErrorModel errorModel = new()
            {
                StatusCode = statusCode,
                Message = Constants.ErrorMessages.GenericError
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
            ErrorModel errorModel = new()
            {
                StatusCode = ((int)HttpStatusCode.NotFound).ToString(),
                Message = message ?? "The requested page is not found"
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
            ErrorModel errorModel = new()
            {
                StatusCode = ((int)HttpStatusCode.InternalServerError).ToString(),
                Message = "Internal Server Error"
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
            ErrorModel errorModel = new()
            {
                StatusCode = ((int)HttpStatusCode.BadRequest).ToString(),
                Message = message ?? "Invalid Input"
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
            ValidationErrorModel errorModel = new()
            {
                StatusCode = ((int)HttpStatusCode.BadRequest).ToString(),
                Message = message ?? "Conformance Error",
                Error = validationProblemDetails
            };
            return errorModel;
        }

        /// <summary>
        /// Validation Response Helper When there is Conformance Rule Error or Invalid Input
        /// </summary>
        /// <param name="validationErrorDetails">Object for holding validation errors</param>
        /// <param name="validationWarningDetails">Object for holding validation warnings</param>
        /// <param name="message">Message for error response</param>
        /// <returns>
        /// A <see cref="ValidationErrorModel"/> When there is Conformance Rule Error or Invalid Input
        /// </returns>>
        public static RuleValidationErrorModel ValidationBadRequest(Object validationErrorDetails, Object validationWarningDetails, string message = null)
        {
            RuleValidationErrorModel errorModel = new()
            {
                StatusCode = ((int)HttpStatusCode.BadRequest).ToString(),
                Message = message ?? "Conformance Error",
                Error = validationErrorDetails,
                Warning = validationWarningDetails
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
            ErrorModel errorModel = new()
            {
                StatusCode = ((int)HttpStatusCode.MethodNotAllowed).ToString(),
                Message = message ?? "Method Not Allowed"
            };
            return errorModel;
        }
        /// <summary>
        /// Response helper when there is an internal server error
        /// </summary>
        /// <param name="message">Message for error response</param>
        /// <returns>
        /// A <see cref="ErrorModel"/> 
        /// </returns>>
        public static ErrorModel InternalServerError(string message = null)
        {
            ErrorModel errorModel = new()
            {
                StatusCode = ((int)HttpStatusCode.InternalServerError).ToString(),
                Message = message ?? "Internal Server Error"
            };
            return errorModel;
        }

        /// <summary>
        /// Response helper when there is an internal server error
        /// </summary>
        /// <param name="problemDetails">Object for holding error details</param>
        /// <param name="message">Message for error response</param>
        /// <returns>Status code, error message and details</returns>
        public static ValidationErrorModel InternalServerError(Object problemDetails, string message = null)
        {
            ValidationErrorModel errorModel = new()
            {
                StatusCode = ((int)HttpStatusCode.InternalServerError).ToString(),
                Message = message ?? "Internal Server Error",
                Error = problemDetails
            };
            return errorModel;
        }
    }
}
