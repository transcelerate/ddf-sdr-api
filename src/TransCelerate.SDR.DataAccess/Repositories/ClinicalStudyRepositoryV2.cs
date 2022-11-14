using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Entities.StudyV2;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.DataAccess.Filters;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.DataAccess.Repositories
{
    public class ClinicalStudyRepositoryV2 : IClinicalStudyRepositoryV2
    {
        #region Variables     
        private readonly string _databaseName = Config.DatabaseName;
        private readonly ILogHelper _logger;

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        #endregion

        #region Constructor      
        public ClinicalStudyRepositoryV2(IMongoClient client, ILogHelper logger, IChangeAuditRepository changeAuditRepository)
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
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV2)}; Method : {nameof(GetStudyItemsAsync)};");
            try
            {
                IMongoCollection<StudyEntity> collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyV1);


                StudyEntity study = await collection.Find(DataFiltersV2.GetFiltersForGetStudy(studyId, sdruploadversion))
                                                     .SortByDescending(s => s.AuditTrail.EntryDateTime) // Sort by descending on entryDateTime
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
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepositoryV2)}; Method : {nameof(GetStudyItemsAsync)};");
            }
        }

        /// <summary>
        /// GET a Study for a study ID with version filter
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="listofelementsArray">Array of study elements</param>
        /// <returns>
        /// A <see cref="StudyEntity"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<StudyEntity> GetPartialStudyItemsAsync(string studyId, int sdruploadversion, string[] listofelementsArray)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV2)}; Method : {nameof(GetPartialStudyItemsAsync)};");
            try
            {
                IMongoCollection<StudyEntity> collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyV1);


                StudyEntity study = await collection.Find(DataFiltersV2.GetFiltersForGetStudy(studyId, sdruploadversion))
                                                     .SortByDescending(s => s.AuditTrail.EntryDateTime) // Sort by descending on entryDateTime
                                                     .Limit(1)                  //Taking top 1 result
                                                     .Project<StudyEntity>(DataFiltersV2.GetProjectionForPartialStudyElements(listofelementsArray))
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
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepositoryV2)}; Method : {nameof(GetPartialStudyItemsAsync)};");
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
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV2)}; Method : {nameof(GetPartialStudyDesignItemsAsync)};");
            try
            {
                IMongoCollection<StudyEntity> collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyV1);


                StudyEntity study = await collection.Find(DataFiltersV2.GetFiltersForGetStudy(studyId, sdruploadversion))
                                                     .Project<StudyEntity>(DataFiltersV2.GetProjectionForPartialStudyDesignElementsFullStudy())
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
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepositoryV2)}; Method : {nameof(GetPartialStudyDesignItemsAsync)};");
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
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV2)}; Method : {nameof(GetAuditTrail)};");
            try
            {
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyV1);
                List<AuditTrailResponseEntity> auditTrails = new List<AuditTrailResponseEntity>();
                auditTrails = await collection.Find(DataFiltersV2.GetFiltersForGetAudTrail(studyId, fromDate, toDate)) // Condition for matching studyId and date range
                                                  .Project(x => new AuditTrailResponseEntity
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
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepositoryV2)}; Method : {nameof(GetAuditTrail)};");
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
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV2)}; Method : {nameof(GetStudyHistory)};");
            try
            {
                var collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyV1);

                List<StudyHistoryResponseEntity> studyHistories = await collection.Aggregate()
                                                        .Match(DataFiltersV2.GetFiltersForStudyHistory(fromDate, toDate, studyTitle)) // Condition for matching date range
                                                        .Project(x =>
                                                                new StudyHistoryResponseEntity
                                                                {
                                                                    Uuid = x.ClinicalStudy.Uuid,
                                                                    StudyTitle = x.ClinicalStudy.StudyTitle,
                                                                    SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                                    StudyIdentifiers = x.ClinicalStudy.StudyIdentifiers,
                                                                    EntryDateTime = x.AuditTrail.EntryDateTime,
                                                                    StudyType = x.ClinicalStudy.StudyType,
                                                                    ProtocolVersions = x.ClinicalStudy.StudyProtocolVersions.Select(x => x.ProtocolVersion),
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
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepositoryV2)}; Method : {nameof(GetStudyHistory)};");
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
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV2)}; Method : {nameof(PostStudyItemsAsync)};");
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
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepositoryV2)}; Method : {nameof(PostStudyItemsAsync)};");
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
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV2)}; Method : {nameof(UpdateStudyItemsAsync)};");
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
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepositoryV2)}; Method : {nameof(UpdateStudyItemsAsync)};");
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

        #region DELETE Study
        /// <summary>
        /// Delete all versions of a study
        /// </summary>
        /// <param name="study_uuid"> Study Id</param>
        /// <returns></returns>
        public async Task<DeleteResult> DeleteStudyAsync(string study_uuid)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV2)}; Method : {nameof(DeleteStudyAsync)};");
            try
            {
                IMongoCollection<StudyEntity> collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyV1);
                var builder = Builders<StudyEntity>.Filter.Eq(x => x.ClinicalStudy.Uuid, study_uuid);

                var deleteResult = await collection.DeleteManyAsync(builder).ConfigureAwait(false);

                return deleteResult;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepositoryV2)}; Method : {nameof(DeleteStudyAsync)};");
            }
        }
        /// <summary>
        /// Count Documents
        /// </summary>
        /// <param name="study_uuid"> Study Id</param>
        /// <returns></returns>
        public async Task<long> CountAsync(string study_uuid)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV2)}; Method : {nameof(CountAsync)};");
            try
            {
                IMongoCollection<StudyEntity> collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyV1);
                var builder = Builders<StudyEntity>.Filter.Eq(x => x.ClinicalStudy.Uuid, study_uuid);
                long count = await collection.CountDocumentsAsync(builder).ConfigureAwait(false);

                return count;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepositoryV2)}; Method : {nameof(CountAsync)};");
            }
        }
        #endregion

        #region Get only studyType
        public async Task<StudyEntity> GetStudyItemsForCheckingAccessAsync(string studyId, int sdruploadversion)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV2)}; Method : {nameof(GetStudyItemsForCheckingAccessAsync)};");
            try
            {
                IMongoCollection<StudyEntity> collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyV1);


                StudyEntity study = await collection.Find(DataFiltersV2.GetFiltersForGetStudy(studyId, sdruploadversion))
                                                     .SortByDescending(s => s.AuditTrail.EntryDateTime) // Sort by descending on entryDateTime
                                                     .Project<StudyEntity>(DataFiltersV2.GetProjectionForCheckAccessForAStudy())
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
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepositoryV2)}; Method : {nameof(GetStudyItemsForCheckingAccessAsync)};");
            }
        }
        #endregion
        #endregion
    }
}
