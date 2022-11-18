using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Entities.StudyV1;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.DataAccess.Filters;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.DataAccess.Repositories
{
    public class ClinicalStudyRepositoryV1 : IClinicalStudyRepositoryV1
    {
        #region Variables     
        private readonly string _databaseName = Config.DatabaseName;
        private readonly ILogHelper _logger;

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        #endregion

        #region Constructor      
        public ClinicalStudyRepositoryV1(IMongoClient client, ILogHelper logger)
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
        /// A <see cref="StudyEntity"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<StudyEntity> GetStudyItemsAsync(string studyId, int sdruploadversion)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(GetStudyItemsAsync)};");
            try
            {
                IMongoCollection<StudyEntity> collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyV1);
                

                StudyEntity study = await collection.Find(DataFiltersV1.GetFiltersForGetStudy(studyId, sdruploadversion))
                                                     .SortByDescending(s => s.AuditTrail.EntryDateTime) // Sort by descending on entryDateTime
                                                     .Limit(1)                  //Taking top 1 result
                                                     .SingleOrDefaultAsync().ConfigureAwait(false);                

                if (study == null)
                {
                    _logger.LogWarning($"There is no study with StudyId : {studyId} in {Constants.Collections.StudyV1} Collection");
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
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(GetStudyItemsAsync)};");
            }
        }

        /// <summary>
        /// GET Study Designs for a Study Id
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <returns></returns>
        public async Task<StudyEntity> GetPartialStudyDesignItemsAsync(string studyId, int sdruploadversion)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(GetPartialStudyDesignItemsAsync)};");
            try
            {
                IMongoCollection<StudyEntity> collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyV1);


                StudyEntity study = await collection.Find(DataFiltersV1.GetFiltersForGetStudy(studyId, sdruploadversion))
                                                     .Project<StudyEntity>(DataFiltersV1.GetProjectionForPartialStudyDesignElementsFullStudy())
                                                     .SortByDescending(s => s.AuditTrail.EntryDateTime) // Sort by descending on entryDateTime
                                                     .Limit(1)                  //Taking top 1 result                                                     
                                                     .FirstOrDefaultAsync().ConfigureAwait(false);

                if (study == null)
                {
                    _logger.LogWarning($"There is no study with StudyId : {studyId} in {Constants.Collections.Study} Collection");
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
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(GetPartialStudyDesignItemsAsync)};");
            }
        }
        /// <summary>
        /// GET List of study for a study ID
        /// </summary>
        /// <param name="fromDate">Start Date for Date Filter</param>
        /// <param name="toDate">End Date for Date Filter</param>
        /// <param name="studyId">Study ID</param>
        /// <returns>
        /// A <see cref="List{StudyEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<AuditTrailResponseEntity>> GetAuditTrail(string studyId, DateTime fromDate, DateTime toDate)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(GetAuditTrail)};");
            try
            {
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyV1);
                List<AuditTrailResponseEntity> auditTrails = new List<AuditTrailResponseEntity>();
                auditTrails = await collection.Find(DataFiltersV1.GetFiltersForGetAudTrail(studyId,fromDate,toDate)) // Condition for matching studyId and date range
                                                  .Project(x=> new AuditTrailResponseEntity
                                                  {
                                                      StudyType = x.ClinicalStudy.StudyType,
                                                      EntryDateTime = x.AuditTrail.EntryDateTime,
                                                      SDRUploadVersion = x.AuditTrail.SDRUploadVersion
                                                  })
                                                  .SortByDescending(s => s.AuditTrail.EntryDateTime) // Sort by descending on entryDateTime
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
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(GetAuditTrail)};");
            }
        }

        /// <summary>
        /// Get List of all studyId 
        /// </summary>
        /// <param name="fromDate">Start Date for Date Filter</param>
        /// <param name="toDate">End Date for Date Filter</param>
        /// <param name="studyTitle">Study Title Filter</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="List{StudyHistoryEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<StudyHistoryResponseEntity>> GetStudyHistory(DateTime fromDate, DateTime toDate, string studyTitle, LoggedInUser user)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(GetStudyHistory)};");
            try
            {
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyV1);               

                List<StudyHistoryResponseEntity> studyHistories = await collection.Aggregate()
                                                        .Match(DataFiltersV1.GetFiltersForStudyHistory(fromDate,toDate,studyTitle)) // Condition for matching date range
                                                        .Project(x =>
                                                                new StudyHistoryResponseEntity
                                                                {
                                                                    Uuid = x.ClinicalStudy.Uuid,
                                                                    StudyTitle = x.ClinicalStudy.StudyTitle,
                                                                    SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                                    StudyIdentifiers = x.ClinicalStudy.StudyIdentifiers,
                                                                    EntryDateTime = x.AuditTrail.EntryDateTime,
                                                                    StudyType = x.ClinicalStudy.StudyType,
                                                                    ProtocolVersions = x.ClinicalStudy.StudyProtocolVersions.Select(x=>x.ProtocolVersion),
                                                                    StudyVersion = x.ClinicalStudy.StudyVersion
                                                                })  //Project only the required fields                                                        
                                                        .ToListAsync().ConfigureAwait(false);

                studyHistories = GroupFilterForStudyHistory(studyHistories, user);

                if (studyHistories.Count() == 0)
                {
                    _logger.LogWarning($"There are no Study in {Constants.Collections.StudyV1} Collection");
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
        public List<StudyHistoryResponseEntity> GroupFilterForStudyHistory(List<StudyHistoryResponseEntity> studyHistoryEntities, LoggedInUser user)
        {
            if (user.UserRole != Constants.Roles.Org_Admin && Config.isGroupFilterEnabled)
            {
                var groups = GetGroupsOfUser(user).Result;

                if (groups != null && groups.Count > 0)
                {
                    Tuple<List<string>, List<string>> groupFilters = GroupFilters.GetGroupFilters(groups);

                    if (groupFilters.Item1.Contains(Constants.StudyType.ALL.ToLower()))
                        return studyHistoryEntities;

                    studyHistoryEntities = studyHistoryEntities.Where(x => groupFilters.Item1.Contains(x.StudyType?.Decode?.ToLower()) || groupFilters.Item2.Contains(x.Uuid)).ToList();
                }
                else
                {
                    // Filter should not give any results
                    studyHistoryEntities = studyHistoryEntities.Where(x => 1 == 0).ToList();
                }
            }
            return studyHistoryEntities;
        }

        #endregion

        #region POST Data  
        /// <summary>
        /// POST a Study
        /// </summary>
        /// <param name="study">Study for Inserting into Database</param>
        /// <returns>
        /// A studyId which was inserted <br></br> <br></br>        
        /// </returns>
        public async Task<string> PostStudyItemsAsync(StudyEntity study)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(PostStudyItemsAsync)};");
            try
            {
                IMongoCollection<StudyEntity> collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyV1);                
                await collection.InsertOneAsync(study).ConfigureAwait(false); //Insert One Document

                return (study.ClinicalStudy.Uuid);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(PostStudyItemsAsync)};");
            }
        }
        #endregion

        #region Update Study Data

        /// <summary>
        /// Updates a Study
        /// </summary>
        /// <param name="study">Update study in database</param>
        /// <returns>
        /// A studyId which was inserted <br></br> <br></br>        
        /// </returns>
        public async Task<string> UpdateStudyItemsAsync(StudyEntity study)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(UpdateStudyItemsAsync)};");
            try
            {
                IMongoCollection<StudyEntity> collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyV1);
                UpdateDefinition<StudyEntity> updateDefinition = Builders<StudyEntity>.Update
                                    .Set(s => s.ClinicalStudy, study.ClinicalStudy)
                                    .Set(s => s.AuditTrail, study.AuditTrail);
                await collection.UpdateOneAsync(x => (x.ClinicalStudy.Uuid == study.ClinicalStudy.Uuid
                                                   && x.AuditTrail.SDRUploadVersion == study.AuditTrail.SDRUploadVersion), //Match studyId and studyVersion
                                                   updateDefinition).ConfigureAwait(false); // Update clinicalStudy and auditTrail

                return (study.ClinicalStudy.Uuid);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(UpdateStudyItemsAsync)};");
            }
        }
        #endregion

        #region Search
        /// <summary>
        /// Search the collection based on search criteria
        /// </summary>
        /// <param name="searchParameters">Parameters to search in database</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="List{SearchResponseEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<SearchResponseEntity>> SearchStudy(SearchParameters searchParameters, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(SearchStudy)};");
                IMongoCollection<StudyEntity> collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyV1);
                
                List<SearchResponseEntity> studies = await collection.Aggregate()
                                              .Match(DataFiltersV1.GetFiltersForSearchStudy(searchParameters))
                                              .Project(x => new SearchResponseEntity
                                              {
                                                  Uuid = x.ClinicalStudy.Uuid,
                                                  StudyTitle = x.ClinicalStudy.StudyTitle,
                                                  StudyType = x.ClinicalStudy.StudyType,
                                                  StudyPhase = x.ClinicalStudy.StudyPhase,
                                                  StudyIdentifiers = x.ClinicalStudy.StudyIdentifiers,
                                                  InterventionModel = x.ClinicalStudy.StudyDesigns.Select(y=>y.InterventionModel) ?? null,
                                                  StudyIndications = x.ClinicalStudy.StudyDesigns.Select(y=>y.StudyIndications) ?? null,
                                                  EntryDateTime = x.AuditTrail.EntryDateTime,
                                                  SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                              })                                                 
                                              .ToListAsync()
                                              .ConfigureAwait(false);
                List<SearchResponseEntity> studiesAfterGroupFilter = await GroupFilterForSearch(studies, user);

                List<SearchResponseEntity> sortedResults = SortSearchResults(studiesAfterGroupFilter,searchParameters.Header,searchParameters.Asc) // Sort the data based on input
                                                            .Skip((searchParameters.PageNumber - 1) * searchParameters.PageSize) //page number
                                                            .Take(searchParameters.PageSize) //Number of documents per page
                                                            .ToList();

                return sortedResults;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(SearchStudy)};");
            }
        }

        public IEnumerable<SearchResponseEntity> SortSearchResults(List<SearchResponseEntity> searchResponses, string property, bool asc)
        {
            try
            {
                _logger.LogInformation($"Started Method : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(SortSearchResults)};");

                if (!String.IsNullOrWhiteSpace(property))
                {
                    return property.ToLower() switch
                    {
                        //Sort by studyTitle
                        "studytitle" => asc ? searchResponses.OrderBy(s => s.StudyTitle) : searchResponses.OrderByDescending(s => s.StudyTitle),

                        //Sort by studyIdentifier: orgCode
                        "sponsorid" => asc ? searchResponses.OrderBy(s => s.StudyIdentifiers != null ? s.StudyIdentifiers.FindAll(x => x.StudyIdentifierScope?.OrganisationType?.Decode == Constants.IdType.SPONSOR_ID_V1).Any() ? s.StudyIdentifiers.Find(x => x.StudyIdentifierScope?.OrganisationType?.Decode == Constants.IdType.SPONSOR_ID_V1).StudyIdentifierScope.OrganisationIdentifier ?? "" : "" : "")
                                                                                        : searchResponses.OrderByDescending(s => s.StudyIdentifiers != null ? s.StudyIdentifiers.FindAll(x => x.StudyIdentifierScope?.OrganisationType?.Decode == Constants.IdType.SPONSOR_ID_V1).Any() ? s.StudyIdentifiers.Find(x => x.StudyIdentifierScope?.OrganisationType?.Decode == Constants.IdType.SPONSOR_ID_V1).StudyIdentifierScope.OrganisationIdentifier ?? "" : "" : ""),

                        //Sort by studyIndication: description
                        "indication" => asc ? searchResponses.OrderBy(s => (s.StudyIndications != null && s.StudyIndications.Any()) ? (s.StudyIndications.First() != null && s.StudyIndications.First().Any()) ? s.StudyIndications.First().First() != null ? s.StudyIndications.First().First().IndicationDesc ?? "" : "" : "" : "")
                                                                                        : searchResponses.OrderByDescending(s => (s.StudyIndications != null && s.StudyIndications.Any()) ? (s.StudyIndications.First() != null && s.StudyIndications.First().Any()) ? s.StudyIndications.First().First() != null ? s.StudyIndications.First().First().IndicationDesc ?? "" : "" : "" : ""),

                        //Sort by studyDesign: Intervention Model
                        "interventionmodel" => asc ? searchResponses.OrderBy(s => (s.InterventionModel != null && s.InterventionModel.Any()) ? (s.InterventionModel.First() != null && s.InterventionModel.First().Any()) ? s.InterventionModel.First().First() != null ? s.InterventionModel.First().First().Decode ?? "" : "" : "" : "")
                                                               : searchResponses.OrderByDescending(s => (s.InterventionModel != null && s.InterventionModel.Any()) ? (s.InterventionModel.First() != null && s.InterventionModel.First().Any()) ? s.InterventionModel.First().First() != null ? s.InterventionModel.First().First().Decode ?? "" : "" : "" : ""),

                        //Sort by studyPhase
                        "phase" => asc ? searchResponses.OrderBy(s => s.StudyPhase != null ? s.StudyPhase.Decode ?? "" : "") : searchResponses.OrderByDescending(s => s.StudyPhase != null ? s.StudyPhase.Decode ?? "" : ""),
                       
                        //Sort by SDR version
                        "sdrversion" => asc ? searchResponses.OrderBy(s => s.SDRUploadVersion) : searchResponses.OrderByDescending(s => s.SDRUploadVersion),                        

                        //Sort by entryDateTime
                        "lastmodifieddate" => asc ? searchResponses.OrderBy(s => s.EntryDateTime) : searchResponses.OrderByDescending(s => s.EntryDateTime),

                        //Sort by entrySystem Descending by default
                        _ => asc ? searchResponses.OrderBy(s => s.EntryDateTime) : searchResponses.OrderByDescending(s => s.EntryDateTime),
                    };
                }
                else
                {
                    return asc ? searchResponses.OrderBy(s => s.EntryDateTime) : searchResponses.OrderByDescending(s => s.EntryDateTime);
                }


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Method : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(SortSearchResults)};");
            }
        }

        public async Task<List<SearchResponseEntity>> GroupFilterForSearch(List<SearchResponseEntity> searchResponses, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Method : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(GroupFilterForSearch)};");

                if (user.UserRole != Constants.Roles.Org_Admin && Config.isGroupFilterEnabled)
                {
                    List<SDRGroupsEntity> groups = await GetGroupsOfUser(user);
                    
                    if (groups != null && groups.Count > 0)
                    {
                        Tuple<List<string>, List<string>> groupFilters = GroupFilters.GetGroupFilters(groups);
                        if (groupFilters.Item1.Contains(Constants.StudyType.ALL.ToLower()))
                            return searchResponses;  
                        searchResponses = searchResponses.Where(x => groupFilters.Item1.Contains(x.StudyType?.Decode?.ToLower()) || groupFilters.Item2.Contains(x.Uuid)).ToList();                        
                    }
                    else
                    {
                        // Filter should not give any results
                        searchResponses = searchResponses.Where(x => 1 == 0).ToList();
                    }
                }


                return searchResponses;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Method : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(GroupFilterForSearch)};");
            }
        }


        /// <summary>
        /// Search the collection based on search criteria
        /// </summary>
        /// <param name="searchParameters">Parameters to search in database</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="List{SearchResponseEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<SearchResponseEntity>> SearchTitle(SearchTitleParameters searchParameters, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(SearchTitle)};");
                IMongoCollection<StudyEntity> collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyV1);

                List<SearchResponseEntity> studies = await collection.Aggregate()
                                              .Match(DataFiltersV1.GetFiltersForSearchTitle(searchParameters))
                                              .Project(x => new SearchResponseEntity
                                              {
                                                  Uuid = x.ClinicalStudy.Uuid,
                                                  StudyTitle = x.ClinicalStudy.StudyTitle,
                                                  StudyType = x.ClinicalStudy.StudyType,                                                  
                                                  StudyIdentifiers = x.ClinicalStudy.StudyIdentifiers,                                                 
                                                  EntryDateTime = x.AuditTrail.EntryDateTime,
                                                  SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                              })
                                              .ToListAsync()
                                              .ConfigureAwait(false);
                List<SearchResponseEntity> studiesAfterGroupFilter = await GroupFilterForSearch(studies, user);

                return studiesAfterGroupFilter;
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

        #region Get only studyType
        public async Task<StudyEntity> GetStudyItemsForCheckingAccessAsync(string studyId, int sdruploadversion)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(GetStudyItemsForCheckingAccessAsync)};");
            try
            {
                IMongoCollection<StudyEntity> collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyV1);


                StudyEntity study = await collection.Find(DataFiltersV1.GetFiltersForGetStudy(studyId, sdruploadversion))
                                                     .SortByDescending(s => s.AuditTrail.EntryDateTime) // Sort by descending on entryDateTime
                                                     .Project<StudyEntity>(DataFiltersV1.GetProjectionForCheckAccessForAStudy())
                                                     .Limit(1)                  //Taking top 1 result
                                                     .SingleOrDefaultAsync().ConfigureAwait(false);

                if (study == null)
                {
                    _logger.LogWarning($"There is no study with StudyId : {studyId} in {Constants.Collections.Study} Collection");
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
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepositoryV1)}; Method : {nameof(GetStudyItemsForCheckingAccessAsync)};");
            }
        }
        #endregion
        #endregion
    }
}
