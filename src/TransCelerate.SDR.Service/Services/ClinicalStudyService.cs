using AutoMapper;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.Entities.Study;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Enums;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.Core.DTO.Token;


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
        /// <param name="studyId">Study ID</param>
        /// <param name="version">Version of study</param>
        /// <param name="tag">Tag of a study</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<object> GetAllElements(string studyId,int version, string tag, LoggedInUser user)
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
                    var checkStudy = await CheckAccessForAStudy(study, user);
                    if (checkStudy == null)
                        return Constants.ErrorMessages.Forbidden;
                    var studyDTO = _mapper.Map<GetStudyDTO>(study);  //Mapping Entity to Dto                                                  
                    return studyDTO;                  
                }
            }
            catch (Exception)
            {                
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyService)}; Method : {nameof(GetAllElements)};");
            }
        }

        /// <summary>
        /// GET All Elements For a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="version">Version of study</param>
        /// <param name="tag">Tag of a study</param>
        /// <param name="sections">Study sections which have to be fetched</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> of study sections with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<object> GetSections(string studyId, int version, string tag, string[] sections, LoggedInUser user)
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
                    var checkStudy = await CheckAccessForAStudy(study, user);
                    if (checkStudy == null)
                        return Constants.ErrorMessages.Forbidden;
                    var studySectionDTO = _mapper.Map<GetStudySectionsDTO>(study.clinicalStudy);
                    studySectionDTO.studyVersion = study.auditTrail.studyVersion;                                                           
                   
                    return RemoveStudySections.RemoveSections(sections, studySectionDTO); //Remove the sections which are not part of sections array
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyService)}; Method : {nameof(GetSections)};");
            }
        }

        /// <summary>
        /// GET For a StudyDesign sections for a study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="version">Version of study</param>
        /// <param name="tag">Tag of a study</param>
        /// <param name="studyDesignId">Study Design Id</param>
        /// <param name="sections">Study Design sections which have to be fetched</param>   
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> of studyDesign sections with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<object> GetStudyDesignSections(string studyId, string studyDesignId, int version, string tag, string[] sections, LoggedInUser user)
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
                    var checkStudy = await CheckAccessForAStudy(study, user);
                    if (checkStudy == null)
                        return Constants.ErrorMessages.Forbidden;
                    var studySectionDTO = _mapper.Map<GetStudySectionsDTO>(study.clinicalStudy); //Mapping Entity to Dto  
                    studySectionDTO.studyVersion = study.auditTrail.studyVersion;
                    studySectionDTO.studyDesigns = studySectionDTO.studyDesigns != null? studySectionDTO.studyDesigns.FindAll(x => x.studyDesignId == studyDesignId).Count()!=0 ? studySectionDTO.studyDesigns.FindAll(x => x.studyDesignId == studyDesignId).ToList(): new List<GetStudyDesignsDTO>() : new List<GetStudyDesignsDTO>();                    
                   
                    return RemoveStudySections.RemoveSectionsForStudyDesign(sections, studySectionDTO); //Remove the sections which are not part of sections array
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyService)}; Method : {nameof(GetStudyDesignSections)};");
            }
        }

        /// <summary>
        /// GET Audit Trial
        /// </summary>
        /// <param name="fromDate">Start Date for Date Filter</param>
        /// <param name="toDate">End Date for Date Filter</param>
        /// <param name="studyId">Study ID</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<object> GetAuditTrail(DateTime fromDate, DateTime toDate, string studyId, LoggedInUser user)
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
                    studies = await CheckAccessForAuditTrail(studies, user);
                    if (studies == null)
                        return Constants.ErrorMessages.Forbidden;
                    var auditTrailDTOList = _mapper.Map<List<AuditTrailEndpointResponseDTO>>(studies); //Mapping Entity to Dto 
                    GetStudyAuditDTO getStudyAuditDTO = new GetStudyAuditDTO
                    {
                        studyId = studyId, auditTrail = auditTrailDTOList
                    };
                    
                    return getStudyAuditDTO;
                }
            }
            catch (Exception)
            {                
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyService)}; Method : {nameof(GetAuditTrail)};");
            }
        }

        /// <summary>
        /// Get AllStudy Id's
        /// </summary>
        /// <param name="fromDate">Start Date for Date Filter</param>
        /// <param name="toDate">End Date for Date Filter</param>
        /// <param name="studyTitle">Study Title Filter</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="GetStudyHistoryResponseDTO"/> which has list of study ID's <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<GetStudyHistoryResponseDTO> GetAllStudyId(DateTime fromDate, DateTime toDate, string studyTitle, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyService)}; Method : {nameof(GetAllStudyId)};");
                var studies = await _clinicalStudyRepository.GetAllStudyId(fromDate, toDate, studyTitle,user); //Getting List of studyId, studyTitle and Version
                if (studies == null)
                {
                    return null;
                }
                else
                {                    
                    var groupStudy = studies.GroupBy(x => new { x.studyId })
                                            .Select(g => new
                                            {
                                                studyId = g.Key.studyId,
                                                studyTitle = g.Select(x => x).Where(x => x.studyVersion == g.Max(x => x.studyVersion)).FirstOrDefault().studyTitle,
                                                studyVersion = g.Select(x => x.studyVersion).OrderBy(x => x).ToArray(),
                                                date = g.Select(x => x).Where(x => x.studyVersion == g.Max(x => x.studyVersion)).FirstOrDefault().entryDateTime
                                            }) // Grouping the Id's by studyId
                                            .OrderByDescending(x => x.date)
                                            .ToList();

                    GetStudyHistoryResponseDTO allStudyIdResponseDTO = new GetStudyHistoryResponseDTO() //Mapping Group to Study History ResponseDto 
                    {
                        study = JsonConvert.DeserializeObject<List<StudyHistoryDTO>>(JsonConvert.SerializeObject(groupStudy))
                    };

                    return allStudyIdResponseDTO;
                }
            }
            catch (Exception)
            {
                throw;
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
        /// <param name="studyDTO">Study for Inserting/Updating in Database</param>
        /// <param name="entrySystem">System which made the request</param>  
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="PostStudyDTO"/> which has study ID and study design ID's <br></br> <br></br>
        /// <see langword="null"/> If the insert is not done
        /// </returns>
        public async Task<object> PostAllElements(PostStudyDTO studyDTO,string entrySystem, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyService)}; Method : {nameof(PostAllElements)};");
                if (!await CheckPermissionForAUser(user))
                    return Constants.ErrorMessages.PostRestricted;
                var incomingstudyEntity = _mapper.Map<StudyEntity>(studyDTO);           //Mapping Dto to Entity                
                #region Adding Audit Trail for Incoming Study
                AuditTrailEntity auditTrailEntity = new AuditTrailEntity();
                incomingstudyEntity.auditTrail = auditTrailEntity;
                incomingstudyEntity.auditTrail.entryDateTime = DateTime.UtcNow;
                incomingstudyEntity.auditTrail.entrySystem = entrySystem;
                #endregion

                object responseObject; //This object is for sending response for the POST Request
                //PostStudyResponseDTO postStudyDTO = new PostStudyResponseDTO(); //This class is for sending response for the POST Request

                if (String.IsNullOrEmpty(incomingstudyEntity.clinicalStudy.studyId)) 
                {
                    incomingstudyEntity.auditTrail.studyVersion = 1; 
                    incomingstudyEntity.clinicalStudy.studyId = IdGenerator.GenerateId(); 
                    incomingstudyEntity._id = ObjectId.GenerateNewId(); 
                    incomingstudyEntity.clinicalStudy.studyIdentifiers.ForEach(x => x.studyIdentifierId = IdGenerator.GenerateId()); //UUID for studyIdentifiers
                    SectionIdGenerator.GenerateSectionId(incomingstudyEntity); //UUID for all sections

                    #region Previous and Next Items Logic
                    PreviousItemNextItemHelper.PreviousItemsNextItemsWraper(incomingstudyEntity);
                    #endregion

                    _logger.LogInformation($"entrySystem: {entrySystem??"<null>"}; Study Input : {JsonConvert.SerializeObject(incomingstudyEntity)}");
                    await _clinicalStudyRepository.PostStudyItemsAsync(incomingstudyEntity).ConfigureAwait(false);
                    studyDTO = _mapper.Map<PostStudyDTO>(incomingstudyEntity);
                    responseObject = RemoveStudySections.PostResponseRemoveSections(studyDTO);                   
                }
                else //If there is a studyId in the input
                {
                    StudyEntity existingStudyEntity = _clinicalStudyRepository.GetStudyItemsAsync(incomingstudyEntity.clinicalStudy.studyId, 0).Result;                  
                    
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

                        var duplicateExistingStudy = JsonConvert.DeserializeObject<StudyEntity>(JsonConvert.SerializeObject(existingStudyEntity)); // Creating duplicates for existing entity
                        var duplicateIncomingStudy = JsonConvert.DeserializeObject<StudyEntity>(JsonConvert.SerializeObject(incomingstudyEntity)); // Creating duplicates for incoming entity

                        if (PostStudyElementsCheck.StudyComparison(duplicateIncomingStudy, duplicateExistingStudy)) //If the data in existing and incoming are same
                        {
                            _logger.LogInformation($"Study Input : {JsonConvert.SerializeObject(existingStudyEntity)}");
                            await _clinicalStudyRepository.UpdateStudyItemsAsync(existingStudyEntity); //update the existing latest version
                            studyDTO = _mapper.Map<PostStudyDTO>(existingStudyEntity);
                            responseObject = RemoveStudySections.PostResponseRemoveSections(studyDTO);                         
                        }
                        else
                        {
                            incomingstudyEntity._id = ObjectId.GenerateNewId();
                            existingStudyEntity.auditTrail.studyVersion += 1;                            
                            PostStudyElementsCheck.SectionCheck(incomingstudyEntity, existingStudyEntity);

                            _logger.LogInformation($"Study Input : {JsonConvert.SerializeObject(existingStudyEntity)}");
                            existingStudyEntity._id = ObjectId.GenerateNewId();
                            await _clinicalStudyRepository.PostStudyItemsAsync(existingStudyEntity).ConfigureAwait(false);
                            studyDTO = _mapper.Map<PostStudyDTO>(existingStudyEntity);
                            responseObject = RemoveStudySections.PostResponseRemoveSections(studyDTO);                            
                        }                       
                    }
                }
                return responseObject;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyService)}; Method : {nameof(PostAllElements)};");
            }
        }

        /// <summary>
        /// Search Study Elements with search criteria
        /// </summary>
        /// <param name="searchParametersDTO">Parameters to search in database</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="List{GetStudyDTO}"/> which matches serach criteria <br></br> <br></br>
        /// <see langword="null"/> If the insert is not done
        /// </returns>
        public async Task<List<GetStudyDTO>> SearchStudy(SearchParametersDTO searchParametersDTO, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyService)}; Method : {nameof(SearchStudy)};");
                _logger.LogInformation($"Search Parameters : {JsonConvert.SerializeObject(searchParametersDTO)}");

                var searchParameters = _mapper.Map<SearchParameters>(searchParametersDTO);

                var studies = await _clinicalStudyRepository.SearchStudy(searchParameters,user).ConfigureAwait(false);
                
                if (studies == null)
                {                  
                    return null;
                }
                else
                {
                    var studiesDTO = _mapper.Map<List<GetStudyDTO>>(studies); //Mapper to map from Entity to Dto
                    for(int i=0; i < studiesDTO.Count; i++)
                    {
                        var investigationalInterventionsEntity = studies[i].investigationalInterventions?.Where(x => x != null && x.Count() > 0).SelectMany(x => x)
                                                                                .Where(x => x != null && x.Count() > 0).SelectMany(x => x)
                                                                                .Where(x => x != null && x.Count() > 0).SelectMany(x => x).ToList();

                        var studyIndications = studies[i].studyIndications?.Where(x => x != null && x.Count() > 0).SelectMany(x => x).ToList();
                                                                                

                        var investigationalInterventionsDTO = _mapper.Map<List<InvestigationalInterventionDTO>>(investigationalInterventionsEntity);
                        var studyIndicationsDTO = _mapper.Map<List<StudyIndicationDTO>>(studyIndications);
                        var studyDesigns = new List<GetStudyDesignsDTO>();
                        var studyDesign = new GetStudyDesignsDTO
                        {
                            investigationalInterventions = investigationalInterventionsDTO
                        };
                        studyDesigns.Add(studyDesign);
                        studiesDTO[i].clinicalStudy.studyDesigns = studyDesigns;
                        studiesDTO[i].clinicalStudy.studyIndications = studyIndicationsDTO;
                        studiesDTO[i].auditTrail.entryDateTime = Convert.ToDateTime(studiesDTO[i].auditTrail.entryDateTime).ToString(Constants.DateFormats.DateFormatForAuditResponse).ToUpper();
                    }
                                        
                    return studiesDTO;                   
                }
            }
            catch (Exception)
            {                
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyService)}; Method : {nameof(SearchStudy)};");
            }
        }
        #endregion
        #endregion

        #region User Group Mapping For Study
        /// <summary>
        /// Check access for the study
        /// </summary>
        /// <param name="study">Study for which user access have to be checked</param>   
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="StudyEntity"/> if the user have access <br></br> <br></br>
        /// <see langword="null"/> If user doesn't have access to the study
        /// </returns>
        public async Task<StudyEntity> CheckAccessForAStudy(StudyEntity study, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyService)}; Method : {nameof(CheckAccessForAStudy)};");
                
                if (user.UserRole != Constants.Roles.Org_Admin && Config.isGroupFilterEnabled)
                {
                    var groups = await _clinicalStudyRepository.GetGroupsOfUser(user).ConfigureAwait(false);

                    if (groups != null && groups.Count > 0)
                    {
                        List<string> studyTypeFilterValues = new List<string>();
                        List<string> studyIdFilterValues = new List<string>();
                        studyTypeFilterValues.AddRange(groups.SelectMany(x => x.groupFilter)
                                                             .Where(x => x.groupFieldName == GroupFieldNames.studyType.ToString())
                                                             .SelectMany(x => x.groupFilterValues)
                                                             .Select(x => x.groupFilterValueId.ToLower())
                                                             .ToList());
                        studyIdFilterValues.AddRange(groups.SelectMany(x => x.groupFilter)
                                                             .Where(x => x.groupFieldName == GroupFieldNames.study.ToString())
                                                             .SelectMany(x => x.groupFilterValues)
                                                             .Select(x => x.groupFilterValueId)
                                                             .ToList());
                        if (studyIdFilterValues.Contains(study.clinicalStudy.studyId))
                            return study;
                        else if(studyTypeFilterValues.Contains(study.clinicalStudy.studyType.ToLower()))
                            return study;
                        else 
                            return null;
                    }
                    else
                    {
                        // Filter should not give any results
                        return null;
                    }
                }
                else
                    return study;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyService)}; Method : {nameof(CheckAccessForAStudy)};");
            }
        }

        /// <summary>
        /// Check access for the Study Aduit
        /// </summary>
        /// <param name="studyList">Study List for which user access have to be checked</param>   
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="List{StudyEntity}"/> if the user have access <br></br> <br></br>
        /// <see langword="null"/> If user doesn't have access to the study
        /// </returns>
        public async Task<List<StudyEntity>> CheckAccessForAuditTrail(List<StudyEntity> studyList, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyService)}; Method : {nameof(CheckAccessForAuditTrail)};");

                if (user.UserRole != Constants.Roles.Org_Admin && Config.isGroupFilterEnabled)
                {
                    var groups = await _clinicalStudyRepository.GetGroupsOfUser(user).ConfigureAwait(false);

                    if (groups != null && groups.Count > 0)
                    {
                        List<string> studyTypeFilterValues = new List<string>();
                        List<string> studyIdFilterValues = new List<string>();
                        studyTypeFilterValues.AddRange(groups.SelectMany(x => x.groupFilter)
                                                             .Where(x => x.groupFieldName == GroupFieldNames.studyType.ToString())
                                                             .SelectMany(x => x.groupFilterValues)
                                                             .Select(x => x.groupFilterValueId.ToLower())
                                                             .ToList());
                        studyIdFilterValues.AddRange(groups.SelectMany(x => x.groupFilter)
                                                             .Where(x => x.groupFieldName == GroupFieldNames.study.ToString())
                                                             .SelectMany(x => x.groupFilterValues)
                                                             .Select(x => x.groupFilterValueId)
                                                             .ToList());
                        var studyListAfterFiltering = new List<StudyEntity>();
                        if (studyIdFilterValues.Contains(studyList[0].clinicalStudy.studyId))
                            return studyList;
                        else
                        {
                            studyList.RemoveAll(x => !studyTypeFilterValues.Contains(x.clinicalStudy.studyType.ToLower()));
                            return studyList.Count > 0 ? studyList : null;
                        }
                        
                    }
                    else
                    {                        
                        return null;
                    }
                }
                else
                    return studyList;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyService)}; Method : {nameof(CheckAccessForAuditTrail)};");
            }
        }

        /// <summary>
        /// Check READ_WRITE Permission for a user
        /// </summary>    
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// <see langword="true"/> If the user have READ_WRITE access in any of the groups <br></br> <br></br>
        /// <see langword="false"/> If the user does not have READ_WRITE access in any of the groups
        /// </returns>
        public async Task<bool> CheckPermissionForAUser(LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyService)}; Method : {nameof(CheckPermissionForAUser)};");

                if (user.UserRole != Constants.Roles.Org_Admin && Config.isGroupFilterEnabled)
                {
                    var groups = await _clinicalStudyRepository.GetGroupsOfUser(user).ConfigureAwait(false);

                    if (groups != null && groups.Count > 0)
                    {
                        if (groups.Any(x => x.permission == Permissions.READ_WRITE.ToString()))
                            return true;
                        else
                            return false;
                    }
                    else
                    {                        
                        return false;
                    }
                }
                else
                    return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyService)}; Method : {nameof(CheckPermissionForAUser)};");
            }
        }
        #endregion
    }
}
