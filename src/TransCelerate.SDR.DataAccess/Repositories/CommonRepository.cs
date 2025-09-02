using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Entities.Common;
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

        #endregion

        #region Constructor      
        public CommonRepository(IMongoClient client, ILogHelper logger)
        {
            _client = client;
            _database = _client.GetDatabase(_databaseName);
            _logger = logger;
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
                    if (study.AuditTrail.UsdmVersion == Constants.USDMVersions.V1_9 || study.AuditTrail.UsdmVersion == Constants.USDMVersions.V2 || study.AuditTrail.UsdmVersion == Constants.USDMVersions.V3 || study.AuditTrail.UsdmVersion == Constants.USDMVersions.V4)
                    {
                        study.Study = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(studyData[Constants.DbFilter.Study].ToString());
                    }
                    else
                    {
                        study.Study = BsonSerializer.Deserialize<object>(studyData[Constants.DbFilter.Study].AsBsonDocument);
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
                var collection = _database.GetCollection<CommonStudyDefinitionsEntity>(Constants.Collections.StudyDefinitions);

                List<AuditTrailResponseEntity> auditTrails = await collection.Aggregate()
                                                  .Match(DataFilterCommon.GetFiltersForGetAudTrail(studyId, fromDate, toDate)) // Condition for matching studyId and date range
                                                  .Project(x => new AuditTrailResponseEntity
                                                  {
                                                      StudyId = x.Study.StudyId,
                                                      StudyType = x.Study.StudyType,
                                                      EntryDateTime = x.AuditTrail.EntryDateTime,
                                                      SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                      UsdmVersion = x.AuditTrail.UsdmVersion,
                                                      StudyDesignIds = x.Study.StudyDesigns.Select(x => x.StudyDesignId ?? x.Id) ?? null,
                                                      StudyDesignIdsV4 = x.Study.Versions.Select(x => x.StudyDesigns.Select(y => y.Id)) ?? null,
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
            _logger.LogInformation($"Started Repository : {nameof(CommonRepository)}; Method : {nameof(GetStudyHistory)};");
            try
            {
                var collection = _database.GetCollection<CommonStudyDefinitionsEntity>(Constants.Collections.StudyDefinitions);
                var collectionV5 = _database.GetCollection<CommonStudyDefinitionsEntityV5>(Constants.Collections.StudyDefinitions);

                List<StudyHistoryResponseEntity> studyHistories = await collection.Aggregate()
                                                        .Match(DataFilterCommon.GetFiltersForStudyHistory(fromDate, toDate, studyTitle)) // Condition for matching date range
                                                        .Project(x =>
                                                                new StudyHistoryResponseEntity
                                                                {
                                                                    StudyId = x.Study.StudyId,
                                                                    StudyTitle = x.Study.StudyTitle,
                                                                    SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                                    StudyIdentifiers = x.Study.StudyIdentifiers,
                                                                    EntryDateTime = x.AuditTrail.EntryDateTime,
                                                                    StudyType = x.Study.StudyType,
                                                                    ProtocolVersions = x.Study.StudyProtocolVersions.Select(x => x.ProtocolVersion),
                                                                    StudyVersion = x.Study.StudyVersion,
                                                                    UsdmVersion = x.AuditTrail.UsdmVersion,
                                                                    StudyDesignIds = x.Study.StudyDesigns.Select(x => x.StudyDesignId ?? x.Id),
                                                                })  //Project only the required fields                                                        
                                                        .ToListAsync().ConfigureAwait(false);

                List<StudyHistoryResponseEntity> studyHistoriesV4 = await collection.Aggregate()
                                                        .Match(DataFilterCommon.GetFiltersForStudyHistoryV4(fromDate, toDate, studyTitle)) // Condition for matching date range
                                                        .Project(x =>
                                                                new StudyHistoryResponseEntity
                                                                {
                                                                    StudyId = x.Study.Id,
                                                                    StudyTitle = x.Study.Versions != null ? x.Study.Versions.First().Titles : null,
                                                                    SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                                    StudyIdentifiers = x.Study.Versions != null ? x.Study.Versions.First().StudyIdentifiers : null,
                                                                    EntryDateTime = x.AuditTrail.EntryDateTime,
                                                                    StudyType = x.Study.Versions != null ? x.Study.Versions.First().StudyType : null,
                                                                    //ProtocolVersions = x.Study.DocumentedBy != null && x.Study.DocumentedBy.Versions != null ? x.Study.DocumentedBy.Versions.Select(x => x.ProtocolVersion) : null,
                                                                    StudyVersion = x.Study.Versions != null ? x.Study.Versions.First().VersionIdentifier : null,
                                                                    UsdmVersion = x.AuditTrail.UsdmVersion,
                                                                    StudyDesignIdsV4 = x.Study.Versions != null ? x.Study.Versions.Select(x => x.StudyDesigns.Select(y => y.Id)) : null,
                                                                })  //Project only the required fields                                                        
                                                        .ToListAsync().ConfigureAwait(false);
                studyHistories.AddRange(studyHistoriesV4);
                List<StudyHistoryResponseEntity> studyHistoriesV5 = await collectionV5.Aggregate()
                                                    .Match(DataFilterCommon.GetFiltersForStudyHistoryV5(fromDate, toDate, studyTitle)) // Condition for matching date range
                                                    .Project(x =>
                                                            new StudyHistoryResponseEntity
                                                            {
                                                                StudyId = x.Study.Id,
                                                                StudyTitle = x.Study.Versions != null ? x.Study.Versions.First().Titles : null,
                                                                SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                                StudyIdentifiers = x.Study.Versions != null ? x.Study.Versions.First().StudyIdentifiers : null,
                                                                EntryDateTime = x.AuditTrail.EntryDateTime,
                                                                StudyType = x.Study.Versions != null ? x.Study.Versions.First().StudyType : null,
                                                                //ProtocolVersions = x.Study.DocumentedBy.SelectMany(z => z.Versions.Select(y => y.ProtocolVersion)),
                                                                StudyVersion = x.Study.Versions != null ? x.Study.Versions.First().VersionIdentifier : null,
                                                                UsdmVersion = x.AuditTrail.UsdmVersion,
                                                                StudyDesignIdsV4 = x.Study.Versions != null ? x.Study.Versions.Select(x => x.StudyDesigns.Select(y => y.Id)) : null,
                                                            })  //Project only the required fields                                                        
                                                    .ToListAsync().ConfigureAwait(false);
                studyHistories.AddRange(studyHistoriesV5);

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
                _logger.LogInformation($"Ended Repository : {nameof(CommonRepository)}; Method : {nameof(GetStudyHistory)};");
            }
        }
        #endregion

        #region Search
        /// <summary>
        /// Search the collection based on search criteria
        /// </summary>
        /// <param name="searchParameters">Parameters to search in database</param>   
        /// <returns>
        /// A <see cref="List{SearchResponseEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<SearchResponseEntity>> SearchStudy(SearchParametersEntity searchParameters)
        {
            try
            {
                _logger.LogInformation($"Started Repository : {nameof(CommonRepository)}; Method : {nameof(SearchStudy)};");
                IMongoCollection<CommonStudyDefinitionsEntity> collection = _database.GetCollection<CommonStudyDefinitionsEntity>(Constants.Collections.StudyDefinitions);

                List<SearchResponseEntity> studies;

                if (searchParameters.Header?.ToLower() == "phase" || searchParameters.Header?.ToLower() == "sponsorid" || searchParameters.Header?.ToLower() == "interventionmodel" || searchParameters.Header?.ToLower() == "indication")
                {
                    studies = await collection.Aggregate()
                                              .Match(DataFilterCommon.GetFiltersForSearchStudy(searchParameters))
                                              .Project(x => new SearchResponseEntity
                                              {
                                                  StudyId = x.Study.StudyId,
                                                  StudyIdV4 = x.Study.Id,
                                                  StudyTitle = x.Study.StudyTitle,
                                                  StudyTitleV4 = x.Study.Versions != null ? x.Study.Versions.First().Titles : null,
                                                  StudyType = x.Study.StudyType,
                                                  StudyTypeV4 = x.Study.Versions != null ? x.Study.Versions.First().StudyType : null,
                                                  StudyPhase = x.Study.StudyPhase,
                                                  StudyPhaseV4 = x.Study.Versions != null ? x.Study.Versions.First().StudyPhase : null,
                                                  StudyIdentifiers = x.Study.StudyIdentifiers,
                                                  StudyIdentifiersV4 = x.Study.Versions != null ? x.Study.Versions.First().StudyIdentifiers : null,
                                                  InterventionModel = x.Study.StudyDesigns.Select(y => y.InterventionModel) ?? null,
                                                  InterventionModelV4 = x.Study.Versions != null ? x.Study.Versions.Select(x => x.StudyDesigns.Select(y => y.InterventionModel)) : null,
                                                  StudyIndications = x.Study.StudyDesigns.Select(y => y.StudyIndications.Select(z => z.IndicationDescription ?? z.IndicationDesc)) ?? null,
                                                  StudyIndicationsV4 = x.Study.Versions != null ? x.Study.Versions.Select(x => x.StudyDesigns.Select(y => y.Indications)) : null,
                                                  EntryDateTime = x.AuditTrail.EntryDateTime,
                                                  SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                  UsdmVersion = x.AuditTrail.UsdmVersion,
                                                  StudyDesignIds = x.Study.StudyDesigns.Select(x => x.StudyDesignId ?? x.Id) ?? null,
                                                  StudyDesignIdsV4 = x.Study.Versions != null ? x.Study.Versions.Select(y => y.StudyDesigns.Select(x => x.Id)) : null
                                              })
                                              .ToListAsync()
                                              .ConfigureAwait(false);
                }
                else
                {
                    studies = await collection.Aggregate()
                                              .Match(DataFilterCommon.GetFiltersForSearchStudy(searchParameters))
                                              .Project(x => new SearchResponseEntity
                                              {
                                                  StudyId = x.Study.StudyId,
                                                  StudyIdV4 = x.Study.Id,
                                                  StudyTitle = x.Study.StudyTitle,
                                                  StudyTitleV4 = x.Study.Versions != null ? x.Study.Versions.First().Titles : null,
                                                  StudyType = x.Study.StudyType,
                                                  StudyTypeV4 = x.Study.Versions != null ? x.Study.Versions.First().StudyType : null,
                                                  StudyPhase = x.Study.StudyPhase,
                                                  StudyPhaseV4 = x.Study.Versions != null ? x.Study.Versions.First().StudyPhase : null,
                                                  StudyIdentifiers = x.Study.StudyIdentifiers,
                                                  StudyIdentifiersV4 = x.Study.Versions != null ? x.Study.Versions.First().StudyIdentifiers : null,
                                                  InterventionModel = x.Study.StudyDesigns.Select(y => y.InterventionModel) ?? null,
                                                  InterventionModelV4 = x.Study.Versions != null ? x.Study.Versions.Select(x => x.StudyDesigns.Select(y => y.InterventionModel)) : null,
                                                  StudyIndications = x.Study.StudyDesigns.Select(y => y.StudyIndications.Select(z => z.IndicationDescription ?? z.IndicationDesc)) ?? null,
                                                  StudyIndicationsV4 = x.Study.Versions != null ? x.Study.Versions.Select(x => x.StudyDesigns.Select(y => y.Indications)) : null,
                                                  EntryDateTime = x.AuditTrail.EntryDateTime,
                                                  SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                  UsdmVersion = x.AuditTrail.UsdmVersion,
                                                  StudyDesignIds = x.Study.StudyDesigns.Select(x => x.StudyDesignId ?? x.Id) ?? null,
                                                  StudyDesignIdsV4 = x.Study.Versions != null ? x.Study.Versions.Select(y => y.StudyDesigns.Select(x => x.Id)) : null
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
        /// <returns>
        /// A <see cref="List{SearchResponseEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<Core.Entities.StudyV2.SearchResponseEntity>> SearchStudyV2(SearchParametersEntity searchParameters)
        {
            try
            {
                _logger.LogInformation($"Started Repository : {nameof(CommonRepository)}; Method : {nameof(SearchStudyV2)};");
                IMongoCollection<Core.Entities.StudyV2.StudyDefinitionsEntity> collection = _database.GetCollection<Core.Entities.StudyV2.StudyDefinitionsEntity>(Constants.Collections.StudyDefinitions);


                List<Core.Entities.StudyV2.SearchResponseEntity> studies = await collection.Aggregate()
                                              .Match(DataFilterCommon.GetFiltersForSearchV2(searchParameters))
                                              .Project(x => new Core.Entities.StudyV2.SearchResponseEntity
                                              {
                                                  StudyId = x.Study.StudyId,
                                                  StudyTitle = x.Study.StudyTitle,
                                                  StudyType = x.Study.StudyType,
                                                  StudyPhase = x.Study.StudyPhase,
                                                  StudyIdentifiers = x.Study.StudyIdentifiers,
                                                  InterventionModel = x.Study.StudyDesigns.Select(y => y.InterventionModel) ?? null,
                                                  StudyIndications = x.Study.StudyDesigns.Select(y => y.StudyIndications) ?? null,
                                                  EntryDateTime = x.AuditTrail.EntryDateTime,
                                                  SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                  UsdmVersion = x.AuditTrail.UsdmVersion,
                                                  StudyDesignIds = x.Study.StudyDesigns.Select(x => x.Id) ?? null,
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
        /// <summary>
        /// Search the collection based on search criteria
        /// </summary>
        /// <param name="searchParameters">Parameters to search in database</param>
        /// <returns>
        /// A <see cref="List{SearchResponseEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<Core.Entities.StudyV3.SearchResponseEntity>> SearchStudyV3(SearchParametersEntity searchParameters)
        {
            try
            {
                _logger.LogInformation($"Started Repository : {nameof(CommonRepository)}; Method : {nameof(SearchStudyV2)};");
                IMongoCollection<Core.Entities.StudyV3.StudyDefinitionsEntity> collection = _database.GetCollection<Core.Entities.StudyV3.StudyDefinitionsEntity>(Constants.Collections.StudyDefinitions);


                List<Core.Entities.StudyV3.SearchResponseEntity> studies = await collection.Aggregate()
                                              .Match(DataFilterCommon.GetFiltersForSearchV3(searchParameters))
                                              .Project(x => new Core.Entities.StudyV3.SearchResponseEntity
                                              {
                                                  StudyId = x.Study.StudyId,
                                                  StudyTitle = x.Study.StudyTitle,
                                                  StudyType = x.Study.StudyType,
                                                  StudyPhase = x.Study.StudyPhase,
                                                  StudyIdentifiers = x.Study.StudyIdentifiers,
                                                  InterventionModel = x.Study.StudyDesigns.Select(y => y.InterventionModel) ?? null,
                                                  StudyIndications = x.Study.StudyDesigns.Select(y => y.StudyIndications) ?? null,
                                                  EntryDateTime = x.AuditTrail.EntryDateTime,
                                                  SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                  UsdmVersion = x.AuditTrail.UsdmVersion,
                                                  StudyDesignIds = x.Study.StudyDesigns.Select(x => x.Id) ?? null,
                                              })
                                              .ToListAsync()
                                              .ConfigureAwait(false);

                return DataFilterCommon.SortSearchResultsV3(studies, searchParameters.Header, searchParameters.Asc)
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
        /// <summary>
        /// Search the collection based on search criteria
        /// </summary>
        /// <param name="searchParameters">Parameters to search in database</param>    
        /// <returns>
        /// A <see cref="List{SearchResponseEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<Core.Entities.StudyV4.SearchResponseEntity>> SearchStudyV4(SearchParametersEntity searchParameters)
        {
            try
            {
                _logger.LogInformation($"Started Repository : {nameof(CommonRepository)}; Method : {nameof(SearchStudyV2)};");
                IMongoCollection<Core.Entities.StudyV4.StudyDefinitionsEntity> collection = _database.GetCollection<Core.Entities.StudyV4.StudyDefinitionsEntity>(Constants.Collections.StudyDefinitions);


                List<Core.Entities.StudyV4.SearchResponseEntity> studies = await collection.Aggregate()
                                              .Match(DataFilterCommon.GetFiltersForSearchV4(searchParameters))
                                              .Project(x => new Core.Entities.StudyV4.SearchResponseEntity
                                              {
                                                  StudyId = x.Study.Id,
                                                  StudyTitle = x.Study.Versions != null ? x.Study.Versions.First().Titles : null,
                                                  StudyType = x.Study.Versions != null ? x.Study.Versions.First().StudyType : null,
                                                  StudyPhase = x.Study.Versions != null ? x.Study.Versions.First().StudyPhase : null,
                                                  StudyIdentifiers = x.Study.Versions != null ? x.Study.Versions.First().StudyIdentifiers : null,
                                                  InterventionModel = x.Study.Versions != null ? x.Study.Versions.Select(y => y.StudyDesigns.Select(x => x.InterventionModel)) : null,
                                                  StudyIndications = x.Study.Versions != null ? x.Study.Versions.Select(y => y.StudyDesigns.Select(x => x.Indications)) : null,
                                                  EntryDateTime = x.AuditTrail.EntryDateTime,
                                                  SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                  UsdmVersion = x.AuditTrail.UsdmVersion,
                                                  StudyDesignIds = x.Study.Versions != null ? x.Study.Versions.Select(x => x.StudyDesigns.Select(y => y.Id)) : null,
                                              })
                                              .ToListAsync()
                                              .ConfigureAwait(false);

                return DataFilterCommon.SortSearchResultsV4(studies, searchParameters.Header, searchParameters.Asc)
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

        /// <summary>
        /// Search the collection based on search criteria
        /// </summary>
        /// <param name="searchParameters">Parameters to search in database</param>
        /// <returns>
        /// A <see cref="List{SearchResponseEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<Core.Entities.StudyV5.SearchResponseEntity>> SearchStudyV5(SearchParametersEntity searchParameters)
        {
            try
            {
                _logger.LogInformation($"Started Repository : {nameof(CommonRepository)}; Method : {nameof(SearchStudyV2)};");
                IMongoCollection<Core.Entities.StudyV5.StudyDefinitionsEntity> collection = _database.GetCollection<Core.Entities.StudyV5.StudyDefinitionsEntity>(Constants.Collections.StudyDefinitions);


                List<Core.Entities.StudyV5.SearchResponseEntity> studies = await collection.Aggregate()
                                              .Match(DataFilterCommon.GetFiltersForSearchV5(searchParameters))
                                              .Project(x => new Core.Entities.StudyV5.SearchResponseEntity
                                              {
                                                  StudyId = x.Study.Id,
                                                  StudyTitle = x.Study.Versions != null ? x.Study.Versions.First().Titles : null,
                                                  StudyType = x.Study.Versions != null && x.Study.Versions.Any()
                                                                    && x.Study.Versions.First().StudyDesigns != null
                                                                    && x.Study.Versions.First().StudyDesigns.Any()
                                                                    ? x.Study.Versions.First().StudyDesigns.First().StudyType : null,
                                                  StudyPhase = x.Study.Versions != null && x.Study.Versions.Any()
                                                                    && x.Study.Versions.First().StudyDesigns != null
                                                                    && x.Study.Versions.First().StudyDesigns.Any()
                                                                    ? x.Study.Versions.First().StudyDesigns.First().StudyPhase : null,
                                                  StudyIdentifiers = x.Study.Versions != null ? x.Study.Versions.First().StudyIdentifiers : null,
                                                  StudyIndications = x.Study.Versions != null ? x.Study.Versions.Select(y => y.StudyDesigns.Select(x => x.Indications)) : null,
                                                  EntryDateTime = x.AuditTrail.EntryDateTime,
                                                  SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                  UsdmVersion = x.AuditTrail.UsdmVersion,
                                                  StudyDesignIds = x.Study.Versions != null ? x.Study.Versions.Select(x => x.StudyDesigns.Select(y => y.Id)) : null,
                                              })
                                              .ToListAsync()
                                              .ConfigureAwait(false);

                return DataFilterCommon.SortSearchResultsV5(studies, searchParameters.Header, searchParameters.Asc)
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
        /// <returns>
        /// A <see cref="List{SearchTitleResponseEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<SearchTitleResponseEntity>> SearchTitle(SearchTitleParametersEntity searchParameters)
        {
            try
            {
                _logger.LogInformation($"Started Repository : {nameof(CommonRepository)}; Method : {nameof(SearchTitle)};");
                IMongoCollection<CommonStudyDefinitionsEntity> collection = _database.GetCollection<CommonStudyDefinitionsEntity>(Constants.Collections.StudyDefinitions);

                List<SearchTitleResponseEntity> studies = new();
                List<SearchTitleResponseEntity> studiesV4 = new();

                studies = await collection.Aggregate()
                                              .Match(DataFilterCommon.GetFiltersForSearchTitle(searchParameters))
                                              .Project(x => new SearchTitleResponseEntity
                                              {
                                                  StudyId = x.Study.StudyId,
                                                  StudyTitle = x.Study.StudyTitle,
                                                  StudyIdentifiers = x.Study.StudyIdentifiers,
                                                  StudyType = x.Study.StudyType,
                                                  EntryDateTime = x.AuditTrail.EntryDateTime,
                                                  SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                  StudyDesignIds = x.Study.StudyDesigns.Select(x => x.StudyDesignId ?? x.Id) ?? null,
                                                  UsdmVersion = x.AuditTrail.UsdmVersion
                                              })
                                              .ToListAsync()
                                              .ConfigureAwait(false);

                studiesV4 = await collection.Aggregate()
                                              .Match(DataFilterCommon.GetFiltersForSearchTitleV4(searchParameters))
                                              .Project(x => new SearchTitleResponseEntity
                                              {
                                                  StudyId = x.Study.Id,
                                                  StudyTitle = x.Study.Versions != null ? x.Study.Versions.First().Titles : null,
                                                  StudyIdentifiers = x.Study.Versions != null ? x.Study.Versions.First().StudyIdentifiers : null,
                                                  StudyType = x.Study.Versions != null ? x.Study.Versions.First().StudyType : null,
                                                  EntryDateTime = x.AuditTrail.EntryDateTime,
                                                  SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                  StudyDesignIdsV4 = x.Study.Versions != null ? x.Study.Versions.Select(x => x.StudyDesigns.Select(y => y.Id)) : null,
                                                  UsdmVersion = x.AuditTrail.UsdmVersion
                                              })
                                              .ToListAsync()
                                              .ConfigureAwait(false);
                studies.AddRange(studiesV4);
                return studies;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(CommonRepository)}; Method : {nameof(SearchTitle)};");
            }
        }
        #endregion

        #endregion
    }
}
