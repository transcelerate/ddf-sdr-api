using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2;
using TransCelerate.SDR.Services.Interfaces;

namespace TransCelerate.SDR.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    public class StudyV2Controller : ControllerBase
    {
        #region Variables        
        private readonly ILogHelper _logger;
        private readonly IStudyServiceV2 _studyService;
        private readonly IHelperV2 _helper;
        #endregion

        #region Constructor
        public StudyV2Controller(IStudyServiceV2 studyService, ILogHelper logger, IHelperV2 helper)
        {
            _logger = logger;
            _studyService = studyService;
            _helper = helper;
        }
        #endregion

        #region Action Methods

        #region GET Methods
        /// <summary>
        /// GET All Elements For a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param> 
        /// <param name="listofelements">List of elements with comma separated values</param>
        /// <param name="usdmVersion">usdm-vreison header</param>
        /// <response code="200">Returns Study</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The Study for the studyId is Not Found</response>
        [HttpGet]
        [ApiVersion(Constants.USDMVersions.V1_9)]
        [Route(Route.StudyV2)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StudyDefinitionsDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetStudy(string studyId, int sdruploadversion, string listofelements,
                                                  [FromHeader(Name = IdFieldPropertyName.Common.UsdmVersion)][BindRequired] string usdmVersion)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(StudyV2Controller)}; Method : {nameof(GetStudy)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs : studyId = {studyId}; sdruploadversion = {sdruploadversion}; listofelements: {listofelements}");

                    if (!_helper.AreValidStudyElements(listofelements, out string[] listofelementsArray))
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.StudyElementNotValid)).Value);

                    LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);

                    var study = listofelementsArray == null ? await _studyService.GetStudy(studyId, sdruploadversion, user).ConfigureAwait(false)
                                                            : await _studyService.GetPartialStudyElements(studyId, sdruploadversion, user, listofelementsArray).ConfigureAwait(false);

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
                _logger.LogInformation($"Ended Controller : {nameof(StudyV2Controller)}; Method : {nameof(GetStudy)};");
            }
        }

        /// <summary>
        /// GET Study Designs of a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="studyDesignId">Study Design ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="listofelements">List of study design elements with comma separated values</param>
        /// <param name="usdmVersion">USDM Version</param>
        /// <response code="200">Returns Study</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The Study for the studyId is Not Found</response>
        [HttpGet]
        [Route(Route.StudyDesignV2)]
        [ApiVersion(Constants.USDMVersions.V1_9)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StudyDefinitionsDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetStudyDesigns(string studyId, int sdruploadversion, string studyDesignId, string listofelements,
                                                  [FromHeader(Name = IdFieldPropertyName.Common.UsdmVersion)][BindRequired] string usdmVersion)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(StudyV2Controller)}; Method : {nameof(GetStudyDesigns)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs : study_uuid = {studyId}; sdruploadversion = {sdruploadversion}; listofelements: {listofelements}; studydesign_uuid: {studyDesignId}");

                    if (!_helper.AreValidStudyDesignElements(listofelements, out string[] listofelementsArray))
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.StudyDesignElementNotValid)).Value);

                    LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);

                    var study = await _studyService.GetStudyDesigns(studyId, studyDesignId, sdruploadversion, user, listofelementsArray).ConfigureAwait(false);

                    if (study == null)
                    {
                        return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.StudyNotFound)).Value);
                    }
                    else if (study.ToString() == Constants.ErrorMessages.Forbidden)
                    {
                        return StatusCode(((int)HttpStatusCode.Forbidden), new JsonResult(ErrorResponseHelper.Forbidden()).Value);
                    }
                    else if (study.ToString() == Constants.ErrorMessages.StudyDesignNotFound)
                    {
                        return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.StudyDesignNotFound)).Value);
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
                _logger.LogInformation($"Ended Controller : {nameof(StudyV2Controller)}; Method : {nameof(GetStudyDesigns)};");
            }
        }

        /// <summary>
        /// GET SoA For a Study USDM Version 1.9
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="studyDesignId">Study Design ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="scheduleTimelineId">Schedule Timeline Id</param>
        /// <response code="200">Returns Study</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The Study for the studyId is Not Found</response>
        [HttpGet]
        [Route(Route.SoAV2)] 
        [ApiVersion(Constants.USDMVersions.V1_9)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StudyDefinitionsDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetSOAV2(string studyId, string studyDesignId, string scheduleTimelineId, int sdruploadversion)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(StudyV2Controller)}; Method : {nameof(GetStudyDesigns)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs : study_uuid = {studyId}; sdruploadversion = {sdruploadversion}; WorkflowId: {scheduleTimelineId}; studydesign_uuid: {studyDesignId}");
                    if (String.IsNullOrWhiteSpace(studyDesignId) && !String.IsNullOrWhiteSpace(scheduleTimelineId))
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.EnterDesignIdError)).Value);

                    LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);

                    var SoA = await _studyService.GetSOAV2(studyId, studyDesignId, scheduleTimelineId, sdruploadversion, user).ConfigureAwait(false);

                    if (SoA == null)
                    {
                        return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.StudyNotFound)).Value);
                    }
                    else if (SoA.ToString() == Constants.ErrorMessages.Forbidden)
                    {
                        return StatusCode(((int)HttpStatusCode.Forbidden), new JsonResult(ErrorResponseHelper.Forbidden()).Value);
                    }
                    else if (SoA.ToString() == Constants.ErrorMessages.StudyDesignNotFound)
                    {
                        return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.StudyDesignNotFound)).Value);
                    }
                    else if (SoA.ToString() == Constants.ErrorMessages.ScheduleTimelineNotFound)
                    {
                        return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.ScheduleTimelineNotFound)).Value);
                    }
                    else
                    {
                        return Ok(SoA);
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
                _logger.LogInformation($"Ended Controller : {nameof(StudyV2Controller)}; Method : {nameof(GetStudyDesigns)};");
            }
        }

        /// <summary>
        /// GET eCPT Elements For a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param> 
        /// <param name="studydesignId">studyDesignId</param> 
        /// <response code="200">Returns Study</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The Study for the studyId is Not Found</response>
        [HttpGet]
        [Route(Route.GeteCPTV2)]
        [ApiVersion(Constants.USDMVersions.V1_9)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GeteCPTV2(string studyId, int sdruploadversion, string studydesignId)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(CommonController)}; Method : {nameof(GeteCPTV2)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs : studyId = {studyId}; sdruploadversion = {sdruploadversion};");

                    LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);

                    var study = await _studyService.GeteCPTV2(studyId, sdruploadversion, studydesignId, user);

                    if (study == null)
                    {
                        return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.StudyNotFound)).Value);
                    }
                    else if (study.ToString() == Constants.ErrorMessages.Forbidden)
                    {
                        return StatusCode(((int)HttpStatusCode.Forbidden), new JsonResult(ErrorResponseHelper.Forbidden()).Value);
                    }
                    else if (study.ToString() == Constants.ErrorMessages.eCPTError)
                    {
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.eCPTError)).Value);
                    }
                    else if (study.ToString() == Constants.ErrorMessages.StudyDesignNotFoundCPT)
                    {
                        return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.StudyDesignNotFoundCPT)).Value);
                    }
                    else if (study.ToString() == Constants.ErrorMessages.StudyDesignIdNotFoundCPT)
                    {
                        return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.StudyDesignIdNotFoundCPT)).Value);
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
                _logger.LogInformation($"Ended Controller : {nameof(CommonController)}; Method : {nameof(GeteCPTV2)};");
            }
        }
        #endregion

        #region POST/PUT Methods
        /// <summary>
        /// POST/PUT All Elements For a Study  
        /// </summary>        
        /// <param name="studyDTO">Study for Inserting/Updating in Database</param>        
        /// <param name="usdmVersion">USDM Version</param>        
        /// <response code="201">Study Created</response>
        /// <response code="400">Bad Request</response>       
        [HttpPost]
        [ApiVersion(Constants.USDMVersions.V1_9)]
        [Route(Route.PostElementsV2)]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(StudyDefinitionsDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> PostAllElements([FromBody] StudyDefinitionsDto studyDTO, [FromHeader(Name = IdFieldPropertyName.Common.UsdmVersion)][BindRequired] string usdmVersion)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(StudyV2Controller)}; Method : {nameof(PostAllElements)};");
                if (studyDTO != null)
                {
                    bool isInValidReferenceIntegrity = _helper.ReferenceIntegrityValidation(studyDTO, out var errors);
                    if (isInValidReferenceIntegrity)
                    {
                        var errorList = SplitStringIntoArrayHelper.SplitString(JsonConvert.SerializeObject(errors), 32000);//since app insights limit is 32768 characters   
                        errorList.ForEach(e => _logger.LogError($"{Constants.ErrorMessages.ErrorMessageForReferenceIntegrityInResponse} {errorList.IndexOf(e) + 1}: {e}"));
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(errors, Constants.ErrorMessages.ErrorMessageForReferenceIntegrityInResponse)).Value);
                    }


                    LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);

                    var response = await _studyService.PostAllElements(studyDTO, user, Request?.Method)
                                                              .ConfigureAwait(false);

                    if (response?.ToString() == Constants.ErrorMessages.PostRestricted)
                    {
                        return StatusCode(((int)HttpStatusCode.Unauthorized), new JsonResult(ErrorResponseHelper.UnAuthorizedAccess(Constants.ErrorMessages.PostRestricted)).Value);
                    }
                    else
                    {
                        if (response?.ToString() == Constants.ErrorMessages.NotValidStudyId)
                        {
                            return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.NotValidStudyId)).Value);
                        }
                        else
                        {
                            return Created($"study/{studyDTO.Study.StudyId}", new JsonResult(response).Value);
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
                _logger.LogError($"Exception occured. Exception : {ex}");
                return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
            }
            finally
            {
                _logger.LogInformation($"Ended Controller : {nameof(StudyV2Controller)}; Method : {nameof(PostAllElements)};");
            }
        }
        /// <summary>
        /// POST/PUT All Elements For a Study  
        /// </summary>        
        /// <param name="studyDTO">Study for Inserting/Updating in Database</param>        
        /// <param name="usdmVersion">USDM Version</param>        
        /// <param name="studyId">USDM Version</param>        
        /// <response code="201">Study Created</response>
        /// <response code="400">Bad Request</response>       
        [HttpPut]
        [ApiVersion(Constants.USDMVersions.V1_9)]
        [Route(Route.StudyV2)]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(StudyDefinitionsDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> PutStudy([FromBody] StudyDefinitionsDto studyDTO, string studyId, [FromHeader(Name = IdFieldPropertyName.Common.UsdmVersion)][BindRequired] string usdmVersion)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(StudyV2Controller)}; Method : {nameof(PostAllElements)};");
                if (studyDTO != null)
                {
                    bool isInValidReferenceIntegrity = _helper.ReferenceIntegrityValidation(studyDTO, out var errors);
                    if (isInValidReferenceIntegrity)
                    {
                        var errorList = SplitStringIntoArrayHelper.SplitString(JsonConvert.SerializeObject(errors), 32000);//since app insights limit is 32768 characters   
                        errorList.ForEach(e => _logger.LogError($"{Constants.ErrorMessages.ErrorMessageForReferenceIntegrityInResponse} {errorList.IndexOf(e) + 1}: {e}"));
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(errors, Constants.ErrorMessages.ErrorMessageForReferenceIntegrityInResponse)).Value);
                    }
                    studyDTO.Study.StudyId = string.IsNullOrWhiteSpace(studyId) ? studyDTO.Study.StudyId : studyId;

                    LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);

                    var response = await _studyService.PostAllElements(studyDTO, user, Request?.Method)
                                                              .ConfigureAwait(false);

                    if (response?.ToString() == Constants.ErrorMessages.PostRestricted)
                    {
                        return StatusCode(((int)HttpStatusCode.Unauthorized), new JsonResult(ErrorResponseHelper.UnAuthorizedAccess(Constants.ErrorMessages.PostRestricted)).Value);
                    }
                    else
                    {
                        if (response?.ToString() == Constants.ErrorMessages.NotValidStudyId)
                        {
                            return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.NotValidStudyId)).Value);
                        }
                        else
                        {
                            return Created($"study/{studyDTO.Study.StudyId}", new JsonResult(response).Value);
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
                _logger.LogError($"Exception occured. Exception : {ex}");
                return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
            }
            finally
            {
                _logger.LogInformation($"Ended Controller : {nameof(StudyV2Controller)}; Method : {nameof(PostAllElements)};");
            }
        }
        #endregion

        #region DELETE Method
        /// <summary>
        /// Delete a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <response code="200">Deleted all versions of Study with the mentioned studyId</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The Study for the studyId is Not Found</response>
        [HttpDelete]
        [Authorize(Roles = Constants.Roles.Org_Admin)]
        [ApiVersionNeutral]
        [Route(Route.StudyV2)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StudyDefinitionsDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteStudy(string studyId)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(StudyV2Controller)}; Method : {nameof(GetStudy)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs : studyId = {studyId};");

                    LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);

                    var response = await _studyService.DeleteStudy(studyId, user).ConfigureAwait(false);

                    if (response == null)
                    {
                        return BadRequest(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.GenericError)).Value);
                    }
                    else if (response?.ToString() == Constants.ErrorMessages.NotValidStudyId)
                    {
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.NotValidStudyId)).Value);
                    }
                    else if (response?.ToString() == Constants.ErrorMessages.Forbidden)
                    {
                        return StatusCode(((int)HttpStatusCode.Forbidden), new JsonResult(ErrorResponseHelper.Forbidden()).Value);
                    }
                    else
                    {
                        return Ok(new { statusCode = ((int)HttpStatusCode.OK).ToString(), message = $"All versions of study definition with uuid : '{studyId}' are deleted" });
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
                _logger.LogInformation($"Ended Controller : {nameof(StudyV2Controller)}; Method : {nameof(GetStudy)};");
            }
        }
        #endregion        
        #endregion
    }
}
