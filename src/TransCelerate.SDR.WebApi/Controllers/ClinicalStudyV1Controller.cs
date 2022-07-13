using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace TransCelerate.SDR.WebApi.Controllers
{
    [Authorize]
    [ApiController]        
    public class ClinicalStudyV1Controller : ControllerBase
    {
        #region Variables        
        private readonly ILogHelper _logger;        
        private readonly IClinicalStudyServiceV1 _clinicalStudyService;
        #endregion

        #region Constructor
        public ClinicalStudyV1Controller(IClinicalStudyServiceV1 clinicalStudyService, ILogHelper logger)
        {            
            _logger = logger;            
            _clinicalStudyService = clinicalStudyService;
        }
        #endregion

        #region Action Methods

        #region GET Methods
        /// <summary>
        /// GET All Elements For a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="version">Version of study</param>
        /// <response code="200">Returns Study</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The Study for the studyId is Not Found</response>
        [HttpGet]
        [Route(Route.StudyV1)]        
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StudyDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetStudy(string studyId, int version)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyV1Controller)}; Method : {nameof(GetStudy)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    LoggedInUser user = new LoggedInUser
                    {
                        UserName = User?.FindFirst(ClaimTypes.Name)?.Value,
                        UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                    };
                    var study = await _clinicalStudyService.GetStudy(studyId, version, user).ConfigureAwait(false);

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
                _logger.LogError($"Exception occured. Exception : {ex.Message}");
                return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
            }
            finally
            {
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyV1Controller)}; Method : {nameof(GetStudy)};");
            }
        }
        #endregion

        #region POST Methods
        /// <summary>
        /// POST All Elements For a Study  
        /// </summary>        
        /// <param name="studyDTO">Study for Inserting/Updating in Database</param>        
        /// <response code="201">Study Created</response>
        /// <response code="400">Bad Request</response>       
        [HttpPost]
        [Route(Route.PostElementsV1)]        
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(StudyDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> PostAllElements([FromBody] StudyDto studyDTO)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(PostAllElements)};");                
                if (studyDTO != null)
                {
                    LoggedInUser user = new LoggedInUser
                    {
                        UserName = User?.FindFirst(ClaimTypes.Name)?.Value,
                        UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                    };
                    var response = await _clinicalStudyService.PostAllElements(studyDTO, user)
                                                              .ConfigureAwait(false);

                    if (response?.ToString() == Constants.ErrorMessages.PostRestricted)
                    {
                        return StatusCode(((int)HttpStatusCode.Unauthorized), new JsonResult(ErrorResponseHelper.UnAuthorizedAccess(Constants.ErrorMessages.PostRestricted)).Value);
                    }
                    else
                    {
                        if (response?.ToString() == Constants.ErrorMessages.NotValidStudyId)
                        {
                            return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.NotValidStudyId)).Value);
                        }
                        else
                        {
                            return Created($"study/{studyDTO.ClinicalStudy.Uuid}", new JsonResult(response).Value);
                        }
                    }
                }
                else
                {
                    return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.StudyInputError)).Value);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured. Exception : {ex.Message}");
                return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
            }
            finally
            {
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyController)}; Method : {nameof(PostAllElements)};");
            }
        }
        #endregion

        #endregion
    }
}
