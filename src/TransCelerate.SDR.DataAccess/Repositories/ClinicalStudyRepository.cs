﻿using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TransCelerate.SDR.Core.Entities;
using TransCelerate.SDR.Core.Entities.Study;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.DataAccess.Interfaces;

namespace TransCelerate.SDR.DataAccess.Repositories
{
    public class ClinicalStudyRepository : IClinicalStudyRepository
    {

        #region Variables     
        private readonly string _databaseName = Config.databaseName;
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
        }
        #endregion

        #region DB Operations

        #region GET Data

        /// <summary>
        /// GET a Study for a study ID with version filter
        /// </summary>
        /// <param name="studyId"></param>
        /// <param name="version"></param>
        /// <returns>
        /// A <see cref="StudyEntity"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<StudyEntity> GetStudyItemsAsync(string studyId, int version)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(GetStudyItemsAsync)};");
            try
            {
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.Study);
                StudyEntity _study;
                if (version == 0)
                {
                    _study = await collection.Find(s => s.clinicalStudy.studyId == studyId)  // Condition for matching studyId
                                             .SortByDescending(s => s.auditTrail.entryDateTime) // Sort by descending on entryDateTime
                                             .Limit(1)                  //Taking top 1 result
                                             .SingleOrDefaultAsync().ConfigureAwait(false);

                }
                else
                {
                    _study = await collection.Find(s =>
                                                (s.clinicalStudy.studyId == studyId
                                                 && s.auditTrail.studyVersion == version)) // Condition for matching studyId and version
                                             .SortByDescending(s => s.auditTrail.entryDateTime)  // Sort by descending on entryDateTime
                                             .Limit(1)                  //Taking top 1 result
                                             .SingleOrDefaultAsync().ConfigureAwait(false);
                }

