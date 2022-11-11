using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2;

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
        /// <response code="200">Returns Study</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The Study for the studyId is Not Found</response>
        [HttpGet]
        [Route(Route.StudyV2)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StudyDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetStudy(string studyId, int sdruploadversion, string listofelements)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyV1Controller)}; Method : {nameof(GetStudy)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs : studyId = {studyId}; sdruploadversion = {sdruploadversion}; listofelements: {listofelements}");
                    string[] listofelementsArray = listofelements?.Split(Constants.Roles.Seperator);
                    if (!_helper.AreValidStudyElements(listofelements))
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.StudyElementNotValid)).Value);

                    LoggedInUser user = new LoggedInUser
                    {
                        UserName = User?.FindFirst(ClaimTypes.Email)?.Value,
                        UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                    };
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
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyV1Controller)}; Method : {nameof(GetStudy)};");
            }
        }

        /// <summary>
        /// GET Study Designs of a Study
        /// </summary>
        /// <param name="study_uuid">Study ID</param>
        /// <param name="studydesign_uuid">Study Design ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="listofelements">List of study design elements with comma separated values</param>
        /// <response code="200">Returns Study</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The Study for the studyId is Not Found</response>
        [HttpGet]
        [Route(Route.StudyDesignV2)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StudyDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetStudyDesigns(string study_uuid, int sdruploadversion, string studydesign_uuid, string listofelements)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyV1Controller)}; Method : {nameof(GetStudyDesigns)};");
                if (!String.IsNullOrWhiteSpace(study_uuid))
                {
                    _logger.LogInformation($"Inputs : study_uuid = {study_uuid}; sdruploadversion = {sdruploadversion}; listofelements: {listofelements}; studydesign_uuid: {studydesign_uuid}");
                    string[] listofelementsArray = listofelements?.Split(Constants.Roles.Seperator);
                    if (!_helper.AreValidStudyDesignElements(listofelements))
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.StudyDesignElementNotValid)).Value);

                    LoggedInUser user = new LoggedInUser
                    {
                        UserName = User?.FindFirst(ClaimTypes.Email)?.Value,
                        UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                    };
                    var study = await _clinicalStudyService.GetStudyDesigns(study_uuid, studydesign_uuid, sdruploadversion, user, listofelementsArray).ConfigureAwait(false);

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
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyV1Controller)}; Method : {nameof(GetStudyDesigns)};");
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
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<AudiTrailResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetAuditTrail(string studyId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyV1Controller)}; Method : {nameof(GetAuditTrail)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs : studyId = {studyId}; fromDate = {fromDate}; toDate = {toDate}");
                    LoggedInUser user = new LoggedInUser
                    {
                        UserName = User?.FindFirst(ClaimTypes.Email)?.Value,
                        UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                    };

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
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyV1Controller)}; Method : {nameof(GetAuditTrail)};");
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
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StudyHistoryResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetStudyHistory(DateTime fromDate, DateTime toDate, string studyTitle)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyV1Controller)}; Method : {nameof(GetStudyHistory)};");
                LoggedInUser user = new LoggedInUser
                {
                    UserName = User?.FindFirst(ClaimTypes.Email)?.Value,
                    UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                };
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
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyV1Controller)}; Method : {nameof(GetStudyHistory)};");
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
        [Route(Route.PostElementsV2)]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(StudyDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> PostAllElements([FromBody] StudyDto studyDTO)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyV1Controller)}; Method : {nameof(PostAllElements)};");
                if (studyDTO != null)
                {
                    bool isInValidReferenceIntegrity = _helper.ReferenceIntegrityValidation(studyDTO, out var errors);
                    if (isInValidReferenceIntegrity)
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(errors, Constants.ErrorMessages.ErrorMessageForReferenceIntegrityInResponse)).Value);

                    LoggedInUser user = new LoggedInUser
                    {
                        UserName = User?.FindFirst(ClaimTypes.Email)?.Value,
                        UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                    };
                    var response = await _clinicalStudyService.PostAllElements(studyDTO, user, HttpMethods.Post)
                                                              .ConfigureAwait(false);

                    if (response?.ToString() == Constants.ErrorMessages.PostRestricted)
                    {
                        return StatusCode(((int)HttpStatusCode.Unauthorized), new JsonResult(ErrorResponseHelper.UnAuthorizedAccess(Constants.ErrorMessages.PostRestricted)).Value);
                    }
                    else
                    {
                        return Created($"study/{studyDTO.ClinicalStudy.Uuid}", new JsonResult(response).Value);
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
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyV1Controller)}; Method : {nameof(PostAllElements)};");
            }
        }       
        #endregion

        #region PUT Method
        /// <summary>
        /// Create New Version for a study 
        /// </summary>        
        /// <param name="studyDTO">Study for Inserting/Updating in Database</param>        
        /// <response code="201">Study Created</response>
        /// <response code="400">Bad Request</response>       
        [HttpPut]
        [Route(Route.PostElementsV2)]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(StudyDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> CreateNewVersionForAStudy([FromBody] StudyDto studyDTO)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyV1Controller)}; Method : {nameof(PostAllElements)};");
                if (studyDTO != null)
                {
                    bool isInValidReferenceIntegrity = _helper.ReferenceIntegrityValidation(studyDTO, out var errors);
                    if (isInValidReferenceIntegrity)
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(errors, Constants.ErrorMessages.ErrorMessageForReferenceIntegrityInResponse)).Value);

                    LoggedInUser user = new LoggedInUser
                    {
                        UserName = User?.FindFirst(ClaimTypes.Email)?.Value,
                        UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                    };
                    var response = await _clinicalStudyService.PostAllElements(studyDTO, user, HttpMethods.Put)
                                                              .ConfigureAwait(false);

                    if (response?.ToString() == Constants.ErrorMessages.PostRestricted)
                    {
                        return StatusCode(((int)HttpStatusCode.Unauthorized), new JsonResult(ErrorResponseHelper.UnAuthorizedAccess(Constants.ErrorMessages.PostRestricted)).Value);
                    }
                    else
                    {
                        if (response?.ToString() == Constants.ErrorMessages.StudyIdNotFound)
                        {
                            return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.StudyIdNotFound)).Value);
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
                _logger.LogError($"Exception occured. Exception : {ex}");
                return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
            }
            finally
            {
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyV1Controller)}; Method : {nameof(PostAllElements)};");
            }
        }
        #endregion

        #region DELETE Method

        #endregion
        /// <summary>
        /// Delete a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <response code="200">Deleted all versions of Study with the mentioned studyId</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The Study for the studyId is Not Found</response>
        [HttpDelete]
        [Authorize(Roles = Constants.Roles.Org_Admin)]
        [Route(Route.StudyV2)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StudyDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteStudy(string studyId)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyV1Controller)}; Method : {nameof(GetStudy)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs : studyId = {studyId};");

                    LoggedInUser user = new LoggedInUser
                    {
                        UserName = User?.FindFirst(ClaimTypes.Email)?.Value,
                        UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                    };
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
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyV1Controller)}; Method : {nameof(GetStudy)};");
            }
        }
        #endregion
    }
}
