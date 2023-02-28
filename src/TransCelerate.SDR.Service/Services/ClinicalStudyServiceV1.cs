using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV1;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Interfaces;

namespace TransCelerate.SDR.Services.Services
{
    public class ClinicalStudyServiceV1 : IClinicalStudyServiceV1
    {
        #region Variables
        private readonly IClinicalStudyRepositoryV1 _clinicalStudyRepository;
        private readonly IMapper _mapper;
        private readonly ILogHelper _logger;
        private readonly IHelperV1 _helper;
        #endregion

        #region Constructor
        public ClinicalStudyServiceV1(IClinicalStudyRepositoryV1 clinicalStudyRepository, IMapper mapper, ILogHelper logger,IHelperV1 helper)
        {
            _clinicalStudyRepository = clinicalStudyRepository;
            _mapper = mapper;
            _logger = logger;
            _helper = helper;
        }
        #endregion

        #region GET Methods
        /// <summary>
        /// GET All Elements For a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<object> GetStudy(string studyId, int sdruploadversion, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(GetStudy)};");
                studyId = studyId.Trim();

                StudyEntity study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: studyId, sdruploadversion: sdruploadversion).ConfigureAwait(false);

                if (study == null)
                {
                    return null;
                }
                else
                {
                    StudyEntity checkStudy = await CheckAccessForAStudy(study, user);
                    if (checkStudy == null)
                        return Constants.ErrorMessages.Forbidden;
                    var studyDTO = _mapper.Map<StudyDto>(study);  //Mapping Entity to Dto
                    studyDTO.Links = LinksHelper.GetLinksForUi(study.ClinicalStudy.Uuid, study.ClinicalStudy.StudyDesigns?.Select(x => x.Uuid).ToList(), study.AuditTrail.UsdmVersion, study.AuditTrail.SDRUploadVersion);
                    return studyDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(GetStudy)};");
            }
        }

        /// <summary>
        /// GET Study Designs of a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<object> GetStudyDesigns(string studyId, int sdruploadversion, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(GetStudy)};");
                studyId = studyId.Trim();

                StudyEntity study = await _clinicalStudyRepository.GetPartialStudyDesignItemsAsync(studyId: studyId, sdruploadversion: sdruploadversion).ConfigureAwait(false);

                if (study == null)
                {
                    return null;
                }
                else
                {
                    StudyEntity checkStudy = await CheckAccessForAStudy(study, user);
                    if (checkStudy == null)
                        return Constants.ErrorMessages.Forbidden;

                    var studyDesigns = _mapper.Map<List<StudyDesignDto>>(checkStudy?.ClinicalStudy?.StudyDesigns);  //Mapping Entity to Dto

                    if (studyDesigns is not null && studyDesigns.Any())
                        return new StudyDesignsResposeDto 
                                   {
                                       StudyDesigns = studyDesigns, 
                                       Links = LinksHelper.GetLinks(study.ClinicalStudy.Uuid, study.ClinicalStudy.StudyDesigns?.Select(x => x.Uuid), study.AuditTrail.UsdmVersion, study.AuditTrail.SDRUploadVersion)
                                    };
                    
                    return Constants.ErrorMessages.StudyDesignNotFound;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(GetStudy)};");
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
        public async Task<object> GetAuditTrail(string studyId, DateTime fromDate, DateTime toDate, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyService)}; Method : {nameof(GetAuditTrail)};");
                List<AuditTrailResponseEntity> studies = await _clinicalStudyRepository.GetAuditTrail(studyId,fromDate, toDate);
                if (studies == null)
                {
                    return null;
                }
                else
                {
                    studies = await CheckAccessForStudyAudit(studyId, studies, user);
                    if (studies == null)
                        return Constants.ErrorMessages.Forbidden;
                    var auditTrailDtoList = _mapper.Map<List<AuditTrailDto>>(studies); //Mapping Entity to Dto 
                    AudiTrailResponseDto getStudyAuditDto = new AudiTrailResponseDto
                    {
                        Uuid = studyId,
                        AuditTrail = auditTrailDtoList
                    };

                    return getStudyAuditDto;
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
        /// A <see cref="List{StudyHistoryResponseEntity}"/> which has list of study ID's <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<StudyHistoryResponseDto>> GetStudyHistory(DateTime fromDate, DateTime toDate, string studyTitle, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(GetStudyHistory)};");
                List<StudyHistoryResponseEntity> studies = await _clinicalStudyRepository.GetStudyHistory(fromDate, toDate, studyTitle, user); //Getting List of studyId, studyTitle and Version
                if (studies == null)
                {
                    return null;
                }
                else
                {
                    var groupStudy = studies.GroupBy(x => new { x.Uuid })
                                            .Select(g => new //StudyHistoryResponseDto
                                            {
                                                StudyId = g.Key.Uuid,                                               
                                                SDRUploadVersion = _mapper.Map<List<UploadVersionDto>>(g.ToList()),
                                                Date = g.Max(x=>x.EntryDateTime)
                                            }) // Grouping the Id's by studyId
                                            .OrderByDescending(x => x.Date)
                                            .ToList();

                    List<StudyHistoryResponseDto> studyHistory = JsonConvert.DeserializeObject<List<StudyHistoryResponseDto>>(JsonConvert.SerializeObject(groupStudy));

                    return studyHistory;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(GetStudyHistory)};");
            }
        }
        #endregion

        #region POST Methods
        /// <summary>
        /// POST All Elements For a Study
        /// </summary>
        /// <param name="studyDTO">Study for Inserting/Updating in Database</param>        
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> which has study ID and study design ID's <br></br> <br></br>
        /// <see langword="null"/> If the insert is not done
        /// </returns>
        public async Task<object> PostAllElements(StudyDto studyDTO, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(PostAllElements)};");
                if (!await CheckPermissionForAUser(user))
                    return Constants.ErrorMessages.PostRestricted;
                StudyEntity incomingStudyEntity = new StudyEntity
                {
                    ClinicalStudy = _mapper.Map<ClinicalStudyEntity>(studyDTO.ClinicalStudy),
                    AuditTrail = _helper.GetAuditTrail(user?.UserName),
                    _id = MongoDB.Bson.ObjectId.GenerateNewId()
                };

                if(String.IsNullOrWhiteSpace(incomingStudyEntity.ClinicalStudy.Uuid)) // create new study
                {
                    studyDTO = await CreateNewStudy(incomingStudyEntity).ConfigureAwait(false);
                }
                else // create new version for study
                {
                    AuditTrailEntity existingAuditTrail = await _clinicalStudyRepository.GetUsdmVersionAsync(incomingStudyEntity.ClinicalStudy.Uuid, 0);

                    if (existingAuditTrail is null) // If PUT Endpoint and study_uuid is not valid, return not valid study
                    {
                        return Constants.ErrorMessages.NotValidStudyId;
                    }
                    if (existingAuditTrail.UsdmVersion == Constants.USDMVersions.V1) // If previus USDM version is same as incoming
                    {
                        StudyEntity existingStudyEntity = await _clinicalStudyRepository.GetStudyItemsAsync(incomingStudyEntity.ClinicalStudy.Uuid, 0);

                        if (_helper.IsSameStudy(incomingStudyEntity, existingStudyEntity))
                        {
                            studyDTO = await UpdateExistingStudy(incomingStudyEntity, existingStudyEntity).ConfigureAwait(false);
                        }
                        else
                        {
                            studyDTO = await CreateNewVersionForAStudy(incomingStudyEntity, existingStudyEntity).ConfigureAwait(false);
                        }
                    }
                    else if(existingAuditTrail.UsdmVersion == Constants.USDMVersions.MVP)// If previus USDM version is different from incoming
                    {
                        studyDTO = await CreateNewVersionForAStudyWithoutCheck(incomingStudyEntity, existingAuditTrail, incomingStudyEntity.ClinicalStudy.Uuid).ConfigureAwait(false);
                    }    
                    else
                    {
                        return Constants.ErrorMessages.DowngradeError;
                    }
                }                
                studyDTO.Links = LinksHelper.GetLinksForUi(studyDTO.ClinicalStudy.Uuid, studyDTO.ClinicalStudy.StudyDesigns?.Select(x => x.Uuid).ToList(), studyDTO.AuditTrail.UsdmVersion, studyDTO.AuditTrail.SDRUploadVersion);
                return studyDTO;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(PostAllElements)};");
            }
        }

        public async Task<StudyDto> CreateNewStudy(StudyEntity studyEntity)
        {
            studyEntity = _helper.GeneratedSectionId(studyEntity);
            studyEntity.ClinicalStudy.Uuid = IdGenerator.GenerateId();
            studyEntity.AuditTrail.SDRUploadVersion = 1;
            studyEntity.AuditTrail.UsdmVersion = Constants.USDMVersions.V1;
            await _clinicalStudyRepository.PostStudyItemsAsync(studyEntity);
            return _mapper.Map<StudyDto>(studyEntity);
        }
        
        public async Task<StudyDto> UpdateExistingStudy(StudyEntity incomingStudyEntity, StudyEntity existingStudyEntity)
        {
            existingStudyEntity.AuditTrail.EntryDateTime = incomingStudyEntity.AuditTrail.EntryDateTime;
            incomingStudyEntity.AuditTrail.SDRUploadVersion = existingStudyEntity.AuditTrail.SDRUploadVersion;
            existingStudyEntity.AuditTrail.UsdmVersion= Constants.USDMVersions.V1;
            await _clinicalStudyRepository.UpdateStudyItemsAsync(existingStudyEntity);
            return _mapper.Map<StudyDto>(existingStudyEntity);            
        }

        public async Task<StudyDto> CreateNewVersionForAStudy(StudyEntity incomingStudyEntity, StudyEntity existingStudyEntity)
        {
            incomingStudyEntity = _helper.CheckForSections(incomingStudyEntity, existingStudyEntity);
            incomingStudyEntity.AuditTrail.SDRUploadVersion = existingStudyEntity.AuditTrail.SDRUploadVersion + 1;
            incomingStudyEntity.AuditTrail.UsdmVersion= Constants.USDMVersions.V1;
            await _clinicalStudyRepository.PostStudyItemsAsync(incomingStudyEntity);            
            return _mapper.Map<StudyDto>(incomingStudyEntity);
        }
        public async Task<StudyDto> CreateNewVersionForAStudyWithoutCheck(StudyEntity incomingStudyEntity, AuditTrailEntity existingAuditTrailEntity,string studyId)
        {
            incomingStudyEntity = _helper.GeneratedSectionId(incomingStudyEntity);
            incomingStudyEntity.ClinicalStudy.Uuid = studyId;
            incomingStudyEntity.AuditTrail.SDRUploadVersion = existingAuditTrailEntity.SDRUploadVersion + 1;
            incomingStudyEntity.AuditTrail.UsdmVersion = Constants.USDMVersions.V1;
            await _clinicalStudyRepository.PostStudyItemsAsync(incomingStudyEntity);
            return _mapper.Map<StudyDto>(incomingStudyEntity);
        }
        #endregion

        #region Search
        /// <summary>
        /// Search Study Elements with search criteria
        /// </summary>
        /// <param name="searchParametersDto">Parameters to search in database</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="List{StudyDto}"/> which matches serach criteria <br></br> <br></br>
        /// <see langword="null"/> If the insert is not done
        /// </returns>
        public async Task<List<StudyDto>> SearchStudy(SearchParametersDto searchParametersDto, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyService)}; Method : {nameof(SearchStudy)};");
                _logger.LogInformation($"Search Parameters : {JsonConvert.SerializeObject(searchParametersDto)}");

                var searchParameters = _mapper.Map<SearchParameters>(searchParametersDto);

                List<SearchResponseEntity> studies = await _clinicalStudyRepository.SearchStudy(searchParameters, user);

                if(studies is null || !studies.Any())
                {
                    return null;
                }

                List<StudyDto> studyDtos = _mapper.Map<List<StudyDto>>(studies);

                studies.ForEach(study =>
                {                   
                    List<CodeDto> interventionModel = _mapper.Map<List<CodeDto>>(study.InterventionModel?.Where(x => x != null && x.Any()).SelectMany(x => x).ToList());
                    List<IndicationDto> studyIndication = _mapper.Map<List<IndicationDto>>(study.StudyIndications?.Where(x => x != null && x.Any()).SelectMany(x => x).ToList());

                    List<StudyDesignDto> studyDesigns = new()
                    {
                        new StudyDesignDto {StudyIndications = studyIndication, InterventionModel = interventionModel}
                    };

                    studyDtos[studies.IndexOf(study)].ClinicalStudy.StudyDesigns = studyDesigns;
                });                
                return studyDtos;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(SearchStudy)};");
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
        public async Task<List<SearchTitleResponseDto>> SearchTitle(SearchTitleParametersDto searchParametersDTO, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyService)}; Method : {nameof(SearchTitle)};");
                _logger.LogInformation($"Search Parameters : {JsonConvert.SerializeObject(searchParametersDTO)}");

                if (user.UserRole == Constants.Roles.App_User && searchParametersDTO.GroupByStudyId)
                    return new List<SearchTitleResponseDto>();
                var searchParameters = _mapper.Map<SearchTitleParameters>(searchParametersDTO);

                var searchResponse = await _clinicalStudyRepository.SearchTitle(searchParameters, user);
                var searchTitleDTOList = _mapper.Map<List<SearchTitleResponseDto>>(searchResponse);

                if (searchParameters.GroupByStudyId)
                {
                    searchTitleDTOList = searchTitleDTOList.GroupBy(x => x.ClinicalStudy.Uuid)
                                                    .Select(g => new SearchTitleResponseDto
                                                    {
                                                        ClinicalStudy = g.Where(x => x.AuditTrail.SDRUploadVersion == g.Max(x => x.AuditTrail.SDRUploadVersion)).Select(x => x.ClinicalStudy).FirstOrDefault(),
                                                        AuditTrail = g.Where(x => x.AuditTrail.SDRUploadVersion == g.Max(x => x.AuditTrail.SDRUploadVersion)).Select(x => x.AuditTrail).FirstOrDefault()
                                                    }).ToList();
                }


                searchTitleDTOList = SortStudyTitle(searchTitleDTOList, searchParametersDTO)
                                           .Skip((searchParametersDTO.PageNumber - 1) * searchParametersDTO.PageSize)
                                           .Take(searchParametersDTO.PageSize)
                                           .ToList();                

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
        public List<SearchTitleResponseDto> SortStudyTitle(List<SearchTitleResponseDto> searchTitleDTOs, SearchTitleParametersDto searchParametersDTO)
        {
            if (!String.IsNullOrWhiteSpace(searchParametersDTO.SortBy))
            {
                return searchParametersDTO.SortBy.ToLower() switch
                {
                    "studytitle" => searchParametersDTO.SortOrder == SortOrder.asc.ToString() ? searchTitleDTOs.OrderBy(x => x.ClinicalStudy.StudyTitle).ToList() : searchTitleDTOs.OrderByDescending(x => x.ClinicalStudy.StudyTitle).ToList(),
                    "sponsorid" => searchParametersDTO.SortOrder == SortOrder.asc.ToString() ? searchTitleDTOs.OrderBy(s => s.ClinicalStudy.StudyIdentifiers != null ? s.ClinicalStudy.StudyIdentifiers.FindAll(x => x.StudyIdentifierScope?.OrganisationType?.Decode == Constants.IdType.SPONSOR_ID_V1).Any() ? s.ClinicalStudy.StudyIdentifiers.Find(x => x.StudyIdentifierScope?.OrganisationType?.Decode == Constants.IdType.SPONSOR_ID_V1).StudyIdentifierScope.OrganisationIdentifier ?? "" : "" : "").ToList()
                                                                                        : searchTitleDTOs.OrderByDescending(s => s.ClinicalStudy.StudyIdentifiers != null ? s.ClinicalStudy.StudyIdentifiers.FindAll(x => x.StudyIdentifierScope?.OrganisationType?.Decode == Constants.IdType.SPONSOR_ID_V1).Any() ? s.ClinicalStudy.StudyIdentifiers.Find(x => x.StudyIdentifierScope?.OrganisationType?.Decode == Constants.IdType.SPONSOR_ID_V1).StudyIdentifierScope.OrganisationIdentifier ?? "" : "" : "").ToList(),
                    "lastmodifieddate" => searchParametersDTO.SortOrder == SortOrder.asc.ToString() ? searchTitleDTOs.OrderBy(x => x.AuditTrail.EntryDateTime).ToList() : searchTitleDTOs.OrderByDescending(x => x.AuditTrail.EntryDateTime).ToList(),
                    "version" => searchParametersDTO.SortOrder == SortOrder.asc.ToString() ? searchTitleDTOs.OrderBy(x => x.AuditTrail.SDRUploadVersion).ToList() : searchTitleDTOs.OrderByDescending(x => x.AuditTrail.SDRUploadVersion).ToList(),
                    _ => searchParametersDTO.SortOrder == SortOrder.desc.ToString() ? searchTitleDTOs.OrderByDescending(x => x.ClinicalStudy.StudyTitle).ToList() : searchTitleDTOs.OrderBy(x => x.ClinicalStudy.StudyTitle).ToList(),
                };
            }
            else
            {
                return searchParametersDTO.SortOrder == SortOrder.desc.ToString() ? searchTitleDTOs.OrderByDescending(x => x.ClinicalStudy.StudyTitle).ToList() : searchTitleDTOs.OrderBy(x => x.ClinicalStudy.StudyTitle).ToList();
            }
        }
        #endregion
        #region UserGroups
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
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(CheckAccessForAStudy)};");

                if (user.UserRole != Constants.Roles.Org_Admin && Config.isGroupFilterEnabled)
                {
                    var groups = await _clinicalStudyRepository.GetGroupsOfUser(user).ConfigureAwait(false);

                    if (groups != null && groups.Count > 0)
                    {
                        Tuple<List<string>, List<string>> groupFilters = GroupFilters.GetGroupFilters(groups);
                        if (groupFilters.Item2.Contains(study.ClinicalStudy.Uuid))
                            return study;
                        else if (groupFilters.Item1.Contains(Constants.StudyType.ALL.ToLower()))
                            return study;
                        else if (groupFilters.Item1.Contains(study.ClinicalStudy.StudyType?.Decode?.ToLower()))
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
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(CheckAccessForAStudy)};");
            }
        }

        /// <summary>
        /// Check access for the Study Aduit
        /// </summary>
        /// <param name="studyId">StudyId of the study</param>   
        /// <param name="studies">Study List for which user access have to be checked</param>   
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="List{AuditTrailResponseEntity}"/> if the user have access <br></br> <br></br>
        /// <see langword="null"/> If user doesn't have access to the study
        /// </returns>
        public async Task<List<AuditTrailResponseEntity>> CheckAccessForStudyAudit(string studyId, List<AuditTrailResponseEntity> studies, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(CheckAccessForStudyAudit)};");

                if (user.UserRole != Constants.Roles.Org_Admin && Config.isGroupFilterEnabled)
                {
                    var groups = await _clinicalStudyRepository.GetGroupsOfUser(user).ConfigureAwait(false);

                    if (groups != null && groups.Count > 0)
                    {
                        Tuple<List<string>, List<string>> groupFilters = GroupFilters.GetGroupFilters(groups);
                        if (groupFilters.Item2.Contains(studyId))
                            return studies;
                        else if (groupFilters.Item1.Contains(Constants.StudyType.ALL.ToLower()))
                            return studies;
                        else 
                        {
                            studies.RemoveAll(x => !groupFilters.Item1.Contains(x.StudyType?.Decode?.ToLower()));
                            return studies.Count > 0 ? studies : null;
                        }
                    }
                    else
                    {
                        // Filter should not give any results
                        return null;
                    }
                }
                else
                    return studies;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(CheckAccessForStudyAudit)};");
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
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(CheckPermissionForAUser)};");

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
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(CheckPermissionForAUser)};");
            }
        }
        #endregion

        #region Check Access For A Study
        public async Task<bool> GetAccessForAStudy(string studyId, int sdruploadversion, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(GetAccessForAStudy)};");
                studyId = studyId.Trim();

                StudyEntity study = study = await _clinicalStudyRepository.GetStudyItemsForCheckingAccessAsync(studyId: studyId, 0).ConfigureAwait(false);

                StudyEntity checkStudy = await CheckAccessForAStudy(study, user);
                if (checkStudy == null)
                    return false;

                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(GetAccessForAStudy)};");
            }
        }
        #endregion
    }
}
