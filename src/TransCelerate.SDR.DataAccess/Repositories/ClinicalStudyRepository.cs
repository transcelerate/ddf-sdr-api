using MongoDB.Driver;
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
        /// <param name="fromDate">Start Date for Date Filter</param>
        /// <param name="toDate">End Date for Date Filter</param>
        /// <param name="studyTitle">Study Title Filter</param>
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
        /// <param name="searchParameters">Parameters to search in database</param>
        /// <returns>
        /// A <see cref="List{StudyEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<StudyEntity>> SearchStudy(SearchParameters searchParameters)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(SearchStudy)};");
            try
            {
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.Study);

                var groups = await collection.Aggregate()
                                        .Group(x => new
                                               {
                                                   _id = new
                                                   {
                                                       study = x.clinicalStudy.studyId,
                                                       tag = x.clinicalStudy.studyTag
                                                   }
                                               },
                                               group => new
                                               {
                                                   item = group.Select(x => new 
                                                   {
                                                       _id = x._id,
                                                       clinicalStudy = x.clinicalStudy,
                                                       auditTrail = x.auditTrail
                                                   }).Last()
                                               }
                                              ) //Grouping and selecting latest document for each group
                                        .Project(x => new StudyEntity
                                        {
                                            _id = x.item._id,
                                            clinicalStudy = x.item.clinicalStudy,
                                            auditTrail = x.item.auditTrail
                                        }) //Projecting latest document for each group
                                        .Match(Filter(searchParameters))      //Add Filters based on search criteria                                                                        
                                        .ToListAsync().ConfigureAwait(false);

                var sortedList  = ApplyOrderBy(groups, searchParameters.header, searchParameters.asc) // Sort the data based on input
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

        /// <summary>
        /// Search the collection based on search criteria
        /// </summary>
        /// <param name="searchParameters">Parameters to search in database</param>
        /// <returns>
        /// A <see cref="FilterDefinition{StudyEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public FilterDefinition<StudyEntity> Filter(SearchParameters searchParameters)
        {
            var builder = Builders<StudyEntity>.Filter;
            var filter = builder.Empty;

            //Filter for studyIdentifier: orgCode
            if (!String.IsNullOrWhiteSpace(searchParameters.studyId))
            {
                filter &= builder.Where(x => x.clinicalStudy.studyIdentifiers.Any(y => y.orgCode.Contains(searchParameters.studyId.ToLower())));
            }
            //Filter for studyTitle
            if (!String.IsNullOrWhiteSpace(searchParameters.studyTitle))
            {
                filter &= builder.Where(x => x.clinicalStudy.studyTitle.ToLower().Contains(searchParameters.studyTitle.ToLower()));
            }
            //Filter for studyIndication: description
            if (!String.IsNullOrWhiteSpace(searchParameters.indication))
            {
                filter &= builder.Where(x => x.clinicalStudy.currentSections.Any(x => x.studyIndications.Any(x => x.description.ToLower().Contains(searchParameters.indication.ToLower()))));
            }
            //Filter for studyDesign: InvestigationalIntervention: Intervention Model
            if (!String.IsNullOrWhiteSpace(searchParameters.interventionModel))
            {
                filter &= builder.Where(x => x.clinicalStudy.currentSections.Any(x => x.studyDesigns.Any(x => x.currentSections.Any(x => x.investigationalInterventions.Any(x => x.interventionModel.ToLower().Contains(searchParameters.interventionModel.ToLower()))))));
            }
            //Filter for studyPhase
            if (!String.IsNullOrWhiteSpace(searchParameters.phase))
            {
                filter &= builder.Where(x => x.clinicalStudy.studyPhase.ToLower().Contains(searchParameters.phase.ToLower()));
            }

            //Filter for Date Range
            filter &= builder.Where(x => x.auditTrail.entryDateTime >= searchParameters.fromDate
                                         && x.auditTrail.entryDateTime <= searchParameters.toDate);
            return filter;
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
        public IEnumerable<StudyEntity> ApplyOrderBy(List<StudyEntity> filteredResult, string property, bool asc)
        {
            try
            {
                _logger.LogInformation($"Entered : {nameof(ClinicalStudyRepository)}; Method : {nameof(ApplyOrderBy)};");
                if (!String.IsNullOrWhiteSpace(property))
                {
                    switch (property.ToLower())
                    {
                        case "studytitle": //Sort by studyTitle
                            return asc ? filteredResult.OrderBy(s => s.clinicalStudy.studyTitle) : filteredResult.OrderByDescending(s => s.clinicalStudy.studyTitle);                        
                        case "sponsorid": //Sort by studyIdentifier: orgCode
                            return asc ? filteredResult.OrderBy(s => s.clinicalStudy.studyIdentifiers.FindAll(x => x.idType == Constants.IdType.SPONSOR_ID).Count() !=0 ? s.clinicalStudy.studyIdentifiers.Find(x => x.idType == Constants.IdType.SPONSOR_ID).orgCode ?? "" : "")
                                                                : filteredResult.OrderByDescending(s => s.clinicalStudy.studyIdentifiers.FindAll(x => x.idType == Constants.IdType.SPONSOR_ID).Count() != 0 ? s.clinicalStudy.studyIdentifiers.Find(x => x.idType == Constants.IdType.SPONSOR_ID).orgCode ?? "" : "");
                        case "indication": //Sort by studyIndication: description
                            return asc ? filteredResult.OrderBy(s => s.clinicalStudy.currentSections != null ? s.clinicalStudy.currentSections.Where(x => x.studyIndications != null).Count() != 0 ? s.clinicalStudy.currentSections.Select(y => y.studyIndications).FirstOrDefault(s => s != null).Select(z => z.description).FirstOrDefault() ?? "" : "" : "")
                                                                : filteredResult.OrderByDescending(s => s.clinicalStudy.currentSections != null ? s.clinicalStudy.currentSections.Where(x => x.studyIndications != null).Count() != 0 ? s.clinicalStudy.currentSections.Select(y => y.studyIndications).FirstOrDefault(s => s != null).Select(z => z.description).FirstOrDefault() ?? "" : "" : "");
                        case "interventionmodel": //Sort by studyDesign: InvestigationalIntervention: Intervention Model
                            return asc ? filteredResult.OrderBy(s => s.clinicalStudy.currentSections != null ?
                                                                                s.clinicalStudy.currentSections.Where(x => x.studyDesigns != null).Count() != 0 ?
                                                                                        s.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns.FindAll(x => x.currentSections != null).Count() != 0 ?
                                                                                            s.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.FindAll(x => x.investigationalInterventions != null).Count() != 0 ?
                                                                                                s.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.investigationalInterventions != null).investigationalInterventions.Select(x => x.interventionModel).FirstOrDefault() ?? ""
                                                                                             : ""
                                                                                        : ""
                                                                                : ""
                                                                         : "")
                                       : filteredResult.OrderByDescending(s => s.clinicalStudy.currentSections != null ?
                                                                                s.clinicalStudy.currentSections.Where(x => x.studyDesigns != null).Count() != 0 ?
                                                                                        s.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns.FindAll(x => x.currentSections != null).Count() != 0 ?
                                                                                            s.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.FindAll(x => x.investigationalInterventions != null).Count() != 0 ?
                                                                                                s.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.investigationalInterventions != null).investigationalInterventions.Select(x => x.interventionModel).FirstOrDefault() ?? ""
                                                                                             : ""
                                                                                        : ""
                                                                                : ""
                                                                         : "");
                        case "phase": //Sort by studyPhase
                            return asc ? filteredResult.OrderBy(s => s.clinicalStudy.studyPhase ?? "") : filteredResult.OrderByDescending(s => s.clinicalStudy.studyPhase ?? "");
                        case "lastmodifiedbysystem": //Sort by entrySystem
                            return asc ? filteredResult.OrderBy(s => s.auditTrail.entrySystem ?? "") : filteredResult.OrderByDescending(s => s.auditTrail.entrySystem ?? "");
                        case "tag": //Sort by studyTag
                            return asc ? filteredResult.OrderBy(s => s.clinicalStudy.studyTag ?? "") : filteredResult.OrderByDescending(s => s.clinicalStudy.studyTag ?? "");
                        case "sdrversion": //Sort by SDR version
                            return asc ? filteredResult.OrderBy(s => s.auditTrail.studyVersion) : filteredResult.OrderByDescending(s => s.auditTrail.studyVersion);
                        case "status": //Sort by studyStatus
                            return asc ? filteredResult.OrderBy(s => s.clinicalStudy.studyStatus ?? "") : filteredResult.OrderByDescending(s => s.clinicalStudy.studyStatus ?? "");
                        case "lastmodifieddate": //Sort by entryDateTime
                            return asc ? filteredResult.OrderBy(s => s.auditTrail.entryDateTime) : filteredResult.OrderByDescending(s => s.auditTrail.entryDateTime);
                        default: //Sort by entrySystem Descending by default
                            return filteredResult.OrderByDescending(s => s.auditTrail.entryDateTime);
                    }
                }
                else
                {
                    return filteredResult.OrderByDescending(s => s.auditTrail.entryDateTime);
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
        /// <param name="study">Study for Inserting into Database</param>
        /// <returns>
        /// A studyId which was inserted <br></br> <br></br>        
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
        /// <param name="study">Update study in database</param>
        /// <returns>
        /// A studyId which was inserted <br></br> <br></br>        
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
