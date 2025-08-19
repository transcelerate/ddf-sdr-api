using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Entities.StudyV4;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.DataAccess.Filters;
using TransCelerate.SDR.DataAccess.Interfaces;

namespace TransCelerate.SDR.DataAccess.Repositories
{
    public class StudyRepositoryV4 : IStudyRepositoryV4
    {
        #region Variables     
        private readonly string _databaseName = Config.DatabaseName;
        private readonly ILogHelper _logger;

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        #endregion

        #region Constructor      
        public StudyRepositoryV4(IMongoClient client, ILogHelper logger)
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

        #region GET
        /// <summary>
        /// GET a Study for a study ID with version filter
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <returns>
        /// A <see cref="StudyDefinitionsEntity"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<StudyDefinitionsEntity> GetStudyItemsAsync(string studyId, int sdruploadversion)
        {
            _logger.LogInformation($"Started Repository : {nameof(StudyRepositoryV2)}; Method : {nameof(GetStudyItemsAsync)};");
            try
            {
                IMongoCollection<StudyDefinitionsEntity> collection = _database.GetCollection<StudyDefinitionsEntity>(Constants.Collections.StudyDefinitions);


                StudyDefinitionsEntity study = await collection.Find(DataFiltersV4.GetFiltersForGetStudy(studyId, sdruploadversion))
                                                     .SortByDescending(s => s.AuditTrail.EntryDateTime) // Sort by descending on entryDateTime
                                                     .Limit(1)                  //Taking top 1 result
                                                     .SingleOrDefaultAsync().ConfigureAwait(false);

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
                _logger.LogInformation($"Ended Repository : {nameof(StudyRepositoryV2)}; Method : {nameof(GetStudyItemsAsync)};");
            }
        }

        /// <summary>
        /// GET a Study for a study ID with version filter
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="listofelementsArray">Array of study elements</param>
        /// <returns>
        /// A <see cref="StudyDefinitionsEntity"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<StudyDefinitionsEntity> GetPartialStudyItemsAsync(string studyId, int sdruploadversion, string[] listofelementsArray)
        {
            _logger.LogInformation($"Started Repository : {nameof(StudyRepositoryV4)}; Method : {nameof(GetPartialStudyItemsAsync)};");
            try
            {
                IMongoCollection<StudyDefinitionsEntity> collection = _database.GetCollection<StudyDefinitionsEntity>(Constants.Collections.StudyDefinitions);


                StudyDefinitionsEntity study = await collection.Find(DataFiltersV4.GetFiltersForGetStudy(studyId, sdruploadversion))
                                                     .SortByDescending(s => s.AuditTrail.EntryDateTime) // Sort by descending on entryDateTime
                                                     .Limit(1)                  //Taking top 1 result
                                                     //.Project<StudyDefinitionsEntity>(DataFiltersV4.GetProjectionForPartialStudyElements(listofelementsArray))
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
                _logger.LogInformation($"Ended Repository : {nameof(StudyRepositoryV4)}; Method : {nameof(GetPartialStudyItemsAsync)};");
            }
        }

