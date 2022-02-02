using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.DataAccess.Interfaces;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;
using TransCelerate.SDR.Core.Entities.Study;
using Microsoft.Extensions.Logging;
using TransCelerate.SDR.Core.Utilities;

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
        /// GET a Collection for a study ID with version filter
        /// </summary>
        /// <param name="studyId"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public async Task<StudyEntity> GetStudyItemsAsync(string studyId,int version)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(GetStudyItemsAsync)};");
            try
            {                
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.Study);
                StudyEntity _study;
                if(version == 0)
                {
                    _study = await collection.Find(s => s.clinicalStudy.studyId == studyId)
                                             .SortByDescending(s => s.auditTrail.studyVersion)
                                             .Limit(1)
                                             .SingleOrDefaultAsync().ConfigureAwait(false);                    

                }
                else
                {
                    _study = await collection.Find(s => 
                                                (s.clinicalStudy.studyId== studyId
                                                 && s.auditTrail.studyVersion==version))
                                             .SortByDescending(s => s.auditTrail.studyVersion)
                                             .Limit(1)
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
            catch (Exception ex)
            {                                
                throw ex;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(GetStudyItemsAsync)};");
            }
        }

        /// <summary>
        /// GET a Collection for a study ID with version and status filters
        /// </summary>
        /// <param name="studyId"></param>
        /// <param name="version"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public async Task<StudyEntity> GetStudyItemsAsync(string studyId, int version, string tag)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(GetStudyItemsAsync)};");
            try
            {                
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.Study);
                object _study;
                if(version == 0)
                {
                    _study = await collection.Find(s => 
                                                (s.clinicalStudy.studyId == studyId)
                                                && s.clinicalStudy.tag == tag)
                                             .SortByDescending(s => s.auditTrail.studyVersion)
                                             .Limit(1)
                                             .SingleOrDefaultAsync().ConfigureAwait(false);
                }
                else
                {
                    _study = await collection.Find(s => 
                                                (s.clinicalStudy.studyId == studyId
                                                 && s.clinicalStudy.tag == tag
                                                 && s.auditTrail.studyVersion == version))
                                             .SortByDescending(s => s.auditTrail.studyVersion)
                                             .Limit(1)
                                             .SingleOrDefaultAsync().ConfigureAwait(false);
                    
                }

                if (_study == null)
                {
                    _logger.LogWarning($"There is no study with StudyId : {studyId} in {Constants.Collections.Study} Collection");                    
                    return null;
                }
                else
                {                   
                    return _study as StudyEntity;
                }
            }
            catch (Exception ex)
            {               
                throw ex;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(GetStudyItemsAsync)};");
            }
        }

        /// <summary>
        /// GET AudiTrial
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="studyId"></param>
        /// <returns></returns>
        public async Task<List<StudyEntity>> GetAuditTrail(DateTime fromDate, DateTime toDate, string studyId)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(GetAuditTrail)};");
            try
            {
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.Study);
                List<StudyEntity> studies= new List<StudyEntity>();
                studies = await collection.Find(s =>
                                                  (s.clinicalStudy.studyId == studyId)
                                                  && s.auditTrail.entryDateTime >= fromDate
                                                  && s.auditTrail.entryDateTime <= toDate)
                                                  .SortByDescending(s => s.auditTrail.entryDateTime)
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
            catch (Exception ex)
            {                
                throw ex;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(GetAuditTrail)};");
            }
        }

        #endregion

        #region POST Data       

        /// <summary>
        /// Search a Collection  
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        public async Task<List<StudyEntity>> SearchStudy(SearchParameters searchParameters)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(SearchStudy)};");
            try
            {                
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.Study);
                var builder = Builders<StudyEntity>.Filter;
                var filter = builder.Empty;

                #region Search conditions
                if (!String.IsNullOrWhiteSpace(searchParameters.studyId))
                {
                    filter &= builder.Where(x=>x.clinicalStudy.studyIdentifiers.Any(y=>y.orgCode.Contains(searchParameters.studyId.ToLower())));
                }
                if (!String.IsNullOrWhiteSpace(searchParameters.studyTitle))
                {
                    filter &= builder.Where(x => x.clinicalStudy.studyTitle.ToLower().Contains(searchParameters.studyTitle.ToLower()));
                }
                if (!String.IsNullOrWhiteSpace(searchParameters.briefTitle))
                {
                    filter &= builder.Where(x => x.clinicalStudy.currentSections.Any(x=>x.studyProtocol.briefTitle.ToLower().Contains(searchParameters.briefTitle.ToLower())));
                }
                if (!String.IsNullOrWhiteSpace(searchParameters.indication))
                {
                    filter &= builder.Where(x => x.clinicalStudy.currentSections.Any(x=>x.studyIndications.Any(x=>x.description.ToLower().Contains(searchParameters.indication.ToLower()))));
                }
                if (!String.IsNullOrWhiteSpace(searchParameters.interventionModel))
                {
                    filter &= builder.Where(x => x.clinicalStudy.interventionModel.ToLower().Contains(searchParameters.interventionModel.ToLower()));
                }
                if (!String.IsNullOrWhiteSpace(searchParameters.phase))
                {
                    filter &= builder.Where(x => x.clinicalStudy.studyPhase.ToLower().Contains(searchParameters.phase.ToLower()));
                }

                filter &= builder.Where(x => x.auditTrail.entryDateTime >= searchParameters.fromDate
                                             && x.auditTrail.entryDateTime <= searchParameters.toDate);

                #endregion

                #region Group Query 
                var groups = collection.AsQueryable()
                                        .GroupBy(x => new { x.clinicalStudy.studyId,x.clinicalStudy.tag })                                        
                                        .Select(g => new
                                        {
                                            id = g.Key.studyId,
                                            total = g.Count(),
                                            version = g.Key.tag,
                                            recordVersion = g.Max(x => x.auditTrail.studyVersion)
                                        })
                                        .ToList();             
                #endregion

                #region Basic Query Without version 
                var studyResult = await collection.Find(filter)                                                 
                                                  .ToListAsync()                                                  
                                                  .ConfigureAwait(false);
                #endregion
                
                var filteredResult = (from collect in studyResult
                                      join grp in groups
                                      on (collect.clinicalStudy.studyId, collect.auditTrail.studyVersion) 
                                          equals (grp.id, grp.recordVersion)
                                      select collect)                                                                                                             
                                     .ToList();
                var sortedList = ApplyOrderBy(filteredResult, searchParameters.header,searchParameters.asc)
                                            .Skip((searchParameters.pageNumber - 1) * searchParameters.pageSize)
                                            .Take(searchParameters.pageSize)
                                            .ToList();
                if(sortedList.Count!=0)
                {
                    return sortedList;
                }
                else
                {
                    return null;
                }
               
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(SearchStudy)};");
            }
        }
        public IEnumerable<StudyEntity> ApplyOrderBy(List<StudyEntity> filteredResult, string property, bool asc)
        {
            try
            {
                _logger.LogInformation($"Entered : {nameof(ClinicalStudyRepository)}; Method : {nameof(ApplyOrderBy)};");
                if (!String.IsNullOrWhiteSpace(property))
                {
                    switch (property.ToLower())
                    {
                        case "studytitle":
                            return asc ? filteredResult.OrderBy(s => s.clinicalStudy.studyTitle) : filteredResult.OrderByDescending(s => s.clinicalStudy.studyTitle);
                        case "brieftitle":
                            return asc ? filteredResult.OrderBy(s => s.clinicalStudy.currentSections!=null ? s.clinicalStudy.currentSections.Select(y => y.studyProtocol).FirstOrDefault(s => s != null) != null ? s.clinicalStudy.currentSections.Select(y => y.studyProtocol).FirstOrDefault(s => s != null).briefTitle ?? "" : "" : "")
                                                                : filteredResult.OrderByDescending(s => s.clinicalStudy.currentSections != null ? s.clinicalStudy.currentSections.Select(y => y.studyProtocol).FirstOrDefault(s => s != null) != null ? s.clinicalStudy.currentSections.Select(y => y.studyProtocol).FirstOrDefault(s => s != null).briefTitle ?? "" : "" : "");
                        case "sponsorid":
                            return asc ? filteredResult.OrderBy(s => s.clinicalStudy.studyIdentifiers.Find(x=>x.idType==Constants.IdType.SPONSOR_ID).orgCode ?? "")
                                                                : filteredResult.OrderByDescending(s => s.clinicalStudy.studyIdentifiers.Find(x => x.idType == Constants.IdType.SPONSOR_ID).orgCode ?? "");
                        case "indication":
                            return asc ? filteredResult.OrderBy(s => s.clinicalStudy.currentSections!=null ? s.clinicalStudy.currentSections.Where(x => x.studyIndications != null).Count() != 0 ? s.clinicalStudy.currentSections.Select(y => y.studyIndications).FirstOrDefault(s => s != null).Select(z => z.description).FirstOrDefault() ?? "" : "" : "" )
                                                                : filteredResult.OrderByDescending(s => s.clinicalStudy.currentSections != null ? s.clinicalStudy.currentSections.Where(x => x.studyIndications != null).Count() != 0 ? s.clinicalStudy.currentSections.Select(y => y.studyIndications).FirstOrDefault(s => s != null).Select(z => z.description).FirstOrDefault() ?? "" : "" : "");
                        case "interventionmodel":
                            return asc ? filteredResult.OrderBy(s => s.clinicalStudy.interventionModel ?? "") : filteredResult.OrderByDescending(s => s.clinicalStudy.interventionModel ?? "");
                        case "phase":
                            return asc ? filteredResult.OrderBy(s => s.clinicalStudy.studyPhase ?? "") : filteredResult.OrderByDescending(s => s.clinicalStudy.studyPhase ?? "");
                        case "lastmodifiedbysystem":
                            return asc ? filteredResult.OrderBy(s => s.auditTrail.entrySystem ?? "") : filteredResult.OrderByDescending(s => s.auditTrail.entrySystem ?? "");
                        case "sdrversion":
                            return asc ? filteredResult.OrderBy(s => s.clinicalStudy.tag ?? "") : filteredResult.OrderByDescending(s => s.clinicalStudy.tag ?? "");
                        case "lastmodifieddate":
                            return asc ? filteredResult.OrderBy(s => s.auditTrail.entryDateTime) : filteredResult.OrderByDescending(s => s.auditTrail.entryDateTime);
                        default:
                            return filteredResult.OrderByDescending(s => s.auditTrail.entryDateTime);
                    }
                }
                else
                {
                    return filteredResult.OrderByDescending(s => s.auditTrail.entryDateTime);
                }
            }
            catch (Exception e)
            {                
                throw e;
            }
            finally
            {
                _logger.LogInformation($"Ended : {nameof(ClinicalStudyRepository)}; Method : {nameof(ApplyOrderBy)};");
            }
        }

        /// <summary>
        /// POST a Collection for a study ID 
        /// </summary>
        /// <param name="study"></param>
        /// <returns></returns>
        public async Task<string> PostStudyItemsAsync(StudyEntity study)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(PostStudyItemsAsync)};");
            try
            {
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.Study);
                await collection.InsertOneAsync(study).ConfigureAwait(false);

                return (study.clinicalStudy.studyId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(PostStudyItemsAsync)};");
            }
        }
        #endregion


        #region Update Study Data

        /// <summary>
        /// Update a Collection for a study ID  
        /// </summary>
        /// <param name="study"></param>
        /// <returns></returns>
        public async Task<string> UpdateStudyItemsAsync(StudyEntity study)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(UpdateStudyItemsAsync)};");
            try
            {
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.Study);
                var updateDefinition = Builders<StudyEntity>.Update
                                    .Set(s => s.clinicalStudy, study.clinicalStudy)
                                    .Set(s=>s.auditTrail,study.auditTrail);
                await collection.UpdateOneAsync(x => (x.clinicalStudy.studyId == study.clinicalStudy.studyId
                                                   && x.auditTrail.studyVersion == study.auditTrail.studyVersion),
                                                   updateDefinition).ConfigureAwait(false);

                return (study.clinicalStudy.studyId);
            }
            catch (Exception ex)
            {
                throw ex;
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
