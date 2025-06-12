using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Common;
using TransCelerate.SDR.Core.DTO.eCPT;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Interfaces;

namespace TransCelerate.SDR.Services.Services
{
    public class CommonServices : ICommonService
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
                    var jsonObject = JObject.Parse(JsonConvert.SerializeObject(study.Study));

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
                    else if (study.AuditTrail.UsdmVersion == Constants.USDMVersions.V1_9)
                    {
                        if (!await CheckAccessForAStudy(studyId, (string)jsonObject["studyType"]["decode"], user))
                            return Constants.ErrorMessages.Forbidden;
                    }
                    else if (study.AuditTrail.UsdmVersion == Constants.USDMVersions.V2)
                    {
                        if (!await CheckAccessForAStudy(studyId, (string)jsonObject["studyType"]["decode"], user))
                            return Constants.ErrorMessages.Forbidden;
                    }
                    else if (study.AuditTrail.UsdmVersion == Constants.USDMVersions.V3)
                    {
                        if (jsonObject["versions"] != null)
                        {
                            foreach(var version in jsonObject["versions"])
                            {
                                if (!await CheckAccessForAStudy(studyId, (string)version["studyType"]["decode"], user))
                                    return Constants.ErrorMessages.Forbidden;
                            }
                        }
                    }
					else if (study.AuditTrail.UsdmVersion == Constants.USDMVersions.V4)
					{
						if (jsonObject["versions"] != null)
						{
							foreach (var version in jsonObject["versions"])
							{
								if (!await CheckAccessForAStudy(studyId, (string)version["studyType"]["decode"], user))
									return Constants.ErrorMessages.Forbidden;
							}
						}
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

                    studies?.ForEach(x => x.StudyId =  studyId);
                    var auditTrailDtoList = _mapper.Map<List<AuditTrailResponseWithLinksDto>>(studies); //Mapping Entity to Dto 
                    AuditTrailResponseDto getStudyAuditDto = new()
                    {
                        StudyId = studyId,
                        RevisionHistory = auditTrailDtoList
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
                List<StudyHistoryResponseEntity> studies = await _commonRepository.GetStudyHistory(fromDate, toDate, studyTitle, user); //Getting List of studyId, studyTitle and Version
                if (studies == null)
                {
                    return null;
                }
                else
                {
                    var studyHistory = studies.GroupBy(x => new { x.StudyId })
                                              .Select(g => new
                                              {
                                                  g.Key.StudyId,
                                                  SDRUploadVersion = _mapper.Map<List<UploadVersionDto>>(g.ToList()),
                                                  Date = g.Max(x => x.EntryDateTime)
                                              }) // Grouping the Id's by studyId
                                              .OrderByDescending(x => x.Date)
                                              .Select(x => new StudyHistoryResponseDto
                                              {
                                                  StudyId = x.StudyId,
                                                  SDRUploadVersion = x.SDRUploadVersion
                                              })
                                              .ToList();                    

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

        /// <summary>
        /// GET Links
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="user">Logged in user</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<object> GetLinks(string studyId, int sdruploadversion, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(CommonServices)}; Method : {nameof(GetRawJson)};");
                studyId = studyId.Trim();

                var usdmVersion = await _commonRepository.GetUsdmVersion(studyId: studyId, sdruploadversion: sdruploadversion).ConfigureAwait(false);

                if (usdmVersion == null)
                {
                    return null;
                }
                else
                {
                    return new LinksResponseDto
                    {
                        StudyId = studyId,
                        UsdmVersion = usdmVersion,
                        SDRUploadVersion = sdruploadversion,
                        Links = LinksHelper.GetLinksForEndpoint(studyId, usdmVersion, sdruploadversion)
                    };
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
                _logger.LogInformation($"Started Service : {nameof(CommonServices)}; Method : {nameof(SearchTitle)};");
                _logger.LogInformation($"Search Parameters : {JsonConvert.SerializeObject(searchParametersDTO)}");

                if (user.UserRole == Constants.Roles.App_User && searchParametersDTO.GroupByStudyId)
                    return new List<SearchTitleResponseDto>();
                var searchParameters = _mapper.Map<SearchTitleParametersEntity>(searchParametersDTO);

                var searchResponse = await _commonRepository.SearchTitle(searchParameters, user);

                var searchTitleDTOList = _mapper.Map<List<SearchTitleResponseDto>>(searchResponse);

                if (searchParameters.GroupByStudyId)
                {
                    searchTitleDTOList = searchTitleDTOList.GroupBy(x => x.Study.StudyId)
                                                    .Select(g => new SearchTitleResponseDto
                                                    {
                                                        Study = g.Where(x => x.AuditTrail.SDRUploadVersion == g.Max(x => x.AuditTrail.SDRUploadVersion)).Select(x => x.Study).FirstOrDefault(),
                                                        AuditTrail = g.Where(x => x.AuditTrail.SDRUploadVersion == g.Max(x => x.AuditTrail.SDRUploadVersion)).Select(x => x.AuditTrail).FirstOrDefault()
                                                    }).ToList();
                }
                searchTitleDTOList = AssignStudyIdentifiers(searchTitleDTOList, searchResponse);

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
                _logger.LogInformation($"Ended Service : {nameof(CommonServices)}; Method : {nameof(SearchTitle)};");
            }
        }

        public List<SearchTitleResponseDto> AssignStudyIdentifiers(List<SearchTitleResponseDto> searchTitleDTOs, List<SearchTitleResponseEntity> searchTitleResponses)
        {
            searchTitleDTOs.ForEach(searchTitleDTO =>
            {
                var searchResponse = searchTitleResponses.Where(x => x.StudyId == searchTitleDTO.Study.StudyId && x.SDRUploadVersion == searchTitleDTO.AuditTrail.SDRUploadVersion).FirstOrDefault();

                if (searchResponse.UsdmVersion == Constants.USDMVersions.V3)
                {
                    var studyTitleV4 = searchResponse.StudyTitle != null ? JsonConvert.DeserializeObject<List<CommonStudyTitle>>(JsonConvert.SerializeObject(searchResponse.StudyTitle)) : null;
                    searchTitleDTO.Study.StudyTitle = studyTitleV4.GetStudyTitle(Constants.StudyTitle.OfficialStudyTitle);
                }

                if (searchResponse.StudyIdentifiers != null)
                {
                    if ((searchResponse.UsdmVersion == Constants.USDMVersions.V3) || (searchResponse.UsdmVersion == Constants.USDMVersions.V4))
                    {
                        searchTitleDTO.Study.StudyIdentifiers = _mapper.Map<List<CommonStudyIdentifiersDto>>(JsonConvert.DeserializeObject<List<Core.DTO.StudyV4.StudyIdentifierDto>>(JsonConvert.SerializeObject(searchResponse.StudyIdentifiers)));
                    }
                    else
                    {
                        searchTitleDTO.Study.StudyIdentifiers = JsonConvert.DeserializeObject<List<CommonStudyIdentifiersDto>>(JsonConvert.SerializeObject(searchResponse.StudyIdentifiers));
                    }
                }

                var studyDesignIds = searchResponse.UsdmVersion == Constants.USDMVersions.MVP ? searchResponse.StudyDesignIdsMVP?.Where(x => x != null && x.Any()).SelectMany(x => x)?.ToList() : searchResponse.UsdmVersion == Constants.USDMVersions.V3 ? searchResponse.StudyDesignIdsV4?.Where(x => x != null && x.Any()).SelectMany(x => x)?.ToList() : searchResponse.StudyDesignIds?.ToList();
                searchTitleDTO.Links = LinksHelper.GetLinksForUi(searchResponse.StudyId, studyDesignIds, searchResponse.UsdmVersion, searchResponse.SDRUploadVersion);
            });

            return searchTitleDTOs;
        }

        public static List<SearchTitleResponseDto> SortStudyTitle(List<SearchTitleResponseDto> searchTitleDTOs, SearchTitleParametersDto searchParametersDTO)
        {
            if (!String.IsNullOrWhiteSpace(searchParametersDTO.SortBy))
            {
                return searchParametersDTO.SortBy.ToLower() switch
                {                    
                    "sponsorid" => searchParametersDTO.SortOrder == SortOrder.asc.ToString() ? searchTitleDTOs.OrderBy(s => s.Study.StudyIdentifiers != null ? s.Study.StudyIdentifiers.FindAll(x => x.StudyIdentifierScope?.OrganisationType?.Decode?.ToLower() == Constants.IdType.SPONSOR_ID_V1?.ToLower() || x.StudyIdentifierScope?.OrganisationType?.Decode?.ToLower() == Constants.IdType.SPONSOR_ID?.ToLower()).Any() ? s.Study.StudyIdentifiers.Find(x => x.StudyIdentifierScope?.OrganisationType?.Decode?.ToLower() == Constants.IdType.SPONSOR_ID_V1?.ToLower() || x.StudyIdentifierScope?.OrganisationType?.Decode?.ToLower() == Constants.IdType.SPONSOR_ID?.ToLower()).StudyIdentifierScope.OrganisationIdentifier ?? "" : "" : "").ToList()
                                                                                         : searchTitleDTOs.OrderByDescending(s => s.Study.StudyIdentifiers != null ? s.Study.StudyIdentifiers.FindAll(x => x.StudyIdentifierScope?.OrganisationType?.Decode?.ToLower() == Constants.IdType.SPONSOR_ID_V1?.ToLower() || x.StudyIdentifierScope?.OrganisationType?.Decode?.ToLower() == Constants.IdType.SPONSOR_ID?.ToLower()).Any() ? s.Study.StudyIdentifiers.Find(x => x.StudyIdentifierScope?.OrganisationType?.Decode?.ToLower() == Constants.IdType.SPONSOR_ID_V1?.ToLower() || x.StudyIdentifierScope?.OrganisationType?.Decode?.ToLower() == Constants.IdType.SPONSOR_ID?.ToLower()).StudyIdentifierScope.OrganisationIdentifier ?? "" : "" : "").ToList(),
                    "studytitle" => searchParametersDTO.SortOrder == SortOrder.asc.ToString() ? searchTitleDTOs.OrderBy(s => s.Study.StudyTitle).ToList() : searchTitleDTOs.OrderByDescending(s => s.Study.StudyTitle).ToList(),
                    "version" => searchParametersDTO.SortOrder == SortOrder.asc.ToString() ? searchTitleDTOs.OrderBy(s => s.AuditTrail.SDRUploadVersion).ToList() : searchTitleDTOs.OrderByDescending(s => s.AuditTrail.SDRUploadVersion).ToList(),
                    "lastmodifieddate" => searchParametersDTO.SortOrder == SortOrder.asc.ToString() ? searchTitleDTOs.OrderBy(s => s.AuditTrail.EntryDateTime).ToList() : searchTitleDTOs.OrderByDescending(s => s.AuditTrail.EntryDateTime).ToList(),
                    "usdmversion" => searchParametersDTO.SortOrder == SortOrder.asc.ToString() ? searchTitleDTOs.OrderBy(s => s.AuditTrail.UsdmVersion).ToList() : searchTitleDTOs.OrderByDescending(s => s.AuditTrail.UsdmVersion).ToList(),
                    _ => searchParametersDTO.SortOrder == SortOrder.desc.ToString() ? searchTitleDTOs.OrderByDescending(x => x.Study.StudyTitle).ToList() : searchTitleDTOs.OrderBy(x => x.Study.StudyTitle).ToList(),
                };
            }
            else
            {
                return searchParametersDTO.SortOrder == SortOrder.desc.ToString() ? searchTitleDTOs.OrderByDescending(x => x.Study.StudyTitle).ToList() : searchTitleDTOs.OrderBy(x => x.Study.StudyTitle).ToList();
            }
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
        public async Task<object> SearchStudy(SearchParametersDto searchParametersDto, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(CommonServices)}; Method : {nameof(SearchStudy)};");                

                var searchParameters = _mapper.Map<SearchParametersEntity>(searchParametersDto);

                if (searchParameters.ValidateUsdmVersion)
                {
                    return await GetSearchResultsWithUsdmVersionFilter(searchParameters, user);
                }
                List<SearchResponseEntity> searchResponseEntities = await _commonRepository.SearchStudy(searchParameters, user);

                if (searchResponseEntities is null || !searchResponseEntities.Any())
                    return null;

                searchResponseEntities.ForEach(x =>
                {
                    if (x.UsdmVersion == Constants.USDMVersions.V3 || x.UsdmVersion == Constants.USDMVersions.V4)
                        x.StudyId = x.StudyIdV4;
                });
                var searchResponseDtos = _mapper.Map<List<SearchResponseDto>>(searchResponseEntities);
                searchResponseDtos = AssignDynamicValues(searchResponseDtos, searchResponseEntities);
                if (searchParameters.Header?.ToLower() == "phase" || searchParameters.Header?.ToLower() == "sponsorid" || searchParameters.Header?.ToLower() == "interventionmodel" || searchParameters.Header?.ToLower() == "indication")
                {
                    searchResponseDtos = SortSearchResults(searchResponseDtos, searchParametersDto)
                                         .Skip((searchParameters.PageNumber - 1) * searchParameters.PageSize)
                                         .Take(searchParameters.PageSize)
                                         .ToList();
                }
                return searchResponseDtos;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(CommonServices)}; Method : {nameof(SearchStudy)};");
            }
        }