        /// <summary>
        /// GET Study Designs for a Study Id
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <returns></returns>
        public async Task<StudyDefinitionsEntity> GetPartialStudyDesignItemsAsync(string studyId, int sdruploadversion)
        {
            _logger.LogInformation($"Started Repository : {nameof(StudyRepositoryV2)}; Method : {nameof(GetPartialStudyDesignItemsAsync)};");
            try
            {
                IMongoCollection<StudyDefinitionsEntity> collection = _database.GetCollection<StudyDefinitionsEntity>(Constants.Collections.StudyDefinitions);


                StudyDefinitionsEntity study = await collection.Find(DataFiltersV4.GetFiltersForGetStudy(studyId, sdruploadversion))
                                                     //.Project<StudyDefinitionsEntity>(DataFiltersV4.GetProjectionForPartialStudyDesignElementsFullStudy())
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
                _logger.LogInformation($"Ended Repository : {nameof(StudyRepositoryV2)}; Method : {nameof(GetPartialStudyDesignItemsAsync)};");
            }
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
        public async Task<string> PostStudyItemsAsync(StudyDefinitionsEntity study)
        {
            _logger.LogInformation($"Started Repository : {nameof(StudyRepositoryV2)}; Method : {nameof(PostStudyItemsAsync)};");
            try
            {
                IMongoCollection<StudyDefinitionsEntity> collection = _database.GetCollection<StudyDefinitionsEntity>(Constants.Collections.StudyDefinitions);
				//***** Added by Swathi for 441
				var filterOldVersion = Builders<StudyDefinitionsEntity>.Filter.Eq(s => s.Study.Id, study.Study.Id) &
									  Builders<StudyDefinitionsEntity>.Filter.Eq(s => s.AuditTrail.SDRUploadFlag, 1);

				var updateOldVersion = Builders<StudyDefinitionsEntity>.Update
					.Set(s => s.AuditTrail.SDRUploadFlag, 0);

				var result = await collection.UpdateOneAsync(filterOldVersion, updateOldVersion).ConfigureAwait(false);
				// **** *****************************************

				await collection.InsertOneAsync(study).ConfigureAwait(false); //Insert One Document                
                return (study.Study.Id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(StudyRepositoryV2)}; Method : {nameof(PostStudyItemsAsync)};");
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
        public async Task<string> UpdateStudyItemsAsync(StudyDefinitionsEntity study)
        {
            _logger.LogInformation($"Started Repository : {nameof(StudyRepositoryV2)}; Method : {nameof(UpdateStudyItemsAsync)};");
            try
            {
                IMongoCollection<StudyDefinitionsEntity> collection = _database.GetCollection<StudyDefinitionsEntity>(Constants.Collections.StudyDefinitions);

                UpdateDefinition<StudyDefinitionsEntity> updateDefinition = Builders<StudyDefinitionsEntity>.Update
                                    .Set(s => s.Study, study.Study)
                                    .Set(s => s.AuditTrail, study.AuditTrail);
                await collection.UpdateOneAsync(x => (x.Study.Id == study.Study.Id
                                                   && x.AuditTrail.SDRUploadVersion == study.AuditTrail.SDRUploadVersion), //Match studyId and studyVersion
                                                   updateDefinition).ConfigureAwait(false); // Update study and auditTrail

                return (study.Study.Id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(StudyRepositoryV2)}; Method : {nameof(UpdateStudyItemsAsync)};");
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
            _logger.LogInformation($"Started Repository : {nameof(StudyRepositoryV2)}; Method : {nameof(DeleteStudyAsync)};");
            try
            {
                IMongoCollection<StudyDefinitionsEntity> collection = _database.GetCollection<StudyDefinitionsEntity>(Constants.Collections.StudyDefinitions);
                var builder = Builders<StudyDefinitionsEntity>.Filter.Eq(x => x.Study.Id, study_uuid);

                var deleteResult = await collection.DeleteManyAsync(builder).ConfigureAwait(false);

                return deleteResult;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(StudyRepositoryV2)}; Method : {nameof(DeleteStudyAsync)};");
            }
        }
        /// <summary>
        /// Count Documents
        /// </summary>
        /// <param name="study_uuid"> Study Id</param>
        /// <returns></returns>
        public async Task<long> CountAsync(string study_uuid)
        {
            _logger.LogInformation($"Started Repository : {nameof(StudyRepositoryV2)}; Method : {nameof(CountAsync)};");
            try
            {
                IMongoCollection<StudyDefinitionsEntity> collection = _database.GetCollection<StudyDefinitionsEntity>(Constants.Collections.StudyDefinitions);
                var builder = Builders<StudyDefinitionsEntity>.Filter.Eq(x => x.Study.Id, study_uuid);
                long count = await collection.CountDocumentsAsync(builder).ConfigureAwait(false);

                return count;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(StudyRepositoryV2)}; Method : {nameof(CountAsync)};");
            }
        }
        #endregion

        #region GET USDM Version Of a StudyId
        /// <summary>
        /// GET a Study for a study ID with version filter
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <returns>
        /// A <see cref="AuditTrailEntity"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<AuditTrailEntity> GetUsdmVersionAsync(string studyId, int sdruploadversion)
        {
            _logger.LogInformation($"Started Repository : {nameof(StudyRepositoryV2)}; Method : {nameof(GetUsdmVersionAsync)};");
            try
            {
                IMongoCollection<StudyDefinitionsEntity> collection = _database.GetCollection<StudyDefinitionsEntity>(Constants.Collections.StudyDefinitions);


                AuditTrailEntity auditTrail = await collection.Find(DataFiltersV4.GetFiltersForGetAuditTrailOfAStudy(studyId, sdruploadversion))
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
                _logger.LogInformation($"Ended Repository : {nameof(StudyRepositoryV2)}; Method : {nameof(GetUsdmVersionAsync)};");
            }
        }
        #endregion
        #endregion
    }
}
