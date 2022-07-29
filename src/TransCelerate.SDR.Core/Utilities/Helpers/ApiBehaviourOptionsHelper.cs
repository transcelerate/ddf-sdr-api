using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public class ApiBehaviourOptionsHelper
    {
        private readonly ILogHelper _logger;       
        public ApiBehaviourOptionsHelper(ILogHelper logger)
        {
            _logger = logger;
        }
        public ObjectResult ModelStateResponse(ActionContext context)
        {                       
            var errors = context.ModelState.ToDictionary(
                    kvp => string.Join(".", kvp.Key.Split(".").Select(key => key.Substring(0, 1).ToLower() + key.Substring(1))),
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );
            context.HttpContext?.Response?.Headers?.Add("InvalidInput", "True");
            var errorList = SplitStringIntoArrayHelper.SplitString(JsonConvert.SerializeObject(errors), 32000);//since app insights limit is 32768 characters                                                              

            //For Conformance error
            if ((JsonConvert.SerializeObject(errors).ToLower().Contains(Constants.ValidationErrorMessage.PropertyEmptyError.ToLower()) || JsonConvert.SerializeObject(errors).ToLower().Contains(Constants.ValidationErrorMessage.PropertyMissingError.ToLower())
                || JsonConvert.SerializeObject(errors).ToLower().Contains(Constants.ValidationErrorMessage.SelectAtleastOneGroup.ToLower()) || JsonConvert.SerializeObject(errors).ToLower().Contains(Constants.ValidationErrorMessage.InvalidPermissionValue.ToLower())
                || JsonConvert.SerializeObject(errors).ToLower().Contains(Constants.ValidationErrorMessage.GroupFilterEmptyError.ToLower())) && !JsonConvert.SerializeObject(errors).ToLower().Contains(Constants.TokenConstants.Username.ToLower()) && !JsonConvert.SerializeObject(errors).ToLower().Contains(Constants.TokenConstants.Password.ToLower()))
            {

                errorList.ForEach(e => _logger.LogError($"Conformance Error {errorList.IndexOf(e) + 1}: {e}"));
                return new BadRequestObjectResult(ErrorResponseHelper.BadRequest(errors));
            }
            //Other errors
            else
            {
                errorList.ForEach(e => _logger.LogError($"Invalid Input {errorList.IndexOf(e) + 1}: {e}"));
                return new BadRequestObjectResult(ErrorResponseHelper.BadRequest(errors, "Invalid Input"));
            }
        }
    }
}
