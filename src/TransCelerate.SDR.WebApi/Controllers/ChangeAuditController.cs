using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Common;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.Services.Interfaces;

namespace TransCelerate.SDR.WebApi.Controllers
{
    [Authorize]
    [ApiVersionNeutral]
    [ApiController]
    public class ChangeAuditController : ControllerBase
    {
        #region Variables        
        private readonly ILogHelper _logger;
        private readonly IChangeAuditService _changeAuditService;
        #endregion

        #region Constructor
        public ChangeAuditController(IChangeAuditService changeAuditService, ILogHelper logger)
        {
            _logger = logger;
            _changeAuditService = changeAuditService;
        }
        #endregion

        #region Action Methods

        #region GET Methods
        /// <summary>
        /// GET Change Audit For a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <response code="200">Returns Change Audit</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The Study for the studyId is Not Found</response>
        /// <response code="403">The Access for Study is Forbidden</response>
        [HttpGet]
        [Route(Route.ChangeAudit)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChangeAuditDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetChangeAudit(string studyId)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ChangeAuditController)}; Method : {nameof(GetChangeAudit)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    LoggedInUser user = new()
                    {
                        UserName = User?.FindFirst(ClaimTypes.Email)?.Value,
                        UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                    };
                    var changeAudit = await _changeAuditService.GetChangeAudit(studyId, user).ConfigureAwait(false);

                    if (changeAudit == null)
                    {
                        return NotFound(new JsonResult(ErrorResponseHelper.NotFound($"{Constants.ErrorMessages.ChangeAuditNotFound} {studyId}")).Value);
                    }
                    else if (changeAudit.ToString() == Constants.ErrorMessages.Forbidden)
                    {
                        return StatusCode(((int)HttpStatusCode.Forbidden), new JsonResult(ErrorResponseHelper.Forbidden()).Value);
                    }
                    else
                    {
                        return Ok(changeAudit);
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
                _logger.LogInformation($"Ended Controller : {nameof(ChangeAuditController)}; Method : {nameof(GetChangeAudit)};");
            }
        }

        #endregion
        #endregion
    }
}