                if (_study == null)
                {
                    _logger.LogWarning($"There is no study with StudyId : {studyId} in {Constants.Collections.Study} Collection");
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
        /// <param name="studyId"></param>
        /// <param name="version"></param>
        /// <param name="tag"></param>
        /// <returns>
        /// A <see cref="StudyEntity"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<StudyEntity> GetStudyItemsAsync(string studyId, int version, string tag)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(GetStudyItemsAsync)};");
            try
            {
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.Study);
                StudyEntity study = new StudyEntity();
                if (version == 0)
                {
                    study = await collection.Find(s =>
                                                (s.clinicalStudy.studyId == studyId)
                                                && s.clinicalStudy.studyTag == tag)   // Condition for matching studyId and studyTag
                                             .SortByDescending(s => s.auditTrail.entryDateTime) // Sort by descending on entryDateTime
                                             .Limit(1)                  //Taking top 1 result
                                             .SingleOrDefaultAsync().ConfigureAwait(false);
                }
                else
                {
                    study = await collection.Find(s =>
                                                (s.clinicalStudy.studyId == studyId
                                                 && s.clinicalStudy.studyTag == tag
                                                 && s.auditTrail.studyVersion == version)) // Condition for matching studyId, version and studyTag
                                             .SortByDescending(s => s.auditTrail.entryDateTime) // Sort by descending on entryDateTime
                                             .Limit(1)               //Taking top 1 result    
                                             .SingleOrDefaultAsync().ConfigureAwait(false);

                }

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
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(GetStudyItemsAsync)};");
            }
        }

        /// <summary>
        /// GET List of study for a study ID
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="study"></param>
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
                List<StudyEntity> studies = new List<StudyEntity>();
                studies = await collection.Find(s =>
                                                  (s.clinicalStudy.studyId == studyId)
                                                  && s.auditTrail.entryDateTime >= fromDate
                                                  && s.auditTrail.entryDateTime <= toDate) // Condition for matching studyId and date range
                                                  .SortByDescending(s => s.auditTrail.entryDateTime) // Sort by descending on entryDateTime
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
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="studyTitle"></param>
        /// <returns>
        /// A <see cref="List{StudyHistoryEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<StudyHistoryEntity>> GetAllStudyId(DateTime fromDate, DateTime toDate, string studyTitle)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(GetAllStudyId)};");
            try
            {
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.Study);
                List<StudyHistoryEntity> studyHistories;
                if(String.IsNullOrEmpty(studyTitle))
                {
                    studyHistories = await collection.Find(s =>
                                                    s.auditTrail.entryDateTime >= fromDate
                                                    && s.auditTrail.entryDateTime <= toDate) // Condition for matching date range
                                            .Project(x => new StudyHistoryEntity { studyId = x.clinicalStudy.studyId, studyTitle = x.clinicalStudy.studyTitle, studyVersion = x.auditTrail.studyVersion, entryDateTime = x.auditTrail.entryDateTime })  //Project only the required fields
                                            .SortByDescending(s => s.auditTrail.entryDateTime)  // Sort by descending on entryDateTime
                                            .ToListAsync().ConfigureAwait(false);                    
                }
                else
                {
                    studyHistories = await collection.Find(s =>
                                                    s.auditTrail.entryDateTime >= fromDate
                                                    && s.auditTrail.entryDateTime <= toDate
                                                    && s.clinicalStudy.studyTitle.ToLower().Contains(studyTitle.ToLower())) // Condition for matching studyTitle and date range
                                            .Project(x => new StudyHistoryEntity { studyId = x.clinicalStudy.studyId, studyTitle = x.clinicalStudy.studyTitle, studyVersion = x.auditTrail.studyVersion, entryDateTime = x.auditTrail.entryDateTime })  //Project only the required fields
                                            .SortByDescending(s => s.auditTrail.entryDateTime)  // Sort by descending on entryDateTime
                                            .ToListAsync().ConfigureAwait(false);                    
                }

                if (studyHistories.Count()==0)
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

        #region POST Data       

        /// <summary>
        /// Search the collection based on search criteria
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns>
        /// A <see cref="List{SearchResponseEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<SearchResponse>> SearchStudy(SearchParameters searchParameters)
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
                                                     studyId = x.clinicalStudy.studyId ?? null,
                                                     studyTag = x.clinicalStudy.studyTag ?? null,
                                                     studyType = x.clinicalStudy.studyType ?? null,
                                                     studyPhase = x.clinicalStudy.studyPhase ?? null,
                                                     studyTitle = x.clinicalStudy.studyTitle ?? null,
                                                     studyStatus = x.clinicalStudy.studyStatus ?? null,
                                                     studyIdentifiers = x.clinicalStudy.studyIdentifiers ?? null,
                                                     studyIndications = x.clinicalStudy.currentSections.Select(x => x.studyIndications) ?? null,
                                                     investigationalInterventions = x.clinicalStudy.currentSections.Select(x => x.studyDesigns).Select(x => x.Select(x => x.currentSections.Select(x => x.investigationalInterventions))) ?? null,
                                                     entryDateTime = x.auditTrail.entryDateTime,
                                                     entrySystem = x.auditTrail.entrySystem ?? null,
                                                     studyVersion = x.auditTrail.studyVersion,
                                                 })
                                                 .ToListAsync().ConfigureAwait(false);

                var groupResult = filteredResult.GroupBy(x => new { x.studyId, x.studyTag })
                                                .Select(g => new
                                                {
                                                    studyId = g.Key.studyId,
                                                    studyTag = g.Key.studyTag,
                                                    studyVersion = g.Max(x => x.studyVersion)
                                                }).ToList();
                var finalGroupResult = (from filter in filteredResult
                                     join grp in groupResult
                                     on (filter.studyId, filter.studyVersion) equals (grp.studyId, grp.studyVersion)
                                     select filter)                            
                            .ToList();

                var searchResults = LinqFilter(finalGroupResult, searchParameters);

                var sortedList = ApplyOrderBy(searchResults, searchParameters.header, searchParameters.asc) // Sort the data based on input
                                           .Skip((searchParameters.pageNumber - 1) * searchParameters.pageSize) //page number
                                           .Take(searchParameters.pageSize) //Number of documents per page
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

        public List<SearchResponse> LinqFilter(List<SearchResponse> searchResults,SearchParameters searchParameters)
        {
            if (!String.IsNullOrWhiteSpace(searchParameters.studyId))
            {
                searchResults= searchResults.Where(x => x.studyIdentifiers.Any(x => x.orgCode.ToLower().Contains(searchParameters.studyId.ToLower()))).ToList();
            }
            //Filter for studyTitle
            if (!String.IsNullOrWhiteSpace(searchParameters.studyTitle))
            {
                searchResults= searchResults.Where(x => x.studyTitle.ToLower().Contains(searchParameters.studyTitle.ToLower())).ToList();
            }
            //Filter for studyIndication: description
            if (!String.IsNullOrWhiteSpace(searchParameters.indication))
            {
                searchResults= searchResults.Where(x => x.studyIndications != null && x.studyIndications.Count() > 0 ? x.studyIndications.Any(x => x.Any(x => x.description.ToLower().Contains(searchParameters.indication.ToLower()))) : 1 == 0).ToList();
            }
            //Filter for studyDesign: InvestigationalIntervention: Intervention Model
            if (!String.IsNullOrWhiteSpace(searchParameters.interventionModel))
            {
                searchResults= searchResults.Where(x => x.investigationalInterventions != null && x.investigationalInterventions.Count() > 0 ? x.investigationalInterventions.Any(x => x.Any(x => x.Any(x => x.Any(x => x.interventionModel.ToLower().Contains(searchParameters.interventionModel.ToLower()))))) : 1 == 0).ToList();
            }
            //Filter for studyPhase
            if (!String.IsNullOrWhiteSpace(searchParameters.phase))
            {
                searchResults= searchResults.Where(x => x.studyPhase.ToLower().Contains(searchParameters.phase.ToLower())).ToList();
            }
            return searchResults;
        }

        /// <summary>
        /// Add filter definition for querying DB
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns>
        /// A Filtered <see cref="FilterDefinition{StudyEntity}"/> .ToList()
        /// </returns>
        public FilterDefinition<StudyEntity> Filter(SearchParameters searchParameters)
        {
            var builder = Builders<StudyEntity>.Filter;
            var filter = builder.Empty;            

            //Filter for Date Range
            filter &= builder.Where(x => x.auditTrail.entryDateTime >= searchParameters.fromDate
                                         && x.auditTrail.entryDateTime <= searchParameters.toDate);
            return filter;
        }

        /// <summary>
        /// Sorting the result set
        /// </summary>
        /// <param name="filteredResult"></param>
        /// <param name="property"></param>
        /// <param name="asc"></param>
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
                    switch (property.ToLower())
                    {
                        case "studytitle": //Sort by studyTitle
                            return asc ? filteredResult.OrderBy(s => s.studyTitle) : filteredResult.OrderByDescending(s => s.studyTitle);
                        case "sponsorid": //Sort by studyIdentifier: orgCode
                            return asc ? filteredResult.OrderBy(s => s.studyIdentifiers.FindAll(x => x.idType == Constants.IdType.SPONSOR_ID).Count() != 0 ? s.studyIdentifiers.Find(x => x.idType == Constants.IdType.SPONSOR_ID).orgCode ?? "" : "")
                                                                : filteredResult.OrderByDescending(s => s.studyIdentifiers.FindAll(x => x.idType == Constants.IdType.SPONSOR_ID).Count() != 0 ? s.studyIdentifiers.Find(x => x.idType == Constants.IdType.SPONSOR_ID).orgCode ?? "" : "");
                        case "indication": //Sort by studyIndication: description
                            return asc ? filteredResult.OrderBy(s => s.studyIndications!=null ? s.studyIndications.Count() > 0 ? s.studyIndications.First().Count() > 0 ? s.studyIndications.First().First().description ?? "" : "" : "" : "")
                                                                : filteredResult.OrderByDescending(s => s.studyIndications != null ? s.studyIndications.Count() > 0 ? s.studyIndications.First().Count() > 0 ? s.studyIndications.First().First().description ?? "" : "" : "" : "");
                        case "interventionmodel": //Sort by studyDesign: InvestigationalIntervention: Intervention Model
                            return asc ? filteredResult.OrderBy(s => s.investigationalInterventions!=null ? s.investigationalInterventions.Count() > 0 ? s.investigationalInterventions.First().Count() > 0 ? s.investigationalInterventions.First().First().Count() > 0 ? s.investigationalInterventions.First().First().First().Count() > 0 ? s.investigationalInterventions.First().First().First().Count() > 0 ? s.investigationalInterventions.First().First().First().First().interventionModel ?? "" : "" : "" : "" : "" : "" :"")
                                       : filteredResult.OrderByDescending(s => s.investigationalInterventions != null ? s.investigationalInterventions.Count() > 0 ? s.investigationalInterventions.First().Count() > 0 ? s.investigationalInterventions.First().First().Count() > 0 ? s.investigationalInterventions.First().First().First().Count() > 0 ? s.investigationalInterventions.First().First().First().Count() > 0 ? s.investigationalInterventions.First().First().First().First().interventionModel ?? "" : "" : "" : "" : "" : "" : "");
                        case "phase": //Sort by studyPhase
                            return asc ? filteredResult.OrderBy(s => s.studyPhase ?? "") : filteredResult.OrderByDescending(s => s.studyPhase ?? "");
                        case "lastmodifiedbysystem": //Sort by entrySystem
                            return asc ? filteredResult.OrderBy(s => s.entrySystem ?? "") : filteredResult.OrderByDescending(s => s.entrySystem ?? "");
                        case "tag": //Sort by studyTag
                            return asc ? filteredResult.OrderBy(s => s.studyTag ?? "") : filteredResult.OrderByDescending(s => s.studyTag ?? "");
                        case "sdrversion": //Sort by SDR version
                            return asc ? filteredResult.OrderBy(s => s.studyVersion) : filteredResult.OrderByDescending(s => s.studyVersion);
                        case "status": //Sort by studyStatus
                            return asc ? filteredResult.OrderBy(s => s.studyStatus ?? "") : filteredResult.OrderByDescending(s => s.studyStatus ?? "");
                        case "lastmodifieddate": //Sort by entryDateTime
                            return asc ? filteredResult.OrderBy(s => s.entryDateTime) : filteredResult.OrderByDescending(s => s.entryDateTime);
                        default: //Sort by entrySystem Descending by default
                            return filteredResult.OrderByDescending(s => s.entryDateTime);
                    }
                }
                else
                {
                    return filteredResult.OrderByDescending(s => s.entryDateTime);
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

        /// <summary>
        /// POST a Study
        /// </summary>
        /// <param name="study"></param>
        /// <returns>
        /// A studyId which was inserted     
        /// </returns>
        public async Task<string> PostStudyItemsAsync(StudyEntity study)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(PostStudyItemsAsync)};");
            try
            {
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.Study);
                await collection.InsertOneAsync(study).ConfigureAwait(false); //Insert One Document

                return (study.clinicalStudy.studyId);
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
        /// <param name="study"></param>
        /// <returns>
        /// A studyId which was inserted        
        /// </returns>
        public async Task<string> UpdateStudyItemsAsync(StudyEntity study)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(UpdateStudyItemsAsync)};");
            try
            {
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.Study);
                var updateDefinition = Builders<StudyEntity>.Update
                                    .Set(s => s.clinicalStudy, study.clinicalStudy)
                                    .Set(s => s.auditTrail, study.auditTrail);
                await collection.UpdateOneAsync(x => (x.clinicalStudy.studyId == study.clinicalStudy.studyId
                                                   && x.auditTrail.studyVersion == study.auditTrail.studyVersion), //Match studyId and studyVersion
                                                   updateDefinition).ConfigureAwait(false); // Update clinicalStudy and auditTrail

                return (study.clinicalStudy.studyId);
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
        #endregion       
    }
}
