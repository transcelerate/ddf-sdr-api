using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.Entities.Study;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Interfaces;

namespace TransCelerate.SDR.Services.Services
{
    public class ClinicalStudyService : IClinicalStudyService
    {
        #region Variables
        private readonly IClinicalStudyRepository _clinicalStudyRepository;
        private readonly IMapper _mapper;
        private readonly ILogHelper _logger;
        #endregion

        #region Constructor
        public ClinicalStudyService(IClinicalStudyRepository clinicalStudyRepository, IMapper mapper, ILogHelper logger)
        {
            _clinicalStudyRepository = clinicalStudyRepository;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion

        #region ActionMethods
        #region GET Methods
        #region Depricated Methods
        ////GET InterventionModel For a Study

        //public async Task<InterventionModelResponse> InterventionModel(string study, string version, string status)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServie)}; Method : {nameof(InterventionModel)};");
        //        study = study.Trim();
        //        StudyEntity _study;
        //        if (String.IsNullOrWhiteSpace(status))
        //        {
        //            _study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: study, version: version).ConfigureAwait(false);
        //        }
        //        else
        //        {
        //            status = status.Trim();
        //            _study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: study, version: version, status: status).ConfigureAwait(false);
        //        }

        //        if (_study == null || String.IsNullOrEmpty(_study.clinicalStudy.interventionModel))
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            InterventionModelResponse interventionModelDTO = new InterventionModelResponse()
        //            {
        //                interventionModel = _study.clinicalStudy.interventionModel,
        //            };
        //            return interventionModelDTO;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServie)}; Method : {nameof(InterventionModel)};");
        //    }
        //}

        ////GET Investigationalinterventions For a Study
        //public async Task<object> Investigationalinterventions(string study, string version, string status)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServie)}; Method : {nameof(Investigationalinterventions)};");
        //        study = study.Trim();
        //        StudyEntity _study;
        //        if (String.IsNullOrWhiteSpace(status))
        //        {
        //            _study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: study, version: version).ConfigureAwait(false);
        //        }
        //        else
        //        {
        //            status = status.Trim();
        //            _study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: study, version: version, status: status).ConfigureAwait(false);
        //        }

        //        if (_study == null || _study.clinicalStudy.investigationalIntervention == null)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            InvestigationalInterventionResponse InvestigationalIntervention = new InvestigationalInterventionResponse()
        //            {
        //                investigationalIntervention = _mapper.Map<List<InvestigationalInterventionDTO>>(_study.clinicalStudy.investigationalIntervention),
        //            };
        //            var InvestigationalInterventionResponse = JsonConvert.DeserializeObject(
        //                   JsonConvert.SerializeObject(InvestigationalIntervention, JsonSettings.JsonSerializerSettings()));
        //            return InvestigationalInterventionResponse;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServie)}; Method : {nameof(Investigationalinterventions)};");
        //    }
        //}

        ////GET StudyIdentifiers For a Study      
        //public async Task<object> StudyIdentifiers(string study, string version, string status)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServie)}; Method : {nameof(StudyIdentifiers)};");
        //        study = study.Trim();
        //        StudyEntity _study;
        //        if (String.IsNullOrWhiteSpace(status))
        //        {
        //            _study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: study, version: version).ConfigureAwait(false);
        //        }
        //        else
        //        {
        //            status = status.Trim();
        //            _study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: study, version: version, status: status).ConfigureAwait(false);
        //        }

        //        if (_study == null || _study.clinicalStudy.studyIdentifier == null)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            StudyIdentifierResponse studyIdentifier = new StudyIdentifierResponse()
        //            {
        //                studyIdentifier = _mapper.Map<List<StudyIdentifierDTO>>(_study.clinicalStudy.studyIdentifier),
        //            };
        //            var studyIdentifierResponse = JsonConvert.DeserializeObject(
        //                   JsonConvert.SerializeObject(studyIdentifier, JsonSettings.JsonSerializerSettings()));
        //            return studyIdentifierResponse;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServie)}; Method : {nameof(StudyIdentifiers)};");
        //    }
        //}

        ////GET StudyPhase For a Study       
        //public async Task<StudyPhaseResponse> StudyPhase(string study, string version, string status)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServie)}; Method : {nameof(StudyPhase)};");
        //        study = study.Trim();
        //        StudyEntity _study;
        //        if (String.IsNullOrWhiteSpace(status))
        //        {
        //            _study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: study, version: version).ConfigureAwait(false);
        //        }
        //        else
        //        {
        //            status = status.Trim();
        //            _study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: study, version: version, status: status).ConfigureAwait(false);
        //        }

        //        if (_study == null || _study.clinicalStudy.studyPhase == null)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            StudyPhaseResponse studyphase = new StudyPhaseResponse()
        //            {
        //                studyPhase = String.IsNullOrEmpty(_study.clinicalStudy.studyPhase.ToString()) == true ? string.Empty : _study.clinicalStudy.studyPhase.ToString()
        //            };
        //            return studyphase;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServie)}; Method : {nameof(StudyPhase)};");
        //    }
        //}

        ////GET StudyProtocol For a Study

        //public async Task<object> StudyProtocol(string study, string version, string status)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServie)}; Method : {nameof(StudyProtocol)};");
        //        study = study.Trim();
        //        StudyEntity _study;
        //        if (String.IsNullOrWhiteSpace(status))
        //        {
        //            _study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: study, version: version).ConfigureAwait(false);
        //        }
        //        else
        //        {
        //            status = status.Trim();
        //            _study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: study, version: version, status: status).ConfigureAwait(false);
        //        }

        //        if (_study == null || _study.clinicalStudy.studyProtocol == null)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            StudyProtocolResponse protocol = new StudyProtocolResponse()
        //            {
        //                studyProtocol = _mapper.Map<StudyProtocolDTO>(_study.clinicalStudy.studyProtocol),
        //            };
        //            var protocolResponse = JsonConvert.DeserializeObject(
        //                   JsonConvert.SerializeObject(protocol, JsonSettings.JsonSerializerSettings()));
        //            return protocolResponse;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServie)}; Method : {nameof(StudyProtocol)};");
        //    }
        //}

        ////GET StudyObjectives For a Study        
        //public async Task<object> StudyObjectives(string study, string version, string status)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServie)}; Method : {nameof(StudyObjectives)};");
        //        study = study.Trim();
        //        StudyEntity _study;
        //        if (String.IsNullOrWhiteSpace(status))
        //        {
        //            _study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: study, version: version).ConfigureAwait(false);
        //        }
        //        else
        //        {
        //            status = status.Trim();
        //            _study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: study, version: version, status: status).ConfigureAwait(false);
        //        }

        //        if (_study == null || _study.clinicalStudy.studyObjective == null)
        //        {
        //            return null;
        //        }
        //        else
        //        {

        //            StudyObjectivesResponse studyObjective = new StudyObjectivesResponse()
        //            {
        //                studyObjective = _mapper.Map<List<StudyObjectiveDTO>>(_study.clinicalStudy.studyObjective)
        //            };
        //            var studyObjectivesResponse = JsonConvert.DeserializeObject(
        //                   JsonConvert.SerializeObject(studyObjective, JsonSettings.JsonSerializerSettings()));
        //            return studyObjectivesResponse;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServie)}; Method : {nameof(StudyObjectives)};");
        //    }
        //}

        ////GET StudyTargetPopulation For a Study

        //public async Task<object> StudyTargetPopulation(string study, string version, string status)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServie)}; Method : {nameof(StudyTargetPopulation)};");
        //        study = study.Trim();
        //        StudyEntity _study;
        //        if (String.IsNullOrWhiteSpace(status))
        //        {
        //            _study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: study, version: version).ConfigureAwait(false);
        //        }
        //        else
        //        {
        //            status = status.Trim();
        //            _study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: study, version: version, status: status).ConfigureAwait(false);
        //        }

        //        if (_study == null || _study.clinicalStudy.studyTargetPopulation == null)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            StudyTargetPopulationResponse studyTargetPopulation = new StudyTargetPopulationResponse()
        //            {
        //                studyTargetPopulation = _mapper.Map<List<StudyTargetPopulationDTO>>(_study.clinicalStudy.studyTargetPopulation),
        //            };
        //            var studyTargetPopulationResponse = JsonConvert.DeserializeObject(
        //                   JsonConvert.SerializeObject(studyTargetPopulation, JsonSettings.JsonSerializerSettings()));
        //            return studyTargetPopulation;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServie)}; Method : {nameof(StudyTargetPopulation)};");
        //    }
        //}

        ////GET StudyTitle For a Study     
        //public async Task<StudyTitleResponse> StudyTitle(string study, string version, string status)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServie)}; Method : {nameof(StudyTitle)};");
        //        study = study.Trim();
        //        StudyEntity _study;
        //        if (String.IsNullOrWhiteSpace(status))
        //        {
        //            _study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: study, version: version).ConfigureAwait(false);
        //        }
        //        else
        //        {
        //            status = status.Trim();
        //            _study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: study, version: version, status: status).ConfigureAwait(false);
        //        }

        //        if (_study == null || String.IsNullOrEmpty(_study.clinicalStudy.studyTitle))
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            StudyTitleResponse studytitle = new StudyTitleResponse()
        //            {
        //                studyTitle = _study.clinicalStudy.studyTitle,

        //            };
        //            return studytitle;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServie)}; Method : {nameof(StudyTitle)};");
        //    }
        //}

        ////GET StudyIndication For a Study       
        //public async Task<object> StudyIndication(string study, string version, string status)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServie)}; Method : {nameof(StudyIndication)};");
        //        study = study.Trim();
        //        StudyEntity _study;
        //        if (String.IsNullOrWhiteSpace(status))
        //        {
        //            _study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: study, version: version).ConfigureAwait(false);
        //        }
        //        else
        //        {
        //            status = status.Trim();
        //            _study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: study, version: version, status: status).ConfigureAwait(false);
        //        }

        //        if (_study == null || _study.clinicalStudy.studyTargetIndication == null)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            StudyIndicationResponse studyIndication = new StudyIndicationResponse()
        //            {
        //                studyTargetIndication = _mapper.Map<StudyTargetIndicationDTO>(_study.clinicalStudy.studyTargetIndication),
        //            };
        //            var studyIndicationResponse = JsonConvert.DeserializeObject(
        //                   JsonConvert.SerializeObject(studyIndication, JsonSettings.JsonSerializerSettings()));
        //            return studyIndicationResponse;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServie)}; Method : {nameof(StudyIndication)};");
        //    }
        //}

        ////GET StudyType For a Study       
        //public async Task<StudyTypeResponse> StudyType(string study, string version, string status)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServie)}; Method : {nameof(StudyType)};");
        //        study = study.Trim();
        //        StudyEntity _study;
        //        if (String.IsNullOrWhiteSpace(status))
        //        {
        //            _study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: study, version: version).ConfigureAwait(false);
        //        }
        //        else
        //        {
        //            status = status.Trim();
        //            _study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: study, version: version, status: status).ConfigureAwait(false);
        //        }

        //        if (_study == null || _study.clinicalStudy.studyType == null)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            StudyTypeResponse studytype = new StudyTypeResponse()
        //            {
        //                studyType = String.IsNullOrEmpty(_study.clinicalStudy.studyType.ToString()) == true ? string.Empty : _study.clinicalStudy.studyType.ToString(),
        //            };
        //            return studytype;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServie)}; Method : {nameof(StudyType)};");
        //    }
        //} 
        #endregion

        /// <summary>
        /// GET All Elements For a Study
        /// </summary>
        /// <param name="studyId"></param>
        /// <param name="version"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public async Task<object> GetAllElements(string studyId,int version, string tag)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyService)}; Method : {nameof(GetAllElements)};");
                studyId = studyId.Trim();
               
                StudyEntity study;
                if (String.IsNullOrWhiteSpace(tag))
                {
                    study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: studyId, version: version).ConfigureAwait(false);
                }
                else
                {
                    tag = tag.Trim();
                    study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: studyId, version: version, tag: tag).ConfigureAwait(false);
                }

