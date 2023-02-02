using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
    public  class CommonRepository : ICommonRepository
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
            var conventionPack = new ConventionPack();
            conventionPack.Add(new CamelCaseElementNameConvention());
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
                IMongoCollection<GetRawJsonEntity> collection = _database.GetCollection<GetRawJsonEntity>(Constants.Collections.StudyDefinitions);

                GetRawJsonEntity study = await collection.Find(DataFilterCommon.GetFiltersForGetStudy(studyId, sdruploadversion))
                                                     .SortByDescending(s => s.AuditTrail.EntryDateTime) // Sort by descending on entryDateTime
                                                     .Limit(1)                  //Taking top 1 result
                                                     .SingleOrDefaultAsync().ConfigureAwait(false);

               
                if (study == null)
                {
                    _logger.LogWarning($"There is no study with StudyId : {studyId} in {Constants.Collections.StudyDefinitions} Collection");
                    return null;
                }
                else
                {
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
                                                     .Project(x=>x.AuditTrail.UsdmVersion)
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

                if (studyHistories.Count() == 0)
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
        public async Task<List<SearchResponseEntity>> SearchStudy(SearchParametersEntity searchParameters,LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Repository : {nameof(CommonRepository)}; Method : {nameof(SearchStudy)};");
                IMongoCollection<CommonStudyEntity> collection = _database.GetCollection<CommonStudyEntity>(Constants.Collections.StudyDefinitions);

                List<SearchResponseEntity> studies;

                if (searchParameters.Header == "studyphase" || searchParameters.Header == "sponsorid" || searchParameters.Header == "interventionmodel" || searchParameters.Header == "indication")
                {                    
                    studies = await collection.Aggregate()
                                              .Match(DataFilterCommon.GetFiltersForSearchStudy(searchParameters, GetGroupsOfUser(user).Result,user))
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
                                              .Match(DataFilterCommon.GetFiltersForSearchStudy(searchParameters, GetGroupsOfUser(user).Result,user))
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
                _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(SearchTitle)};");
                IMongoCollection<CommonStudyEntity> collection = _database.GetCollection<CommonStudyEntity>(Constants.Collections.StudyDefinitions);

                List<SearchTitleResponseEntity> studies = await collection.Aggregate()
                                                                          .Match(DataFilterCommon.GetFiltersForSearchTitle(searchParameters))
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
                                                               .Where(x => x.groupEnabled == true)
                                                               .Where(x => x.users != null)
                                                               .Where(x => x.users.Any(x => (x.email == user.UserName && x.isActive == true)))
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
        #endregion
    }
}
