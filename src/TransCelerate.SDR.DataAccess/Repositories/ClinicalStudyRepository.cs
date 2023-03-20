using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities;
using TransCelerate.SDR.Core.Entities.Study;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Enums;
using TransCelerate.SDR.DataAccess.Interfaces;

namespace TransCelerate.SDR.DataAccess.Repositories
{
    public class ClinicalStudyRepository : IClinicalStudyRepository
    {

        #region Variables     
        private readonly string _databaseName = Config.DatabaseName;
        private readonly ILogHelper _logger;

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        #endregion

        #region Constructor      
        public ClinicalStudyRepository(IMongoClient client, ILogHelper logger)
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

        #region GET Data

        /// <summary>
        /// GET a Study for a study ID with version filter
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="version">Version of study</param>
        /// <returns>
        /// A <see cref="StudyEntity"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<StudyEntity> GetStudyItemsAsync(string studyId, int version)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(GetStudyItemsAsync)};");
            try
            {
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyDefinitions);
                StudyEntity _study;
                if (version == 0)
                {
                    _study = await collection.Find(s => s.ClinicalStudy.StudyId == studyId && s.AuditTrail.UsdmVersion == Constants.USDMVersions.MVP)  // Condition for matching studyId
                                             .SortByDescending(s => s.AuditTrail.EntryDateTime) // Sort by descending on entryDateTime
                                             .Limit(1)                  //Taking top 1 result
                                             .SingleOrDefaultAsync().ConfigureAwait(false);

                }
                else
                {
                    _study = await collection.Find(s =>
                                                (s.ClinicalStudy.StudyId == studyId
                                                 && s.AuditTrail.StudyVersion == version && s.AuditTrail.UsdmVersion == Constants.USDMVersions.MVP)) // Condition for matching studyId and version
                                             .SortByDescending(s => s.AuditTrail.EntryDateTime)  // Sort by descending on entryDateTime
                                             .Limit(1)                  //Taking top 1 result
                                             .SingleOrDefaultAsync().ConfigureAwait(false);
                }

