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


                ChangeAuditStudyEntity changeAuditStudyEntity = await collection.Find(DataFiltersV2.GetFiltersForChangeAudit(studyId))
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
        public async Task<string> InsertChangeAudit(string study_uuid, int sdruploadversion, DateTime entrydatetime)
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

        #endregion
    }


}