        public List<SearchResponseDto> AssignDynamicValues(List<SearchResponseDto> searchResponseDtos, List<SearchResponseEntity> searchResponseEntities)
        {
            searchResponseDtos.ForEach(searchResponseDto =>
            {
                var searchResponseEntity = searchResponseEntities.Where(x => x.StudyId == searchResponseDto.Study.StudyId && x.SDRUploadVersion == searchResponseDto.AuditTrail.SDRUploadVersion).FirstOrDefault();

                #region StudyId
                if (searchResponseEntity.UsdmVersion == Constants.USDMVersions.V3 || searchResponseEntity.UsdmVersion == Constants.USDMVersions.V4)
                {
                    searchResponseDto.Study.StudyId = searchResponseEntity.StudyIdV4;
                }
                #endregion
                #region StudyTitle
                if (searchResponseEntity.UsdmVersion == Constants.USDMVersions.V3 || searchResponseEntity.UsdmVersion == Constants.USDMVersions.V4)
                {
                    var studyTitleV4 = searchResponseEntity.StudyTitleV4 != null ? JsonConvert.DeserializeObject<List<CommonStudyTitle>>(JsonConvert.SerializeObject(searchResponseEntity.StudyTitleV4)) : null;
                    searchResponseDto.Study.StudyTitle = studyTitleV4.GetStudyTitle(Constants.StudyTitle.OfficialStudyTitle);
                }
                #endregion
                #region StudyIdentifiers
                if (searchResponseEntity.StudyIdentifiers != null || searchResponseEntity.StudyIdentifiersV4 != null)
                {
                    if (searchResponseDto.AuditTrail.UsdmVersion == Constants.USDMVersions.V3)
                    {
                        searchResponseDto.Study.StudyIdentifiers = _mapper.Map<List<CommonStudyIdentifiersDto>>(JsonConvert.DeserializeObject<List<Core.DTO.StudyV4.StudyIdentifierDto>>(JsonConvert.SerializeObject(searchResponseEntity.StudyIdentifiersV4)));
                    }
                    else if(searchResponseDto.AuditTrail.UsdmVersion == Constants.USDMVersions.V4)
                    {
						searchResponseDto.Study.StudyIdentifiers = _mapper.Map<List<CommonStudyIdentifiersDto>>(JsonConvert.DeserializeObject<List<Core.DTO.StudyV5.StudyIdentifierDto>>(JsonConvert.SerializeObject(searchResponseEntity.StudyIdentifiersV4)));
					}
                    else
                    {
                        searchResponseDto.Study.StudyIdentifiers = JsonConvert.DeserializeObject<List<CommonStudyIdentifiersDto>>(JsonConvert.SerializeObject(searchResponseEntity.StudyIdentifiers));
                    }
                }
                #endregion
                var jsonObject = JObject.Parse(JsonConvert.SerializeObject(searchResponseEntity, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    }
                }));
                #region Study Type
                if (searchResponseEntity.StudyType != null || searchResponseEntity.StudyTypeV4 != null)
                {
                    if (searchResponseDto.AuditTrail.UsdmVersion == Constants.USDMVersions.V3)
                    {
                        searchResponseDto.Study.StudyType = JsonConvert.DeserializeObject<CommonCodeDto>(JsonConvert.SerializeObject(searchResponseEntity.StudyTypeV4));
                    }
                    else if (searchResponseDto.AuditTrail.UsdmVersion == Constants.USDMVersions.V4)
					{
						searchResponseDto.Study.StudyType = JsonConvert.DeserializeObject<CommonCodeDto>(JsonConvert.SerializeObject(searchResponseEntity.StudyTypeV4));
					}
					else
                    {
                        searchResponseDto.Study.StudyType = JsonConvert.DeserializeObject<CommonCodeDto>(JsonConvert.SerializeObject(searchResponseEntity.StudyType));
                    }
                }
                if (searchResponseEntity.StudyPhase != null || searchResponseEntity.StudyPhaseV4 != null)
                {                    
                    if (searchResponseDto.AuditTrail.UsdmVersion == Constants.USDMVersions.V3)
                        searchResponseDto.Study.StudyPhase = JsonConvert.DeserializeObject<CommonCodeDto>(JsonConvert.SerializeObject(jsonObject["studyPhaseV4"]["standardCode"]));
					else if (searchResponseDto.AuditTrail.UsdmVersion == Constants.USDMVersions.V4)
						searchResponseDto.Study.StudyPhase = JsonConvert.DeserializeObject<CommonCodeDto>(JsonConvert.SerializeObject(jsonObject["studyPhaseV4"]["standardCode"]));
					else
                        searchResponseDto.Study.StudyPhase = JsonConvert.DeserializeObject<CommonCodeDto>(JsonConvert.SerializeObject(jsonObject["studyPhase"]["standardCode"]));
                }
                #endregion
                searchResponseDto.Study.StudyDesigns = new List<CommonStudyDesign>();
                var studyDesign = new CommonStudyDesign();
                #region Indications                
                if ((searchResponseDto.AuditTrail.UsdmVersion != Constants.USDMVersions.V3 || searchResponseDto.AuditTrail.UsdmVersion != Constants.USDMVersions.V4) && searchResponseEntity.StudyIndications != null)
                {
                    studyDesign.StudyIndications = new List<Core.DTO.Common.CommonStudyIndication>();
                    var listOfIndicationDescriptions = searchResponseEntity.StudyIndications.Where(x => x != null && x.Any()).SelectMany(x => x).ToList();
                    listOfIndicationDescriptions.ForEach(ind => studyDesign.StudyIndications.Add(new Core.DTO.Common.CommonStudyIndication { IndicationDescription = ind }));
                }
                else if((searchResponseDto.AuditTrail.UsdmVersion == Constants.USDMVersions.V3|| searchResponseDto.AuditTrail.UsdmVersion != Constants.USDMVersions.V4) && searchResponseEntity.StudyIndicationsV4 != null)
                {
                    studyDesign.StudyIndications = new List<Core.DTO.Common.CommonStudyIndication>();
                    var listOfIndicationDescriptions = searchResponseEntity.StudyIndicationsV4.Where(x => x != null && x.Any()).SelectMany(x => x).ToList();
                    listOfIndicationDescriptions.ForEach(ind => ind.ToList().ForEach(ind2 => studyDesign.StudyIndications.Add(new Core.DTO.Common.CommonStudyIndication { IndicationDescription = ind2.Description })));
                }
                #endregion

                #region InterventionModel
                if (searchResponseDto.AuditTrail.UsdmVersion != Constants.USDMVersions.V3 && searchResponseEntity.InterventionModel != null)
                {
                    studyDesign.InterventionModel = new List<CommonCodeDto>();
                    var listOfInterventionModels = searchResponseEntity.InterventionModel.ToList();
                    if (searchResponseDto.AuditTrail.UsdmVersion == Constants.USDMVersions.V1)
                        listOfInterventionModels.ForEach(ind => studyDesign.InterventionModel.AddRange(JsonConvert.DeserializeObject<List<CommonCodeDto>>(JsonConvert.SerializeObject(ind))));
                    else
                        listOfInterventionModels.ForEach(ind => studyDesign.InterventionModel.Add(JsonConvert.DeserializeObject<CommonCodeDto>(JsonConvert.SerializeObject(ind))));
                }
                else if ((searchResponseDto.AuditTrail.UsdmVersion == Constants.USDMVersions.V3 || searchResponseDto.AuditTrail.UsdmVersion == Constants.USDMVersions.V4) && searchResponseEntity.StudyIndicationsV4 != null)
                {
                    studyDesign.InterventionModel = new List<CommonCodeDto>();
                    var listOfInterventionModels = searchResponseEntity.InterventionModelV4.ToList();
                    listOfInterventionModels.ForEach(ind => studyDesign.InterventionModel.AddRange(JsonConvert.DeserializeObject<List<CommonCodeDto>>(JsonConvert.SerializeObject(ind))));
                }
                #endregion

                searchResponseDto.Study.StudyDesigns.Add(studyDesign);
                var studyDesignIds = (searchResponseEntity.UsdmVersion == Constants.USDMVersions.V3) || (searchResponseEntity.UsdmVersion == Constants.USDMVersions.V4) ? searchResponseEntity.StudyDesignIdsV4?.Where(x => x != null && x.Any()).SelectMany(x => x).ToList() : searchResponseEntity.StudyDesignIds?.ToList();
                searchResponseDto.Links = LinksHelper.GetLinksForUi(searchResponseDto.Study.StudyId, studyDesignIds, searchResponseDto.AuditTrail.UsdmVersion, searchResponseDto.AuditTrail.SDRUploadVersion);
            });
            return searchResponseDtos;
        }

        public static List<SearchResponseDto> SortSearchResults(List<SearchResponseDto> searchResponseDtos, SearchParametersDto searchParameters)
        {
            if (!String.IsNullOrWhiteSpace(searchParameters.Header))
            {
                return searchParameters.Header.ToLower() switch
                {                    
                    "phase" => searchParameters.Asc ? searchResponseDtos.OrderBy(s => s.Study.StudyPhase != null ? s.Study.StudyPhase.Decode ?? "" : "").ToList() : searchResponseDtos.OrderByDescending(s => s.Study.StudyPhase != null ? s.Study.StudyPhase.Decode ?? "" : "").ToList(),
                    "sponsorid" => searchParameters.Asc ? searchResponseDtos.OrderBy(s => s.Study.StudyIdentifiers != null ? s.Study.StudyIdentifiers.FindAll(x => x.StudyIdentifierScope?.OrganisationType?.Decode.ToLower() == Constants.IdType.SPONSOR_ID_V1.ToLower() || x.StudyIdentifierScope?.OrganisationType?.Decode.ToLower() == Constants.IdType.SPONSOR_ID.ToLower()).Any() ? s.Study.StudyIdentifiers.Find(x => x.StudyIdentifierScope?.OrganisationType?.Decode.ToLower() == Constants.IdType.SPONSOR_ID_V1.ToLower() || x.StudyIdentifierScope?.OrganisationType?.Decode.ToLower() == Constants.IdType.SPONSOR_ID.ToLower()).StudyIdentifierScope.OrganisationIdentifier ?? "" : "" : "").ToList()
                                                            : searchResponseDtos.OrderByDescending(s => s.Study.StudyIdentifiers != null ? s.Study.StudyIdentifiers.FindAll(x => x.StudyIdentifierScope?.OrganisationType?.Decode.ToLower() == Constants.IdType.SPONSOR_ID_V1.ToLower() || x.StudyIdentifierScope?.OrganisationType?.Decode.ToLower() == Constants.IdType.SPONSOR_ID.ToLower()).Any() ? s.Study.StudyIdentifiers.Find(x => x.StudyIdentifierScope?.OrganisationType?.Decode.ToLower() == Constants.IdType.SPONSOR_ID_V1.ToLower() || x.StudyIdentifierScope?.OrganisationType?.Decode.ToLower() == Constants.IdType.SPONSOR_ID.ToLower()).StudyIdentifierScope.OrganisationIdentifier ?? "" : "" : "").ToList(),                    

                    "interventionmodel" => searchParameters.Asc ? searchResponseDtos.OrderBy(x => x.Study.StudyDesigns != null && x.Study.StudyDesigns.Any() ? x.Study.StudyDesigns.First().InterventionModel != null && x.Study.StudyDesigns.First().InterventionModel.Any() ? x.Study.StudyDesigns.First().InterventionModel.First().Decode : "" : "").ToList()
                                                                : searchResponseDtos.OrderByDescending(x => x.Study.StudyDesigns != null && x.Study.StudyDesigns.Any() ? x.Study.StudyDesigns.First().InterventionModel != null && x.Study.StudyDesigns.First().InterventionModel.Any() ? x.Study.StudyDesigns.First().InterventionModel.First().Decode : "" : "").ToList(),

                    "indication" => searchParameters.Asc ? searchResponseDtos.OrderBy(x => x.Study.StudyDesigns != null && x.Study.StudyDesigns.Any() ? x.Study.StudyDesigns.First().StudyIndications != null && x.Study.StudyDesigns.First().StudyIndications.Any() ? x.Study.StudyDesigns.First().StudyIndications.First().IndicationDescription : "" : "").ToList()
                                                                    : searchResponseDtos.OrderByDescending(x => x.Study.StudyDesigns != null && x.Study.StudyDesigns.Any() ? x.Study.StudyDesigns.First().StudyIndications != null && x.Study.StudyDesigns.First().StudyIndications.Any() ? x.Study.StudyDesigns.First().StudyIndications.First().IndicationDescription : "" : "").ToList(),
                    _ => searchResponseDtos
                };
            }
            else
            {                
                return searchResponseDtos;
            }
        }

        public async Task<object> GetSearchResultsWithUsdmVersionFilter(SearchParametersEntity searchParameters, LoggedInUser loggedInUser)
        {                        
            if (searchParameters.UsdmVersion == Constants.USDMVersions.V1_9)
            {
                var searchResponse = await _commonRepository.SearchStudyV2(searchParameters, loggedInUser);
                var searchResponseDtos = _mapper.Map<List<SearchResponseDto>>(searchResponse);

                if (searchResponseDtos.Any())
                {
                    searchResponseDtos.ForEach(searchResponseDto =>
                    {
                        var searchResponseV2 = searchResponse.FirstOrDefault(x => x.StudyId == searchResponseDto.Study.StudyId && x.SDRUploadVersion == searchResponseDto.AuditTrail.SDRUploadVersion);

                        searchResponseDto.Study.StudyIdentifiers = _mapper.Map<List<CommonStudyIdentifiersDto>>(searchResponseV2.StudyIdentifiers);
                        searchResponseDto.Study.StudyPhase = _mapper.Map<CommonCodeDto>(searchResponseV2.StudyPhase?.StandardCode);
                        searchResponseDto.Study.StudyDesigns = new List<CommonStudyDesign> { new() {
                            InterventionModel = _mapper.Map<List<CommonCodeDto>>(searchResponseV2.InterventionModel?.ToList()),
                            StudyIndications = _mapper.Map<List<Core.DTO.Common.CommonStudyIndication>>(searchResponseV2.StudyIndications?.Where(x => x != null && x.Any()).SelectMany(x=>x).ToList())
                        } };
                        searchResponseDto.Links = LinksHelper.GetLinksForUi(searchResponseDto.Study.StudyId, searchResponseV2.StudyDesignIds?.ToList(), searchResponseDto.AuditTrail.UsdmVersion, searchResponseDto.AuditTrail.SDRUploadVersion);
                    });
                    return searchResponseDtos;
                }

                return null;
            }
            if (searchParameters.UsdmVersion == Constants.USDMVersions.V2)
            {
                var searchResponse = await _commonRepository.SearchStudyV3(searchParameters, loggedInUser);
                var searchResponseDtos = _mapper.Map<List<SearchResponseDto>>(searchResponse);

                if (searchResponseDtos.Any())
                {
                    searchResponseDtos.ForEach(searchResponseDto =>
                    {
                        var searchResponseV2 = searchResponse.FirstOrDefault(x => x.StudyId == searchResponseDto.Study.StudyId && x.SDRUploadVersion == searchResponseDto.AuditTrail.SDRUploadVersion);

                        searchResponseDto.Study.StudyIdentifiers = _mapper.Map<List<CommonStudyIdentifiersDto>>(searchResponseV2.StudyIdentifiers);
                        searchResponseDto.Study.StudyPhase = _mapper.Map<CommonCodeDto>(searchResponseV2.StudyPhase?.StandardCode);
                        searchResponseDto.Study.StudyDesigns = new List<CommonStudyDesign> { new() {
                            InterventionModel = _mapper.Map<List<CommonCodeDto>>(searchResponseV2.InterventionModel?.ToList()),
                            StudyIndications = _mapper.Map<List<Core.DTO.Common.CommonStudyIndication>>(searchResponseV2.StudyIndications?.Where(x => x != null && x.Any()).SelectMany(x=>x).ToList())
                        } };
                        searchResponseDto.Links = LinksHelper.GetLinksForUi(searchResponseDto.Study.StudyId, searchResponseV2.StudyDesignIds?.ToList(), searchResponseDto.AuditTrail.UsdmVersion, searchResponseDto.AuditTrail.SDRUploadVersion);
                    });
                    return searchResponseDtos;
                }

                return null;
            }
            if (searchParameters.UsdmVersion == Constants.USDMVersions.V3)
            {
				var searchResponse = await _commonRepository.SearchStudyV4(searchParameters, loggedInUser);
				var searchResponseDtos = _mapper.Map<List<SearchResponseDto>>(searchResponse);

				if (searchResponseDtos.Any())
                {
                    searchResponseDtos.ForEach(searchResponseDto =>
                    {
                        var searchResponseV2 = searchResponse.FirstOrDefault(x => x.StudyId == searchResponseDto.Study.StudyId && x.SDRUploadVersion == searchResponseDto.AuditTrail.SDRUploadVersion);
                        var studyTitleV4 = searchResponseV2.StudyTitle != null ? JsonConvert.DeserializeObject<List<CommonStudyTitle>>(JsonConvert.SerializeObject(searchResponseV2.StudyTitle)) : null;
                        searchResponseDto.Study.StudyTitle = studyTitleV4 != null && studyTitleV4.Any(x => x.Type?.Decode == Constants.StudyTitle.OfficialStudyTitle) ? studyTitleV4.Find(x => x.Type?.Decode == Constants.StudyTitle.OfficialStudyTitle).Text : null;
                        searchResponseDto.Study.StudyIdentifiers = _mapper.Map<List<CommonStudyIdentifiersDto>>(searchResponseV2.StudyIdentifiers);
                        searchResponseDto.Study.StudyIdentifiers?.ForEach(x =>
                        {
                            var scope = searchResponseV2.StudyIdentifiers.Find(y => y.Id == x.Id).StudyIdentifierScope;
                            x.StudyIdentifierScope.OrganisationIdentifierScheme = scope.IdentifierScheme;
                            x.StudyIdentifierScope.OrganisationIdentifier = scope.Identifier;
                            x.StudyIdentifierScope.OrganisationName = scope.Name;
                            x.StudyIdentifierScope.OrganisationType = _mapper.Map<CommonCodeDto>(scope.OrganizationType);
                        });
                        searchResponseDto.Study.StudyPhase = _mapper.Map<CommonCodeDto>(searchResponseV2.StudyPhase?.StandardCode);
                        searchResponseDto.Study.StudyDesigns = new List<CommonStudyDesign> { new() {
                            InterventionModel = _mapper.Map<List<CommonCodeDto>>(searchResponseV2.InterventionModel?.Where(x => x != null && x.Any()).SelectMany(x=>x).ToList()),
                            StudyIndications = _mapper.Map<List<Core.DTO.Common.CommonStudyIndication>>(searchResponseV2.StudyIndications?.Where(x => x != null && x.Any()).SelectMany(x=>x).Where(x => x != null && x.Any()).SelectMany(x=>x).ToList())
                        } };
                        searchResponseDto.Links = LinksHelper.GetLinksForUi(searchResponseDto.Study.StudyId, searchResponseV2.StudyDesignIds?.SelectMany(x=>x).ToList(), searchResponseDto.AuditTrail.UsdmVersion, searchResponseDto.AuditTrail.SDRUploadVersion);
                    });
                    return searchResponseDtos;
                }

                return null;
            }
            if (searchParameters.UsdmVersion == Constants.USDMVersions.V4)
            {
                var searchResponse = await _commonRepository.SearchStudyV5(searchParameters, loggedInUser);
                var searchResponseDtos = _mapper.Map<List<SearchResponseDto>>(searchResponse);

                if (searchResponseDtos.Any())
                {
                    searchResponseDtos.ForEach(searchResponseDto =>
                    {
                        var searchResponseV2 = searchResponse.FirstOrDefault(x => x.StudyId == searchResponseDto.Study.StudyId && x.SDRUploadVersion == searchResponseDto.AuditTrail.SDRUploadVersion);
                        var studyTitleV4 = searchResponseV2.StudyTitle != null ? JsonConvert.DeserializeObject<List<CommonStudyTitle>>(JsonConvert.SerializeObject(searchResponseV2.StudyTitle)) : null;
                        searchResponseDto.Study.StudyTitle = studyTitleV4 != null && studyTitleV4.Any(x => x.Type?.Decode == Constants.StudyTitle.OfficialStudyTitle) ? studyTitleV4.Find(x => x.Type?.Decode == Constants.StudyTitle.OfficialStudyTitle).Text : null;
                        searchResponseDto.Study.StudyIdentifiers = _mapper.Map<List<CommonStudyIdentifiersDto>>(searchResponseV2.StudyIdentifiers);
                        searchResponseDto.Study.StudyIdentifiers?.ForEach(x =>
                        {
                            var scope = searchResponseV2.StudyIdentifiers.Find(y => y.Id == x.Id).StudyIdentifierScope;
                            x.StudyIdentifierScope.OrganisationIdentifierScheme = scope.IdentifierScheme;
                            x.StudyIdentifierScope.OrganisationIdentifier = scope.Identifier;
                            x.StudyIdentifierScope.OrganisationName = scope.Name;
                            x.StudyIdentifierScope.OrganisationType = _mapper.Map<CommonCodeDto>(scope.Type);
                        });
                        searchResponseDto.Study.StudyPhase = _mapper.Map<CommonCodeDto>(searchResponseV2.StudyPhase?.StandardCode);
                        searchResponseDto.Study.StudyDesigns = new List<CommonStudyDesign> { new() {
                            InterventionModel = _mapper.Map<List<CommonCodeDto>>(searchResponseV2.InterventionModel?.Where(x => x != null && x.Any()).SelectMany(x=>x).ToList()),
                            StudyIndications = _mapper.Map<List<Core.DTO.Common.CommonStudyIndication>>(searchResponseV2.StudyIndications?.Where(x => x != null && x.Any()).SelectMany(x=>x).Where(x => x != null && x.Any()).SelectMany(x=>x).ToList())
                        } };
                        searchResponseDto.Links = LinksHelper.GetLinksForUi(searchResponseDto.Study.StudyId, searchResponseV2.StudyDesignIds?.SelectMany(x => x).ToList(), searchResponseDto.AuditTrail.UsdmVersion, searchResponseDto.AuditTrail.SDRUploadVersion);
                    });
                    return searchResponseDtos;
                }

                return null;
            }
            else
                return null;
        }
        #endregion

        #region UserGroupsMapping
        public async Task<bool> CheckAccessForAStudy(string studyId, string studyType, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(CommonServices)}; Method : {nameof(CheckAccessForAStudy)};");

                if (user.UserRole != Constants.Roles.Org_Admin && Config.IsGroupFilterEnabled)
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

                if (user.UserRole != Constants.Roles.Org_Admin && Config.IsGroupFilterEnabled)
                {
                    var groups = await _commonRepository.GetGroupsOfUser(user).ConfigureAwait(false);

                    if (groups != null && groups.Count > 0)
                    {
                        studies.ForEach(x => x.HasAccess = true);
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
                                if (study.UsdmVersion == Constants.USDMVersions.V3 || study.UsdmVersion == Constants.USDMVersions.V4)
                                {
                                    if (!groupFilters.Item1.Contains((string)jsonObject["studyType"]["decode"].ToString().ToLower()))
                                        study.HasAccess = false;
                                }
                                else if (study.UsdmVersion == Constants.USDMVersions.V1_9)
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

                if (user.UserRole != Constants.Roles.Org_Admin && Config.IsGroupFilterEnabled)
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
                                if (study.UsdmVersion == Constants.USDMVersions.V3)
                                {
                                    if (!groupFilters.Item1.Contains((string)jsonObject["studyType"]["decode"].ToString().ToLower()))
                                        study.HasAccess = false;
                                }
                                else if (study.UsdmVersion == Constants.USDMVersions.V1_9)
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