using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.Core.Utilities.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.Utilities;
using System.Linq;
using TransCelerate.SDR.Core.DTO;

namespace TransCelerate.SDR.WebApi.Controllers
{

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

        #region Depricated EndPoints
        ////GET InterventionModel For a Study
        //[HttpGet]
        //[Route(Route.InterventionModel)]
        //public async Task<IActionResult> InterventionModel(string study, string version, string status)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(InterventionModel)};");
        //        if (!String.IsNullOrWhiteSpace(study))
        //        {
        //            _logger.LogInformation($"Inputs: StudyId: {study}; Version: {version ?? "<null>"}; Status: {status ?? "<null>"};");
        //            #region Validations                                      
        //            if (!String.IsNullOrWhiteSpace(status))
        //            {
        //                bool isValidStatus = Enum.GetNames(typeof(Status)).Contains(status);
        //                if (!isValidStatus)
        //                {
        //                    return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid status")).Value);
        //                }
        //            }
        //            #endregion

        //            var interventionModel = await _clinicalStudyService.InterventionModel(study: study, version: version, status: status).ConfigureAwait(false);

        //            if (interventionModel == null)
        //            {
        //                if (Request != null)
        //                    Response.Headers.Add("Controller", "True");
        //                return NotFound(new JsonResult(ErrorResponseHelper.NotFound("The requested InterventionModel for the study : " + study + (version != null ? $" for version : {version}" : "") + (status != null ? $" for status : {status}" : "") + " is not found")).Value);
        //            }
        //            else
        //            {
        //                return Ok(interventionModel);
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid study")).Value);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Exception occured. Exception : {ex.Message}");
        //        return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
        //    }
        //    finally
        //    {
        //        _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyController)}; Method : {nameof(InterventionModel)};");
        //    }
        //}

        ////GET Investigationalinterventions For a Study
        //[HttpGet]
        //[Route(Route.Investigationalinterventions)]
        //public async Task<IActionResult> Investigationalinterventions(string study, string version, string status)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(Investigationalinterventions)};");
        //        if (!String.IsNullOrWhiteSpace(study))
        //        {
        //            _logger.LogInformation($"Inputs: StudyId: {study}; Version: {version ?? "<null>"}; Status: {status ?? "<null>"};");
        //            #region Validation                                       
        //            if (!String.IsNullOrWhiteSpace(status))
        //            {
        //                bool isValidStatus = Enum.GetNames(typeof(Status)).Contains(status);
        //                if (!isValidStatus)
        //                {
        //                    return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid status")).Value);
        //                }
        //            }
        //            #endregion                    

        //            var investigationalIntervention = await _clinicalStudyService.Investigationalinterventions(study: study, version: version, status: status).ConfigureAwait(false);

        //            if (investigationalIntervention == null)
        //            {
        //                if (Request != null)
        //                    Response.Headers.Add("Controller", "True");
        //                return NotFound(new JsonResult(ErrorResponseHelper.NotFound("The requested Investigationalinterventions for the study : " + study + (version != null ? $" for version : {version}" : "") + (status != null ? $" for status : {status}" : "") + " is not found")).Value);
        //            }
        //            else
        //            {
        //                return Ok(investigationalIntervention);
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid study")).Value);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Exception occured. Exception : {ex.Message}");
        //        return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
        //    }
        //    finally
        //    {
        //        _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyController)}; Method : {nameof(Investigationalinterventions)};");
        //    }
        //}

        ////GET StudyIdentifiers For a Study
        //[HttpGet]
        //[Route(Route.StudyIdentifiers)]
        //public async Task<IActionResult> StudyIdentifiers(string study, string version, string status)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(StudyIdentifiers)};");
        //        if (!String.IsNullOrWhiteSpace(study))
        //        {
        //            _logger.LogInformation($"Inputs: StudyId: {study}; Version: {version ?? "<null>"}; Status: {status ?? "<null>"};");
        //            #region Validations

        //            if (!String.IsNullOrWhiteSpace(status))
        //            {
        //                bool isValidStatus = Enum.GetNames(typeof(Status)).Contains(status);
        //                if (!isValidStatus)
        //                {
        //                    return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid status")).Value);
        //                }
        //            }
        //            #endregion

        //            var studyIdentifier = await _clinicalStudyService.StudyIdentifiers(study: study, version: version, status: status).ConfigureAwait(false);

        //            if (studyIdentifier == null)
        //            {
        //                if (Request != null)
        //                    Response.Headers.Add("Controller", "True");
        //                return NotFound(new JsonResult(ErrorResponseHelper.NotFound("The requested studyidentifiers for the study : " + study + (version != null ? $" for version : {version}" : "") + (status != null ? $" for status : {status}" : "") + " is not found")).Value);
        //            }
        //            else
        //            {
        //                return Ok(studyIdentifier);
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid study")).Value);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Exception occured. Exception : {ex.Message}");
        //        return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
        //    }
        //    finally
        //    {
        //        _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyController)}; Method : {nameof(StudyIdentifiers)};");
        //    }
        //}

        ////GET StudyPhase For a Study
        //[HttpGet]
        //[Route(Route.StudyPhase)]
        //public async Task<IActionResult> StudyPhase(string study, string version, string status)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(StudyPhase)};");
        //        if (!String.IsNullOrWhiteSpace(study))
        //        {
        //            _logger.LogInformation($"Inputs: StudyId: {study}; Version: {version ?? "<null>"}; Status: {status ?? "<null>"};");
        //            #region Validations

        //            if (!String.IsNullOrWhiteSpace(status))
        //            {
        //                bool isValidStatus = Enum.GetNames(typeof(Status)).Contains(status);
        //                if (!isValidStatus)
        //                {
        //                    _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyController)}; Method : {nameof(StudyPhase)};");
        //                    return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid status")).Value);
        //                }
        //            }
        //            #endregion

        //            var studyphase = await _clinicalStudyService.StudyPhase(study: study, version: version, status: status).ConfigureAwait(false);

        //            if (studyphase == null)
        //            {
        //                if (Request != null)
        //                    Response.Headers.Add("Controller", "True");
        //                return NotFound(new JsonResult(ErrorResponseHelper.NotFound("The requested StudyPhase for the study : " + study + (version != null ? $" for version : {version}" : "") + (status != null ? $" for status : {status}" : "") + " is not found")).Value);
        //            }
        //            else
        //            {
        //                return Ok(studyphase);
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid study")).Value);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Exception occured. Exception : {ex.Message}");
        //        return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
        //    }
        //    finally
        //    {
        //        _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyController)}; Method : {nameof(StudyPhase)};");
        //    }
        //}

        ////GET StudyProtocol For a Study
        //[HttpGet]
        //[Route(Route.StudyProtocol)]
        //public async Task<IActionResult> StudyProtocol(string study, string version, string status)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(StudyProtocol)};");
        //        if (!String.IsNullOrWhiteSpace(study))
        //        {
        //            _logger.LogInformation($"Inputs: StudyId: {study}; Version: {version ?? "<null>"}; Status: {status ?? "<null>"};");
        //            #region Validations

        //            if (!String.IsNullOrWhiteSpace(status))
        //            {
        //                bool isValidStatus = Enum.GetNames(typeof(Status)).Contains(status);
        //                if (!isValidStatus)
        //                {
        //                    return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid status")).Value);
        //                }
        //            }
        //            #endregion

        //            var studyProtocol = await _clinicalStudyService.StudyProtocol(study: study, version: version, status: status).ConfigureAwait(false);
        //            if (studyProtocol == null)
        //            {
        //                if (Request != null)
        //                    Response.Headers.Add("Controller", "True");
        //                return NotFound(new JsonResult(ErrorResponseHelper.NotFound("The requested study protocol for the study : " + study + (version != null ? $" for version : {version}" : "") + (status != null ? $" for status : {status}" : "") + " is not found")).Value);
        //            }
        //            else
        //            {
        //                return Ok(studyProtocol);
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid study")).Value);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Exception occured. Exception : {ex.Message}");
        //        return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
        //    }
        //    finally
        //    {
        //        _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyController)}; Method : {nameof(StudyProtocol)};");
        //    }
        //}

        ////GET StudyObjectives For a Study
        //[HttpGet]
        //[Route(Route.StudyObjectives)]
        //public async Task<IActionResult> StudyObjectives(string study, string version, string status)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(StudyObjectives)};");
        //        if (!String.IsNullOrWhiteSpace(study))
        //        {
        //            _logger.LogInformation($"Inputs: StudyId: {study}; Version: {version ?? "<null>"}; Status: {status ?? "<null>"};");
        //            #region Validations

        //            if (!String.IsNullOrWhiteSpace(status))
        //            {
        //                bool isValidStatus = Enum.GetNames(typeof(Status)).Contains(status);
        //                if (!isValidStatus)
        //                {
        //                    return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid status")).Value);
        //                }
        //            }
        //            #endregion

        //            var studyObjectives = await _clinicalStudyService.StudyObjectives(study: study, version: version, status: status).ConfigureAwait(false);

        //            if (studyObjectives == null)
        //            {
        //                if (Request != null)
        //                    Response.Headers.Add("Controller", "True");
        //                return NotFound(new JsonResult(ErrorResponseHelper.NotFound("The requested study objectives for the study : " + study + (version != null ? $" for version : {version}" : "") + (status != null ? $" for status : {status}" : "") + " is not found")).Value);
        //            }
        //            else
        //            {
        //                return Ok(studyObjectives);
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid study")).Value);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Exception occured. Exception : {ex.Message}");
        //        return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
        //    }
        //    finally
        //    {
        //        _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyController)}; Method : {nameof(StudyObjectives)};");
        //    }
        //}

        ////GET StudyTargetPopulation For a Study
        //[HttpGet]
        //[Route(Route.StudyTargetPopulation)]
        //public async Task<IActionResult> StudyTargetPopulation(string study, string version, string status)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(StudyTargetPopulation)};");
        //        if (!String.IsNullOrWhiteSpace(study))
        //        {
        //            _logger.LogInformation($"Inputs: StudyId: {study}; Version: {version ?? "<null>"}; Status: {status ?? "<null>"};");
        //            #region Validations

        //            if (!String.IsNullOrWhiteSpace(status))
        //            {
        //                bool isValidStatus = Enum.GetNames(typeof(Status)).Contains(status);
        //                if (!isValidStatus)
        //                {
        //                    return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid status")).Value);
        //                }
        //            }
        //            #endregion

        //            var studyTargetPopulation = await _clinicalStudyService.StudyTargetPopulation(study: study, version: version, status: status).ConfigureAwait(false);

        //            if (studyTargetPopulation == null)
        //            {
        //                if (Request != null)
        //                    Response.Headers.Add("Controller", "True");
        //                return NotFound(new JsonResult(ErrorResponseHelper.NotFound("The requested StudyTargetPopulation for the study : " + study + (version != null ? $" for version : {version}" : "") + (status != null ? $" for status : {status}" : "") + " is not found")).Value);
        //            }
        //            else
        //            {
        //                return Ok(studyTargetPopulation);
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid study")).Value);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Exception occured. Exception : {ex.Message}");
        //        return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
        //    }
        //    finally
        //    {
        //        _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyController)}; Method : {nameof(StudyTargetPopulation)};");
        //    }
        //}

        ////GET StudyTitle For a Study
        //[HttpGet]
        //[Route(Route.StudyTitle)]
        //public async Task<IActionResult> StudyTitle(string study, string version, string status)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(StudyTitle)};");
        //        if (!String.IsNullOrWhiteSpace(study))
        //        {
        //            _logger.LogInformation($"Inputs: StudyId: {study}; Version: {version ?? "<null>"}; Status: {status ?? "<null>"};");
        //            #region Validations

        //            if (!String.IsNullOrWhiteSpace(status))
        //            {
        //                bool isValidStatus = Enum.GetNames(typeof(Status)).Contains(status);
        //                if (!isValidStatus)
        //                {
        //                    return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid status")).Value);
        //                }
        //            }
        //            #endregion

        //            var studyTitle = await _clinicalStudyService.StudyTitle(study: study, version: version, status: status).ConfigureAwait(false);

        //            if (studyTitle == null)
        //            {
        //                if (Request != null)
        //                    Response.Headers.Add("Controller", "True");
        //                return NotFound(new JsonResult(ErrorResponseHelper.NotFound("The requested studytitle for the study : " + study + (version != null ? $" for version : {version}" : "") + (status != null ? $" for status : {status}" : "") + " is not found")).Value);
        //            }
        //            else
        //            {
        //                return Ok(studyTitle);
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid study")).Value);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Exception occured. Exception : {ex.Message}");
        //        return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
        //    }
        //    finally
        //    {
        //        _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyController)}; Method : {nameof(StudyTitle)};");
        //    }
        //}

        ////GET StudyIndication For a Study
        //[HttpGet]
        //[Route(Route.StudyIndication)]
        //public async Task<IActionResult> StudyIndication(string study, string version, string status)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(StudyIndication)};");
        //        if (!String.IsNullOrWhiteSpace(study))
        //        {
        //            #region Validations
        //            _logger.LogInformation($"Inputs: StudyId: {study}; Version: {version ?? "<null>"}; Status: {status ?? "<null>"};");

        //            if (!String.IsNullOrWhiteSpace(status))
        //            {
        //                bool isValidStatus = Enum.GetNames(typeof(Status)).Contains(status);
        //                if (!isValidStatus)
        //                {
        //                    return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid status")).Value);
        //                }
        //            }
        //            #endregion
        //            var studyIndication = await _clinicalStudyService.StudyIndication(study: study, version: version, status: status).ConfigureAwait(false);

        //            if (studyIndication == null)
        //            {
        //                if (Request != null)
        //                    Response.Headers.Add("Controller", "True");
        //                return NotFound(new JsonResult(ErrorResponseHelper.NotFound("The requested studyindication for the study : " + study + (version != null ? $" for version : {version}" : "") + (status != null ? $" for status : {status}" : "") + " is not found")).Value);
        //            }
        //            else
        //            {
        //                return Ok(studyIndication);
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid study")).Value);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Exception occured. Exception : {ex.Message}");
        //        return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
        //    }
        //    finally
        //    {
        //        _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyController)}; Method : {nameof(StudyIndication)};");
        //    }
        //}

        ////GET StudyType For a Study
        //[HttpGet]
        //[Route(Route.StudyType)]
        //public async Task<IActionResult> StudyType(string study, string version, string status)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(StudyType)};");
        //        if (!String.IsNullOrWhiteSpace(study))
        //        {
        //            _logger.LogInformation($"Inputs: StudyId: {study}; Version: {version ?? "<null>"}; Status: {status ?? "<null>"};");
        //            #region Validations

        //            if (!String.IsNullOrWhiteSpace(status))
        //            {
        //                bool isValidStatus = Enum.GetNames(typeof(Status)).Contains(status);
        //                if (!isValidStatus)
        //                {
        //                    return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid status")).Value);
        //                }
        //            }
        //            #endregion
        //            var studyType = await _clinicalStudyService.StudyType(study: study, version: version, status: status).ConfigureAwait(false);

        //            if (studyType == null)
        //            {
        //                if (Request != null)
        //                    Response.Headers.Add("Controller", "True");
        //                return NotFound(new JsonResult(ErrorResponseHelper.NotFound("The requested studytype for the study : " + study + (version != null ? $" for version : {version}" : "") + (status != null ? $" for status : {status}" : "") + " is not found")).Value);
        //            }
        //            else
        //            {
        //                return Ok(studyType);
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid study")).Value);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Exception occured. Exception : {ex.Message}");
        //        return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
        //    }
        //    finally
        //    {
        //        _logger.LogInformation($"Ended Controller : {nameof(ClinicalStudyController)}; Method : {nameof(StudyType)};");
        //    }
        //} 
        #endregion

        /// <summary>
        /// GET All Elements For a Study
        /// </summary>
        /// <param name="studyId"></param>
        /// <param name="version"></param>
        /// <param name="tag"></param>
        /// <param name="sections"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(Route.Study)]
        public async Task<IActionResult> GetStudy(string studyId,int version, string tag,[FromQuery] string sections)
        {
            try
            {                
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(GetStudy)};");                
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs: StudyId: {studyId}; Version: {version}; Status: {tag ?? "<null>"}; Sections: {sections}");
                    string[] sectionArray = new string[] { };
                    if(!String.IsNullOrWhiteSpace(sections))
                    {
                        sectionArray = sections.Split(',');
                    }
                    bool isValidSection = true;
                    object study;
                    if (sectionArray.Count()!=0)
                    {
                        foreach (var item in sectionArray)
                        {                            
                            isValidSection = Enum.GetNames(typeof(StudySections)).Contains(item.Trim().ToLower());
                            if(!isValidSection)
                            {
                                return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid section")).Value);
                            }
                        }
                        study = await _clinicalStudyService.GetSections(studyId: studyId, version: version, tag: tag,sections: sectionArray).ConfigureAwait(false);
                    }
                    else
                    {
                        study = await _clinicalStudyService.GetAllElements(studyId: studyId, version: version, tag: tag).ConfigureAwait(false);
                    }                  

                    if (study == null)
                    {
                        if (Request != null)
                            Response.Headers.Add("Controller", "True");
                        return NotFound(new JsonResult(ErrorResponseHelper.NotFound("The requested study document not found")).Value);
                    }
                    else
                    {
                        return Ok(study);
                    }
                }
                else
                {
                    return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid study")).Value);
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
        /// <param name="studyId"></param>
        /// <param name="studyDesignId"></param>
        /// <param name="version"></param>
        /// <param name="tag"></param>
        /// <param name="sections"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(Route.StudyDesign)]
        public async Task<IActionResult> GetStudyDesignSections(string studyId, string studyDesignId, int version, string tag, [FromQuery] string sections)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(GetStudy)};");
                if (!String.IsNullOrWhiteSpace(studyId))
                {
                    _logger.LogInformation($"Inputs: StudyId: {studyId}; StudyDesignId: {studyDesignId}; Version: {version}; Status: {tag ?? "<null>"}; Sections: {sections}");
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
                                return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid section")).Value);
                            }
                        }
                        
                    }
                    var study = await _clinicalStudyService.GetStudyDesignSections(studyDesignId:studyDesignId,studyId: studyId, version: version, tag: tag, sections: sectionArray).ConfigureAwait(false);

                    //If StudyId is not found
                    if (study == null)
                    {
                        if (Request != null)
                            Response.Headers.Add("Controller", "True");
                        return NotFound(new JsonResult(ErrorResponseHelper.NotFound("The requested study document not found")).Value);
                    }
                    else
                    {
                        //If StudyDesignId is not found
                        if (!study.ToString().Contains("studyDesignId"))
                        {
                            if (Request != null)
                                Response.Headers.Add("Controller", "True");
                            return NotFound(new JsonResult(ErrorResponseHelper.NotFound("The requested study design not found")).Value);
                        }
                        else
                        {
                            return Ok(study);
                        }
                    }
                }
                else
                {
                    return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid study")).Value);
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
        /// GET Audit Trial
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="studyId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(Route.AuditTrail)]
        public async Task<IActionResult> GetAuditTrail(string studyId,DateTime fromDate, DateTime toDate)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(GetAuditTrail)};");

                _logger.LogInformation($"Inputs: FromDate: {fromDate}; ToDate: {toDate}; Study: {studyId ?? "<null>"};");
               
                if(toDate==DateTime.MinValue)
                {
                    toDate = DateTime.UtcNow;
                }
                if (toDate != DateTime.MinValue)
                {
                    toDate = toDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                }
                if (fromDate!=DateTime.MinValue)
                {
                    fromDate = fromDate.Date;
                }
                if (fromDate <= toDate)
                {
                    var studyAuditResponse = await _clinicalStudyService.GetAuditTrail(fromDate, toDate, studyId);
                    if (studyAuditResponse == null)
                    {
                        if (Request != null)
                            Response.Headers.Add("Controller", "True");
                        return NotFound(new JsonResult(ErrorResponseHelper.NotFound("The audit trial for the study elements is not found")).Value);
                    }
                    else
                    {
                        return Ok(studyAuditResponse);
                    }
                }
                else
                {
                    return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("ToDate must be greater than or equal to FromDate")).Value);
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
        #endregion

        #region POST Methods

        /// <summary>
        /// POST All Elements For a Study  
        /// </summary>        
        /// <param name="studyDTO"></param>
        /// <param name="entrySystemId"></param>
        /// <param name="entrySystem"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(Route.PostElements)]
        public async Task<IActionResult> PostAllElements([FromBody] PostStudyDTO studyDTO,[FromHeader] string entrySystemId,[FromHeader] string entrySystem)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(PostAllElements)};");
                    if (studyDTO != null)
                    {
                        var response = await _clinicalStudyService.PostAllElements(studyDTO, entrySystem: entrySystem, entrySystemId: entrySystemId)
                                                                  .ConfigureAwait(false);

                        if (response == null)
                        {
                            if (Request != null)
                                Response.Headers.Add("Controller", "True");
                            return BadRequest(new JsonResult(ErrorResponseHelper.NotFound("The POST request for the study has failed")).Value);
                        }
                        else
                        {
                            return Created($"study/{studyDTO.clinicalStudy.studyId}", new JsonResult(response).Value);
                        }
                    }
                    else
                    {
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide a valid study")).Value);
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
        /// <param name="searchparameters"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(Route.SearchElements)]
        public async Task<IActionResult> SearchStudy([FromBody] SearchParametersDTO searchparameters)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ClinicalStudyController)}; Method : {nameof(SearchStudy)};");                
                if(ModelState.IsValid)
                {
                    if (searchparameters != null)
                    {
                        if(String.IsNullOrWhiteSpace(searchparameters.briefTitle) && String.IsNullOrWhiteSpace(searchparameters.indication)
                           && String.IsNullOrWhiteSpace(searchparameters.interventionModel) && String.IsNullOrWhiteSpace(searchparameters.phase)
                           && String.IsNullOrWhiteSpace(searchparameters.studyId) && String.IsNullOrWhiteSpace(searchparameters.studyTitle)
                           && String.IsNullOrWhiteSpace(searchparameters.fromDate) && String.IsNullOrWhiteSpace(searchparameters.toDate))                         
                        {
                            return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Any one of the field is required")).Value);
                        }
                        if(String.IsNullOrWhiteSpace(searchparameters.toDate))
                        {
                            searchparameters.toDate = DateTime.UtcNow.Date.AddHours(23).AddMinutes(59).AddSeconds(59).ToString();
                        }
                        else
                        {
                            searchparameters.toDate = Convert.ToDateTime(searchparameters.toDate).Date.AddHours(23).AddMinutes(59).AddSeconds(59).ToString();
                        }
                        if((!String.IsNullOrWhiteSpace(searchparameters.fromDate)) && (!String.IsNullOrWhiteSpace(searchparameters.toDate)))
                        {
                            if (Convert.ToDateTime(searchparameters.fromDate) > Convert.ToDateTime(searchparameters.toDate))
                            {
                                return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("To date must be greater than From date")).Value);
                            }
                        }
                        var response = await _clinicalStudyService.SearchStudy(searchparameters).ConfigureAwait(false);

                        if (response == null)
                        {
                            if (Request != null)
                                Response.Headers.Add("Controller", "True");
                            return NotFound(new JsonResult(ErrorResponseHelper.NotFound("No Study Matches the search keywords")).Value);
                        }
                        else
                        {                           
                            return Ok(response);
                        }
                    }
                    else
                    {
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest("Kindly provide valid search parameters")).Value);
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
        #endregion

        #endregion

    }
}
