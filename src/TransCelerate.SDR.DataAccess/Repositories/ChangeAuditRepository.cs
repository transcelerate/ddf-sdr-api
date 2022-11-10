using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Entities.StudyV2;
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
            var conventionPack = new ConventionPack();
            conventionPack.Add(new CamelCaseElementNameConvention());
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
        #endregion
    }


}
