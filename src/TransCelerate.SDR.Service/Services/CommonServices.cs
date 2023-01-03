using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Common;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.DataAccess.Repositories;
using TransCelerate.SDR.Services.Interfaces;

namespace TransCelerate.SDR.Services.Services
{
    public  class CommonServices:ICommonService
    {
        #region Variable
        private readonly ICommonRepository _commonRepository;
        private readonly ILogHelper _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public CommonServices(ICommonRepository commonRepository, ILogHelper logger, IMapper mapper)
        {
            _commonRepository = commonRepository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region GET Methods
        /// <summary>
        /// GET All Elements For a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="user">Logged in user</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<object> GetRawJson(string studyId, int sdruploadversion, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(CommonServices)}; Method : {nameof(GetRawJson)};");
                studyId = studyId.Trim();

                var study = await _commonRepository.GetStudyItemsAsync(studyId: studyId, sdruploadversion: sdruploadversion).ConfigureAwait(false);

                if (study == null)
                {
                    return null;
                }
                else
                {
                    var jsonObject = JObject.Parse(JsonConvert.SerializeObject(study.ClinicalStudy));                    

                    if (study.AuditTrail.UsdmVersion == Constants.USDMVersions.MVP)
                    {                                                                     
                        if (!await CheckAccessForAStudy(studyId, (string)jsonObject["studyType"], user))
                            return Constants.ErrorMessages.Forbidden;
                    }
                    else if (study.AuditTrail.UsdmVersion == Constants.USDMVersions.V1)
                    {
                        if (!await CheckAccessForAStudy(studyId, (string)jsonObject["studyType"]["decode"], user))
                            return Constants.ErrorMessages.Forbidden;
                    }
                    else if (study.AuditTrail.UsdmVersion == Constants.USDMVersions.V2)
                    {                        
                        if (!await CheckAccessForAStudy(studyId, (string)jsonObject["studyType"]["decode"], user))
                            return Constants.ErrorMessages.Forbidden;
                    }

                    return study;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(CommonServices)}; Method : {nameof(GetRawJson)};");
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
                _logger.LogInformation($"Started Service : {nameof(CommonServices)}; Method : {nameof(GetAuditTrail)};");
                List<AuditTrailResponseEntity> studies = await _commonRepository.GetAuditTrail(studyId, fromDate, toDate);
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
                    AuditTrailResponseDto getStudyAuditDto = new AuditTrailResponseDto
                    {
                        StudyId= studyId,
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
                _logger.LogInformation($"Ended Service : {nameof(CommonServices)}; Method : {nameof(GetAuditTrail)};");
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
                _logger.LogInformation($"Started Service : {nameof(CommonServices)}; Method : {nameof(GetStudyHistory)};");
                List<StudyHistoryResponseEntity> studies = await _commonRepository.GetStudyHistory(fromDate, toDate, studyTitle); //Getting List of studyId, studyTitle and Version
                if (studies == null)
                {
                    return null;
                }
                else
                {
                    studies = await CheckAccessForListOfStudies(studies, user);
                    if (studies == null)
                        return null;
                    var studyHistory = studies.GroupBy(x => new { x.StudyId })
                                              .Select(g => new 
                                              {
                                                  StudyId = g.Key.StudyId,
                                                  SDRUploadVersion = _mapper.Map<List<UploadVersionDto>>(g.ToList()),
                                                  Date = g.Max(x => x.EntryDateTime)
                                              }) // Grouping the Id's by studyId
                                              .OrderByDescending(x => x.Date)
                                              .Select(x=> new StudyHistoryResponseDto
                                              {
                                                  StudyId = x.StudyId,
                                                  SDRUploadVersion = x.SDRUploadVersion                                                
                                              })
                                              .ToList();

                    //List<StudyHistoryResponseDto> studyHistory = JsonConvert.DeserializeObject<List<StudyHistoryResponseDto>>(JsonConvert.SerializeObject(groupStudy));

                    return studyHistory;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(CommonServices)}; Method : {nameof(GetStudyHistory)};");
            }
        }
        #endregion

