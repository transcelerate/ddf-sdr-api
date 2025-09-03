using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.DataAccess.Filters;
using TransCelerate.SDR.DataAccess.Interfaces;

namespace TransCelerate.SDR.DataAccess.Repositories
{
    public class ChangeAuditRepository : IChangeAuditRepository
    {
        #region Variables
        private readonly string _databaseName = Config.DatabaseName;
        private readonly ILogHelper _logger;

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        #endregion

        #region Constructor
        public ChangeAuditRepository(IMongoClient client, ILogHelper logger)
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
        public async Task<ChangeAuditStudyEntity> GetChangeAuditAsync(string studyId)
        {
            _logger.LogInformation($"Started Repository : {nameof(ChangeAuditRepository)}; Method : {nameof(GetChangeAuditAsync)};");
            try
            {
                IMongoCollection<ChangeAuditStudyEntity> collection = _database.GetCollection<ChangeAuditStudyEntity>(Constants.Collections.ChangeAudit);

                ChangeAuditStudyEntity changeAuditStudyEntity = await collection.Find(DataFilterCommon.GetFiltersForChangeAudit(studyId))
                                                                .SingleOrDefaultAsync().ConfigureAwait(false);

                if (changeAuditStudyEntity == null)
                {
                    _logger.LogWarning($"There is no study with StudyId : {studyId} in {Constants.Collections.ChangeAudit} Collection");
                    return null;
                }
                else
                {
                    return changeAuditStudyEntity;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ChangeAuditRepository)}; Method : {nameof(GetChangeAuditAsync)};");
            }
        }

