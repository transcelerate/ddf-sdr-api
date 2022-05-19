using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Core.DTO.UserGroups;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.DataAccess.Repositories
{
    public class UserGroupMappingRepository : IUserGroupMappingRepository
    {
        #region Variables     
        private readonly string _databaseName = Config.databaseName;
        private readonly ILogHelper _logger;

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        #endregion

        #region Constructor      
        public UserGroupMappingRepository(IMongoClient client, ILogHelper logger)
        {
            _client = client;
            _database = _client.GetDatabase(_databaseName);
            _logger = logger;
        }
        #endregion

        #region DB Operations

        #region GET

        /// <summary>
        /// GET All Users for a group
        /// </summary>        
        /// <returns> A <see cref="UserGroupMappingEntity"/> with List of Groups <br></br>
        /// <see langword="null"/> if there are no groups
        /// </returns>      
        public async Task<UserGroupMappingEntity> GetAllUserGroups()
        {
            _logger.LogInformation($"Started Repository : {nameof(UserGroupMappingRepository)}; Method : {nameof(GetAllUserGroups)};");
            try
            {
                var collection = _database.GetCollection<UserGroupMappingEntity>(Constants.Collections.SDRGrouping);                
                var userGroupMapping = await collection.Find(_ =>true).FirstOrDefaultAsync().ConfigureAwait(false);              
               
                if (userGroupMapping == null)
                {
                    _logger.LogWarning($"There is no group in {Constants.Collections.SDRGrouping} Collection");
                    return null;
                }
                else
                {
                    return userGroupMapping;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(UserGroupMappingRepository)}; Method : {nameof(GetAllUserGroups)};");
            }
        }

        /// <summary>
        /// GET all groups
        /// </summary>        
        /// <returns> A <see cref="SDRGroupsEntity"/> with List of Groups <br></br>
        /// <see langword="null"/> if there are no groups
        /// </returns>      
        public async Task<List<GroupDetailsEntity>> GetGroups(UserGroupsQueryParameters userGroupsQueryParameters)
        {
            _logger.LogInformation($"Started Repository : {nameof(UserGroupMappingRepository)}; Method : {nameof(GetGroups)};");
            try
            {
                var collection = _database.GetCollection<UserGroupMappingEntity>(Constants.Collections.SDRGrouping);
                
                var userGroups = await collection.Find(_ => true)
                                                       .Project(x=>x.SDRGroups.Where(x=>x.groupEnabled==true)
                                                       .Select(g=> new GroupDetailsEntity
                                                       {
                                                           groupName = g.groupName,
                                                           groupId = g.groupId,
                                                           groupDescription = g.groupDescription,  
                                                           groupFilter = g.groupFilter,
                                                           permission = g.permission,
                                                           groupCreatedOn = g.groupCreatedOn,
                                                           groupModifiedOn = g.groupModifiedOn,
                                                           groupEnabled = g.groupEnabled,
                                                           groupCreatedBy = g.groupCreatedBy,
                                                           groupModifiedBy = g.groupModifiedBy
                                                       })
                                                       .OrderGroups(userGroupsQueryParameters)
                                                       .Skip((userGroupsQueryParameters.pageNumber - 1) * userGroupsQueryParameters.pageSize)
                                                       .Take(userGroupsQueryParameters.pageSize).ToList())                                                       
                                                       .FirstOrDefaultAsync().ConfigureAwait(false);                
                if (userGroups == null)
                {
                    _logger.LogWarning($"There is no group in {Constants.Collections.SDRGrouping} Collection");
                    return null;
                }
                else
                {
                    return userGroups;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(UserGroupMappingRepository)}; Method : {nameof(GetGroups)};");
            }
        }

        /// <summary>
        /// GET group list
        /// </summary>        
        /// <returns> A <see cref="List{GroupListEntity}"/> with List of Group Name and Group Id <br></br>
        /// <see langword="null"/> if there are no groups
        /// </returns>      
        public async Task<List<GroupListEntity>> GetGroupList()
        {
            _logger.LogInformation($"Started Repository : {nameof(UserGroupMappingRepository)}; Method : {nameof(GetGroups)};");
            try
            {
                var collection = _database.GetCollection<UserGroupMappingEntity>(Constants.Collections.SDRGrouping);

                var userGroups = await collection.Find(_ => true)
                                                       .Project(x => x.SDRGroups.Where(x => x.groupEnabled == true)
                                                       .Select(g => new GroupListEntity
                                                       {
                                                           groupName = g.groupName,
                                                           groupId = g.groupId,                                                           
                                                       })
                                                       .OrderBy(x => x.groupName).ToList())
                                                       .FirstOrDefaultAsync().ConfigureAwait(false);
                if (userGroups == null)
                {
                    _logger.LogWarning($"There is no group in {Constants.Collections.SDRGrouping} Collection");
                    return null;
                }
                else
                {
                    return userGroups;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(UserGroupMappingRepository)}; Method : {nameof(GetGroups)};");
            }
        }

        /// <summary>
        /// GET a group with groupId
        /// </summary>        
        /// <returns> A <see cref="SDRGroupsEntity"/> with List of Group Name and Group Id <br></br>
        /// <see langword="null"/> if there are no groups
        /// </returns>      
        public async Task<SDRGroupsEntity> GetAGroupById(string groupId)
        {
            _logger.LogInformation($"Started Repository : {nameof(UserGroupMappingRepository)}; Method : {nameof(GetGroups)};");
            try
            {
                var collection = _database.GetCollection<UserGroupMappingEntity>(Constants.Collections.SDRGrouping);

                var userGroups = await collection.Find(_ => true)
                                                       .Project(x => x.SDRGroups.Where(x => x.groupId == groupId))
                                                       .FirstOrDefaultAsync()
                                                       .ConfigureAwait(false);
                if (userGroups == null)
                {
                    _logger.LogWarning($"There is no group in {Constants.Collections.SDRGrouping} Collection");
                    return null;
                }
                else
                {
                    return userGroups.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(UserGroupMappingRepository)}; Method : {nameof(GetGroups)};");
            }
        }
        #endregion
        #region POST
        /// <summary>
        /// Add a Group 
        /// </summary>
        /// <param name="group">Update User Group Mapping</param>
        /// <returns>
        /// A <see cref="SDRGroupsEntity"/> <br></br>        
        /// </returns>
        public async Task<SDRGroupsEntity> AddAGroup(SDRGroupsEntity group)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(AddAGroup)};");
            try
            {
                var collection = _database.GetCollection<UserGroupMappingEntity>(Constants.Collections.SDRGrouping);
                var updateDefinition = Builders<UserGroupMappingEntity>.Update
                                  .Push(s => s.SDRGroups, group);
                await collection.UpdateOneAsync(_=>true,
                                                   updateDefinition).ConfigureAwait(false);

                return (group);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(AddAGroup)};");
            }
        }

        /// <summary>
        /// Updates A Group 
        /// </summary>
        /// <param name="group">Add User Group Mapping</param>
        /// <returns>
        /// A <see cref="SDRGroupsEntity"/> <br></br>        
        /// </returns>
        public async Task<SDRGroupsEntity> UpdateAGroup(SDRGroupsEntity group)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(UpdateAGroup)};");
            try
            {
                var collection = _database.GetCollection<UserGroupMappingEntity>(Constants.Collections.SDRGrouping);
                var filter = Builders<UserGroupMappingEntity>.Filter.Where(x => x.SDRGroups.Any(x => x.groupId == group.groupId));
                var updateDefinition = Builders<UserGroupMappingEntity>.Update
                                    .Set(s => s.SDRGroups[-1], group);
                await collection.UpdateOneAsync(filter, 
                                                   updateDefinition).ConfigureAwait(false); 

                return (group);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(UpdateAGroup)};");
            }
        }

        /// <summary>
        /// Updates User Group Mapping
        /// </summary>
        /// <param name="userGroupMappingEntity">Add User Group Mapping</param>
        /// <returns>
        /// A <see cref="UserGroupMappingEntity"/> <br></br>         
        /// </returns>
        public async Task<UserGroupMappingEntity> UpdateUsersToGroups(UserGroupMappingEntity userGroupMappingEntity)
        {
            _logger.LogInformation($"Started Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(UpdateUsersToGroups)};");
            try
            {
                var collection = _database.GetCollection<UserGroupMappingEntity>(Constants.Collections.SDRGrouping);
                var updateDefinition = Builders<UserGroupMappingEntity>.Update
                                  .Set(s => s.SDRGroups, userGroupMappingEntity.SDRGroups);
                await collection.UpdateOneAsync(_ => true,
                                                   updateDefinition).ConfigureAwait(false);

                return (userGroupMappingEntity);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(ClinicalStudyRepository)}; Method : {nameof(UpdateUsersToGroups)};");
            }
        }
        #endregion
        #endregion
    }
}
