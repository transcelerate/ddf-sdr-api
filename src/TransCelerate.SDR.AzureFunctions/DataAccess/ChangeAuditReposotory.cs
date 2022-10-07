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

namespace TransCelerate.SDR.AzureFunctions.DataAccess
{
    public class ChangeAuditReposotory : IChangeAuditReposotory
    {
        #region Variables     
        private readonly string _databaseName = Config.DatabaseName;
        private readonly ILogHelper _logger;

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        #endregion

        #region Constructor      
        public ChangeAuditReposotory(IMongoClient client, ILogHelper logger)
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
        /// <summary>
        /// Get Current and previous version of study for study Id
        /// </summary>
        /// <param name="studyId">Study UUID</param>
        /// <param name="sdruploadversion">current version</param>
        /// <returns> A <see cref="List{StudyEntity}"/> with matching studyId
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public List<StudyEntity> GetStudyItemsAsync(string studyId, int sdruploadversion)
        {
            _logger.LogInformation($"Started Repository : {nameof(ChangeAuditReposotory)}; Method : {nameof(GetStudyItemsAsync)};");
            try
            {
                IMongoCollection<StudyEntity> collection = _database.GetCollection<StudyEntity>(Constants.Collections.StudyV1);


                List<StudyEntity> studies = collection.Find(x => (x.ClinicalStudy.Uuid == studyId) &&
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
                _logger.LogInformation($"Ended Repository : {nameof(ChangeAuditReposotory)}; Method : {nameof(GetStudyItemsAsync)};");
            }
        }
        /// <summary>
        /// Get Audit Details for a Study Id from Change Audit Collections
        /// </summary>
        /// <param name="studyId">Study UUID</param>
        /// <returns> A <see cref="ChangeAuditEntity"/> with matching studyId
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>

        public ChangeAuditEntity GetChangeAuditAsync(string studyId)
        {
            _logger.LogInformation($"Started Repository : {nameof(ChangeAuditReposotory)}; Method : {nameof(GetChangeAuditAsync)};");
            try
            {
                IMongoCollection<ChangeAuditEntity> collection = _database.GetCollection<ChangeAuditEntity>(Constants.Collections.ChangeAudit);


                ChangeAuditEntity changeAudit = collection.Find(x => x.Study_uuid == studyId)
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
                _logger.LogInformation($"Ended Repository : {nameof(ChangeAuditReposotory)}; Method : {nameof(GetChangeAuditAsync)};");
            }
        }
        /// <summary>
        /// Insert a Change Audit for a study
        /// </summary>
        /// <param name="changeAudit"></param>
        public void InsertChangeAudit(ChangeAuditEntity changeAudit)
        {
            _logger.LogInformation($"Started Repository : {nameof(ChangeAuditReposotory)}; Method : {nameof(InsertChangeAudit)};");
            try
            {
                IMongoCollection<ChangeAuditEntity> collection = _database.GetCollection<ChangeAuditEntity>(Constants.Collections.ChangeAudit);
                collection.InsertOne(changeAudit); //Insert One Document               
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ChangeAuditReposotory)}; Method : {nameof(InsertChangeAudit)};");
            }
        }
        /// <summary>
        /// Update existing change audit
        /// </summary>
        /// <param name="changeAudit"></param>
        public void UpdateChangeAudit(ChangeAuditEntity changeAudit)
        {
            _logger.LogInformation($"Started Repository : {nameof(ChangeAuditReposotory)}; Method : {nameof(UpdateChangeAudit)};");
            try
            {
                IMongoCollection<ChangeAuditEntity> collection = _database.GetCollection<ChangeAuditEntity>(Constants.Collections.ChangeAudit);
                UpdateDefinition<ChangeAuditEntity> updateDefinition = Builders<ChangeAuditEntity>.Update
                                    .Set(s => s.Changes, changeAudit.Changes);
                collection.UpdateOne(x => x.Study_uuid == changeAudit.Study_uuid,
                                                   updateDefinition); // Update clinicalStudy and auditTrail           
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ChangeAuditReposotory)}; Method : {nameof(UpdateChangeAudit)};");
            }
        }
        #endregion
    }
}
