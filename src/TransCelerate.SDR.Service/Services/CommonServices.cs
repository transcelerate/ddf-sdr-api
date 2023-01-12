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
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyService)}; Method : {nameof(SearchTitle)};");
            }
        }

        public List<SearchTitleResponseDto> AssignStudyIdentifiers(List<SearchTitleResponseDto> searchTitleDTOs,List<SearchTitleResponseEntity> searchTitleResponses)
        {
            searchTitleDTOs.ForEach(searchTitleDTO =>
            {
                var searchResponse = searchTitleResponses.Where(x => x.StudyId == searchTitleDTO.ClinicalStudy.StudyId && x.SDRUploadVersion == searchTitleDTO.AuditTrail.SDRUploadVersion).FirstOrDefault();
                if (searchResponse.StudyIdentifiers != null)
                {
                    if (searchTitleDTO.AuditTrail.UsdmVersion == Constants.USDMVersions.MVP)
                    {
                        searchTitleDTO.ClinicalStudy.StudyIdentifiers = new List<CommonStudyIdentifiersDto>();

                        searchResponse.StudyIdentifiers.ForEach(identifier =>
                        {
                            var jsonObject = JObject.Parse(JsonConvert.SerializeObject(identifier, new JsonSerializerSettings
                            {
                                ContractResolver = new DefaultContractResolver()
                                {
                                    NamingStrategy = new CamelCaseNamingStrategy()
                                }
                            }));
                            CommonStudyIdentifiersDto studyIdentifier = new CommonStudyIdentifiersDto
                            {
                                StudyIdentifierScope = new CommonOrganisationDto
                                {
                                    OrganisationIdentifier = (string)jsonObject["orgCode"].ToString(),
                                    OrganisationType = new CommonCodeDto
                                    {
                                        Decode = (string)jsonObject["idType"].ToString()
                                    }
                                }
                            };
                            searchTitleDTO.ClinicalStudy.StudyIdentifiers.Add(studyIdentifier);
                        });
                    }
                    else
                    {
                        searchTitleDTO.ClinicalStudy.StudyIdentifiers = JsonConvert.DeserializeObject<List<CommonStudyIdentifiersDto>>(JsonConvert.SerializeObject(searchResponse.StudyIdentifiers));
                    }
                }
            });

            return searchTitleDTOs;
        }

        public List<SearchTitleResponseDto> SortStudyTitle(List<SearchTitleResponseDto> searchTitleDTOs, SearchTitleParametersDto searchParametersDTO)
        {
            if (!String.IsNullOrWhiteSpace(searchParametersDTO.SortBy))
            {
                return searchParametersDTO.SortBy.ToLower() switch
                {
                    "studytitle" => searchParametersDTO.SortOrder == SortOrder.asc.ToString() ? searchTitleDTOs.OrderBy(x => x.ClinicalStudy.StudyTitle).ToList() : searchTitleDTOs.OrderByDescending(x => x.ClinicalStudy.StudyTitle).ToList(),
                   "sponsorid" => searchParametersDTO.SortOrder == SortOrder.asc.ToString() ? searchTitleDTOs.OrderBy(s => s.ClinicalStudy.StudyIdentifiers != null ? s.ClinicalStudy.StudyIdentifiers.FindAll(x => x.StudyIdentifierScope?.OrganisationType?.Decode == Constants.IdType.SPONSOR_ID_V1 || x.StudyIdentifierScope?.OrganisationType?.Decode == Constants.IdType.SPONSOR_ID).Any() ? s.ClinicalStudy.StudyIdentifiers.Find(x => x.StudyIdentifierScope?.OrganisationType?.Decode == Constants.IdType.SPONSOR_ID_V1 || x.StudyIdentifierScope?.OrganisationType?.Decode == Constants.IdType.SPONSOR_ID).StudyIdentifierScope.OrganisationIdentifier ?? "" : "" : "").ToList()
                                                                                        : searchTitleDTOs.OrderByDescending(s => s.ClinicalStudy.StudyIdentifiers != null ? s.ClinicalStudy.StudyIdentifiers.FindAll(x => x.StudyIdentifierScope?.OrganisationType?.Decode == Constants.IdType.SPONSOR_ID_V1 || x.StudyIdentifierScope?.OrganisationType?.Decode == Constants.IdType.SPONSOR_ID).Any() ? s.ClinicalStudy.StudyIdentifiers.Find(x => x.StudyIdentifierScope?.OrganisationType?.Decode == Constants.IdType.SPONSOR_ID_V1 || x.StudyIdentifierScope?.OrganisationType?.Decode == Constants.IdType.SPONSOR_ID).StudyIdentifierScope.OrganisationIdentifier ?? "" : "" : "").ToList(),
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
                _logger.LogInformation($"Search Parameters : {JsonConvert.SerializeObject(searchParametersDto)}");

                var searchParameters = _mapper.Map<SearchParametersEntity>(searchParametersDto);

                List<SearchResponseEntity> searchResponseEntities = await _commonRepository.SearchStudy(searchParameters);

                if (searchResponseEntities is null || !searchResponseEntities.Any())
                    return null;
                searchResponseEntities = await CheckAccessForListOfStudies(searchResponseEntities, user);
                if (searchResponseEntities is null || !searchResponseEntities.Any())
                    return null;

                var searchResponseDtos = _mapper.Map<List<SearchResponseDto>>(searchResponseEntities);
                searchResponseDtos = AssignDynamicValues(searchResponseDtos, searchResponseEntities);
                searchResponseDtos = SortSearchResults(searchResponseDtos,searchParametersDto)
                                         .Skip((searchParameters.PageNumber - 1) * searchParameters.PageSize)
                                         .Take(searchParameters.PageSize) 
                                         .ToList();
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
                var searchResponseEntity = searchResponseEntities.Where(x => x.StudyId == searchResponseDto.ClinicalStudy.StudyId && x.SDRUploadVersion == searchResponseDto.AuditTrail.SDRUploadVersion).FirstOrDefault();
                #region StudyIdentifiers
                if (searchResponseEntity.StudyIdentifiers != null)
                {
                    if (searchResponseDto.AuditTrail.UsdmVersion == Constants.USDMVersions.MVP)
                    {
                        searchResponseDto.ClinicalStudy.StudyIdentifiers = new List<CommonStudyIdentifiersDto>();

                        searchResponseEntity.StudyIdentifiers.ForEach(identifier =>
                        {
                            var jsonObject = JObject.Parse(JsonConvert.SerializeObject(identifier, new JsonSerializerSettings
                            {
                                ContractResolver = new DefaultContractResolver()
                                {
                                    NamingStrategy = new CamelCaseNamingStrategy()
                                }
                            }));
                            CommonStudyIdentifiersDto studyIdentifier = new CommonStudyIdentifiersDto
                            {
                                StudyIdentifierScope = new CommonOrganisationDto
                                {
                                    OrganisationIdentifier = (string)jsonObject["orgCode"].ToString(),
                                    OrganisationType = new CommonCodeDto
                                    {
                                        Decode = (string)jsonObject["idType"].ToString()
                                    }
                                }
                            };
                            searchResponseDto.ClinicalStudy.StudyIdentifiers.Add(studyIdentifier);
                        });
                    }
                    else
                    {
                        searchResponseDto.ClinicalStudy.StudyIdentifiers = JsonConvert.DeserializeObject<List<CommonStudyIdentifiersDto>>(JsonConvert.SerializeObject(searchResponseEntity.StudyIdentifiers));
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
                if (searchResponseEntity.StudyType != null)
                {
                    if (searchResponseDto.AuditTrail.UsdmVersion == Constants.USDMVersions.MVP)
                        searchResponseDto.ClinicalStudy.StudyType = new CommonCodeDto { Decode = (string)jsonObject["studyType"].ToString() };
                    else
                        searchResponseDto.ClinicalStudy.StudyType = JsonConvert.DeserializeObject<CommonCodeDto>(JsonConvert.SerializeObject(searchResponseEntity.StudyType));
                }
                if (searchResponseEntity.StudyPhase != null)
                {
                    if (searchResponseDto.AuditTrail.UsdmVersion == Constants.USDMVersions.MVP)
                        searchResponseDto.ClinicalStudy.StudyPhase = new CommonCodeDto { Decode = (string)jsonObject["studyPhase"].ToString() };
                    else if (searchResponseDto.AuditTrail.UsdmVersion == Constants.USDMVersions.V1)
                        searchResponseDto.ClinicalStudy.StudyPhase = JsonConvert.DeserializeObject<CommonCodeDto>(JsonConvert.SerializeObject(jsonObject["studyPhase"]));
                    else
                        searchResponseDto.ClinicalStudy.StudyPhase = JsonConvert.DeserializeObject<CommonCodeDto>(JsonConvert.SerializeObject(jsonObject["studyPhase"]["standardCode"]));
                }
                #endregion
                searchResponseDto.ClinicalStudy.StudyDesigns = new List<CommonStudyDesign>();
                var studyDesign = new CommonStudyDesign();
                #region Indications
                if (searchResponseDto.AuditTrail.UsdmVersion == Constants.USDMVersions.MVP && searchResponseEntity.StudyIndicationsMVP != null)
                {
                    studyDesign.StudyIndications = new List<Core.DTO.Common.CommonStudyIndication>();
                    var listOfIndicationDescriptions = searchResponseEntity.StudyIndicationsMVP.Where(x => x != null && x.Count() > 0).SelectMany(x => x).ToList();
                    listOfIndicationDescriptions.ForEach(ind => studyDesign.StudyIndications.Add(new Core.DTO.Common.CommonStudyIndication { IndicationDescription = ind }));
                }
                if (searchResponseDto.AuditTrail.UsdmVersion != Constants.USDMVersions.MVP && searchResponseEntity.StudyIndications != null)
                {
                    studyDesign.StudyIndications = new List<Core.DTO.Common.CommonStudyIndication>();
                    var listOfIndicationDescriptions = searchResponseEntity.StudyIndications.Where(x => x != null && x.Count() > 0).SelectMany(x => x).ToList();
                    listOfIndicationDescriptions.ForEach(ind => studyDesign.StudyIndications.Add(new Core.DTO.Common.CommonStudyIndication { IndicationDescription = ind }));
                }
                #endregion

                #region InterventionModel
                if (searchResponseDto.AuditTrail.UsdmVersion == Constants.USDMVersions.MVP && searchResponseEntity.InterventionModelMVP != null)
                {
                    studyDesign.InterventionModel = new List<CommonCodeDto>();
                    var listOfInterventionModels = searchResponseEntity.InterventionModelMVP.Where(x => x != null && x.Count() > 0).SelectMany(x => x)
                                                                                .Where(x => x != null && x.Count() > 0).SelectMany(x => x)
                                                                                .Where(x => x != null && x.Count() > 0).SelectMany(x => x).ToList();
                    listOfInterventionModels.ForEach(ind => studyDesign.InterventionModel.Add(new CommonCodeDto { Decode = ind }));
                }
                if (searchResponseDto.AuditTrail.UsdmVersion != Constants.USDMVersions.MVP && searchResponseEntity.InterventionModel != null)
                {
                    studyDesign.InterventionModel = new List<CommonCodeDto>();
                    var listOfInterventionModels = searchResponseEntity.InterventionModel.ToList();
                    listOfInterventionModels.ForEach(ind => studyDesign.InterventionModel.AddRange(JsonConvert.DeserializeObject<List<CommonCodeDto>>(JsonConvert.SerializeObject(ind))));
                }
                #endregion

                searchResponseDto.ClinicalStudy.StudyDesigns.Add(studyDesign);
                var studyDesignIds = searchResponseDto.AuditTrail.UsdmVersion == Constants.USDMVersions.MVP ? searchResponseEntity.StudyDesignIdsMVP?.Where(x => x != null && x.Count() > 0).SelectMany(x => x)?.ToList() : searchResponseEntity.StudyDesignIds?.ToList();
                searchResponseDto.Links = LinksHelper.GetLinksForUi(searchResponseDto.ClinicalStudy.StudyId,studyDesignIds, searchResponseDto.AuditTrail.UsdmVersion, searchResponseDto.AuditTrail.SDRUploadVersion);
            });
            return searchResponseDtos;
        }

        public List<SearchResponseDto> SortSearchResults(List<SearchResponseDto> searchResponseDtos, SearchParametersDto searchParameters)
        {
            if (!String.IsNullOrWhiteSpace(searchParameters.Header))
            {
                return searchParameters.Header.ToLower() switch
                {
                    "studytitle" => searchParameters.Asc ? searchResponseDtos.OrderBy(x => x.ClinicalStudy.StudyTitle).ToList() : searchResponseDtos.OrderByDescending(x => x.ClinicalStudy.StudyTitle).ToList(),
                    "studyphase" => searchParameters.Asc ? searchResponseDtos.OrderBy(s => s.ClinicalStudy.StudyPhase != null ? s.ClinicalStudy.StudyPhase.Decode ?? "" : "").ToList() : searchResponseDtos.OrderByDescending(s => s.ClinicalStudy.StudyPhase != null ? s.ClinicalStudy.StudyPhase.Decode ?? "" : "").ToList(),
                    "sponsorid" => searchParameters.Asc ? searchResponseDtos.OrderBy(s => s.ClinicalStudy.StudyIdentifiers != null ? s.ClinicalStudy.StudyIdentifiers.FindAll(x => x.StudyIdentifierScope?.OrganisationType?.Decode.ToLower() == Constants.IdType.SPONSOR_ID_V1.ToLower() || x.StudyIdentifierScope?.OrganisationType?.Decode.ToLower() == Constants.IdType.SPONSOR_ID.ToLower()).Any() ? s.ClinicalStudy.StudyIdentifiers.Find(x => x.StudyIdentifierScope?.OrganisationType?.Decode.ToLower() == Constants.IdType.SPONSOR_ID_V1.ToLower() || x.StudyIdentifierScope?.OrganisationType?.Decode.ToLower() == Constants.IdType.SPONSOR_ID.ToLower()).StudyIdentifierScope.OrganisationIdentifier ?? "" : "" : "").ToList()
                                                            : searchResponseDtos.OrderByDescending(s => s.ClinicalStudy.StudyIdentifiers != null ? s.ClinicalStudy.StudyIdentifiers.FindAll(x => x.StudyIdentifierScope?.OrganisationType?.Decode.ToLower() == Constants.IdType.SPONSOR_ID_V1.ToLower() || x.StudyIdentifierScope?.OrganisationType?.Decode.ToLower() == Constants.IdType.SPONSOR_ID.ToLower()).Any() ? s.ClinicalStudy.StudyIdentifiers.Find(x => x.StudyIdentifierScope?.OrganisationType?.Decode.ToLower() == Constants.IdType.SPONSOR_ID_V1.ToLower() || x.StudyIdentifierScope?.OrganisationType?.Decode.ToLower() == Constants.IdType.SPONSOR_ID.ToLower()).StudyIdentifierScope.OrganisationIdentifier ?? "" : "" : "").ToList(),
                    "lastmodifieddate" => searchParameters.Asc ? searchResponseDtos.OrderBy(x => x.AuditTrail.EntryDateTime).ToList() : searchResponseDtos.OrderByDescending(x => x.AuditTrail.EntryDateTime).ToList(),
                    "sdrversion" => searchParameters.Asc ? searchResponseDtos.OrderBy(x => x.AuditTrail.SDRUploadVersion).ToList() : searchResponseDtos.OrderByDescending(x => x.AuditTrail.SDRUploadVersion).ToList(),

                    "interventionmodel" => searchParameters.Asc ? searchResponseDtos.OrderBy(x => x.ClinicalStudy.StudyDesigns!= null && x.ClinicalStudy.StudyDesigns.Any() ? x.ClinicalStudy.StudyDesigns.First().InterventionModel != null && x.ClinicalStudy.StudyDesigns.First().InterventionModel.Any() ? x.ClinicalStudy.StudyDesigns.First().InterventionModel.First().Decode : "" : "").ToList() 
                                                                : searchResponseDtos.OrderByDescending(x => x.ClinicalStudy.StudyDesigns != null && x.ClinicalStudy.StudyDesigns.Any() ? x.ClinicalStudy.StudyDesigns.First().InterventionModel != null && x.ClinicalStudy.StudyDesigns.First().InterventionModel.Any() ? x.ClinicalStudy.StudyDesigns.First().InterventionModel.First().Decode : "" : "").ToList(),

                    "indication" => searchParameters.Asc ? searchResponseDtos.OrderBy(x => x.ClinicalStudy.StudyDesigns != null && x.ClinicalStudy.StudyDesigns.Any() ? x.ClinicalStudy.StudyDesigns.First().StudyIndications != null && x.ClinicalStudy.StudyDesigns.First().StudyIndications.Any() ? x.ClinicalStudy.StudyDesigns.First().StudyIndications.First().IndicationDescription : "" : "").ToList() 
                                                                    : searchResponseDtos.OrderByDescending(x => x.ClinicalStudy.StudyDesigns != null && x.ClinicalStudy.StudyDesigns.Any() ? x.ClinicalStudy.StudyDesigns.First().StudyIndications != null && x.ClinicalStudy.StudyDesigns.First().StudyIndications.Any() ? x.ClinicalStudy.StudyDesigns.First().StudyIndications.First().IndicationDescription : "" : "").ToList(),
                    _ => searchParameters.Asc ? searchResponseDtos.OrderByDescending(x => x.AuditTrail.EntryDateTime).ToList() : searchResponseDtos.OrderBy(x => x.AuditTrail.EntryDateTime).ToList(),
                };
            }
            else
            {
                return searchParameters.Asc ? searchResponseDtos.OrderBy(x => x.AuditTrail.EntryDateTime).ToList() : searchResponseDtos.OrderByDescending(x => x.AuditTrail.EntryDateTime).ToList();
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