                if (_study == null)
                {
                    _logger.LogWarning($"There is no study with StudyId : {studyId} in {Constants.Collections.StudyDefinitions} Collection");
                    return null;
                }
                else
                {
                    return _study;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(GetStudyItemsAsync)};");
            }
        }


        /// <summary>
        /// GET a Study for a study ID with version and tag filter
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="version">Version of study</param>
        /// <param name="tag">Tag of a study</param>
        /// <returns>
        /// A <see cref="StudyEntity"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<StudyEntity> GetStudyItemsAsync(string studyId, int version, string tag)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(GetStudyItemsAsync)};");
            try
            {
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyDefinitions);
                StudyEntity study = new();
                if (version == 0)
                {
                    study = await collection.Find(s =>
                                                (s.ClinicalStudy.StudyId == studyId)
                                                && s.ClinicalStudy.StudyTag == tag && s.AuditTrail.UsdmVersion == Constants.USDMVersions.MVP)   // Condition for matching studyId and studyTag
                                             .SortByDescending(s => s.AuditTrail.EntryDateTime) // Sort by descending on entryDateTime
                                             .Limit(1)                  //Taking top 1 result
                                             .SingleOrDefaultAsync().ConfigureAwait(false);
                }
                else
                {
                    study = await collection.Find(s =>
                                                (s.ClinicalStudy.StudyId == studyId
                                                 && s.ClinicalStudy.StudyTag == tag
                                                 && s.AuditTrail.StudyVersion == version && s.AuditTrail.UsdmVersion == Constants.USDMVersions.MVP)) // Condition for matching studyId, version and studyTag
                                             .SortByDescending(s => s.AuditTrail.EntryDateTime) // Sort by descending on entryDateTime
                                             .Limit(1)               //Taking top 1 result    
                                             .SingleOrDefaultAsync().ConfigureAwait(false);

                }

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
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(GetStudyItemsAsync)};");
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
        public async Task<List<StudyEntity>> GetAuditTrail(DateTime fromDate, DateTime toDate, string studyId)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(GetAuditTrail)};");
            try
            {
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.Study);
                List<StudyEntity> studies = new();
                studies = await collection.Find(s =>
                                                  (s.ClinicalStudy.StudyId == studyId)
                                                  && s.AuditTrail.EntryDateTime >= fromDate
                                                  && s.AuditTrail.EntryDateTime <= toDate) // Condition for matching studyId and date range
                                                  .SortByDescending(s => s.AuditTrail.EntryDateTime) // Sort by descending on entryDateTime
                                                  .ToListAsync().ConfigureAwait(false);

                if (studies.Count == 0)
                {
                    _logger.LogWarning($"There is no study with StudyId : {studyId} in {Constants.Collections.Study} Collection");
                    return null;
                }
                else
                {
                    return studies;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(GetAuditTrail)};");
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
        public async Task<List<StudyHistoryEntity>> GetAllStudyId(DateTime fromDate, DateTime toDate, string studyTitle, LoggedInUser user)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(GetAllStudyId)};");
            try
            {
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.Study);
                var builder = Builders<StudyEntity>.Filter;
                var filter = builder.Empty;
                filter &= builder.Where(x => x.AuditTrail.EntryDateTime >= fromDate
                                         && x.AuditTrail.EntryDateTime <= toDate);
                if (!String.IsNullOrEmpty(studyTitle))
                    filter &= builder.Where(x => x.ClinicalStudy.StudyTitle.ToLower().Contains(studyTitle.ToLower()));

                List<StudyHistoryEntity> studyHistories = await collection
                                                        .Find(filter) // Condition for matching date range
                                                        .Project(x =>
                                                                new StudyHistoryEntity
                                                                {
                                                                    StudyId = x.ClinicalStudy.StudyId,
                                                                    StudyTitle = x.ClinicalStudy.StudyTitle,
                                                                    StudyVersion = x.AuditTrail.StudyVersion,
                                                                    EntryDateTime = x.AuditTrail.EntryDateTime,
                                                                    StudyType = x.ClinicalStudy.StudyType,
                                                                    UsdmVersion = x.AuditTrail.UsdmVersion
                                                                })  //Project only the required fields
                                                        .SortByDescending(s => s.AuditTrail.EntryDateTime)  // Sort by descending on entryDateTime
                                                        .ToListAsync().ConfigureAwait(false);

                studyHistories = GroupFilterForStudyHistory(studyHistories, user);

                if (studyHistories.Count == 0)
                {
                    _logger.LogWarning($"There are no Study in {Constants.Collections.Study} Collection");
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
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(GetAllStudyId)};");
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
        public async Task<List<SearchResponse>> SearchStudy(SearchParameters searchParameters, LoggedInUser user)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(SearchStudy)};");
            try
            {
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.Study);

                var filteredResult = await collection
                                                 .Aggregate()
                                                 .Match(Filter(searchParameters))
                                                 .Project(x => new SearchResponse
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
                                                     UsdmVersion = x.AuditTrail.UsdmVersion
                                                 })
                                                 .ToListAsync().ConfigureAwait(false);

                var groupResult = filteredResult.GroupBy(x => new { x.StudyId, x.StudyTag })
                                                .Select(g => new
                                                {
                                                    g.Key.StudyId,
                                                    g.Key.StudyTag,
                                                    studyVersion = g.Max(x => x.StudyVersion)
                                                }).ToList();
                var finalGroupResult = (from filter in filteredResult
                                        join grp in groupResult
                                        on (filter.StudyId, filter.StudyVersion) equals (grp.StudyId, grp.studyVersion)
                                        select filter)
                            .ToList();

                var searchResults = LinqFilter(finalGroupResult, searchParameters, user);

                var sortedList = ApplyOrderBy(searchResults, searchParameters.Header, searchParameters.Asc) // Sort the data based on input
                                           .Skip((searchParameters.PageNumber - 1) * searchParameters.PageSize) //page number
                                           .Take(searchParameters.PageSize) //Number of documents per page
                                           .ToList();


                if (sortedList.Count != 0)
                {
                    return sortedList;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(SearchStudy)};");
            }
        }

        public List<SearchResponse> LinqFilter(List<SearchResponse> searchResults, SearchParameters searchParameters, LoggedInUser user)
        {
            #region StudyFilters
            if (!String.IsNullOrWhiteSpace(searchParameters.StudyId))
            {
                searchResults = searchResults.Where(x => x.StudyIdentifiers.Any(x => x.OrgCode.ToLower().Contains(searchParameters.StudyId.ToLower()))).ToList();
            }
            //Filter for studyTitle
            if (!String.IsNullOrWhiteSpace(searchParameters.StudyTitle))
            {
                searchResults = searchResults.Where(x => x.StudyTitle.ToLower().Contains(searchParameters.StudyTitle.ToLower())).ToList();
            }
            //Filter for studyIndication: description
            if (!String.IsNullOrWhiteSpace(searchParameters.Indication))
            {
                searchResults = searchResults.Where(x => x.StudyIndications != null && x.StudyIndications.Any() && x.StudyIndications.Any(x => x.Any(x => x.Description != null && x.Description.ToLower().Contains(searchParameters.Indication.ToLower())))).ToList();
            }
            //Filter for studyDesign: InvestigationalIntervention: Intervention Model
            if (!String.IsNullOrWhiteSpace(searchParameters.InterventionModel))
            {
                searchResults = searchResults.Where(x => x.InvestigationalInterventions != null && x.InvestigationalInterventions.Any() && x.InvestigationalInterventions.Any(x => x.Any(x => x.Any(x => x.Any(x => x.InterventionModel != null && x.InterventionModel.ToLower().Contains(searchParameters.InterventionModel.ToLower())))))).ToList();
            }
            //Filter for studyPhase
            if (!String.IsNullOrWhiteSpace(searchParameters.Phase))
            {
                searchResults = searchResults.Where(x => x.StudyPhase.ToLower().Contains(searchParameters.Phase.ToLower())).ToList();
            }
            #endregion

            #region GroupFilters
            if (user.UserRole != Constants.Roles.Org_Admin && Config.IsGroupFilterEnabled)
            {
                var groups = GetGroupsOfUser(user).Result;

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
                    if (studyTypeFilterValues.Contains(Constants.StudyType.ALL.ToLower()))
                        return searchResults;
                    searchResults = searchResults.Where(x => studyTypeFilterValues.Contains(x.StudyType.ToLower()) || studyIdFilterValues.Contains(x.StudyId)).ToList();
                }
                else
                {
                    // Filter should not give any results
                    searchResults = searchResults.Where(x => 1 == 0).ToList();
                }
            }
            #endregion

            return searchResults;
        }
        public SortDefinition<SearchResponse> SortSearchResult(string property, bool asc)
        {
            try
            {
                var builder = Builders<SearchResponse>.Sort;

                _logger.LogInformation($"Entered : {nameof(ClinicalStudyRepository)}; Method : {nameof(SortSearchResult)};");
                if (!String.IsNullOrWhiteSpace(property))
                {
                    return property.ToLower() switch
                    {
                        //Sort by studyTitle
                        "studytitle" => asc ? builder.Ascending(s => s.StudyTitle) : builder.Descending(s => s.StudyTitle),

                        //Sort by studyIdentifier: orgCode
                        "sponsorid" => asc ? builder.Ascending(s => s.StudyIdentifiers.Any(x => x.IdType == Constants.IdType.SPONSOR_ID) ? s.StudyIdentifiers.Where(x => x.IdType == Constants.IdType.SPONSOR_ID).First().OrgCode ?? "" : "")
                                                                                        : builder.Descending(s => s.StudyIdentifiers.Any(x => x.IdType == Constants.IdType.SPONSOR_ID) ? s.StudyIdentifiers.Where(x => x.IdType == Constants.IdType.SPONSOR_ID).First().OrgCode ?? "" : ""),

                        //Sort by studyIndication: description
                        "indication" => asc ? builder.Ascending(s => s.StudyIndications.Any() ? s.StudyIndications.First().Count > 0 ? s.StudyIndications.First().First().Description ?? "" : "" : "")
                                                                                        : builder.Descending(s => s.StudyIndications.Any() ? s.StudyIndications.First().Count > 0 ? s.StudyIndications.First().First().Description ?? "" : "" : ""),

                        "interventionmodel" => asc ? builder.Ascending(s => s.InvestigationalInterventions.First().First().First().First().InterventionModel)
                                                        : builder.Descending(s => s.InvestigationalInterventions.First().First().First().First().InterventionModel),

                        //Sort by studyPhase
                        "phase" => asc ? builder.Ascending(s => s.StudyPhase ?? "") : builder.Descending(s => s.StudyPhase ?? ""),

                        //Sort by entrySystem
                        "lastmodifiedbysystem" => asc ? builder.Ascending(s => s.EntrySystem ?? "") : builder.Descending(s => s.EntrySystem ?? ""),

                        //Sort by studyTag
                        "tag" => asc ? builder.Ascending(s => s.StudyTag ?? "") : builder.Descending(s => s.StudyTag ?? ""),

                        //Sort by SDR version
                        "sdrversion" => asc ? builder.Ascending(s => s.StudyVersion) : builder.Descending(s => s.StudyVersion),

                        //Sort by studyStatus
                        "status" => asc ? builder.Ascending(s => s.StudyStatus ?? "") : builder.Descending(s => s.StudyStatus ?? ""),

                        //Sort by entryDateTime
                        "lastmodifieddate" => asc ? builder.Ascending(s => s.EntryDateTime) : builder.Descending(s => s.EntryDateTime),

                        //Sort by entrySystem Descending by 
                        _ => asc ? builder.Ascending(s => s.EntryDateTime) : builder.Descending(s => s.EntryDateTime),
                    };
                }
                else
                {
                    return asc ? builder.Ascending(s => s.EntryDateTime) : builder.Descending(s => s.EntryDateTime);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended : {nameof(ClinicalStudyRepository)}; Method : {nameof(SortSearchResult)};");
            }
        }


        /// <summary>
        /// Search the collection based on search criteria
        /// </summary>
        /// <param name="searchParameters">Parameters to search in database</param>
        /// <returns>
        /// A <see cref="FilterDefinition{StudyEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public static FilterDefinition<StudyEntity> Filter(SearchParameters searchParameters)
        {
            var builder = Builders<StudyEntity>.Filter;
            var filter = builder.Empty;

            //Filter for Date Range
            filter &= builder.Where(x => x.AuditTrail.EntryDateTime >= searchParameters.FromDate
                                         && x.AuditTrail.EntryDateTime <= searchParameters.ToDate);
            return filter;
        }

        /// <summary>
        /// Search the collection based on search criteria
        /// </summary>
        /// <param name="searchParameters">Parameters to search in database</param>
        /// <returns>
        /// A <see cref="FilterDefinition{StudyEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public static FilterDefinition<StudyEntity> Filter(SearchTitleParameters searchParameters)
        {
            var builder = Builders<StudyEntity>.Filter;
            var filter = builder.Empty;

            //Filter for Date Range
            filter &= builder.Where(x => x.AuditTrail.EntryDateTime >= searchParameters.FromDate
                                         && x.AuditTrail.EntryDateTime <= searchParameters.ToDate);

            if (!String.IsNullOrWhiteSpace(searchParameters.StudyTitle))
                filter &= builder.Where(x => x.ClinicalStudy.StudyTitle.ToLower().Contains(searchParameters.StudyTitle.ToLower()));

            return filter;
        }

        /// <summary>
        /// Search the collection based on search criteria
        /// </summary>
        /// <param name="searchParameters">Parameters to search in database</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="List{SearchTitleEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<SearchTitleEntity>> SearchTitle(SearchTitleParameters searchParameters, LoggedInUser user)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(SearchStudy)};");
            try
            {
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.Study);

                var builder = Builders<StudyEntity>.Filter;
                var filter = builder.Empty;

                var filteredResult = await collection
                                                 .Aggregate()
                                                 .Match(Filter(searchParameters))
                                                 .Project(x => new SearchTitleEntity
                                                 {
                                                     StudyId = x.ClinicalStudy.StudyId ?? null,
                                                     StudyTag = x.ClinicalStudy.StudyTag ?? null,
                                                     StudyType = x.ClinicalStudy.StudyType ?? null,
                                                     StudyTitle = x.ClinicalStudy.StudyTitle ?? null,
                                                     EntryDateTime = x.AuditTrail.EntryDateTime,
                                                     StudyVersion = x.AuditTrail.StudyVersion,
                                                     UsdmVersion = x.AuditTrail.UsdmVersion
                                                 })
                                                 .ToListAsync().ConfigureAwait(false);

                var groupFilterResult = GroupFilterForSearchTitle(filteredResult, user).ToList();


                return groupFilterResult;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(SearchStudy)};");
            }
        }
        /// <summary>
        /// Sorting the result set
        /// </summary>
        /// <param name="filteredResult">Filtered result from database</param>
        /// <param name="property">Property by which the sorting must be done</param>
        /// <param name="asc">Ascending/Descending</param>
        /// <returns>
        /// A Sorted <see cref="IEnumerable{StudyEntity}"/>  
        /// </returns>
        public IEnumerable<SearchResponse> ApplyOrderBy(List<SearchResponse> filteredResult, string property, bool asc)
        {
            try
            {
                _logger.LogInformation($"Entered : {nameof(ClinicalStudyRepository)}; Method : {nameof(ApplyOrderBy)};");
                if (!String.IsNullOrWhiteSpace(property))
                {
                    return property.ToLower() switch
                    {
                        //Sort by studyTitle
                        "studytitle" => asc ? filteredResult.OrderBy(s => s.StudyTitle) : filteredResult.OrderByDescending(s => s.StudyTitle),
                        //Sort by studyIdentifier: orgCode
                        "sponsorid" => asc ? filteredResult.OrderBy(s => s.StudyIdentifiers.FindAll(x => x.IdType == Constants.IdType.SPONSOR_ID).Count != 0 ? s.StudyIdentifiers.Find(x => x.IdType == Constants.IdType.SPONSOR_ID).OrgCode ?? "" : "")
                                                                                        : filteredResult.OrderByDescending(s => s.StudyIdentifiers.FindAll(x => x.IdType == Constants.IdType.SPONSOR_ID).Count != 0 ? s.StudyIdentifiers.Find(x => x.IdType == Constants.IdType.SPONSOR_ID).OrgCode ?? "" : ""),
                        //Sort by studyIndication: description
                        "indication" => asc ? filteredResult.OrderBy(s => s.StudyIndications != null ? s.StudyIndications.Any() ? s.StudyIndications.First().Count > 0 ? s.StudyIndications.First().First().Description ?? "" : "" : "" : "")
                                                                                        : filteredResult.OrderByDescending(s => s.StudyIndications != null ? s.StudyIndications.Any() ? s.StudyIndications.First().Count > 0 ? s.StudyIndications.First().First().Description ?? "" : "" : "" : ""),
                        //Sort by studyDesign: InvestigationalIntervention: Intervention Model
                        "interventionmodel" => asc ? filteredResult.OrderBy(s => s.InvestigationalInterventions != null ? s.InvestigationalInterventions.Any() ? s.InvestigationalInterventions.First().Any() ? s.InvestigationalInterventions.First().First().Any() ? s.InvestigationalInterventions.First().First().First().Count > 0 ? s.InvestigationalInterventions.First().First().First().Count > 0 ? s.InvestigationalInterventions.First().First().First().First().InterventionModel ?? "" : "" : "" : "" : "" : "" : "")
                                                               : filteredResult.OrderByDescending(s => s.InvestigationalInterventions != null ? s.InvestigationalInterventions.Any() ? s.InvestigationalInterventions.First().Any() ? s.InvestigationalInterventions.First().First().Any() ? s.InvestigationalInterventions.First().First().First().Count > 0 ? s.InvestigationalInterventions.First().First().First().Count > 0 ? s.InvestigationalInterventions.First().First().First().First().InterventionModel ?? "" : "" : "" : "" : "" : "" : ""),
                        //Sort by studyPhase
                        "phase" => asc ? filteredResult.OrderBy(s => s.StudyPhase ?? "") : filteredResult.OrderByDescending(s => s.StudyPhase ?? ""),
                        //Sort by entrySystem
                        "lastmodifiedbysystem" => asc ? filteredResult.OrderBy(s => s.EntrySystem ?? "") : filteredResult.OrderByDescending(s => s.EntrySystem ?? ""),
                        //Sort by studyTag
                        "tag" => asc ? filteredResult.OrderBy(s => s.StudyTag ?? "") : filteredResult.OrderByDescending(s => s.StudyTag ?? ""),
                        //Sort by SDR version
                        "sdrversion" => asc ? filteredResult.OrderBy(s => s.StudyVersion) : filteredResult.OrderByDescending(s => s.StudyVersion),
                        //Sort by studyStatus
                        "status" => asc ? filteredResult.OrderBy(s => s.StudyStatus ?? "") : filteredResult.OrderByDescending(s => s.StudyStatus ?? ""),
                        //Sort by entryDateTime
                        "lastmodifieddate" => asc ? filteredResult.OrderBy(s => s.EntryDateTime) : filteredResult.OrderByDescending(s => s.EntryDateTime),
                        //Sort by entrySystem Descending by default
                        _ => filteredResult.OrderByDescending(s => s.EntryDateTime),
                    };
                }
                else
                {
                    return filteredResult.OrderByDescending(s => s.EntryDateTime);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended : {nameof(ClinicalStudyRepository)}; Method : {nameof(ApplyOrderBy)};");
            }
        }
        #endregion

        #region Data Filtering based on groups    

        public List<StudyHistoryEntity> GroupFilterForStudyHistory(List<StudyHistoryEntity> studyHistoryEntities, LoggedInUser user)
        {
            if (user.UserRole != Constants.Roles.Org_Admin && Config.IsGroupFilterEnabled)
            {
                var groups = GetGroupsOfUser(user).Result;

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

                    if (studyTypeFilterValues.Contains(Constants.StudyType.ALL.ToLower()))
                        return studyHistoryEntities;
                    studyHistoryEntities = studyHistoryEntities.Where(x => studyTypeFilterValues.Contains(x.StudyType.ToLower()) || studyIdFilterValues.Contains(x.StudyId)).ToList();
                }
                else
                {
                    // Filter should not give any results
                    studyHistoryEntities = studyHistoryEntities.Where(x => 1 == 0).ToList();
                }
            }
            return studyHistoryEntities;
        }
        public List<SearchTitleEntity> GroupFilterForSearchTitle(List<SearchTitleEntity> searchTitleEntities, LoggedInUser user)
        {
            if (user.UserRole != Constants.Roles.Org_Admin && Config.IsGroupFilterEnabled)
            {
                var groups = GetGroupsOfUser(user).Result;

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

                    if (studyTypeFilterValues.Contains(Constants.StudyType.ALL.ToLower()))
                        return searchTitleEntities;
                    searchTitleEntities = searchTitleEntities.Where(x => studyTypeFilterValues.Contains(x.StudyType.ToLower()) || studyIdFilterValues.Contains(x.StudyId)).ToList();
                }
                else
                {
                    // Filter should not give any results
                    searchTitleEntities = searchTitleEntities.Where(x => 1 == 0).ToList();
                }
            }
            return searchTitleEntities;
        }

        public async Task<List<SDRGroupsEntity>> GetGroupsOfUser(LoggedInUser user)
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
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(PostStudyItemsAsync)};");
            try
            {
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyDefinitions);
                await collection.InsertOneAsync(study).ConfigureAwait(false); //Insert One Document

                return (study.ClinicalStudy.StudyId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(PostStudyItemsAsync)};");
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
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(UpdateStudyItemsAsync)};");
            try
            {
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyDefinitions);
                var updateDefinition = Builders<StudyEntity>.Update
                                    .Set(s => s.ClinicalStudy, study.ClinicalStudy)
                                    .Set(s => s.AuditTrail, study.AuditTrail);
                await collection.UpdateOneAsync(x => (x.ClinicalStudy.StudyId == study.ClinicalStudy.StudyId
                                                   && x.AuditTrail.StudyVersion == study.AuditTrail.StudyVersion), //Match studyId and studyVersion
                                                   updateDefinition).ConfigureAwait(false); // Update clinicalStudy and auditTrail

                return (study.ClinicalStudy.StudyId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(UpdateStudyItemsAsync)};");
            }
        }
        #endregion

        /// <summary>
        /// GET a Study for a study ID with version filter
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="version">Version of study</param>
        /// <returns>
        /// A <see cref="AuditTrailEntity"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<AuditTrailEntity> GetUsdmVersionAsync(string studyId, int version)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(GetUsdmVersionAsync)};");
            try
            {
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyDefinitions);
                AuditTrailEntity auditTrail = await collection.Find(s => s.ClinicalStudy.StudyId == studyId)  // Condition for matching studyId
                                             .SortByDescending(s => s.AuditTrail.EntryDateTime) // Sort by descending on entryDateTime
                                             .Limit(1)                  //Taking top 1 result
                                             .Project(x => x.AuditTrail)
                                             .SingleOrDefaultAsync().ConfigureAwait(false);

                if (auditTrail == null)
                {
                    _logger.LogWarning($"There is no study with StudyId : {studyId} in {Constants.Collections.StudyDefinitions} Collection");
                    return null;
                }
                else
                {
                    return auditTrail;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(GetUsdmVersionAsync)};");
            }
        }
        #endregion       
    }
}
