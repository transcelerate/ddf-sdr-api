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
using TransCelerate.SDR.Core.Entities;

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
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyService)}; Method : {nameof(GetStudyDesignSections)};");
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
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyService)}; Method : {nameof(GetStudyDesignSections)};");
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

        /// <summary>
        /// Get AllStudy Id's
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public async Task<object> GetAllStudyId(DateTime fromDate, DateTime toDate)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyService)}; Method : {nameof(GetAllStudyId)};");
                var studies = await _clinicalStudyRepository.GetAllStudyId(fromDate, toDate);
                if (studies == null)
                {
                    return null;
                }
                else
                {
                    List<StudyHistoryEntity> studyHistories = JsonConvert.DeserializeObject<List<StudyHistoryEntity>>(JsonConvert.SerializeObject(studies));
                    var groupStudy = studyHistories.GroupBy(x => new { x.studyId })
                                            .Select(g => new
                                            {
                                                studyId = g.Key.studyId,
                                                studyTitle = g.Select(x => x).Where(x => x.studyVersion == g.Max(x => x.studyVersion)).FirstOrDefault().studyTitle,
                                                studyVersion = g.Select(x => x.studyVersion).OrderBy(x => x).ToArray(),
                                                date = g.Select(x => x).Where(x => x.studyVersion == g.Max(x => x.studyVersion)).FirstOrDefault().entryDateTime
                                            })
                                            .OrderByDescending(x => x.date)
                                            .ToList();

                    AllStudyIdResponseDTO allStudyIdResponseDTO = new AllStudyIdResponseDTO()
                    {
                        study = JsonConvert.DeserializeObject<List<StudyHistory>>(JsonConvert.SerializeObject(groupStudy))
                    };

                    return allStudyIdResponseDTO;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyService)}; Method : {nameof(GetAllStudyId)};");
            }
        }
        #endregion


        #region POST Methods
        /// <summary>
        /// POST All Elements For a Study
        /// </summary>
        /// <param name="studyDTO"></param>
        /// <param name="entrySystem"></param>
        /// <param name="entrySystemId"></param>
        /// <returns></returns>
        public async Task<object> PostAllElements(PostStudyDTO studyDTO,string entrySystem)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyService)}; Method : {nameof(PostAllElements)};");                  
                var incomingstudyEntity = _mapper.Map<StudyEntity>(studyDTO);
                AuditTrailEntity auditTrailEntity = new AuditTrailEntity();
                incomingstudyEntity.auditTrail = auditTrailEntity;
                incomingstudyEntity.auditTrail.entryDateTime = DateTime.UtcNow;   
                incomingstudyEntity.auditTrail.entrySystem = entrySystem;
                PostStudyResponseDTO postStudyDTO = new PostStudyResponseDTO();
                if (String.IsNullOrEmpty(incomingstudyEntity.clinicalStudy.studyId))
                {
                    incomingstudyEntity.auditTrail.studyVersion = 1;
                    incomingstudyEntity.clinicalStudy.studyId = IdGenerator.GenerateId();
                    incomingstudyEntity._id = ObjectId.GenerateNewId();
                    incomingstudyEntity.clinicalStudy.studyIdentifiers.ForEach(x => x.studyIdentifierId = IdGenerator.GenerateId());
                    SectionIdGenerator.GenerateSectionId(incomingstudyEntity);
                    if (incomingstudyEntity.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns.FindAll(x => x.currentSections != null).Count() != 0)
                    {
                        incomingstudyEntity.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns
                                                              .Find(x => x.currentSections != null).currentSections
                                                              .FindAll(x => x.plannedWorkflows != null)
                                                              .ForEach(x => x.plannedWorkflows
                                                                    .ForEach(p => p.workflowItemMatrix.matrix
                                                                            .ForEach(m => m.items = PreviousItemNextItemHelper.GetPreviousNextItems(m.items))));
                    }

                    _logger.LogInformation($"entrySystem: {entrySystem??"<null>"}; Study Input : {JsonConvert.SerializeObject(incomingstudyEntity)}");
                    postStudyDTO.studyId =  await _clinicalStudyRepository.PostStudyItemsAsync(incomingstudyEntity).ConfigureAwait(false);
                    postStudyDTO.studyVersion = incomingstudyEntity.auditTrail.studyVersion;
                    var studyDesign = incomingstudyEntity.clinicalStudy.currentSections!=null?incomingstudyEntity.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).ToList():new List<CurrentSectionsEntity>();
                    if (studyDesign.Count() != 0)
                    {
                        var designIdList = new List<string>();
                        foreach (var item in studyDesign.Find(x => x.studyDesigns != null).studyDesigns)
                        {
                            designIdList.Add(item.studyDesignId);
                        }
                        postStudyDTO.studyDesignId = designIdList;
                    }
                }
                else
                {
                    StudyEntity existingStudyEntity = _clinicalStudyRepository.GetStudyItemsAsync(incomingstudyEntity.clinicalStudy.studyId, 0).Result;
                    //var existingStudyDTO = _mapper.Map<PostStudyDTO>(existingStudyEntity);
                    
                    if(existingStudyEntity == null)
                    {
                        return Constants.ErrorMessages.NotValidStudyId;
                    }
                    else
                    {
                        existingStudyEntity.auditTrail.entryDateTime = incomingstudyEntity.auditTrail.entryDateTime;
                        existingStudyEntity.auditTrail.entrySystem = incomingstudyEntity.auditTrail.entrySystem;                        
                        incomingstudyEntity._id = existingStudyEntity._id;
                        incomingstudyEntity.auditTrail.studyVersion = existingStudyEntity.auditTrail.studyVersion;
                        var duplicateExistingStudy = JsonConvert.DeserializeObject<StudyEntity>(JsonConvert.SerializeObject(existingStudyEntity));
                        var duplicateIncomingStudy = JsonConvert.DeserializeObject<StudyEntity>(JsonConvert.SerializeObject(incomingstudyEntity));
                        if (PostStudyElementsCheck.StudyComparison(duplicateIncomingStudy, duplicateExistingStudy))
                        {
                            _logger.LogInformation($"Study Input : {JsonConvert.SerializeObject(existingStudyEntity)}");
                            postStudyDTO.studyId = await _clinicalStudyRepository.UpdateStudyItemsAsync(existingStudyEntity);
                            postStudyDTO.studyVersion = existingStudyEntity.auditTrail.studyVersion;
                        }
                        else
                        {
                            incomingstudyEntity._id = ObjectId.GenerateNewId();
                            existingStudyEntity.auditTrail.studyVersion += 1;
                            //existingStudyEntity = _mapper.Map<StudyEntity>(incomingstudyEntity);
                            PostStudyElementsCheck.SectionCheck(incomingstudyEntity, existingStudyEntity);

                            _logger.LogInformation($"Study Input : {JsonConvert.SerializeObject(existingStudyEntity)}");
                            existingStudyEntity._id = ObjectId.GenerateNewId();
                            postStudyDTO.studyId = await _clinicalStudyRepository.PostStudyItemsAsync(existingStudyEntity).ConfigureAwait(false);
                            postStudyDTO.studyVersion = existingStudyEntity.auditTrail.studyVersion;
                        }
                        var studyDesign = existingStudyEntity.clinicalStudy.currentSections != null ? existingStudyEntity.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).ToList() : new List<CurrentSectionsEntity>();
                        if (studyDesign.Count() != 0)
                        {
                            var designIdList = new List<string>();
                            foreach (var item in studyDesign.Find(x => x.studyDesigns != null).studyDesigns)
                            {
                                designIdList.Add(item.studyDesignId);
                            }
                            postStudyDTO.studyDesignId = designIdList;
                        }
                    }
                }
                return JsonConvert.DeserializeObject(
                           JsonConvert.SerializeObject(postStudyDTO, JsonSettings.JsonSerializerSettings()));
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
