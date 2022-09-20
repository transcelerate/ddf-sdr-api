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
using System.Collections.Generic;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1;

namespace TransCelerate.SDR.WebApi.Controllers
{
    [Authorize]
    [ApiController]        
    public class ClinicalStudyV1Controller : ControllerBase
    {
        #region Variables        
        private readonly ILogHelper _logger;        
        private readonly IClinicalStudyServiceV1 _clinicalStudyService;
        private readonly IHelper _helper;
        #endregion

        #region Constructor
        public ClinicalStudyV1Controller(IClinicalStudyServiceV1 clinicalStudyService, ILogHelper logger, IHelper helper)
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
        [Route(Route.StudyV1)]        
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
        [Route(Route.StudyDesignV1)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StudyDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetStudyDesigns(string study_uuid, int sdruploadversion,string studydesign_uuid,string listofelements)
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
                    var study = await _clinicalStudyService.GetStudyDesigns(study_uuid,studydesign_uuid, sdruploadversion, user,listofelementsArray).ConfigureAwait(false);

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
        [Route(Route.AuditTrailV1)]
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
        [Route(Route.StudyHistoryV1)]
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
        [Route(Route.PostElementsV1)]        
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
                    LoggedInUser user = new LoggedInUser
                    {
                        UserName = User?.FindFirst(ClaimTypes.Email)?.Value,
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
                _logger.LogError($"Exception occured. Exception : {ex}");
                return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
            }
            finally
            {
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyV1Controller)}; Method : {nameof(PostAllElements)};");
            }
        }


        /// <summary>
        /// Search For a Study 
        /// </summary>
        /// <param name="searchparameters">Parameters to search in database</param>
        /// <response code="200">Returns All Study that matches the search criteria</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">There is no study that matches the search criteria</response>
        [HttpPost]
        [Route(Route.SearchElementsV1)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<StudyDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> SearchStudy([FromBody] SearchParametersDto searchparameters)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyV1Controller)}; Method : {nameof(SearchStudy)};");
                LoggedInUser user = new LoggedInUser
                {
                    UserName = User?.FindFirst(ClaimTypes.Email)?.Value,
                    UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                };
                if (searchparameters != null)
                {
                    if (String.IsNullOrWhiteSpace(searchparameters.Indication)
                       && String.IsNullOrWhiteSpace(searchparameters.InterventionModel) && String.IsNullOrWhiteSpace(searchparameters.Phase)
                       && String.IsNullOrWhiteSpace(searchparameters.StudyId) && String.IsNullOrWhiteSpace(searchparameters.StudyTitle)
                       && String.IsNullOrWhiteSpace(searchparameters.FromDate) && String.IsNullOrWhiteSpace(searchparameters.ToDate))
                    {
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ValidationErrorMessage.AnyOneFieldError)).Value);
                    }
                    Tuple<DateTime, DateTime> fromAndToDate = FromDateToDateHelper.GetFromAndToDate(String.IsNullOrWhiteSpace(searchparameters.FromDate) ? DateTime.MinValue : Convert.ToDateTime(searchparameters.FromDate),
                                                                        String.IsNullOrWhiteSpace(searchparameters.ToDate) ? DateTime.MinValue : Convert.ToDateTime(searchparameters.ToDate), -1);

                    searchparameters.FromDate = fromAndToDate.Item1.ToString();
                    searchparameters.ToDate = fromAndToDate.Item2.ToString();
                   
                    if ((!String.IsNullOrWhiteSpace(searchparameters.FromDate)) && (!String.IsNullOrWhiteSpace(searchparameters.ToDate)))
                    {
                        if (Convert.ToDateTime(searchparameters.FromDate) > Convert.ToDateTime(searchparameters.ToDate))
                        {
                            return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.DateError)).Value);
                        }
                    }
                    var response = await _clinicalStudyService.SearchStudy(searchparameters, user).ConfigureAwait(false);

                    if (response == null)
                    {
                        return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.SearchNotFound)).Value);
                    }
                    else
                    {
                        return Ok(response);
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
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyV1Controller)}; Method : {nameof(SearchStudy)};");
            }
        }

        /// <summary>
        /// Search For a Study 
        /// </summary>
        /// <param name="searchparameters">Parameters to search in database</param>
        /// <response code="200">Returns All Study that matches the search criteria</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">There is no study that matches the search criteria</response>
        [HttpPost]
        [Route(Route.SearchTitleV1)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<StudyDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> SearchTitle([FromBody] SearchTitleParametersDto searchparameters)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyV1Controller)}; Method : {nameof(SearchTitle)};");
                LoggedInUser user = new LoggedInUser
                {
                    UserName = User?.FindFirst(ClaimTypes.Email)?.Value,
                    UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                };
                if (searchparameters != null)
                {
                    if (String.IsNullOrWhiteSpace(searchparameters.StudyTitle) && String.IsNullOrWhiteSpace(searchparameters.StudyId)
                       && String.IsNullOrWhiteSpace(searchparameters.FromDate) && String.IsNullOrWhiteSpace(searchparameters.ToDate))
                    {
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ValidationErrorMessage.AnyOneFieldError)).Value);
                    }
                    Tuple<DateTime, DateTime> fromAndToDate = FromDateToDateHelper.GetFromAndToDate(String.IsNullOrWhiteSpace(searchparameters.FromDate) ? DateTime.MinValue : Convert.ToDateTime(searchparameters.FromDate),
                                                                  String.IsNullOrWhiteSpace(searchparameters.ToDate) ? DateTime.MinValue : Convert.ToDateTime(searchparameters.ToDate), -1);

                    searchparameters.FromDate = fromAndToDate.Item1.ToString();
                    searchparameters.ToDate = fromAndToDate.Item2.ToString();
                    if ((!String.IsNullOrWhiteSpace(searchparameters.FromDate)) && (!String.IsNullOrWhiteSpace(searchparameters.ToDate)))
                    {
                        if (Convert.ToDateTime(searchparameters.FromDate) > Convert.ToDateTime(searchparameters.ToDate))
                        {
                            return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.DateError)).Value);
                        }
                    }
                    var response = await _clinicalStudyService.SearchTitle(searchparameters, user).ConfigureAwait(false);

                    if (response == null || response.Count == 0)
                    {
                        return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.SearchNotFound)).Value);
                    }
                    else
                    {
                        return Ok(response);
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
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyV1Controller)}; Method : {nameof(SearchTitle)};");
            }
        }
        #endregion

        #endregion
    }
}
