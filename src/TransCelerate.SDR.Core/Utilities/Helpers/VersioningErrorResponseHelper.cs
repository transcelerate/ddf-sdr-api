using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public class VersioningErrorResponseHelper : IErrorResponseProvider
    {
        public IActionResult CreateResponse(ErrorResponseContext context)
        {
            return context.ErrorCode switch
            {
                Constants.ApiVersionErrorCodes.UnsupportedApiVersion => new BadRequestObjectResult(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.UsdmVersionMapError)).Value),
                Constants.ApiVersionErrorCodes.ApiVersionUnspecified => new BadRequestObjectResult(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.UsdmVersionMissing)).Value),
                Constants.ApiVersionErrorCodes.AmbiguousApiVersion => new BadRequestObjectResult(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.UsdmVersionAmbiguous)).Value),
                Constants.ApiVersionErrorCodes.InvalidApiVersion => new BadRequestObjectResult(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.UsdmVersionMapError)).Value),
                _ => new BadRequestObjectResult(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.UsdmVersionMapError)).Value),
            };
        }
    }
}
