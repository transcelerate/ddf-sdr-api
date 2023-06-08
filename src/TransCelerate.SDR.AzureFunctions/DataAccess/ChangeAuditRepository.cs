using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.AzureFunctions.DataAccess
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
        /// <summary>
        /// Get Current and previous version of study for study Id for V2 API Version
        /// </summary>
        /// <param name="studyId">Study UUID</param>
        /// <param name="sdruploadversion">current version</param>
        /// <returns> A <see cref="List{StudyEntity}"/> with matching studyId
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public List<Core.Entities.StudyV2.StudyDefinitionsEntity> GetStudyItemsAsyncV2(string studyId, int sdruploadversion)
        {
            _logger.LogInformation($"Started Repository : {nameof(ChangeAuditRepository)}; Method : {nameof(GetStudyItemsAsyncV2)};");
            try
            {
                IMongoCollection<Core.Entities.StudyV2.StudyDefinitionsEntity> collection = _database.GetCollection<Core.Entities.StudyV2.StudyDefinitionsEntity>(Constants.Collections.StudyDefinitions);


                List<Core.Entities.StudyV2.StudyDefinitionsEntity> studies = collection.Find(x => (x.Study.StudyId == studyId) &&
                                                           (x.AuditTrail.SDRUploadVersion == sdruploadversion || x.AuditTrail.SDRUploadVersion == sdruploadversion - 1))
                                                     .SortByDescending(s => s.AuditTrail.EntryDateTime)
                                                     .Limit(2)
                                                     .ToList();

                if (studies == null)
                {
                    _logger.LogWarning($"There are no studies with StudyId : {studyId} in {Constants.Collections.StudyV1} Collection");
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
                _logger.LogInformation($"Ended Repository : {nameof(ChangeAuditRepository)}; Method : {nameof(GetStudyItemsAsyncV2)};");
            }
        }
        /// <summary>
        /// Get Current and previous version of study for study Id for V3 API Version
        /// </summary>
        /// <param name="studyId">Study UUID</param>
        /// <param name="sdruploadversion">current version</param>
        /// <returns> A <see cref="List{StudyEntity}"/> with matching studyId
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public List<Core.Entities.StudyV3.StudyDefinitionsEntity> GetStudyItemsAsyncV3(string studyId, int sdruploadversion)
        {
            _logger.LogInformation($"Started Repository : {nameof(ChangeAuditRepository)}; Method : {nameof(GetStudyItemsAsyncV3)};");
            try
            {
                IMongoCollection<Core.Entities.StudyV3.StudyDefinitionsEntity> collection = _database.GetCollection<Core.Entities.StudyV3.StudyDefinitionsEntity>(Constants.Collections.StudyDefinitions);


                List<Core.Entities.StudyV3.StudyDefinitionsEntity> studies = collection.Find(x => (x.Study.StudyId == studyId) &&
                                                           (x.AuditTrail.SDRUploadVersion == sdruploadversion || x.AuditTrail.SDRUploadVersion == sdruploadversion - 1))
                                                     .SortByDescending(s => s.AuditTrail.EntryDateTime)
                                                     .Limit(2)
                                                     .ToList();

                if (studies == null)
                {
                    _logger.LogWarning($"There are no studies with StudyId : {studyId} in {Constants.Collections.StudyV1} Collection");
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
        /// Get Current and previous version of study for study Id
        /// </summary>
        /// <param name="studyId">Study UUID</param>
        /// <param name="sdruploadversion">current version</param>
        /// <returns> A <see cref="List{StudyEntity}"/> with matching studyId
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public List<Core.Entities.Common.AuditTrailEntity> GetAuditTrailsAsync(string studyId, int sdruploadversion)
        {
            _logger.LogInformation($"Started Repository : {nameof(ChangeAuditRepository)}; Method : {nameof(GetAuditTrailsAsync)};");
            try
            {
                IMongoCollection<CommonStudyDefinitionsEntity> collection = _database.GetCollection<CommonStudyDefinitionsEntity>(Constants.Collections.StudyDefinitions);


                List<Core.Entities.Common.AuditTrailEntity> auditTrails = collection.Find(x => (x.Study.StudyId == studyId) &&
                                                           (x.AuditTrail.SDRUploadVersion == sdruploadversion || x.AuditTrail.SDRUploadVersion == sdruploadversion - 1))
                                                     .SortByDescending(s => s.AuditTrail.EntryDateTime)
                                                     .Limit(2)
                                                     .Project(x => x.AuditTrail)
                                                     .ToList();

                if (auditTrails == null)
                {
                    _logger.LogWarning($"There are no studies with StudyId : {studyId} in {Constants.Collections.StudyV1} Collection");
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
                _logger.LogInformation($"Ended Repository : {nameof(ChangeAuditRepository)}; Method : {nameof(GetAuditTrailsAsync)};");
            }
        }
        /// <summary>
        /// Get Audit Details for a Study Id from Change Audit Collections
        /// </summary>
        /// <param name="studyId">Study UUID</param>
        /// <returns> A <see cref="ChangeAuditEntity"/> with matching studyId
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>

        public ChangeAuditStudyEntity GetChangeAuditAsync(string studyId)
        {
            _logger.LogInformation($"Started Repository : {nameof(ChangeAuditRepository)}; Method : {nameof(GetChangeAuditAsync)};");
            try
            {
                IMongoCollection<ChangeAuditStudyEntity> collection = _database.GetCollection<ChangeAuditStudyEntity>(Constants.Collections.ChangeAudit);


                ChangeAuditStudyEntity changeAudit = collection.Find(x => x.ChangeAudit.StudyId == studyId)
                                                     .FirstOrDefault();

                if (changeAudit == null)
                {
                    _logger.LogWarning($"There is no Audit Details for the study with StudyId : {studyId} in {Constants.Collections.ChangeAudit} Collection");
                    return null;
                }
                else
                {
                    return changeAudit;
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
        /// <summary>
        /// Insert a Change Audit for a study
        /// </summary>
        /// <param name="changeAudit"></param>
        public void InsertChangeAudit(ChangeAuditStudyEntity changeAudit)
        {
            _logger.LogInformation($"Started Repository : {nameof(ChangeAuditRepository)}; Method : {nameof(InsertChangeAudit)};");
            try
            {
                IMongoCollection<ChangeAuditStudyEntity> collection = _database.GetCollection<ChangeAuditStudyEntity>(Constants.Collections.ChangeAudit);
                changeAudit.Id = MongoDB.Bson.ObjectId.GenerateNewId();
                collection.InsertOne(changeAudit); //Insert One Document               
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
        /// <summary>
        /// Update existing change audit
        /// </summary>
        /// <param name="changeAudit"></param>
        public void UpdateChangeAudit(ChangeAuditStudyEntity changeAudit)
        {
            _logger.LogInformation($"Started Repository : {nameof(ChangeAuditRepository)}; Method : {nameof(UpdateChangeAudit)};");
            try
            {
                IMongoCollection<ChangeAuditStudyEntity> collection = _database.GetCollection<ChangeAuditStudyEntity>(Constants.Collections.ChangeAudit);
                UpdateDefinition<ChangeAuditStudyEntity> updateDefinition = Builders<ChangeAuditStudyEntity>.Update
                                    .Set(s => s.ChangeAudit.Changes, changeAudit.ChangeAudit.Changes);
                collection.UpdateOne(x => x.ChangeAudit.StudyId == changeAudit.ChangeAudit.StudyId,
                                                   updateDefinition); // Update study and auditTrail           
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ChangeAuditRepository)}; Method : {nameof(UpdateChangeAudit)};");
            }
        }
        #endregion
    }
}
