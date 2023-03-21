using AutoMapper;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.Study;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Enums;
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
        public async Task<object> GetAllElements(string studyId, int version, string tag, LoggedInUser user)
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
                    studyDTO.Links = LinksHelper.GetLinksForUi(studyDTO.ClinicalStudy.StudyId, studyDTO.ClinicalStudy?.StudyDesigns?.Select(x => x.StudyDesignId).ToList(), studyDTO.AuditTrail.UsdmVersion, studyDTO.AuditTrail.StudyVersion);
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
                    var studySectionDTO = _mapper.Map<GetStudySectionsDTO>(study.ClinicalStudy);
                    studySectionDTO.StudyVersion = study.AuditTrail.StudyVersion;

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
                    var studySectionDTO = _mapper.Map<GetStudySectionsDTO>(study.ClinicalStudy); //Mapping Entity to Dto  
                    studySectionDTO.StudyVersion = study.AuditTrail.StudyVersion;
                    studySectionDTO.StudyDesigns = studySectionDTO.StudyDesigns != null ? studySectionDTO.StudyDesigns.FindAll(x => x.StudyDesignId == studyDesignId).Count != 0 ? studySectionDTO.StudyDesigns.FindAll(x => x.StudyDesignId == studyDesignId).ToList() : new List<GetStudyDesignsDTO>() : new List<GetStudyDesignsDTO>();
                    studySectionDTO.Links = LinksHelper.GetLinks(studySectionDTO.StudyId, studySectionDTO.StudyDesigns?.Select(x => x.StudyDesignId), study.AuditTrail.UsdmVersion, study.AuditTrail.StudyVersion);

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
                if (studies == null)
                {
                    return null;
                }
                else
                {
                    studies = await CheckAccessForAuditTrail(studies, user);
                    if (studies == null)
                        return Constants.ErrorMessages.Forbidden;
                    var auditTrailDTOList = _mapper.Map<List<AuditTrailEndpointResponseDTO>>(studies); //Mapping Entity to Dto 
                    GetStudyAuditDTO getStudyAuditDTO = new()
                    {
                        StudyId = studyId,
                        AuditTrail = auditTrailDTOList
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
                var studies = await _clinicalStudyRepository.GetAllStudyId(fromDate, toDate, studyTitle, user); //Getting List of studyId, studyTitle and Version
                if (studies == null)
                {
                    return null;
                }
                else
                {
                    var groupStudy = studies.GroupBy(x => new { x.StudyId })
                                            .Select(g => new
                                            {
                                                g.Key.StudyId,
                                                g.Select(x => x).Where(x => x.StudyVersion == g.Max(x => x.StudyVersion)).FirstOrDefault().StudyTitle,
                                                studyVersion = g.Select(x => x.StudyVersion).OrderBy(x => x).ToArray(),
                                                date = g.Select(x => x).Where(x => x.StudyVersion == g.Max(x => x.StudyVersion)).FirstOrDefault().EntryDateTime,
                                                g.Select(x => x).Where(x => x.StudyVersion == g.Max(x => x.StudyVersion)).FirstOrDefault().UsdmVersion,
                                            }) // Grouping the Id's by studyId
                                            .OrderByDescending(x => x.date)
                                            .Select(x => new StudyHistoryDTO
                                            {
                                                StudyId = x.StudyId,
                                                StudyTitle = x.StudyTitle,
                                                StudyVersion = x.studyVersion,
                                                UsdmVersion = x.UsdmVersion
                                            })
                                            .ToList();

                    GetStudyHistoryResponseDTO allStudyIdResponseDTO = new() //Mapping Group to Study History ResponseDto 
                    {
                        Study = groupStudy
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
        public async Task<object> PostAllElements(PostStudyDTO studyDTO, string entrySystem, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyService)}; Method : {nameof(PostAllElements)};");
                if (!await CheckPermissionForAUser(user))
                    return Constants.ErrorMessages.PostRestricted;
                var incomingstudyEntity = _mapper.Map<StudyEntity>(studyDTO);           //Mapping Dto to Entity                
                #region Adding Audit Trail for Incoming Study
                AuditTrailEntity auditTrailEntity = new();
                incomingstudyEntity.AuditTrail = auditTrailEntity;
                incomingstudyEntity.AuditTrail.EntryDateTime = DateTime.UtcNow;
                incomingstudyEntity.AuditTrail.EntrySystem = entrySystem;
                #endregion

                object responseObject; //This object is for sending response for the POST Request
                //PostStudyResponseDTO postStudyDTO = new PostStudyResponseDTO(); //This class is for sending response for the POST Request

                if (String.IsNullOrEmpty(incomingstudyEntity.ClinicalStudy.StudyId))
                {
                    incomingstudyEntity.AuditTrail.StudyVersion = 1;
                    incomingstudyEntity.ClinicalStudy.StudyId = IdGenerator.GenerateId();
                    incomingstudyEntity.Id = ObjectId.GenerateNewId();
                    incomingstudyEntity.ClinicalStudy.StudyIdentifiers.ForEach(x => x.StudyIdentifierId = IdGenerator.GenerateId()); //UUID for studyIdentifiers
                    SectionIdGenerator.GenerateSectionId(incomingstudyEntity); //UUID for all sections

                    #region Previous and Next Items Logic
                    PreviousItemNextItemHelper.PreviousItemsNextItemsWraper(incomingstudyEntity);
                    #endregion

                    incomingstudyEntity.AuditTrail.UsdmVersion = Constants.USDMVersions.MVP;
                    _logger.LogInformation($"entrySystem: {entrySystem ?? "<null>"}; Study Input : {JsonConvert.SerializeObject(incomingstudyEntity)}");
                    await _clinicalStudyRepository.PostStudyItemsAsync(incomingstudyEntity).ConfigureAwait(false);
                    studyDTO = _mapper.Map<PostStudyDTO>(incomingstudyEntity);
                    responseObject = RemoveStudySections.PostResponseRemoveSections(studyDTO);
                }
                else //If there is a studyId in the input
                {
                    AuditTrailEntity previousAuditTrailEntity = await _clinicalStudyRepository.GetUsdmVersionAsync(incomingstudyEntity.ClinicalStudy.StudyId, 0).ConfigureAwait(false);

                    if (previousAuditTrailEntity is null)
                    {
                        return Constants.ErrorMessages.NotValidStudyId;
                    }

                    if (previousAuditTrailEntity.UsdmVersion == Constants.USDMVersions.MVP)
                    {
                        StudyEntity existingStudyEntity = _clinicalStudyRepository.GetStudyItemsAsync(incomingstudyEntity.ClinicalStudy.StudyId, 0).Result;

                        existingStudyEntity.AuditTrail.EntryDateTime = incomingstudyEntity.AuditTrail.EntryDateTime;
                        existingStudyEntity.AuditTrail.EntrySystem = incomingstudyEntity.AuditTrail.EntrySystem;
                        incomingstudyEntity.Id = existingStudyEntity.Id;
                        incomingstudyEntity.AuditTrail.StudyVersion = existingStudyEntity.AuditTrail.StudyVersion;

                        var duplicateExistingStudy = JsonConvert.DeserializeObject<StudyEntity>(JsonConvert.SerializeObject(existingStudyEntity)); // Creating duplicates for existing entity
                        var duplicateIncomingStudy = JsonConvert.DeserializeObject<StudyEntity>(JsonConvert.SerializeObject(incomingstudyEntity)); // Creating duplicates for incoming entity

                        if (PostStudyElementsCheck.StudyComparison(duplicateIncomingStudy, duplicateExistingStudy)) //If the data in existing and incoming are same
                        {
                            existingStudyEntity.AuditTrail.UsdmVersion = Constants.USDMVersions.MVP;
                            _logger.LogInformation($"Study Input : {JsonConvert.SerializeObject(existingStudyEntity)}");
                            await _clinicalStudyRepository.UpdateStudyItemsAsync(existingStudyEntity); //update the existing latest version
                            studyDTO = _mapper.Map<PostStudyDTO>(existingStudyEntity);
                            responseObject = RemoveStudySections.PostResponseRemoveSections(studyDTO);
                        }
                        else
                        {
                            incomingstudyEntity.Id = ObjectId.GenerateNewId();
                            existingStudyEntity.AuditTrail.StudyVersion += 1;
                            PostStudyElementsCheck.SectionCheck(incomingstudyEntity, existingStudyEntity);

                            _logger.LogInformation($"Study Input : {JsonConvert.SerializeObject(existingStudyEntity)}");
                            existingStudyEntity.Id = ObjectId.GenerateNewId();
                            existingStudyEntity.AuditTrail.UsdmVersion = Constants.USDMVersions.MVP;
                            await _clinicalStudyRepository.PostStudyItemsAsync(existingStudyEntity).ConfigureAwait(false);
                            studyDTO = _mapper.Map<PostStudyDTO>(existingStudyEntity);
                            responseObject = RemoveStudySections.PostResponseRemoveSections(studyDTO);
                        }
                    }
                    else
                    {
                        return Constants.ErrorMessages.DowngradeError;
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

                var studies = await _clinicalStudyRepository.SearchStudy(searchParameters, user).ConfigureAwait(false);

                if (studies == null)
                {
                    return null;
                }
                else
                {
                    var studiesDTO = _mapper.Map<List<GetStudyDTO>>(studies); //Mapper to map from Entity to Dto
                    for (int i = 0; i < studiesDTO.Count; i++)
                    {
                        var investigationalInterventionsEntity = studies[i].InvestigationalInterventions?.Where(x => x != null && x.Any()).SelectMany(x => x)
                                                                                .Where(x => x != null && x.Any()).SelectMany(x => x)
                                                                                .Where(x => x != null && x.Any()).SelectMany(x => x).ToList();

                        var studyIndications = studies[i].StudyIndications?.Where(x => x != null && x.Any()).SelectMany(x => x).ToList();


                        var investigationalInterventionsDTO = _mapper.Map<List<InvestigationalInterventionDTO>>(investigationalInterventionsEntity);
                        var studyIndicationsDTO = _mapper.Map<List<StudyIndicationDTO>>(studyIndications);
                        var studyDesigns = new List<GetStudyDesignsDTO>();
                        var studyDesign = new GetStudyDesignsDTO
                        {
                            InvestigationalInterventions = investigationalInterventionsDTO
                        };
                        studyDesigns.Add(studyDesign);
                        studiesDTO[i].ClinicalStudy.StudyDesigns = studyDesigns;
                        studiesDTO[i].ClinicalStudy.StudyIndications = studyIndicationsDTO;
                        studiesDTO[i].AuditTrail.EntryDateTime = Convert.ToDateTime(studiesDTO[i].AuditTrail.EntryDateTime).ToString(Constants.DateFormats.DateFormatForAuditResponse).ToUpper();
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


        /// <summary>
        /// Search Study Elements with search criteria
        /// </summary>
        /// <param name="searchParametersDTO">Parameters to search in database</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="List{SearchTitleDTO}"/> which matches serach criteria <br></br> <br></br>
        /// <see langword="null"/> If the insert is not done
        /// </returns>
        public async Task<List<SearchTitleDTO>> SearchTitle(SearchTitleParametersDTO searchParametersDTO, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyService)}; Method : {nameof(SearchTitle)};");
                _logger.LogInformation($"Search Parameters : {JsonConvert.SerializeObject(searchParametersDTO)}");

                if (user.UserRole == Constants.Roles.App_User && searchParametersDTO.GroupByStudyId)
                    return new List<SearchTitleDTO>();
                var searchParameters = _mapper.Map<SearchTitleParameters>(searchParametersDTO);

                var searchResponse = await _clinicalStudyRepository.SearchTitle(searchParameters, user);
                var searchTitleDTOList = _mapper.Map<List<SearchTitleDTO>>(searchResponse);

                if (searchParameters.GroupByStudyId)
                {
                    searchTitleDTOList = searchTitleDTOList.GroupBy(x => x.ClinicalStudy.StudyId)
                                                    .Select(g => new SearchTitleDTO
                                                    {
                                                        ClinicalStudy = g.Where(x => x.AuditTrail.StudyVersion == g.Max(x => x.AuditTrail.StudyVersion)).Select(x => x.ClinicalStudy).FirstOrDefault(),
                                                        AuditTrail = g.Where(x => x.AuditTrail.StudyVersion == g.Max(x => x.AuditTrail.StudyVersion)).Select(x => x.AuditTrail).FirstOrDefault()
                                                    }).ToList();
                }


                searchTitleDTOList = SortStudyTitle(searchTitleDTOList, searchParametersDTO)
                                           .Skip((searchParametersDTO.PageNumber - 1) * searchParametersDTO.PageSize)
                                           .Take(searchParametersDTO.PageSize)
                                           .ToList();

                searchTitleDTOList.ForEach(x => x.AuditTrail.EntryDateTime = Convert.ToDateTime(x.AuditTrail.EntryDateTime).ToString(Constants.DateFormats.DateFormatForAuditResponse).ToUpper());

                return searchTitleDTOList;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyService)}; Method : {nameof(SearchTitle)};");
            }
        }

        public static List<SearchTitleDTO> SortStudyTitle(List<SearchTitleDTO> searchTitleDTOs, SearchTitleParametersDTO searchParametersDTO)
        {
            if (!String.IsNullOrWhiteSpace(searchParametersDTO.SortBy))
            {
                return searchParametersDTO.SortBy.ToLower() switch
                {
                    "studytitle" => searchParametersDTO.SortOrder == SortOrder.asc.ToString() ? searchTitleDTOs.OrderBy(x => x.ClinicalStudy.StudyTitle).ToList() : searchTitleDTOs.OrderByDescending(x => x.ClinicalStudy.StudyTitle).ToList(),
                    "tag" => searchParametersDTO.SortOrder == SortOrder.asc.ToString() ? searchTitleDTOs.OrderBy(x => x.ClinicalStudy.StudyTag).ToList() : searchTitleDTOs.OrderByDescending(x => x.ClinicalStudy.StudyTag).ToList(),
                    "lastmodifieddate" => searchParametersDTO.SortOrder == SortOrder.asc.ToString() ? searchTitleDTOs.OrderBy(x => DateTime.Parse(x.AuditTrail.EntryDateTime)).ToList() : searchTitleDTOs.OrderByDescending(x => DateTime.Parse(x.AuditTrail.EntryDateTime)).ToList(),
                    "version" => searchParametersDTO.SortOrder == SortOrder.asc.ToString() ? searchTitleDTOs.OrderBy(x => x.AuditTrail.StudyVersion).ToList() : searchTitleDTOs.OrderByDescending(x => x.AuditTrail.StudyVersion).ToList(),
                    _ => searchParametersDTO.SortOrder == SortOrder.desc.ToString() ? searchTitleDTOs.OrderByDescending(x => x.ClinicalStudy.StudyTitle).ToList() : searchTitleDTOs.OrderBy(x => x.ClinicalStudy.StudyTitle).ToList(),
                };
            }
            else
            {
                return searchParametersDTO.SortOrder == SortOrder.desc.ToString() ? searchTitleDTOs.OrderByDescending(x => x.ClinicalStudy.StudyTitle).ToList() : searchTitleDTOs.OrderBy(x => x.ClinicalStudy.StudyTitle).ToList();
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

                if (user.UserRole != Constants.Roles.Org_Admin && Config.IsGroupFilterEnabled)
                {
                    var groups = await _clinicalStudyRepository.GetGroupsOfUser(user).ConfigureAwait(false);

                    if (groups != null && groups.Count > 0)
                    {
                        List<string> studyTypeFilterValues = new();
                        List<string> studyIdFilterValues = new();
                        studyTypeFilterValues.AddRange(groups.SelectMany(x => x.GroupFilter)
                                                             .Where(x => x.GroupFieldName == GroupFieldNames.studyType.ToString())
                                                             .SelectMany(x => x.GroupFilterValues)
                                                             .Select(x => x.GroupFilterValueId.ToLower())
                                                             .ToList());
                        studyIdFilterValues.AddRange(groups.SelectMany(x => x.GroupFilter)
                                                             .Where(x => x.GroupFieldName == GroupFieldNames.study.ToString())
                                                             .SelectMany(x => x.GroupFilterValues)
                                                             .Select(x => x.GroupFilterValueId)
                                                             .ToList());
                        if (studyIdFilterValues.Contains(study.ClinicalStudy.StudyId))
                            return study;
                        else if (studyTypeFilterValues.Contains(Constants.StudyType.ALL.ToLower()))
                            return study;
                        else if (studyTypeFilterValues.Contains(study.ClinicalStudy.StudyType.ToLower()))
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

                if (user.UserRole != Constants.Roles.Org_Admin && Config.IsGroupFilterEnabled)
                {
                    var groups = await _clinicalStudyRepository.GetGroupsOfUser(user).ConfigureAwait(false);

                    if (groups != null && groups.Count > 0)
                    {
                        List<string> studyTypeFilterValues = new();
                        List<string> studyIdFilterValues = new();
                        studyTypeFilterValues.AddRange(groups.SelectMany(x => x.GroupFilter)
                                                             .Where(x => x.GroupFieldName == GroupFieldNames.studyType.ToString())
                                                             .SelectMany(x => x.GroupFilterValues)
                                                             .Select(x => x.GroupFilterValueId.ToLower())
                                                             .ToList());
                        studyIdFilterValues.AddRange(groups.SelectMany(x => x.GroupFilter)
                                                             .Where(x => x.GroupFieldName == GroupFieldNames.study.ToString())
                                                             .SelectMany(x => x.GroupFilterValues)
                                                             .Select(x => x.GroupFilterValueId)
                                                             .ToList());
                        var studyListAfterFiltering = new List<StudyEntity>();
                        if (studyIdFilterValues.Contains(studyList[0].ClinicalStudy.StudyId))
                            return studyList;
                        else if (studyTypeFilterValues.Contains(Constants.StudyType.ALL.ToLower()))
                            return studyList;
                        else
                        {
                            studyList.RemoveAll(x => !studyTypeFilterValues.Contains(x.ClinicalStudy.StudyType.ToLower()));
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

                if (user.UserRole != Constants.Roles.Org_Admin && Config.IsGroupFilterEnabled)
                {
                    var groups = await _clinicalStudyRepository.GetGroupsOfUser(user).ConfigureAwait(false);

                    if (groups != null && groups.Count > 0)
                    {
                        if (groups.Any(x => x.Permission == Permissions.READ_WRITE.ToString()))
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