                if (study == null)
                {                    
                    return null;
                }
                else
                {                       
                    var studyDTO = _mapper.Map<GetStudyDTO>(study);
                    if(DateTime.ParseExact(studyDTO.auditTrail.entryDateTime,Constants.DateFormats.DateFormatForAuditResponse,CultureInfo.InvariantCulture)
                        == DateTime.MinValue)
                    {
                        studyDTO.auditTrail.entryDateTime = null;
                    }
                    var studyResponse = JsonConvert.DeserializeObject(
                           JsonConvert.SerializeObject(studyDTO, JsonSettings.JsonSerializerSettings()));                    
                    return studyResponse;                  
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyService)}; Method : {nameof(GetAllElements)};");
            }
        }

        /// <summary>
        /// GET All Elements For a Study
        /// </summary>
        /// <param name="studyId"></param>
        /// <param name="version"></param>
        /// <param name="tag"></param>
        /// <param name="sections"></param>
        /// <returns></returns>
        public async Task<object> GetSections(string studyId, int version, string tag, string[] sections)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyService)}; Method : {nameof(GetSections)};");
                studyId = studyId.Trim();

                StudyEntity study;
                if (String.IsNullOrWhiteSpace(tag))
                {
                    study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: studyId, version: version).ConfigureAwait(false);
                }
                else
                {
                    tag = tag.Trim();
                    study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: studyId, version: version, tag: tag).ConfigureAwait(false);
                }

                if (study == null)
                {
                    return null;
                }
                else
                {                    
                    var studySectionDTO = _mapper.Map<GetStudySectionsDTO>(study.clinicalStudy);
                    studySectionDTO.studyVersion = study.auditTrail.studyVersion;                    
                    studySectionDTO = RemoveStudySections.RemoveSections(sections, studySectionDTO);
                    var studyResponse = JsonConvert.DeserializeObject(
                           JsonConvert.SerializeObject(studySectionDTO, JsonSettings.JsonSerializerSettings()));
                    return studyResponse;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyService)}; Method : {nameof(GetSections)};");
            }
        } 
        
        /// <summary>
        /// GET For a StudyDesign sections for a study
        /// </summary>
        /// <param name="studyId"></param>
        /// <param name="version"></param>
        /// <param name="tag"></param>
        /// <param name="sections"></param>
        /// <param name="studyDesignId"></param>
        /// <returns></returns>
        public async Task<object> GetStudyDesignSections(string studyId, string studyDesignId, int version, string tag, string[] sections)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyService)}; Method : {nameof(GetSections)};");
                studyId = studyId.Trim();

                StudyEntity study;
                if (String.IsNullOrWhiteSpace(tag))
                {
                    study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: studyId, version: version).ConfigureAwait(false);
                }
                else
                {
                    tag = tag.Trim();
                    study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: studyId, version: version, tag: tag).ConfigureAwait(false);
                }

                if (study == null)
                {
                    return null;
                }
                else
                {                    
                    var studySectionDTO = _mapper.Map<GetStudySectionsDTO>(study.clinicalStudy);
                    studySectionDTO.studyVersion = study.auditTrail.studyVersion;
                    studySectionDTO.studyDesigns = studySectionDTO.studyDesigns.FindAll(x => x.studyDesignId == studyDesignId).ToList();
                    studySectionDTO = RemoveStudySections.RemoveSectionsForStudyDesign(sections, studySectionDTO);
                    var studyResponse = JsonConvert.DeserializeObject(
                           JsonConvert.SerializeObject(studySectionDTO, JsonSettings.JsonSerializerSettings()));
                    return studyResponse;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyService)}; Method : {nameof(GetSections)};");
            }
        }

        /// <summary>
        /// GET Audit Trial
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="studyId"></param>
        /// <returns></returns>
        public async Task<object> GetAuditTrail(DateTime fromDate, DateTime toDate, string studyId)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyService)}; Method : {nameof(GetAuditTrail)};");                             
                var studies = await _clinicalStudyRepository.GetAuditTrail(fromDate, toDate, studyId);
                if(studies == null)
                {                    
                    return null;
                }
                else
                {
                    var auditTrailDTOList = _mapper.Map<List<AuditTrailDTO>>(studies);
                    GetStudyAuditDTO getStudyAuditDTO = new GetStudyAuditDTO
                    {
                        studyId = studyId, auditTrail = auditTrailDTOList
                    };
                    var studyAuditResponsesObject = JsonConvert.DeserializeObject(
                           JsonConvert.SerializeObject(getStudyAuditDTO, JsonSettings.JsonSerializerSettings()));
                    return studyAuditResponsesObject;
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyService)}; Method : {nameof(GetAuditTrail)};");
            }
        }
        #endregion


        #region POST Methods
        /// <summary>
        /// POST All Elements For a Study
        /// </summary>
        /// <param name="study"></param>
        /// <param name="entrySystem"></param>
        /// <param name="entrySystemId"></param>
        /// <returns></returns>
        public async Task<object> PostAllElements(PostStudyDTO study,string entrySystem, string entrySystemId)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyService)}; Method : {nameof(PostAllElements)};");                               
                var incomingstudyEntity = _mapper.Map<StudyEntity>(study);
                StudyEntity existingStudyEntity = _clinicalStudyRepository.GetStudyItemsAsync(incomingstudyEntity.clinicalStudy.studyId, 0).Result;
                AuditTrailEntity auditTrailEntity = new AuditTrailEntity();
                incomingstudyEntity.auditTrail = auditTrailEntity;
                incomingstudyEntity.auditTrail.entryDateTime = DateTime.UtcNow;
                incomingstudyEntity.auditTrail.entrySystemId = entrySystemId;
                incomingstudyEntity.auditTrail.entrySystem = entrySystem;                
                incomingstudyEntity.clinicalStudy.tag = incomingstudyEntity.clinicalStudy.tag.Trim();
                PostStudyResponseDTO postStudyDTO = new PostStudyResponseDTO();
                
                if (existingStudyEntity == null)
                {
                    incomingstudyEntity.auditTrail.studyVersion = 1;
                    incomingstudyEntity.clinicalStudy.studyId = Guid.NewGuid().ToString();
                    incomingstudyEntity._id = ObjectId.GenerateNewId();

                    _logger.LogInformation($"Study Input : {JsonConvert.SerializeObject(incomingstudyEntity)}");
                    postStudyDTO.studyId =  await _clinicalStudyRepository.PostStudyItemsAsync(incomingstudyEntity).ConfigureAwait(false);
                }
                else
                {
                    if (PostStudyElementsCheck.StudyComparison(incomingstudyEntity, existingStudyEntity))
                    {
                        incomingstudyEntity.auditTrail.studyVersion = existingStudyEntity.auditTrail.studyVersion;

                        _logger.LogInformation($"Study Input : {JsonConvert.SerializeObject(incomingstudyEntity)}");
                        postStudyDTO.studyId = await _clinicalStudyRepository.UpdateStudyItemsAsync(incomingstudyEntity);
                    }
                    else
                    {
                        incomingstudyEntity._id = ObjectId.GenerateNewId();                        
                        incomingstudyEntity.auditTrail.studyVersion = existingStudyEntity.auditTrail.studyVersion + 1;

                        _logger.LogInformation($"Study Input : {JsonConvert.SerializeObject(incomingstudyEntity)}");
                        postStudyDTO.studyId = await _clinicalStudyRepository.PostStudyItemsAsync(incomingstudyEntity).ConfigureAwait(false);
                    }
                }
                return postStudyDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyService)}; Method : {nameof(PostAllElements)};");
            }
        }

        /// <summary>
        /// POST All Elements For a Study
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        public async Task<object> SearchStudy(SearchParametersDTO searchParametersDTO)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyService)}; Method : {nameof(SearchStudy)};");
                _logger.LogInformation($"Search Parameters : {JsonConvert.SerializeObject(searchParametersDTO)}");

                var searchParameters = _mapper.Map<SearchParameters>(searchParametersDTO);

                var studies = await _clinicalStudyRepository.SearchStudy(searchParameters).ConfigureAwait(false);
                
                if (studies == null)
                {                  
                    return null;
                }
                else
                {
                    var studiesDTO = _mapper.Map<List<GetStudyDTO>>(studies);
                    var searchResponse = JsonConvert.DeserializeObject(
                           JsonConvert.SerializeObject(studiesDTO, JsonSettings.JsonSerializerSettings()));
                    return searchResponse;
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyService)}; Method : {nameof(SearchStudy)};");
            }
        }
        #endregion
        #endregion

    }
}
