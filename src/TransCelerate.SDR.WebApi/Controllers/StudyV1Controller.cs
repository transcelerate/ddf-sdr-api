using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1;
using TransCelerate.SDR.Services.Interfaces;

namespace TransCelerate.SDR.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    public class StudyV1Controller : ControllerBase
    {
        #region Variables        
        private readonly ILogHelper _logger;
        private readonly IStudyServiceV1 _studyService;
        private readonly IHelperV1 _helper;
        #endregion

        #region Constructor
        public StudyV1Controller(IStudyServiceV1 studyService, ILogHelper logger, IHelperV1 helper)
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
        /// <param name="usdmVersion">usdmVersion</param> 
        /// <response code="200">Returns Study</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The Study for the studyId is Not Found</response>
        [HttpGet]
        [Route(Route.StudyV1)]
        [ApiVersion(Constants.USDMVersions.V1)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StudyDefinitionsDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetStudy(string studyId, int sdruploadversion,
                                            [FromHeader(Name = IdFieldPropertyName.Common.UsdmVersion)][BindRequired] string usdmVersion)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(StudyV1Controller)}; Method : {nameof(GetStudy)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs : studyId = {studyId}; sdruploadversion = {sdruploadversion};");
                    LoggedInUser user = new()
                    {
                        UserName = User?.FindFirst(ClaimTypes.Email)?.Value,
                        UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                    };
                    var study = await _studyService.GetStudy(studyId, sdruploadversion, user).ConfigureAwait(false);

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
                _logger.LogInformation($"Ended Controller : {nameof(StudyV1Controller)}; Method : {nameof(GetStudy)};");
            }
        }

        /// <summary>
        /// GET Study Designs of a Study
        /// </summary>
        /// <param name="study_uuid">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="usdmVersion">USDM Version</param>
        /// <response code="200">Returns Study</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The Study for the studyId is Not Found</response>
        [HttpGet]
        [Route(Route.StudyDesignV1)]
        [ApiVersion(Constants.USDMVersions.V1)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StudyDefinitionsDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetStudyDesigns(string study_uuid, int sdruploadversion,
                                            [FromHeader(Name = IdFieldPropertyName.Common.UsdmVersion)][BindRequired] string usdmVersion)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(StudyV1Controller)}; Method : {nameof(GetStudyDesigns)};");
                if (!String.IsNullOrWhiteSpace(study_uuid))
                {
                    _logger.LogInformation($"Inputs : study_uuid = {study_uuid}; sdruploadversion = {sdruploadversion};");

                    LoggedInUser user = new()
                    {
                        UserName = User?.FindFirst(ClaimTypes.Email)?.Value,
                        UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                    };
                    var study = await _studyService.GetStudyDesigns(study_uuid, sdruploadversion, user).ConfigureAwait(false);

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
                _logger.LogInformation($"Ended Controller : {nameof(StudyV1Controller)}; Method : {nameof(GetStudyDesigns)};");
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
        [ApiVersionNeutral]
        [Route(Route.AuditTrailV1)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<AudiTrailResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetAuditTrail(string studyId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(StudyV1Controller)}; Method : {nameof(GetAuditTrail)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs : studyId = {studyId}; fromDate = {fromDate}; toDate = {toDate}");
                    LoggedInUser user = new()
                    {
                        UserName = User?.FindFirst(ClaimTypes.Email)?.Value,
                        UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                    };

                    Tuple<DateTime, DateTime> fromAndToDate = FromDateToDateHelper.GetFromAndToDate(fromDate, toDate, -1);

                    fromDate = fromAndToDate.Item1;
                    toDate = fromAndToDate.Item2;
                    if (fromDate <= toDate)
                    {
                        var studyAuditResponse = await _studyService.GetAuditTrail(studyId, fromDate, toDate, user);
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
                _logger.LogInformation($"Ended Controller : {nameof(StudyV1Controller)}; Method : {nameof(GetAuditTrail)};");
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
        [ApiVersionNeutral]
        [Route(Route.StudyHistoryV1)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StudyHistoryResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetStudyHistory(DateTime fromDate, DateTime toDate, string studyTitle)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(StudyV1Controller)}; Method : {nameof(GetStudyHistory)};");
                LoggedInUser user = new()
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
                    var studyHistoryResponse = await _studyService.GetStudyHistory(fromDate, toDate, studyTitle, user);
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
                _logger.LogInformation($"Ended Controller : {nameof(StudyV1Controller)}; Method : {nameof(GetStudyHistory)};");
            }
        }
        #endregion

        #region POST Methods
        /// <summary>
        /// POST All Elements For a Study  
        /// </summary>        
        /// <param name="studyDTO">Study for Inserting/Updating in Database</param>        
        /// <param name="usdmVersion">USDM Version</param>        
        /// <response code="201">Study Created</response>
        /// <response code="400">Bad Request</response>       
        [HttpPost]
        [Route(Route.PostElementsV1)]
        [ApiVersion(Constants.USDMVersions.V1)]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(StudyDefinitionsDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> PostAllElements([FromBody] StudyDefinitionsDto studyDTO,
                                            [FromHeader(Name = IdFieldPropertyName.Common.UsdmVersion)][BindRequired] string usdmVersion)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(StudyV1Controller)}; Method : {nameof(PostAllElements)};");
                if (studyDTO != null)
                {
                    LoggedInUser user = new()
                    {
                        UserName = User?.FindFirst(ClaimTypes.Email)?.Value,
                        UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                    };
                    var response = await _studyService.PostAllElements(studyDTO, user)
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
                            return Created($"study/{studyDTO.Study.Uuid}", new JsonResult(response).Value);
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
                _logger.LogInformation($"Ended Controller : {nameof(StudyV1Controller)}; Method : {nameof(PostAllElements)};");
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
        [ApiVersionNeutral]
        [Route(Route.SearchElementsV1)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<StudyDefinitionsDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> SearchStudy([FromBody] SearchParametersDto searchparameters)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(StudyV1Controller)}; Method : {nameof(SearchStudy)};");
                LoggedInUser user = new()
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
                    var response = await _studyService.SearchStudy(searchparameters, user).ConfigureAwait(false);

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
                _logger.LogInformation($"Ended Controller : {nameof(StudyV1Controller)}; Method : {nameof(SearchStudy)};");
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
        [ApiVersionNeutral]
        [Route(Route.SearchTitleV1)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<StudyDefinitionsDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> SearchTitle([FromBody] SearchTitleParametersDto searchparameters)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(StudyV1Controller)}; Method : {nameof(SearchTitle)};");
                LoggedInUser user = new()
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
                    var response = await _studyService.SearchTitle(searchparameters, user).ConfigureAwait(false);

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
                _logger.LogInformation($"Ended Controller : {nameof(StudyV1Controller)}; Method : {nameof(SearchTitle)};");
            }
        }
        #endregion
        #endregion
    }
}