        #region SearchTitle
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
                var searchParameters = _mapper.Map<SearchTitleParametersEntity>(searchParametersDTO);

                var searchResponse = await _commonRepository.SearchTitle(searchParameters);
                searchResponse = await CheckAccessForListOfStudies(searchResponse, user);
                if (searchResponse == null)
                    return new List<SearchTitleResponseDto>();
                var searchTitleDTOList = _mapper.Map<List<SearchTitleResponseDto>>(searchResponse);

                if (searchParameters.GroupByStudyId)
                {
                    searchTitleDTOList = searchTitleDTOList.GroupBy(x => x.ClinicalStudy.StudyId)
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
                   //"sponsorid" => searchParametersDTO.SortOrder == SortOrder.asc.ToString() ? searchTitleDTOs.OrderBy(s => s.ClinicalStudy.StudyIdentifiers != null ? s.ClinicalStudy.StudyIdentifiers.FindAll(x => x.StudyIdentifierScope?.OrganisationType?.Decode == Constants.IdType.SPONSOR_ID_V1).Any() ? s.ClinicalStudy.StudyIdentifiers.Find(x => x.StudyIdentifierScope?.OrganisationType?.Decode == Constants.IdType.SPONSOR_ID_V1).StudyIdentifierScope.OrganisationIdentifier ?? "" : "" : "").ToList()
                   //                                                                     : searchTitleDTOs.OrderByDescending(s => s.ClinicalStudy.StudyIdentifiers != null ? s.ClinicalStudy.StudyIdentifiers.FindAll(x => x.StudyIdentifierScope?.OrganisationType?.Decode == Constants.IdType.SPONSOR_ID_V1).Any() ? s.ClinicalStudy.StudyIdentifiers.Find(x => x.StudyIdentifierScope?.OrganisationType?.Decode == Constants.IdType.SPONSOR_ID_V1).StudyIdentifierScope.OrganisationIdentifier ?? "" : "" : "").ToList(),
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

        #region UserGroupsMapping
        public async Task<bool> CheckAccessForAStudy(string studyId, string studyType, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(CommonServices)}; Method : {nameof(CheckAccessForAStudy)};");

                if (user.UserRole != Constants.Roles.Org_Admin && Config.isGroupFilterEnabled)
                {
                    var groups = await _commonRepository.GetGroupsOfUser(user).ConfigureAwait(false);

                    if (groups != null && groups.Count > 0)
                    {
                        Tuple<List<string>, List<string>> groupFilters = GroupFilters.GetGroupFilters(groups);
                        if (groupFilters.Item2.Contains(studyId))
                            return true;
                        else if (groupFilters.Item1.Contains(Constants.StudyType.ALL.ToLower()))
                            return true;
                        else if (groupFilters.Item1.Contains(studyType.ToLower()))
                            return true;
                        else
                            return false;
                    }
                    else
                    {
                        // Filter should not give any results
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
                _logger.LogInformation($"Ended Service : {nameof(CommonServices)}; Method : {nameof(CheckAccessForAStudy)};");
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
                _logger.LogInformation($"Started Service : {nameof(CommonServices)}; Method : {nameof(CheckAccessForStudyAudit)};");

                if (user.UserRole != Constants.Roles.Org_Admin && Config.isGroupFilterEnabled)
                {
                    var groups = await _commonRepository.GetGroupsOfUser(user).ConfigureAwait(false);

                    if (groups != null && groups.Count > 0)
                    {
                        Tuple<List<string>, List<string>> groupFilters = GroupFilters.GetGroupFilters(groups);
                        if (groupFilters.Item2.Contains(studyId))
                            return studies;
                        else if (groupFilters.Item1.Contains(Constants.StudyType.ALL.ToLower()))
                            return studies;
                        else
                        {
                            studies.ForEach(study =>
                            {
                                var jsonObject = JObject.Parse(JsonConvert.SerializeObject(study, new JsonSerializerSettings
                                {
                                    ContractResolver = new DefaultContractResolver()
                                    {
                                        NamingStrategy = new CamelCaseNamingStrategy()
                                    }                                  
                                }));
                                if (study.UsdmVersion == Constants.USDMVersions.MVP)
                                {
                                    if (!groupFilters.Item1.Contains((string)jsonObject["studyType"].ToString().ToLower()))
                                        study.HasAccess = false;
                                }
                                else if (study.UsdmVersion == Constants.USDMVersions.V1)
                                {
                                    if (!groupFilters.Item1.Contains((string)jsonObject["studyType"]["decode"].ToString().ToLower()))
                                        study.HasAccess = false;
                                }
                                else if (study.UsdmVersion == Constants.USDMVersions.V2)
                                {
                                    if (!groupFilters.Item1.Contains((string)jsonObject["studyType"]["decode"].ToString().ToLower()))
                                        study.HasAccess = false;
                                }
                            });
                            studies.RemoveAll(x => !x.HasAccess);

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
                _logger.LogInformation($"Ended Service : {nameof(CommonServices)}; Method : {nameof(CheckAccessForStudyAudit)};");
            }
        }

        /// <summary>
        /// Check access for the Study Aduit
        /// </summary>        
        /// <param name="studies">Study List for which user access have to be checked</param>   
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="List{AuditTrailResponseEntity}"/> if the user have access <br></br> <br></br>
        /// <see langword="null"/> If user doesn't have access to the study
        /// </returns>
        public async Task<List<T>> CheckAccessForListOfStudies<T>(List<T> studies, LoggedInUser user) where T : class, ICheckAccess
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(CommonServices)}; Method : {nameof(CheckAccessForStudyAudit)};");

                if (user.UserRole != Constants.Roles.Org_Admin && Config.isGroupFilterEnabled)
                {
                    var groups = await _commonRepository.GetGroupsOfUser(user).ConfigureAwait(false);

                    if (groups != null && groups.Count > 0)
                    {
                        studies.ForEach(x => x.HasAccess = true);
                        Tuple<List<string>, List<string>> groupFilters = GroupFilters.GetGroupFilters(groups);                        
                        if (groupFilters.Item1.Contains(Constants.StudyType.ALL.ToLower()))
                            return studies;
                        else
                        {
                            studies.ForEach(study =>
                            {
                                var jsonObject = JObject.Parse(JsonConvert.SerializeObject(study, new JsonSerializerSettings
                                {
                                    ContractResolver = new DefaultContractResolver()
                                    {
                                        NamingStrategy = new CamelCaseNamingStrategy()
                                    }
                                }));
                                if (groupFilters.Item2.Contains(study.StudyId))
                                    study.HasAccess = true;
                                else if (study.UsdmVersion == Constants.USDMVersions.MVP)
                                {
                                    if (!groupFilters.Item1.Contains((string)jsonObject["studyType"].ToString().ToLower()))
                                        study.HasAccess = false;
                                }
                                else if (study.UsdmVersion == Constants.USDMVersions.V1)
                                {
                                    if (!groupFilters.Item1.Contains((string)jsonObject["studyType"]["decode"].ToString().ToLower()))
                                        study.HasAccess = false;
                                }
                                else if (study.UsdmVersion == Constants.USDMVersions.V2)
                                {
                                    if (!groupFilters.Item1.Contains((string)jsonObject["studyType"]["decode"].ToString().ToLower()))
                                        study.HasAccess = false;
                                }
                            });
                            studies.RemoveAll(x => !x.HasAccess);

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
                _logger.LogInformation($"Ended Service : {nameof(CommonServices)}; Method : {nameof(CheckAccessForStudyAudit)};");
            }
        }
        #endregion
    }
}
