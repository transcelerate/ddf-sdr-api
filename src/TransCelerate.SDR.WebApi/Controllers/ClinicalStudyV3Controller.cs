﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV3;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3;
using TransCelerate.SDR.Services.Interfaces;

namespace TransCelerate.SDR.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    public class ClinicalStudyV3Controller : ControllerBase
    {
        #region Variables        
        private readonly ILogHelper _logger;
        private readonly IClinicalStudyServiceV3 _clinicalStudyService;
        private readonly IHelperV3 _helper;
        #endregion

        #region Constructor
        public ClinicalStudyV3Controller(IClinicalStudyServiceV3 clinicalStudyService, ILogHelper logger, IHelperV3 helper)
        {
            _logger = logger;
            _clinicalStudyService = clinicalStudyService;
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
        [ApiVersion(Constants.USDMVersions.V2)]
        [Route(Route.StudyV3)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StudyDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetStudy(string studyId, int sdruploadversion, string listofelements,
                                                  [FromHeader(Name = IdFieldPropertyName.Common.UsdmVersion)][BindRequired] string usdmVersion)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyV2Controller)}; Method : {nameof(GetStudy)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs : studyId = {studyId}; sdruploadversion = {sdruploadversion}; listofelements: {listofelements}");

                    if (!_helper.AreValidStudyElements(listofelements, out string[] listofelementsArray))
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.StudyElementNotValid)).Value);

                    LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);

                    var study = listofelementsArray == null ? await _clinicalStudyService.GetStudy(studyId, sdruploadversion, user).ConfigureAwait(false)
                                                            : await _clinicalStudyService.GetPartialStudyElements(studyId, sdruploadversion, user, listofelementsArray).ConfigureAwait(false);

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
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyV2Controller)}; Method : {nameof(GetStudy)};");
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
        [Route(Route.StudyDesignV3)]
        [ApiVersion(Constants.USDMVersions.V2)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StudyDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetStudyDesigns(string studyId, int sdruploadversion, string studyDesignId, string listofelements,
                                                  [FromHeader(Name = IdFieldPropertyName.Common.UsdmVersion)][BindRequired] string usdmVersion)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyV2Controller)}; Method : {nameof(GetStudyDesigns)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs : study_uuid = {studyId}; sdruploadversion = {sdruploadversion}; listofelements: {listofelements}; studydesign_uuid: {studyDesignId}");

                    if (!_helper.AreValidStudyDesignElements(listofelements, out string[] listofelementsArray))
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.StudyDesignElementNotValid)).Value);

                    LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);

                    var study = await _clinicalStudyService.GetStudyDesigns(studyId, studyDesignId, sdruploadversion, user, listofelementsArray).ConfigureAwait(false);

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
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyV2Controller)}; Method : {nameof(GetStudyDesigns)};");
            }
        }

        /// <summary>
        /// GET SoA For a Study USDM Version 2.0
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="studyDesignId">Study Design ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="scheduleTimelineId">Schedule Timeline Id</param>
        /// <response code="200">Returns Study</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The Study for the studyId is Not Found</response>
        [HttpGet]
        [Route(Route.SoAV3)] 
        [ApiVersion(Constants.USDMVersions.V2)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StudyDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetSOAV3(string studyId, string studyDesignId, string scheduleTimelineId, int sdruploadversion)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyV2Controller)}; Method : {nameof(GetStudyDesigns)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs : study_uuid = {studyId}; sdruploadversion = {sdruploadversion}; WorkflowId: {scheduleTimelineId}; studydesign_uuid: {studyDesignId}");
                    if (String.IsNullOrWhiteSpace(studyDesignId) && !String.IsNullOrWhiteSpace(scheduleTimelineId))
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.EnterDesignIdError)).Value);

                    LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);

                    var SoA = await _clinicalStudyService.GetSOAV3(studyId, studyDesignId, scheduleTimelineId, sdruploadversion, user).ConfigureAwait(false);

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
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyV2Controller)}; Method : {nameof(GetStudyDesigns)};");
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
        [Route(Route.GeteCPTV3)]
        [ApiVersion(Constants.USDMVersions.V2)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GeteCPTV3(string studyId, int sdruploadversion, string studydesignId)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(CommonController)}; Method : {nameof(GeteCPTV3)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs : studyId = {studyId}; sdruploadversion = {sdruploadversion};");

                    LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);

                    var study = await _clinicalStudyService.GeteCPTV3(studyId, sdruploadversion, studydesignId, user);

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
                _logger.LogInformation($"Ended Controller : {nameof(CommonController)}; Method : {nameof(GeteCPTV3  )};");
            }
        }
        #endregion

        #region POST/PUT Methods
        /// <summary>
        /// POST All Elements For a Study  
        /// </summary>        
        /// <param name="studyDTO">Study for Inserting/Updating in Database</param>        
        /// <param name="usdmVersion">USDM Version</param>        
        /// <response code="201">Study Created</response>
        /// <response code="400">Bad Request</response>       
        [HttpPost]
        [ApiVersion(Constants.USDMVersions.V2)]
        [Route(Route.PostElementsV3)]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(StudyDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> PostAllElements([FromBody] StudyDto studyDTO, [FromHeader(Name = IdFieldPropertyName.Common.UsdmVersion)][BindRequired] string usdmVersion)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyV2Controller)}; Method : {nameof(PostAllElements)};");
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

                    var response = await _clinicalStudyService.PostAllElements(studyDTO, user, Request?.Method)
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
                            return Created($"study/{studyDTO.ClinicalStudy.StudyId}", new JsonResult(response).Value);
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
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyV2Controller)}; Method : {nameof(PostAllElements)};");
            }
        }
        /// <summary>
        /// PUT All Elements For a Study  
        /// </summary>        
        /// <param name="studyDTO">Study for Inserting/Updating in Database</param>        
        /// <param name="usdmVersion">USDM Version</param>        
        /// <param name="studyId">USDM Version</param>        
        /// <response code="201">Study Created</response>
        /// <response code="400">Bad Request</response>       
        [HttpPut]
        [ApiVersion(Constants.USDMVersions.V2)]
        [Route(Route.StudyV3)]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(StudyDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> PutStudy([FromBody] StudyDto studyDTO, string studyId, [FromHeader(Name = IdFieldPropertyName.Common.UsdmVersion)][BindRequired] string usdmVersion)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyV2Controller)}; Method : {nameof(PostAllElements)};");
                if (studyDTO != null)
                {
                    bool isInValidReferenceIntegrity = _helper.ReferenceIntegrityValidation(studyDTO, out var errors);
                    if (isInValidReferenceIntegrity)
                    {
                        var errorList = SplitStringIntoArrayHelper.SplitString(JsonConvert.SerializeObject(errors), 32000);//since app insights limit is 32768 characters   
                        errorList.ForEach(e => _logger.LogError($"{Constants.ErrorMessages.ErrorMessageForReferenceIntegrityInResponse} {errorList.IndexOf(e) + 1}: {e}"));
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(errors, Constants.ErrorMessages.ErrorMessageForReferenceIntegrityInResponse)).Value);
                    }
                    studyDTO.ClinicalStudy.StudyId = string.IsNullOrWhiteSpace(studyId) ? studyDTO.ClinicalStudy.StudyId : studyId;

                    LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);

                    var response = await _clinicalStudyService.PostAllElements(studyDTO, user, Request?.Method)
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
                            return Created($"study/{studyDTO.ClinicalStudy.StudyId}", new JsonResult(response).Value);
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
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyV2Controller)}; Method : {nameof(PostAllElements)};");
            }
        }
        #endregion

        #region USDM Conformance Validation
        /// <summary>
        /// Validate USDM Conformance rules for a Study
        /// </summary>        
        /// <param name="studyDTO">Study for Validation</param>        
        /// <param name="usdmVersion">USDM Version</param>        
        /// <response code="201">Study Created</response>
        /// <response code="400">Bad Request</response>       
        [HttpPost]
        [ApiVersion(Constants.USDMVersions.V2)]
        [Route(Route.ValidateUsdmConformanceV3)]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(StudyDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public IActionResult ValidateUsdmConformance([FromBody] StudyDto studyDTO, [FromHeader(Name = IdFieldPropertyName.Common.UsdmVersion)][BindRequired] string usdmVersion)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyV2Controller)}; Method : {nameof(PostAllElements)};");
                if (studyDTO != null)
                {
                    bool isInValidReferenceIntegrity = _helper.ReferenceIntegrityValidation(studyDTO, out var errors);
                    if (isInValidReferenceIntegrity)
                    {
                        var errorList = SplitStringIntoArrayHelper.SplitString(JsonConvert.SerializeObject(errors), 32000);//since app insights limit is 32768 characters   
                        errorList.ForEach(e => _logger.LogError($"{Constants.ErrorMessages.ErrorMessageForReferenceIntegrityInResponse} {errorList.IndexOf(e) + 1}: {e}"));
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(errors, Constants.ErrorMessages.ErrorMessageForReferenceIntegrityInResponse)).Value);
                    }
                    return Ok(new JsonResult(SuccessResponseHelper.ValidationSuccess($"{Constants.SuccessMessages.ValidationSuccess}{usdmVersion}")).Value);
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
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyV2Controller)}; Method : {nameof(PostAllElements)};");
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
        [Route(Route.StudyV3)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StudyDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteStudy(string studyId)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyV2Controller)}; Method : {nameof(GetStudy)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs : studyId = {studyId};");

                    LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);

                    var response = await _clinicalStudyService.DeleteStudy(studyId, user).ConfigureAwait(false);

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
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyV2Controller)}; Method : {nameof(GetStudy)};");
            }
        }
        #endregion        
        #endregion
    }
}
