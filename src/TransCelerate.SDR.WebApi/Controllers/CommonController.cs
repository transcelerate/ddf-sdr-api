using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
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

        #region Get Methods
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
        public async Task<IActionResult> GetRawJson(string studyId, int sdruploadversion)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(CommonController)}; Method : {nameof(GetRawJson)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs : studyId = {studyId}; sdruploadversion = {sdruploadversion};");

                    LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);

                    var study = await _commonService.GetRawJson(studyId, sdruploadversion, user);

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
                        return Ok(new GetRawJsonDto()
                        {
                            StudyDefinitions = JsonConvert.SerializeObject(study, new JsonSerializerSettings
                            {
                                ContractResolver = new DefaultContractResolver()
                                {
                                    NamingStrategy = new CamelCaseNamingStrategy()
                                }
                            })
                        });
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

        /// <summary>
        /// GET API -> USDM Version Mapping
        /// </summary>
        /// <response code="200">API -> USDM Version Mapping</response>
        /// <response code="400">Bad Request</response>
        [HttpGet]
        [Route(Route.GetApiUsdmMapping)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ApiUsdmVersionMapping))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public IActionResult GetApiUsdmMapping()
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(CommonController)}; Method : {nameof(GetApiUsdmMapping)};");
                ApiUsdmVersionMapping_NonStatic apiUsdmVersionMapping = new()
                {
                    SDRVersions = ApiUsdmVersionMapping.SDRVersions
                };
                return Ok(apiUsdmVersionMapping);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured. Exception : {ex}");
                return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
            }
            finally
            {
                _logger.LogInformation($"Ended Controller : {nameof(CommonController)}; Method : {nameof(GetApiUsdmMapping)};");
            }
        }

        /// <summary>
        /// GET Revision History of a study
        /// </summary>
        /// <param name="fromDate">Start Date for Date Filter</param>
        /// <param name="toDate">End Date for Date Filter</param>
        /// <param name="studyId">Study ID</param>
        /// <response code="200">Returns a list of Audit Trail of a study</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The Audit trail for the study is Not Found</response>
        [HttpGet]
        [Route(Route.GetRevisionHistory)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<AuditTrailResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetAuditTrail(string studyId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(CommonController)}; Method : {nameof(GetAuditTrail)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs : studyId = {studyId}; fromDate = {fromDate}; toDate = {toDate}");
                    LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);

                    Tuple<DateTime, DateTime> fromAndToDate = FromDateToDateHelper.GetFromAndToDate(fromDate, toDate, -1);

                    fromDate = fromAndToDate.Item1;
                    toDate = fromAndToDate.Item2;
                    if (fromDate <= toDate)
                    {
                        var studyAuditResponse = await _commonService.GetAuditTrail(studyId, fromDate, toDate, user);
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
                _logger.LogInformation($"Ended Controller : {nameof(CommonController)}; Method : {nameof(GetAuditTrail)};");
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
        [Route(Route.GetStudyHistory)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StudyHistoryResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetStudyHistory(DateTime fromDate, DateTime toDate, string studyTitle)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(CommonController)}; Method : {nameof(GetStudyHistory)};");

                LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);

                _logger.LogInformation($"Inputs: FromDate: {fromDate}; ToDate: {toDate}; DateRange from Key Vault :{Config.DateRange}");

                Tuple<DateTime, DateTime> fromAndToDate = FromDateToDateHelper.GetFromAndToDate(fromDate, toDate, Convert.ToInt32(Config.DateRange));

                fromDate = fromAndToDate.Item1;
                toDate = fromAndToDate.Item2;

                if (fromDate <= toDate)
                {
                    var studyHistoryResponse = await _commonService.GetStudyHistory(fromDate, toDate, studyTitle, user);
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
                _logger.LogInformation($"Ended Controller : {nameof(CommonController)}; Method : {nameof(GetStudyHistory)};");
            }
        }

        /// <summary>
        /// GET Links
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param> 
        /// <response code="200">Returns Study</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The Study for the studyId is Not Found</response>
        [HttpGet]
        [Route(Route.GetLinksForAStudy)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetLinks(string studyId, int sdruploadversion)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(CommonController)}; Method : {nameof(GetRawJson)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs : studyId = {studyId}; sdruploadversion = {sdruploadversion};");

                    LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);

                    var study = await _commonService.GetLinks(studyId, sdruploadversion, user);

                    if (study == null)
                    {
                        return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.StudyNotFound)).Value);
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

        #region Search
        /// <summary>
        /// Search For a Study 
        /// </summary>
        /// <param name="searchparameters">Parameters to search in database</param>
        /// <response code="200">Returns All Study that matches the search criteria</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">There is no study that matches the search criteria</response>
        [HttpPost]
        [ApiVersionNeutral]
        [Route(Route.CommonSearch)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<SearchResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> SearchStudy([FromBody] SearchParametersDto searchparameters)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(CommonController)}; Method : {nameof(SearchStudy)};");
                LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);
                if (searchparameters != null)
                {
                    if (String.IsNullOrWhiteSpace(searchparameters.Indication)
                       && String.IsNullOrWhiteSpace(searchparameters.InterventionModel) && String.IsNullOrWhiteSpace(searchparameters.Phase)
                       && String.IsNullOrWhiteSpace(searchparameters.SponsorId) && String.IsNullOrWhiteSpace(searchparameters.StudyTitle)
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
                    var response = await _commonService.SearchStudy(searchparameters, user).ConfigureAwait(false);

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
                _logger.LogInformation($"Ended Controller : {nameof(CommonController)}; Method : {nameof(SearchStudy)};");
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
        [Route(Route.SearchStudyTitle)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<SearchTitleResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> SearchTitle([FromBody] SearchTitleParametersDto searchparameters)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(CommonController)}; Method : {nameof(SearchTitle)};");
                LoggedInUser user = LoggedInUserHelper.GetLoggedInUser(User);
                if (searchparameters != null)
                {
                    if (String.IsNullOrWhiteSpace(searchparameters.StudyTitle) && String.IsNullOrWhiteSpace(searchparameters.SponsorId)
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
                    var response = await _commonService.SearchTitle(searchparameters, user).ConfigureAwait(false);

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
                _logger.LogInformation($"Ended Controller : {nameof(CommonController)}; Method : {nameof(SearchTitle)};");
            }
        }
        #endregion
        #endregion
    }
}
