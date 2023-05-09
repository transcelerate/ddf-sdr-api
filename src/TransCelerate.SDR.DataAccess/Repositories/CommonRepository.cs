using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.DataAccess.Filters;
using TransCelerate.SDR.DataAccess.Interfaces;

namespace TransCelerate.SDR.DataAccess.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        #region Variables     
        private readonly string _databaseName = Config.DatabaseName;
        private readonly ILogHelper _logger;

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IClinicalStudyRepositoryV1 _clinicalStudyRepositoryV1;
        private readonly IClinicalStudyRepository _clinicalStudyRepository;

        #endregion

        #region Constructor      
        public CommonRepository(IMongoClient client, ILogHelper logger,IClinicalStudyRepositoryV1 clinicalStudyRepositoryV1, IClinicalStudyRepository clinicalStudyRepository)
        {
            _client = client;
            _database = _client.GetDatabase(_databaseName);
            _logger = logger;
            _clinicalStudyRepositoryV1 = clinicalStudyRepositoryV1;
            _clinicalStudyRepository = clinicalStudyRepository;
            var conventionPack = new ConventionPack
            {
                new CamelCaseElementNameConvention()
            };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);
        }
        #endregion

        #region DB Operations
        #region GET
        /// <summary>
        /// GET a Study for a study ID with version filter
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <returns>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<GetRawJsonEntity> GetStudyItemsAsync(string studyId, int sdruploadversion)
        {
            _logger.LogInformation($"Started Repository : {nameof(CommonRepository)}; Method : {nameof(GetStudyItemsAsync)};");
            try
            {
                IMongoCollection<BsonDocument> collection = _database.GetCollection<BsonDocument>(Constants.Collections.StudyDefinitions);

                GetRawJsonEntity study = new();
                var studyData = await collection.Find(DataFilterCommon.GetFiltersForGetStudyBsonDocument(studyId, sdruploadversion))
                                                     .Sort(DataFilterCommon.GetSorterForBsonDocument()) // Sort by descending on entryDateTime
                                                     .Limit(1)                  //Taking top 1 result
                                                     .Project(DataFilterCommon.GetProjectorForBsonDocument())
                                                     .SingleOrDefaultAsync().ConfigureAwait(false);

                if (studyData == null)
                {
                    _logger.LogWarning($"There is no study with StudyId : {studyId} in {Constants.Collections.StudyDefinitions} Collection");
                    return null;
                }
                else
                {
                    study.AuditTrail = BsonSerializer.Deserialize<AuditTrailEntity>(studyData[Constants.DbFilter.AuditTrail].AsBsonDocument);
                    if (study.AuditTrail.UsdmVersion == Constants.USDMVersions.V1_9 || study.AuditTrail.UsdmVersion == Constants.USDMVersions.V2)
                    {
                        study.ClinicalStudy = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(studyData[Constants.DbFilter.ClinicalStudy].ToString());
                    }
                    else
                    {
                        study.ClinicalStudy = BsonSerializer.Deserialize<object>(studyData[Constants.DbFilter.ClinicalStudy].AsBsonDocument);
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
                _logger.LogInformation($"Ended Repository : {nameof(CommonRepository)}; Method : {nameof(GetStudyItemsAsync)};");
            }
        }

        /// <summary>
        /// GET List of study for a study ID
        /// </summary>
        /// <param name="fromDate">Start Date for Date Filter</param>
        /// <param name="toDate">End Date for Date Filter</param>
        /// <param name="studyId">Study ID</param>
        /// <returns>
        /// A <see cref="List{AuditTrailResponseEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<AuditTrailResponseEntity>> GetAuditTrail(string studyId, DateTime fromDate, DateTime toDate)
        {
            _logger.LogInformation($"Started Repository : {nameof(CommonRepository)}; Method : {nameof(GetAuditTrail)};");
            try
            {
                var collection = _database.GetCollection<CommonStudyEntity>(Constants.Collections.StudyDefinitions);

                List<AuditTrailResponseEntity> auditTrails = await collection.Aggregate()
                                                  .Match(DataFilterCommon.GetFiltersForGetAudTrail(studyId, fromDate, toDate)) // Condition for matching studyId and date range
                                                  .Project(x => new AuditTrailResponseEntity
                                                  {
                                                      StudyId = x.ClinicalStudy.StudyId,
                                                      StudyType = x.ClinicalStudy.StudyType,
                                                      EntryDateTime = x.AuditTrail.EntryDateTime,
                                                      SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                      UsdmVersion = x.AuditTrail.UsdmVersion,
                                                      StudyDesignIds = x.ClinicalStudy.StudyDesigns.Select(x => x.StudyDesignId ?? x.Uuid) ?? null,
                                                      StudyDesignIdsMVP = x.ClinicalStudy.CurrentSections.Select(x => x.StudyDesigns.Select(x => x.StudyDesignId)) ?? null,
                                                      HasAccess = true
                                                  })
                                                  .SortByDescending(s => s.EntryDateTime) // Sort by descending on entryDateTime
                                                  .ToListAsync().ConfigureAwait(false);

                if (auditTrails.Count == 0)
                {
                    _logger.LogWarning($"There is no study with StudyId : {studyId} in {Constants.Collections.StudyV1} Collection");
                    return null;
                }
                else
                {
                    return auditTrails;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(CommonRepository)}; Method : {nameof(GetAuditTrail)};");
            }
        }

        /// <summary>
        /// GET UsdmVersion
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <returns>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<string> GetUsdmVersion(string studyId, int sdruploadversion)
        {
            _logger.LogInformation($"Started Repository : {nameof(CommonRepository)}; Method : {nameof(GetStudyItemsAsync)};");
            try
            {
                IMongoCollection<GetRawJsonEntity> collection = _database.GetCollection<GetRawJsonEntity>(Constants.Collections.StudyDefinitions);

                string usdmVersion = await collection.Find(DataFilterCommon.GetFiltersForGetStudy(studyId, sdruploadversion))
                                                     .SortByDescending(s => s.AuditTrail.EntryDateTime) // Sort by descending on entryDateTime
                                                     .Project(x => x.AuditTrail.UsdmVersion)
                                                     .Limit(1)                  //Taking top 1 result
                                                     .SingleOrDefaultAsync().ConfigureAwait(false);


                if (usdmVersion == null)
                {
                    _logger.LogWarning($"There is no study with StudyId : {studyId} in {Constants.Collections.StudyDefinitions} Collection");
                    return null;
                }
                else
                {
                    return usdmVersion;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(CommonRepository)}; Method : {nameof(GetStudyItemsAsync)};");
            }
        }

        /// <summary>
        /// Get List of all studyId 
        /// </summary>
        /// <param name="fromDate">Start Date for Date Filter</param>
        /// <param name="toDate">End Date for Date Filter</param>
        /// <param name="studyTitle">Study Title Filter</param>        
        /// <returns>
        /// A <see cref="List{StudyHistoryResponseEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<StudyHistoryResponseEntity>> GetStudyHistory(DateTime fromDate, DateTime toDate, string studyTitle)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(GetStudyHistory)};");
            try
            {
                var collection = _database.GetCollection<CommonStudyEntity>(Constants.Collections.StudyDefinitions);

                List<StudyHistoryResponseEntity> studyHistories = await collection.Aggregate()
                                                        .Match(DataFilterCommon.GetFiltersForStudyHistory(fromDate, toDate, studyTitle)) // Condition for matching date range
                                                        .Project(x =>
                                                                new StudyHistoryResponseEntity
                                                                {
                                                                    StudyId = x.ClinicalStudy.StudyId,
                                                                    StudyTitle = x.ClinicalStudy.StudyTitle,
                                                                    SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                                    StudyIdentifiers = x.ClinicalStudy.StudyIdentifiers,
                                                                    EntryDateTime = x.AuditTrail.EntryDateTime,
                                                                    StudyType = x.ClinicalStudy.StudyType,
                                                                    ProtocolVersions = x.ClinicalStudy.StudyProtocolVersions.Select(x => x.ProtocolVersion),
                                                                    StudyVersion = x.ClinicalStudy.StudyVersion,
                                                                    UsdmVersion = x.AuditTrail.UsdmVersion,
                                                                    StudyDesignIds = x.ClinicalStudy.StudyDesigns.Select(x => x.StudyDesignId ?? x.Uuid) ?? null,
                                                                    StudyDesignIdsMVP = x.ClinicalStudy.CurrentSections.Select(x => x.StudyDesigns.Select(x => x.StudyDesignId)) ?? null,
                                                                    HasAccess = true,
                                                                })  //Project only the required fields                                                        
                                                        .ToListAsync().ConfigureAwait(false);

                if (studyHistories.Count == 0)
                {
                    _logger.LogWarning($"There are no Study in {Constants.Collections.StudyDefinitions} Collection");
                    return null;
                }
                else
                {
                    return studyHistories;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(GetStudyHistory)};");
            }
        }
        #endregion

        #region Search
        /// <summary>
        /// Search the collection based on search criteria
        /// </summary>
        /// <param name="searchParameters">Parameters to search in database</param>        
        /// <param name="user">Loggedin User</param>        
        /// <returns>
        /// A <see cref="List{SearchResponseEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<SearchResponseEntity>> SearchStudy(SearchParametersEntity searchParameters, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Repository : {nameof(CommonRepository)}; Method : {nameof(SearchStudy)};");
                IMongoCollection<CommonStudyEntity> collection = _database.GetCollection<CommonStudyEntity>(Constants.Collections.StudyDefinitions);

                List<SearchResponseEntity> studies;

                if (searchParameters.Header?.ToLower() == "phase" || searchParameters.Header?.ToLower() == "sponsorid" || searchParameters.Header?.ToLower() == "interventionmodel" || searchParameters.Header?.ToLower() == "indication")
                {
                    studies = await collection.Aggregate()
                                              .Match(DataFilterCommon.GetFiltersForSearchStudy(searchParameters, GetGroupsOfUser(user).Result, user))
                                              .Project(x => new SearchResponseEntity
                                              {
                                                  StudyId = x.ClinicalStudy.StudyId,
                                                  StudyTitle = x.ClinicalStudy.StudyTitle,
                                                  StudyType = x.ClinicalStudy.StudyType,
                                                  StudyPhase = x.ClinicalStudy.StudyPhase,
                                                  StudyIdentifiers = x.ClinicalStudy.StudyIdentifiers,
                                                  InterventionModel = x.ClinicalStudy.StudyDesigns.Select(y => y.InterventionModel) ?? null,
                                                  StudyIndications = x.ClinicalStudy.StudyDesigns.Select(y => y.StudyIndications.Select(z => z.IndicationDescription ?? z.IndicationDesc)) ?? null,
                                                  StudyIndicationsMVP = x.ClinicalStudy.CurrentSections.Select(x => x.StudyIndications.Select(x => x.Description)) ?? null,
                                                  InterventionModelMVP = x.ClinicalStudy.CurrentSections.Select(x => x.StudyDesigns).Select(x => x.Select(x => x.CurrentSections.Select(x => x.InvestigationalInterventions.Select(x => x.InterventionModel)))) ?? null,
                                                  EntryDateTime = x.AuditTrail.EntryDateTime,
                                                  SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                  UsdmVersion = x.AuditTrail.UsdmVersion,
                                                  StudyDesignIds = x.ClinicalStudy.StudyDesigns.Select(x => x.StudyDesignId ?? x.Uuid) ?? null,
                                                  StudyDesignIdsMVP = x.ClinicalStudy.CurrentSections.Select(x => x.StudyDesigns.Select(x => x.StudyDesignId)) ?? null,
                                              })
                                              .ToListAsync()
                                              .ConfigureAwait(false);
                }
                else
                {
                    studies = await collection.Aggregate()
                                              .Match(DataFilterCommon.GetFiltersForSearchStudy(searchParameters, GetGroupsOfUser(user).Result, user))
                                              .Project(x => new SearchResponseEntity
                                              {
                                                  StudyId = x.ClinicalStudy.StudyId,
                                                  StudyTitle = x.ClinicalStudy.StudyTitle,
                                                  StudyType = x.ClinicalStudy.StudyType,
                                                  StudyPhase = x.ClinicalStudy.StudyPhase,
                                                  StudyIdentifiers = x.ClinicalStudy.StudyIdentifiers,
                                                  InterventionModel = x.ClinicalStudy.StudyDesigns.Select(y => y.InterventionModel) ?? null,
                                                  StudyIndications = x.ClinicalStudy.StudyDesigns.Select(y => y.StudyIndications.Select(z => z.IndicationDescription ?? z.IndicationDesc)) ?? null,
                                                  StudyIndicationsMVP = x.ClinicalStudy.CurrentSections.Select(x => x.StudyIndications.Select(x => x.Description)) ?? null,
                                                  InterventionModelMVP = x.ClinicalStudy.CurrentSections.Select(x => x.StudyDesigns).Select(x => x.Select(x => x.CurrentSections.Select(x => x.InvestigationalInterventions.Select(x => x.InterventionModel)))) ?? null,
                                                  EntryDateTime = x.AuditTrail.EntryDateTime,
                                                  SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                  UsdmVersion = x.AuditTrail.UsdmVersion,
                                                  StudyDesignIds = x.ClinicalStudy.StudyDesigns.Select(x => x.StudyDesignId ?? x.Uuid) ?? null,
                                                  StudyDesignIdsMVP = x.ClinicalStudy.CurrentSections.Select(x => x.StudyDesigns.Select(x => x.StudyDesignId)) ?? null,
                                              })
                                              .Sort(DataFilterCommon.GetSorterForSearchStudy(searchParameters))
                                              .Skip((searchParameters.PageNumber - 1) * searchParameters.PageSize)
                                              .Limit(searchParameters.PageSize)
                                              .ToListAsync()
                                              .ConfigureAwait(false);
                }

                return studies;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(CommonRepository)}; Method : {nameof(SearchStudy)};");
            }
        }

        /// <summary>
        /// Search the collection based on search criteria
        /// </summary>
        /// <param name="searchParameters">Parameters to search in database</param>        
        /// <param name="user">Loggedin User</param>        
        /// <returns>
        /// A <see cref="List{SearchResponseEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<Core.Entities.Study.SearchResponse>> SearchStudyMVP(SearchParametersEntity searchParameters, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Repository : {nameof(CommonRepository)}; Method : {nameof(SearchStudyMVP)};");
                IMongoCollection<Core.Entities.Study.StudyEntity> collection = _database.GetCollection<Core.Entities.Study.StudyEntity>(Constants.Collections.StudyDefinitions);
                

                List<Core.Entities.Study.SearchResponse> studies = await collection.Aggregate()
                                                                    .Match(DataFilterCommon.GetFiltersForSearchMVP(searchParameters, GetGroupsOfUser(user).Result, user))
                                                                    .Project(x => new Core.Entities.Study.SearchResponse
                                                                    {
                                                                        StudyId = x.ClinicalStudy.StudyId ?? null,
                                                                        StudyTag = x.ClinicalStudy.StudyTag ?? null,
                                                                        StudyType = x.ClinicalStudy.StudyType ?? null,
                                                                        StudyPhase = x.ClinicalStudy.StudyPhase ?? null,
                                                                        StudyTitle = x.ClinicalStudy.StudyTitle ?? null,
                                                                        StudyStatus = x.ClinicalStudy.StudyStatus ?? null,
                                                                        StudyIdentifiers = x.ClinicalStudy.StudyIdentifiers ?? null,
                                                                        StudyIndications = x.ClinicalStudy.CurrentSections.Select(x => x.StudyIndications) ?? null,
                                                                        InvestigationalInterventions = x.ClinicalStudy.CurrentSections.Select(x => x.StudyDesigns).Select(x => x.Select(x => x.CurrentSections.Select(x => x.InvestigationalInterventions))) ?? null,
                                                                        EntryDateTime = x.AuditTrail.EntryDateTime,
                                                                        EntrySystem = x.AuditTrail.EntrySystem ?? null,
                                                                        StudyVersion = x.AuditTrail.StudyVersion,
                                                                        UsdmVersion = x.AuditTrail.UsdmVersion,
                                                                        StudyDesignIds = x.ClinicalStudy.CurrentSections.Select(x => x.StudyDesigns.Select(x => x.StudyDesignId)) ?? null,
                                                                    })
                                                                    .ToListAsync()
                                                                    .ConfigureAwait(false);

                return _clinicalStudyRepository.ApplyOrderBy(studies,searchParameters.Header,searchParameters.Asc)
                                               .Skip((searchParameters.PageNumber - 1) * searchParameters.PageSize)
                                               .Take(searchParameters.PageSize)
                                               .ToList(); 
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(CommonRepository)}; Method : {nameof(SearchStudyMVP)};");
            }
        }

        /// <summary>
        /// Search the collection based on search criteria
        /// </summary>
        /// <param name="searchParameters">Parameters to search in database</param>        
        /// <param name="user">Loggedin User</param>        
        /// <returns>
        /// A <see cref="List{SearchResponseEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<Core.Entities.StudyV1.SearchResponseEntity>> SearchStudyV1(SearchParametersEntity searchParameters, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Repository : {nameof(CommonRepository)}; Method : {nameof(SearchStudyV1)};");
                IMongoCollection<Core.Entities.StudyV1.StudyEntity> collection = _database.GetCollection<Core.Entities.StudyV1.StudyEntity>(Constants.Collections.StudyDefinitions);


                List<Core.Entities.StudyV1.SearchResponseEntity> studies = await collection.Aggregate()
                                              .Match(DataFilterCommon.GetFiltersForSearchV1(searchParameters, GetGroupsOfUser(user).Result, user))
                                              .Project(x => new Core.Entities.StudyV1.SearchResponseEntity
                                              {
                                                  StudyId = x.ClinicalStudy.Uuid,
                                                  StudyTitle = x.ClinicalStudy.StudyTitle,
                                                  StudyType = x.ClinicalStudy.StudyType,
                                                  StudyPhase = x.ClinicalStudy.StudyPhase,
                                                  StudyIdentifiers = x.ClinicalStudy.StudyIdentifiers,
                                                  InterventionModel = x.ClinicalStudy.StudyDesigns.Select(y => y.InterventionModel) ?? null,
                                                  StudyIndications = x.ClinicalStudy.StudyDesigns.Select(y => y.StudyIndications) ?? null,
                                                  EntryDateTime = x.AuditTrail.EntryDateTime,
                                                  SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                  UsdmVersion = x.AuditTrail.UsdmVersion,
                                                  StudyDesignIds = x.ClinicalStudy.StudyDesigns.Select(x => x.Uuid) ?? null,
                                              })
                                              .ToListAsync()
                                              .ConfigureAwait(false);

                return _clinicalStudyRepositoryV1.SortSearchResults(studies, searchParameters.Header, searchParameters.Asc)
                                                 .Skip((searchParameters.PageNumber - 1) * searchParameters.PageSize)
                                                 .Take(searchParameters.PageSize)
                                                 .ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(CommonRepository)}; Method : {nameof(SearchStudyV1)};");
            }
        }

        /// <summary>
        /// Search the collection based on search criteria
        /// </summary>
        /// <param name="searchParameters">Parameters to search in database</param>        
        /// <param name="user">Loggedin User</param>        
        /// <returns>
        /// A <see cref="List{SearchResponseEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<Core.Entities.StudyV2.SearchResponseEntity>> SearchStudyV2(SearchParametersEntity searchParameters, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Repository : {nameof(CommonRepository)}; Method : {nameof(SearchStudyV2)};");
                IMongoCollection<Core.Entities.StudyV2.StudyEntity> collection = _database.GetCollection<Core.Entities.StudyV2.StudyEntity>(Constants.Collections.StudyDefinitions);


                List<Core.Entities.StudyV2.SearchResponseEntity> studies = await collection.Aggregate()
                                              .Match(DataFilterCommon.GetFiltersForSearchV2(searchParameters, GetGroupsOfUser(user).Result, user))
                                              .Project(x => new Core.Entities.StudyV2.SearchResponseEntity
                                              {
                                                  StudyId = x.ClinicalStudy.StudyId,
                                                  StudyTitle = x.ClinicalStudy.StudyTitle,
                                                  StudyType = x.ClinicalStudy.StudyType,
                                                  StudyPhase = x.ClinicalStudy.StudyPhase,
                                                  StudyIdentifiers = x.ClinicalStudy.StudyIdentifiers,
                                                  InterventionModel = x.ClinicalStudy.StudyDesigns.Select(y => y.InterventionModel) ?? null,
                                                  StudyIndications = x.ClinicalStudy.StudyDesigns.Select(y => y.StudyIndications) ?? null,
                                                  EntryDateTime = x.AuditTrail.EntryDateTime,
                                                  SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                  UsdmVersion = x.AuditTrail.UsdmVersion,
                                                  StudyDesignIds = x.ClinicalStudy.StudyDesigns.Select(x => x.Id) ?? null,
                                              })
                                              .ToListAsync()
                                              .ConfigureAwait(false);

                return DataFilterCommon.SortSearchResultsV2(studies, searchParameters.Header, searchParameters.Asc)
                                       .Skip((searchParameters.PageNumber - 1) * searchParameters.PageSize)
                                       .Take(searchParameters.PageSize)
                                       .ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(CommonRepository)}; Method : {nameof(SearchStudyV2)};");
            }
        }
        #endregion

        #region Search Title
        /// <summary>
        /// Search the collection based on search criteria
        /// </summary>
        /// <param name="searchParameters">Parameters to search in database</param>        
        /// <param name="user">LoggedIn User</param>        
        /// <returns>
        /// A <see cref="List{SearchTitleResponseEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<SearchTitleResponseEntity>> SearchTitle(SearchTitleParametersEntity searchParameters, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(SearchTitle)};");
                IMongoCollection<CommonStudyEntity> collection = _database.GetCollection<CommonStudyEntity>(Constants.Collections.StudyDefinitions);

                List<SearchTitleResponseEntity> studies = new();

                if (searchParameters.SortBy?.ToLower() == "sponsorid")
                {
                    studies = await collection.Aggregate()
                                              .Match(DataFilterCommon.GetFiltersForSearchTitle(searchParameters, GetGroupsOfUser(user).Result, user))
                                              .Project(x => new SearchTitleResponseEntity
                                              {
                                                  StudyId = x.ClinicalStudy.StudyId,
                                                  StudyTitle = x.ClinicalStudy.StudyTitle,
                                                  StudyIdentifiers = x.ClinicalStudy.StudyIdentifiers,
                                                  StudyType = x.ClinicalStudy.StudyType,
                                                  EntryDateTime = x.AuditTrail.EntryDateTime,
                                                  SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                  StudyDesignIds = x.ClinicalStudy.StudyDesigns.Select(x => x.StudyDesignId ?? x.Uuid) ?? null,
                                                  StudyDesignIdsMVP = x.ClinicalStudy.CurrentSections.Select(x => x.StudyDesigns.Select(x => x.StudyDesignId)) ?? null,
                                                  UsdmVersion = x.AuditTrail.UsdmVersion
                                              })
                                              .ToListAsync()
                                              .ConfigureAwait(false);
                }
                else
                {
                    studies = await collection.Aggregate()
                                             .Match(DataFilterCommon.GetFiltersForSearchTitle(searchParameters, GetGroupsOfUser(user).Result, user))
                                             .Project(x => new SearchTitleResponseEntity
                                             {
                                                 StudyId = x.ClinicalStudy.StudyId,
                                                 StudyTitle = x.ClinicalStudy.StudyTitle,
                                                 StudyIdentifiers = x.ClinicalStudy.StudyIdentifiers,
                                                 StudyType = x.ClinicalStudy.StudyType,
                                                 EntryDateTime = x.AuditTrail.EntryDateTime,
                                                 SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                 StudyDesignIds = x.ClinicalStudy.StudyDesigns.Select(x => x.StudyDesignId ?? x.Uuid) ?? null,
                                                 StudyDesignIdsMVP = x.ClinicalStudy.CurrentSections.Select(x => x.StudyDesigns.Select(x => x.StudyDesignId)) ?? null,
                                                 UsdmVersion = x.AuditTrail.UsdmVersion
                                             })
                                             .Sort(DataFilterCommon.GetSorterForSearchStudyTitle(searchParameters))
                                             .Skip((searchParameters.PageNumber - 1) * searchParameters.PageSize)
                                             .Limit(searchParameters.PageSize)
                                             .ToListAsync()
                                             .ConfigureAwait(false);
                }
                return studies;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(SearchTitle)};");
            }
        }
        #endregion

        #region UserGroup Mapping

        public async Task<List<SDRGroupsEntity>> GetGroupsOfUser(LoggedInUser user)
        {
            try
            {
                var groupsCollection = _database.GetCollection<UserGroupMappingEntity>(Constants.Collections.SDRGrouping);

                return await groupsCollection.Find(_ => true)
                                                 .Project(x => x.SDRGroups
                                                               .Where(x => x.GroupEnabled == true)
                                                               .Where(x => x.Users != null)
                                                               .Where(x => x.Users.Any(x => (x.Email == user.UserName && x.IsActive == true)))
                                                               .ToList())
                                                 .FirstOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #endregion
    }
}
