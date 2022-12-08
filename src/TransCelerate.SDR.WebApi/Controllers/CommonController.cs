using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.Services.Interfaces;

namespace TransCelerate.SDR.WebApi.Controllers
{
    [ApiController]
    public class CommonController : ControllerBase
    {
        #region Variables
        private readonly ILogHelper _logger;
        private readonly ICommonService _commonService;
        #endregion

        #region Constructor
        public CommonController(ICommonService commonService, ILogHelper logger)
        {
            _logger = logger;
            _commonService = commonService;
        }
        #endregion

        #region Action Methods

        #region Get Method
        /// <summary>
        /// GET All Elements For a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param> 
        /// <response code="200">Returns Study</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The Study for the studyId is Not Found</response>
        [HttpGet]
        [Route(Route.GetRawJson)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetRawJson(string studyId,int sdruploadversion)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(CommonController)}; Method : {nameof(GetRawJson)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs : studyId = {studyId}; sdruploadversion = {sdruploadversion};");

                    LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);

                    var study = await _commonService.GetRawJson(studyId, sdruploadversion,user);

                    if (study == null)
                    {
                        return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.StudyNotFound)).Value);
                    }
                    else if (study.ToString() == Constants.ErrorMessages.Forbidden)
                    {
                        return StatusCode(((int)HttpStatusCode.Forbidden), new JsonResult(ErrorResponseHelper.Forbidden()).Value);
                    }
                    else
                    {
                        return Ok(study);
                    }
                }
                else
                {
                    return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.StudyInputError)).Value);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured. Exception : {ex}");
                return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
            }
            finally
            {
                _logger.LogInformation($"Ended Controller : {nameof(CommonController)}; Method : {nameof(GetRawJson)};");
            }
        }
        #endregion
        #endregion
    }
}
