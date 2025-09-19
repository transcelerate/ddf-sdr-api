using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    /// <summary>
    /// This class helps to response for the different Api behaviours
    /// </summary>
    public class ApiBehaviourOptionsHelper
    {
        private readonly ILogHelper _logger;
        public ApiBehaviourOptionsHelper(ILogHelper logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// This method helps to format the error response for invalid input and conformance error
        /// </summary>
        /// <param name="context">Action Context</param>
        /// <returns></returns>
        public ObjectResult ModelStateResponse(ActionContext context)
        {
            var httpContext = context.HttpContext;

            var errors = new Dictionary<string, string[]>();
            var warnings = new Dictionary<string, string[]>();

            if (httpContext?.Items.TryGetValue("FV.Errors", out var rawErrors) == true &&
                rawErrors is List<ValidationFailure> failures)
            {
                errors = failures
                    .Where(f => f.Severity == Severity.Error)
                    .GroupBy(f => f.PropertyName)
                        .ToDictionary(
                            kvp => kvp.Key?.Length > 2 ? string.Join(".", kvp.Key?.Split(".").Select(key => $"{key?[..1]?.ToLower()}{key?[1..]}")) : kvp.Key,
                            kvp => kvp.Select(e => e.ErrorCode.StartsWith("DDF") ? $"{e.ErrorCode}: {e.ErrorMessage}" : e.ErrorMessage).ToArray()
                        );

                warnings = failures
                    .Where(f => f.Severity == Severity.Warning)
                    .GroupBy(f => f.PropertyName)
                        .ToDictionary(
                            kvp => kvp.Key?.Length > 2 ? string.Join(".", kvp.Key?.Split(".").Select(key => $"{key?[..1]?.ToLower()}{key?[1..]}")) : kvp.Key,
                            kvp => kvp.Select(e => e.ErrorCode.StartsWith("DDF") ? $"{e.ErrorCode}: {e.ErrorMessage}" : e.ErrorMessage).ToArray()
                        );
            }
            else
            {
                var modelState = context.ModelState.ToList();
                modelState.RemoveAll(x => x.Value.ValidationState == ModelValidationState.Valid || x.Value.ValidationState == ModelValidationState.Skipped);

                errors = modelState
                    .ToDictionary(
                        kvp => kvp.Key?.Length > 2 ? string.Join(".", kvp.Key?.Split(".").Select(key => $"{key?[..1]?.ToLower()}{key?[1..]}")) : kvp.Key,
                        kvp => kvp.Value?.Errors?.Select(e => e.ErrorMessage).ToArray()
                    );
            }

            if (context.HttpContext?.Response?.Headers != null)
            {
                context.HttpContext.Response.Headers["InvalidInput"] = "True";
            }

            var usdmVersion = context?.HttpContext?.Request?.Headers["usdmVersion"];

            // For Conformance error
            if (HasConformanceError(errors)
                && !JsonConvert.SerializeObject(errors).ToLower().Contains(Constants.ErrorMessages.InvalidUsdmVersion.ToLower())
                && !errors.Any(key => key.Key?.ToLower() == nameof(DTO.Common.AuditTrailDto.UsdmVersion).ToLower()))
            {
                _logger.LogError($"Conformance Error: {JsonConvert.SerializeObject(errors)}");
                _logger.LogInformation($"Status Code: {400}; URL: {context?.HttpContext?.Request?.Path}");
                return new BadRequestObjectResult(ErrorResponseHelper.ValidationBadRequest(errors, warnings, $"{Constants.ErrorMessages.ConformanceErrorMessage}{usdmVersion}"));
            }
            // Other errors
            else
            {
                _logger.LogError($"Invalid Input: {JsonConvert.SerializeObject(errors)}");
                _logger.LogInformation($"Status Code: {400}; URL: {context?.HttpContext?.Request?.Path}");
                return new BadRequestObjectResult(ErrorResponseHelper.BadRequest(errors, "Invalid Input"));
            }
        }

        public static bool HasConformanceError(Dictionary<string, string[]> errors)
        {
            string serializedErrors = JsonConvert.SerializeObject(errors).ToLower();

            var validationMessagesToCheck = new[]
            {
                Constants.ValidationErrorMessage.PropertyEmptyError,
                Constants.ValidationErrorMessage.PropertyMissingError,
                "unique",
                Constants.ValidationErrorMessage.BooleanValidationFailed,
                Constants.ValidationErrorMessage.IntegerMinimumValueError,
                Constants.ValidationErrorMessage.IntegerValidationFailed,
                Constants.ValidationErrorMessage.ScheduledInstanceTypesError
            };

            return validationMessagesToCheck.Any(message => serializedErrors.Contains(message.ToLower()));
        }
    }
}
