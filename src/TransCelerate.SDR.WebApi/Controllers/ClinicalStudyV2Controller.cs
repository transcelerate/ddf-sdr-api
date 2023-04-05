using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
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
    public class ClinicalStudyV2Controller : ControllerBase
    {
        #region Variables        
        private readonly ILogHelper _logger;
        private readonly IClinicalStudyServiceV2 _clinicalStudyService;
        private readonly IHelperV2 _helper;
        #endregion

        #region Constructor
        public ClinicalStudyV2Controller(IClinicalStudyServiceV2 clinicalStudyService, ILogHelper logger, IHelperV2 helper)
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
        [Route(Route.StudyV2)]
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
        /// <param name="studydesignId">Study Design ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="listofelements">List of study design elements with comma separated values</param>
        /// <param name="usdmVersion">USDM Version</param>
        /// <response code="200">Returns Study</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The Study for the studyId is Not Found</response>
        [HttpGet]
        [Route(Route.StudyDesignV2)]
        [ApiVersion(Constants.USDMVersions.V2)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StudyDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetStudyDesigns(string studyId, int sdruploadversion, string studydesignId, string listofelements,
                                                  [FromHeader(Name = IdFieldPropertyName.Common.UsdmVersion)][BindRequired] string usdmVersion)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyV2Controller)}; Method : {nameof(GetStudyDesigns)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs : study_uuid = {studyId}; sdruploadversion = {sdruploadversion}; listofelements: {listofelements}; studydesign_uuid: {studydesignId}");

                    if (!_helper.AreValidStudyDesignElements(listofelements, out string[] listofelementsArray))
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.StudyDesignElementNotValid)).Value);

                    LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);

                    var study = await _clinicalStudyService.GetStudyDesigns(studyId, studydesignId, sdruploadversion, user, listofelementsArray).ConfigureAwait(false);

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
        /// GET SoA For a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="studyDesignId">Study Design ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="studyWorkflowId">WorkflowId</param>
        /// <response code="200">Returns Study</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The Study for the studyId is Not Found</response>
        [HttpGet]
        [Route(Route.SoAV2)]
        [ApiVersion(Constants.USDMVersions.V2)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StudyDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetSOA(string studyId, string studyDesignId, string studyWorkflowId, int sdruploadversion)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyV2Controller)}; Method : {nameof(GetStudyDesigns)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs : study_uuid = {studyId}; sdruploadversion = {sdruploadversion}; WorkflowId: {studyWorkflowId}; studydesign_uuid: {studyDesignId}");
                    if (String.IsNullOrWhiteSpace(studyDesignId) && !String.IsNullOrWhiteSpace(studyWorkflowId))
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.EnterDesignIdError)).Value);

                    LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);

                    var SoA = await _clinicalStudyService.GetSOA(studyId, studyDesignId, studyWorkflowId, sdruploadversion, user).ConfigureAwait(false);

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
                    else if (SoA.ToString() == Constants.ErrorMessages.WorkFlowNotFound)
                    {
                        return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.WorkFlowNotFound)).Value);
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
        /// GET Audit Trail of a study
        /// </summary>
        /// <param name="fromDate">Start Date for Date Filter</param>
        /// <param name="toDate">End Date for Date Filter</param>
        /// <param name="studyId">Study ID</param>
        /// <response code="200">Returns a list of Audit Trail of a study</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The Audit trail for the study is Not Found</response>
        [HttpGet]
        [Route(Route.AuditTrailV2)]
        [NonAction]
        [ApiVersionNeutral]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<AudiTrailResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetAuditTrail(string studyId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyV2Controller)}; Method : {nameof(GetAuditTrail)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs : studyId = {studyId}; fromDate = {fromDate}; toDate = {toDate}");

                    LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);

                    Tuple<DateTime, DateTime> fromAndToDate = FromDateToDateHelper.GetFromAndToDate(fromDate, toDate, -1);

                    fromDate = fromAndToDate.Item1;
                    toDate = fromAndToDate.Item2;
                    if (fromDate <= toDate)
                    {
                        var studyAuditResponse = await _clinicalStudyService.GetAuditTrail(studyId, fromDate, toDate, user);
                        if (studyAuditResponse == null)
                        {
                            return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.StudyNotFound)).Value);
                        }
                        else if (studyAuditResponse.ToString() == Constants.ErrorMessages.Forbidden)
                        {
                            return StatusCode(((int)HttpStatusCode.Forbidden), new JsonResult(ErrorResponseHelper.Forbidden()).Value);
                        }
                        else
                        {
                            return Ok(studyAuditResponse);
                        }
                    }
                    else
                    {
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.DateError)).Value);
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
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyV2Controller)}; Method : {nameof(GetAuditTrail)};");
            }
        }

        /// <summary>
        /// Get All StudyId's in the database
        /// </summary>
        /// <param name="fromDate">Start Date for Date Filter</param>
        /// <param name="toDate">End Date for Date Filter</param>
        /// <param name="studyTitle">Study Title Filter</param>
        /// <response code="200">Returns All Study Id's</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">There is no study</response>
        [HttpGet]
        [Route(Route.StudyHistoryV2)]
        [ApiVersionNeutral]
        [NonAction]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StudyHistoryResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetStudyHistory(DateTime fromDate, DateTime toDate, string studyTitle)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyV2Controller)}; Method : {nameof(GetStudyHistory)};");

                LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);

                _logger.LogInformation($"Inputs: FromDate: {fromDate}; ToDate: {toDate}; DateRange from Key Vault :{Config.DateRange}");

                Tuple<DateTime, DateTime> fromAndToDate = FromDateToDateHelper.GetFromAndToDate(fromDate, toDate, Convert.ToInt32(Config.DateRange));

                fromDate = fromAndToDate.Item1;
                toDate = fromAndToDate.Item2;

                if (fromDate <= toDate)
                {
                    var studyHistoryResponse = await _clinicalStudyService.GetStudyHistory(fromDate, toDate, studyTitle, user);
                    if (studyHistoryResponse == null)
                    {
                        return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.StudyNotFound)).Value);
                    }
                    else
                    {
                        return Ok(studyHistoryResponse);
                    }
                }
                else
                {
                    return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.DateError)).Value);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured. Exception : {ex}");
                return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
            }
            finally
            {
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyV2Controller)}; Method : {nameof(GetStudyHistory)};");
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
        [ApiVersion(Constants.USDMVersions.V2)]
        [Route(Route.PostElementsV2)]
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
        /// POST/PUT All Elements For a Study  
        /// </summary>        
        /// <param name="studyDTO">Study for Inserting/Updating in Database</param>        
        /// <param name="usdmVersion">USDM Version</param>        
        /// <param name="studyId">USDM Version</param>        
        /// <response code="201">Study Created</response>
        /// <response code="400">Bad Request</response>       
        [HttpPut]
        [ApiVersion(Constants.USDMVersions.V2)]
        [Route(Route.StudyV2)]
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
