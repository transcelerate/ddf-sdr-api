using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
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
            var modelState = context.ModelState.ToList();
            modelState.RemoveAll(x => x.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid);
            var errors = modelState.ToDictionary(
                    kvp => kvp.Key?.Length > 2 ? string.Join(".", kvp.Key?.Split(".").Select(key => $"{key?[..1]?.ToLower()}{key?[1..]}")) : kvp.Key,
                    kvp => kvp.Value?.Errors?.Select(e => e.ErrorMessage).ToArray()
                );
            var errorsToList = errors.ToList();
            
            errors = errorsToList.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            context.HttpContext?.Response?.Headers?.Add("InvalidInput", "True");
            var errorList = SplitStringIntoArrayHelper.SplitString(JsonConvert.SerializeObject(errors), 32000);//since app insights limit is 32768 characters                                                              
            var AuthToken = context?.HttpContext?.Request?.Headers["Authorization"];
            var usdmVersion = context?.HttpContext?.Request?.Headers["usdmVersion"];

            //For Conformance error
            if ((JsonConvert.SerializeObject(errors).ToLower().Contains(Constants.ValidationErrorMessage.PropertyEmptyError.ToLower()) || JsonConvert.SerializeObject(errors).ToLower().Contains(Constants.ValidationErrorMessage.PropertyMissingError.ToLower())
                || JsonConvert.SerializeObject(errors).ToLower().Contains(Constants.ValidationErrorMessage.SelectAtleastOneGroup.ToLower()) || JsonConvert.SerializeObject(errors).ToLower().Contains(Constants.ValidationErrorMessage.InvalidPermissionValue.ToLower())
                || JsonConvert.SerializeObject(errors).ToLower().Contains("unique") || JsonConvert.SerializeObject(errors).ToLower().Contains(Constants.ValidationErrorMessage.BooleanValidationFailed.ToLower()) || JsonConvert.SerializeObject(errors).ToLower().Contains(Constants.ValidationErrorMessage.IntegerMinimumValueError.ToLower())
                || JsonConvert.SerializeObject(errors).ToLower().Contains(Constants.ValidationErrorMessage.IntegerValidationFailed.ToLower()) || JsonConvert.SerializeObject(errors).ToLower().Contains(Constants.ValidationErrorMessage.ScheduledInstanceTypesError.ToLower())
                || JsonConvert.SerializeObject(errors).ToLower().Contains(Constants.ValidationErrorMessage.GroupFilterEmptyError.ToLower())) && !JsonConvert.SerializeObject(errors).ToLower().Contains(Constants.TokenConstants.Username.ToLower()) && !JsonConvert.SerializeObject(errors).ToLower().Contains(Constants.TokenConstants.Password.ToLower())
                && !JsonConvert.SerializeObject(errors).ToLower().Contains(Constants.ErrorMessages.InvalidUsdmVersion.ToLower()) && !errors.Any(key => key.Key?.ToLower() == nameof(Core.DTO.Common.AuditTrailDto.UsdmVersion).ToLower()))
            {

                errorList.ForEach(e => _logger.LogError($"Conformance Error {errorList.IndexOf(e) + 1}: {e}"));
                _logger.LogInformation($"Status Code: {400}; UserName : {context?.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value}; UserRole : {context?.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value} URL: {context?.HttpContext?.Request?.Path}; AuthToken: {AuthToken}");
                return new BadRequestObjectResult(ErrorResponseHelper.BadRequest(errors,$"{Constants.ErrorMessages.ConformanceErrorMessage}{usdmVersion}"));
            }
            //Other errors
            else
            {
                errorList.ForEach(e => _logger.LogError($"Invalid Input {errorList.IndexOf(e) + 1}: {e}"));
                _logger.LogInformation($"Status Code: {400}; UserName : {context?.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value}; UserRole : {context?.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value} URL: {context?.HttpContext?.Request?.Path}; AuthToken: {AuthToken}");
                return new BadRequestObjectResult(ErrorResponseHelper.BadRequest(errors, "Invalid Input"));
            }
        }
    }
}