        public async Task<string> InsertChangeAudit(string study_uuid, int sdruploadversion, int sdruploadflag, DateTime entrydatetime)
        {
            _logger.LogInformation($"Started Repository : {nameof(ChangeAuditRepository)}; Method : {nameof(InsertChangeAudit)};");
            try
            {
                IMongoCollection<ChangeAuditStudyEntity> collection = _database.GetCollection<ChangeAuditStudyEntity>(Constants.Collections.ChangeAudit);

                ChangeAuditStudyEntity changeAuditStudyEntity = new();

                ChangesEntity change = new()
                {
                    Elements = new List<string>(),
                    EntryDateTime = entrydatetime,
                    SDRUploadVersion = sdruploadversion
                    //,SDRUploadFlag = sdruploadflag
                };

                var changeAuditEntity = new ChangeAuditEntity
                {
                    StudyId = study_uuid,
                    Changes = new List<ChangesEntity>()
                };
                changeAuditEntity.Changes.Add(change);


                changeAuditStudyEntity = new ChangeAuditStudyEntity
                {
                    ChangeAudit = changeAuditEntity,
                    Id = MongoDB.Bson.ObjectId.GenerateNewId()
                };

                await collection.InsertOneAsync(changeAuditStudyEntity);
                return changeAuditEntity.StudyId;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ChangeAuditRepository)}; Method : {nameof(InsertChangeAudit)};");
            }

        }

        public async Task<string> AddOrUpdateChangeAuditAsync(string studyId, List<string> changedValues, AuditTrailEntity currentVersionAuditTrail)
        {
            _logger.LogInformation($"Started Repository : {nameof(ChangeAuditRepository)}; Method : {nameof(AddOrUpdateChangeAuditAsync)};");
            try
            {
                var collection = _database.GetCollection<ChangeAuditStudyEntity>(Constants.Collections.ChangeAudit);

                ChangeAuditStudyEntity changeAuditStudyEntity = await collection.Find(DataFilterCommon.GetFiltersForChangeAudit(studyId))
                                                                .SingleOrDefaultAsync().ConfigureAwait(false);

                ChangesEntity change = new()
                {
                    Elements = changedValues,
                    EntryDateTime = currentVersionAuditTrail.EntryDateTime,
                    SDRUploadVersion = currentVersionAuditTrail.SDRUploadVersion
                };

                if (changeAuditStudyEntity == null)
                {
                    var changeAuditEntity = new ChangeAuditEntity
                    {
                        StudyId = studyId,
                        Changes = []
                    };
                    changeAuditEntity.Changes.Add(change);

                    changeAuditStudyEntity = new ChangeAuditStudyEntity
                    {
                        ChangeAudit = changeAuditEntity,
                        Id = MongoDB.Bson.ObjectId.GenerateNewId()
                    };

                    await collection.InsertOneAsync(changeAuditStudyEntity);

                    return changeAuditStudyEntity.ChangeAudit.StudyId;
                }
                else
                {
                    changeAuditStudyEntity.ChangeAudit.Changes.Add(change);

                    UpdateDefinition<ChangeAuditStudyEntity> updateDefinition = Builders<ChangeAuditStudyEntity>.Update
                                        .Set(s => s.ChangeAudit.Changes, changeAuditStudyEntity.ChangeAudit.Changes);

                    await collection.UpdateOneAsync(x => x.ChangeAudit.StudyId == changeAuditStudyEntity.ChangeAudit.StudyId, updateDefinition)
                                        .ConfigureAwait(false);

                    return changeAuditStudyEntity.ChangeAudit.StudyId;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ChangeAuditRepository)}; Method : {nameof(AddOrUpdateChangeAuditAsync)};");
            }
        }

        /// <summary>
        /// Get Current and previous version of study for study Id for V3 API Version
        /// </summary>
        /// <param name="studyId">Study UUID</param>
        /// <param name="sdruploadversion">current version</param>
        /// <returns> A <see cref="List{StudyDefinitionsEntity}"/> with matching studyId
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<Core.Entities.StudyV3.StudyDefinitionsEntity>> GetStudyItemsAsyncV3(string studyId, int sdruploadversion)
        {
            _logger.LogInformation($"Started Repository : {nameof(ChangeAuditRepository)}; Method : {nameof(GetStudyItemsAsyncV3)};");
            try
            {
                IMongoCollection<Core.Entities.StudyV3.StudyDefinitionsEntity> collection = _database.GetCollection<Core.Entities.StudyV3.StudyDefinitionsEntity>(Constants.Collections.StudyDefinitions);

                List<Core.Entities.StudyV3.StudyDefinitionsEntity> studies = await collection.Find(x => (x.Study.StudyId == studyId) &&
                                                           (x.AuditTrail.SDRUploadVersion == sdruploadversion || x.AuditTrail.SDRUploadVersion == sdruploadversion - 1))
                                                     .SortByDescending(s => s.AuditTrail.EntryDateTime)
                                                     .Limit(2)
                                                     .ToListAsync().ConfigureAwait(false);

                if (studies == null)
                {
                    _logger.LogWarning($"There are no studies with StudyId : {studyId} in {Constants.Collections.StudyDefinitions} Collection");
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
                _logger.LogInformation($"Ended Repository : {nameof(ChangeAuditRepository)}; Method : {nameof(GetStudyItemsAsyncV3)};");
            }
        }

        /// <summary>
        /// Get Current and previous version of study for study Id for V4 API Version
        /// </summary>
        /// <param name="studyId">Study UUID</param>
        /// <param name="sdruploadversion">current version</param>
        /// <returns> A <see cref="List{StudyDefinitionsEntity}"/> with matching studyId
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<Core.Entities.StudyV4.StudyDefinitionsEntity>> GetStudyItemsAsyncV4(string studyId, int sdruploadversion)
        {
            _logger.LogInformation($"Started Repository : {nameof(ChangeAuditRepository)}; Method : {nameof(GetStudyItemsAsyncV4)};");
            try
            {
                IMongoCollection<Core.Entities.StudyV4.StudyDefinitionsEntity> collection = _database.GetCollection<Core.Entities.StudyV4.StudyDefinitionsEntity>(Constants.Collections.StudyDefinitions);

                List<Core.Entities.StudyV4.StudyDefinitionsEntity> studies = await collection.Find(x => (x.Study.Id == studyId) &&
                                                           (x.AuditTrail.SDRUploadVersion == sdruploadversion || x.AuditTrail.SDRUploadVersion == sdruploadversion - 1))
                                                     .SortByDescending(s => s.AuditTrail.EntryDateTime)
                                                     .Limit(2)
                                                     .ToListAsync().ConfigureAwait(false);

                if (studies == null)
                {
                    _logger.LogWarning($"There are no studies with StudyId : {studyId} in {Constants.Collections.StudyDefinitions} Collection");
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
                _logger.LogInformation($"Ended Repository : {nameof(ChangeAuditRepository)}; Method : {nameof(GetStudyItemsAsyncV4)};");
            }
        }

        /// <summary>
		/// Get Current and previous version of study for study Id for V5 API Version
		/// </summary>
		/// <param name="studyId">Study UUID</param>
		/// <param name="sdruploadversion">current version</param>
		/// <returns> A <see cref="List{StudyDefinitionsEntity}"/> with matching studyId
		/// <see langword="null"/> If no study is matching with studyId
		/// </returns>
		public async Task<List<Core.Entities.StudyV5.StudyDefinitionsEntity>> GetStudyItemsAsyncV5(string studyId, int sdruploadversion)
        {
            _logger.LogInformation($"Started Repository : {nameof(ChangeAuditRepository)}; Method : {nameof(GetStudyItemsAsyncV5)};");
            try
            {
                IMongoCollection<Core.Entities.StudyV5.StudyDefinitionsEntity> collection = _database.GetCollection<Core.Entities.StudyV5.StudyDefinitionsEntity>(Constants.Collections.StudyDefinitions);

                List<Core.Entities.StudyV5.StudyDefinitionsEntity> studies = await collection.Find(x => (x.Study.Id == studyId) &&
                                                           (x.AuditTrail.SDRUploadVersion == sdruploadversion || x.AuditTrail.SDRUploadVersion == sdruploadversion - 1))
                                                     .SortByDescending(s => s.AuditTrail.EntryDateTime)
                                                     .Limit(2)
                                                     .ToListAsync().ConfigureAwait(false);

                if (studies == null)
                {
                    _logger.LogWarning($"There are no studies with StudyId : {studyId} in {Constants.Collections.StudyDefinitions} Collection");
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
                _logger.LogInformation($"Ended Repository : {nameof(ChangeAuditRepository)}; Method : {nameof(GetStudyItemsAsyncV5)};");
            }
        }
        #endregion
    }


}
