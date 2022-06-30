using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.Core.Filters;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.Core.DTO.Token;
using System.Net;

namespace TransCelerate.SDR.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    public class ClinicalStudyController : ControllerBase
    {
        #region Variables
        private readonly IClinicalStudyService _clinicalStudyService;
        private readonly ILogHelper _logger;
        #endregion

        #region Constructor
        public ClinicalStudyController(IClinicalStudyService clinicalStudyService, ILogHelper logger)
        {
            _clinicalStudyService = clinicalStudyService;
            _logger = logger;
        }
        #endregion

        #region Action Methods

        #region GET Methods

        /// <summary>
        /// GET All Elements For a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="version">Version of study</param>
        /// <param name="tag">Tag of a study</param>
        /// <param name="sections">Study sections which have to be fetched</param> 
        /// <response code="200">Returns Study</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The Study for the studyId is Not Found</response>
        [HttpGet]
        [Route(Route.Study)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GetStudyDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetStudy(string studyId, int version, string tag, [FromQuery] string sections)
        {
            try
            {                                   
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(GetStudy)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    LoggedInUser user = new LoggedInUser
                    {
                        UserName = User?.FindFirst(ClaimTypes.Name)?.Value,
                        UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                    };
                    _logger.LogInformation($"Inputs: StudyId: {studyId}; Version: {version}; Status: {tag ?? "<null>"}; Sections: {sections ?? "<null>"}");
                    string[] sectionArray = new string[] { };
                    if (!String.IsNullOrWhiteSpace(sections))
                    {
                        sectionArray = sections.Split(',');
                    }
                    bool isValidSection = true;
                    object study;
                    if (sectionArray.Count() != 0)
                    {
                        foreach (var item in sectionArray)
                        {
                            isValidSection = Enum.GetNames(typeof(StudySections)).Contains(item.Trim().ToLower());
                            if (!isValidSection)
                            {
                                return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.SectionNotValid)).Value);
                            }
                        }
                        study = await _clinicalStudyService.GetSections(studyId: studyId, version: version, tag: tag, sections: sectionArray,user:user).ConfigureAwait(false);
                    }
                    else
                    {
                        study = await _clinicalStudyService.GetAllElements(studyId: studyId, version: version, tag: tag,user:user).ConfigureAwait(false);
                    }

                    if (study == null)
                    {
                        return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.StudyNotFound)).Value);
                    }
                    else if(study.ToString() == Constants.ErrorMessages.Forbidden)
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
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyController)}; Method : {nameof(GetStudy)};");
            }
        }

        /// <summary>
        /// GET For a StudyDesign sections for a study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="studyDesignId">Study Design Id</param>
        /// <param name="version">Version of study</param>
        /// <param name="tag">Tag of a study</param>        
        /// <param name="sections">Study Design sections which have to be fetched</param>  
        /// <response code="200">Returns a list of StudyDesigns</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The StudyDesigns for the study is Not Found</response>
        [HttpGet]
        [Route(Route.StudyDesign)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GetStudyDesignsDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetStudyDesignSections(string studyId, string studyDesignId, int version, string tag, [FromQuery] string sections)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(GetStudyDesignSections)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    LoggedInUser user = new LoggedInUser
                    {
                        UserName = User?.FindFirst(ClaimTypes.Name)?.Value,
                        UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                    };
                    _logger.LogInformation($"Inputs: StudyId: {studyId}; StudyDesignId: {studyDesignId}; Version: {version}; Status: {tag ?? "<null>"}; Sections: {sections ?? "<null>"}");
                    string[] sectionArray = new string[] { };
                    if (!String.IsNullOrWhiteSpace(sections))
                    {
                        sectionArray = sections.Split(',');
                    }
                    bool isValidSection = true;
                    if (sectionArray.Count() != 0)
                    {
                        foreach (var item in sectionArray)
                        {
                            isValidSection = Enum.GetNames(typeof(StudyDesignSections)).Contains(item.Trim().ToLower());
                            if (!isValidSection)
                            {
                                return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.SectionNotValid)).Value);
                            }
                        }

                    }
                    var study = await _clinicalStudyService.GetStudyDesignSections(studyDesignId: studyDesignId, studyId: studyId, version: version, tag: tag, sections: sectionArray,user:user).ConfigureAwait(false);

                    //If StudyId is not found
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
                        //If StudyDesignId is not found
                        if (!study.ToString().Contains(studyDesignId))
                        {
                            return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.StudyDesignNotFound)).Value);
                        }
                        else
                        {
                            return Ok(study);
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
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyController)}; Method : {nameof(GetStudyDesignSections)};");
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
        [Route(Route.AuditTrail)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetStudyAuditDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetAuditTrail(string studyId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(GetAuditTrail)};");
                LoggedInUser user = new LoggedInUser
                {
                    UserName = User?.FindFirst(ClaimTypes.Name)?.Value,
                    UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                };
                _logger.LogInformation($"Inputs: FromDate: {fromDate}; ToDate: {toDate}; Study: {studyId ?? "<null>"};");

                if (toDate == DateTime.MinValue)
                {
                    toDate = DateTime.UtcNow;
                }
                if (toDate != DateTime.MinValue)
                {
                    toDate = toDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                }
                if (fromDate != DateTime.MinValue)
                {
                    fromDate = fromDate.Date;
                }
                if (fromDate <= toDate)
                {
                    var studyAuditResponse = await _clinicalStudyService.GetAuditTrail(fromDate, toDate, studyId, user);
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
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured. Exception : {ex.Message}");
                return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
            }
            finally
            {
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyController)}; Method : {nameof(GetAuditTrail)};");
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
        [Route(Route.StudyHistory)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GetStudyHistoryResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllStudyId(DateTime fromDate, DateTime toDate, string studyTitle)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(GetAllStudyId)};");
                LoggedInUser user = new LoggedInUser
                {
                    UserName = User?.FindFirst(ClaimTypes.Name)?.Value,
                    UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                };
                _logger.LogInformation($"Inputs: FromDate: {fromDate}; ToDate: {toDate}; DateRange from Key Vault :{Config.DateRange}");

                if (toDate == DateTime.MinValue)
                {
                    toDate = DateTime.UtcNow;
                }
                if (toDate != DateTime.MinValue)
                {
                    toDate = toDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                }
                if (fromDate != DateTime.MinValue)
                {
                    fromDate = fromDate.Date;
                }
                else
                {
                    fromDate = DateTime.UtcNow.AddDays(-(Convert.ToInt32(Config.DateRange))).Date;
                }
                if (fromDate <= toDate)
                {
                    var studyHistoryResponse = await _clinicalStudyService.GetAllStudyId(fromDate, toDate, studyTitle, user);
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
                _logger.LogError($"Exception occured. Exception : {ex.Message}");
                return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
            }
            finally
            {
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyController)}; Method : {nameof(GetAllStudyId)};");
            }
        }
        #endregion

        #region POST Methods

        /// <summary>
        /// POST All Elements For a Study  
        /// </summary>        
        /// <param name="studyDTO">Study for Inserting/Updating in Database</param>
        /// <param name="entrySystem">System which made the request</param> 
        /// <response code="201">Study Created</response>
        /// <response code="400">Bad Request</response>       
        [HttpPost]
        [Route(Route.PostElements)]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(PostStudyDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> PostAllElements([FromBody] PostStudyDTO studyDTO, [FromHeader] string entrySystem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(PostAllElements)};");
                    if (studyDTO != null)
                    {
                        LoggedInUser user = new LoggedInUser
                        {
                            UserName = User?.FindFirst(ClaimTypes.Name)?.Value,
                            UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                        };
                        var response = await _clinicalStudyService.PostAllElements(studyDTO, entrySystem: entrySystem,user: user)
                                                                  .ConfigureAwait(false);

                        if (response == null)
                        {
                            return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("An Error Occured")).Value);
                        }
                        else if (response.ToString() == Constants.ErrorMessages.PostRestricted)
                        {
                            return StatusCode(((int)HttpStatusCode.Unauthorized), new JsonResult(ErrorResponseHelper.UnAuthorizedAccess(Constants.ErrorMessages.PostRestricted)).Value);
                        }
                        else
                        {
                            if (response.ToString() == Constants.ErrorMessages.NotValidStudyId)
                            {
                                return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.NotValidStudyId)).Value);
                            }
                            else
                            {
                                return Created($"study/{studyDTO.clinicalStudy.studyId}", new JsonResult(response).Value);
                            }
                        }
                    }
                    else
                    {
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.StudyInputError)).Value);
                    }
                }
                else
                {
                    return ValidationProblem();
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

        /// <summary>
        /// Search For a Study 
        /// </summary>
        /// <param name="searchparameters">Parameters to search in database</param>
        /// <response code="200">Returns All Study that matches the search criteria</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">There is no study that matches the search criteria</response>
        [HttpPost]
        [Route(Route.SearchElements)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetStudyDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> SearchStudy([FromBody] SearchParametersDTO searchparameters)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(SearchStudy)};");
                if (ModelState.IsValid)
                {
                    LoggedInUser user = new LoggedInUser
                    {
                        UserName = User?.FindFirst(ClaimTypes.Name)?.Value,
                        UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                    };
                    if (searchparameters != null)
                    {
                        if (String.IsNullOrWhiteSpace(searchparameters.indication)
                           && String.IsNullOrWhiteSpace(searchparameters.interventionModel) && String.IsNullOrWhiteSpace(searchparameters.phase)
                           && String.IsNullOrWhiteSpace(searchparameters.studyId) && String.IsNullOrWhiteSpace(searchparameters.studyTitle)
                           && String.IsNullOrWhiteSpace(searchparameters.fromDate) && String.IsNullOrWhiteSpace(searchparameters.toDate))
                        {
                            return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ValidationErrorMessage.AnyOneFieldError)).Value);
                        }
                        if (String.IsNullOrWhiteSpace(searchparameters.toDate))
                        {
                            searchparameters.toDate = DateTime.UtcNow.Date.AddHours(23).AddMinutes(59).AddSeconds(59).ToString();
                        }
                        else
                        {
                            searchparameters.toDate = Convert.ToDateTime(searchparameters.toDate).Date.AddHours(23).AddMinutes(59).AddSeconds(59).ToString();
                        }
                        if (String.IsNullOrWhiteSpace(searchparameters.fromDate))
                        {
                            searchparameters.fromDate = DateTime.MinValue.ToString();
                        }
                        else
                        {
                            searchparameters.fromDate = Convert.ToDateTime(searchparameters.fromDate).ToString();
                        }
                        if ((!String.IsNullOrWhiteSpace(searchparameters.fromDate)) && (!String.IsNullOrWhiteSpace(searchparameters.toDate)))
                        {
                            if (Convert.ToDateTime(searchparameters.fromDate) > Convert.ToDateTime(searchparameters.toDate))
                            {
                                return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.DateError)).Value);
                            }
                        }
                        var response = await _clinicalStudyService.SearchStudy(searchparameters,user).ConfigureAwait(false);

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
                else
                {
                    return ValidationProblem();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured. Exception : {ex.Message}");
                return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
            }
            finally
            {
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyController)}; Method : {nameof(SearchStudy)};");
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
        [Route(Route.SearchTitle)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetStudyDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> SearchTitle([FromBody] SearchTitleParametersDTO searchparameters)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(SearchTitle)};");
                if (ModelState.IsValid)
                {
                    LoggedInUser user = new LoggedInUser
                    {
                        UserName = User?.FindFirst(ClaimTypes.Name)?.Value,
                        UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                    };
                    if (searchparameters != null)
                    {
                        if (String.IsNullOrWhiteSpace(searchparameters.studyTitle)
                           && String.IsNullOrWhiteSpace(searchparameters.fromDate) && String.IsNullOrWhiteSpace(searchparameters.toDate))
                        {
                            return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ValidationErrorMessage.AnyOneFieldError)).Value);
                        }
                        if (String.IsNullOrWhiteSpace(searchparameters.toDate))
                        {
                            searchparameters.toDate = DateTime.UtcNow.Date.AddHours(23).AddMinutes(59).AddSeconds(59).ToString();
                        }
                        else
                        {
                            searchparameters.toDate = Convert.ToDateTime(searchparameters.toDate).Date.AddHours(23).AddMinutes(59).AddSeconds(59).ToString();
                        }
                        if (String.IsNullOrWhiteSpace(searchparameters.fromDate))
                        {
                            searchparameters.fromDate = DateTime.MinValue.ToString();
                        }
                        else
                        {
                            searchparameters.fromDate = Convert.ToDateTime(searchparameters.fromDate).ToString();
                        }
                        if ((!String.IsNullOrWhiteSpace(searchparameters.fromDate)) && (!String.IsNullOrWhiteSpace(searchparameters.toDate)))
                        {
                            if (Convert.ToDateTime(searchparameters.fromDate) > Convert.ToDateTime(searchparameters.toDate))
                            {
                                return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.DateError)).Value);
                            }
                        }
                        var response = await _clinicalStudyService.SearchTitle(searchparameters, user).ConfigureAwait(false);

                        if (response == null || response.Count==0)
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
                else
                {
                    return ValidationProblem();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured. Exception : {ex.Message}");
                return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
            }
            finally
            {
                _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyController)}; Method : {nameof(SearchTitle)};");
            }
        }
        #endregion

        #endregion

    }
}
