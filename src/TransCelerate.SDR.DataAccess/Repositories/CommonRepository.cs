﻿using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.DataAccess.Filters;
using TransCelerate.SDR.DataAccess.Interfaces;

namespace TransCelerate.SDR.DataAccess.Repositories
{
    public  class CommonRepository : ICommonRepository
    {
        #region Variables     
        private readonly string _databaseName = Config.DatabaseName;
        private readonly ILogHelper _logger;

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        #endregion

        #region Constructor      
        public CommonRepository(IMongoClient client, ILogHelper logger)
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
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<GetRawJsonEntity> GetStudyItemsAsync(string studyId, int sdruploadversion)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepositoryV2)}; Method : {nameof(GetStudyItemsAsync)};");
            try
            {
                IMongoCollection<GetRawJsonEntity> collection = _database.GetCollection<GetRawJsonEntity>(Constants.Collections.StudyDefinitions);

                GetRawJsonEntity study = await collection.Find(DataFilterCommon.GetFiltersForGetStudy(studyId, sdruploadversion))
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
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepositoryV2)}; Method : {nameof(GetStudyItemsAsync)};");
            }
        }

        /// <summary>
        /// GET List of study for a study ID
        /// </summary>
        /// <param name="fromDate">Start Date for Date Filter</param>
        /// <param name="toDate">End Date for Date Filter</param>
        /// <param name="studyId">Study ID</param>
        /// <returns>
        /// A <see cref="List{AuditTrailResponseEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<AuditTrailResponseEntity>> GetAuditTrail(string studyId, DateTime fromDate, DateTime toDate)
        {
            _logger.LogInformation($"Started Repository : {nameof(CommonRepository)}; Method : {nameof(GetAuditTrail)};");
            try
            {
                var collection = _database.GetCollection<CommonStudyEntity>(Constants.Collections.StudyDefinitions);
                List<AuditTrailResponseEntity> auditTrails = new List<AuditTrailResponseEntity>();
                auditTrails = await collection.Find(DataFilterCommon.GetFiltersForGetAudTrail(studyId, fromDate, toDate)) // Condition for matching studyId and date range
                                                  .Project(x => new AuditTrailResponseEntity
                                                  {
                                                      StudyType = x.ClinicalStudy.StudyType,
                                                      EntryDateTime = x.AuditTrail.EntryDateTime,
                                                      SDRUploadVersion = x.AuditTrail.SDRUploadVersion,
                                                      UsdmVersion = x.AuditTrail.UsdmVersion,
                                                      HasAccess = true
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
                _logger.LogInformation($"Ended Repository : {nameof(CommonRepository)}; Method : {nameof(GetAuditTrail)};");
            }
        }

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
        #endregion
        #endregion
    }
}
