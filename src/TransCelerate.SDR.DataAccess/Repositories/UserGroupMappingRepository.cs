using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.UserGroups;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.DataAccess.Interfaces;

namespace TransCelerate.SDR.DataAccess.Repositories
{
    public class UserGroupMappingRepository : IUserGroupMappingRepository
    {
        #region Variables     
        private readonly string _databaseName = Config.DatabaseName;
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
                var userGroupMapping = await collection.Find(_ => true).FirstOrDefaultAsync().ConfigureAwait(false);

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
                List<GroupDetailsEntity> userGroups = new();
                if (userGroupsQueryParameters.PageNumber == 0 || userGroupsQueryParameters.PageSize == 0)
                {
                    userGroups = await collection.Find(_ => true)
                                                       .Project(x => x.SDRGroups.Where(x => x.GroupEnabled == true)
                                                       .Select(g => new GroupDetailsEntity
                                                       {
                                                           GroupName = g.GroupName,
                                                           GroupId = g.GroupId,
                                                           GroupDescription = g.GroupDescription,
                                                           GroupFilter = g.GroupFilter,
                                                           Permission = g.Permission,
                                                           GroupCreatedOn = g.GroupCreatedOn,
                                                           GroupModifiedOn = g.GroupModifiedOn,
                                                           GroupEnabled = g.GroupEnabled,
                                                           GroupCreatedBy = g.GroupCreatedBy,
                                                           GroupModifiedBy = g.GroupModifiedBy
                                                       })
                                                       .OrderGroups(userGroupsQueryParameters)
                                                       .ToList())
                                                       .FirstOrDefaultAsync().ConfigureAwait(false);
                }
                else
                {
                    userGroups = await collection.Find(_ => true)
                                                      .Project(x => x.SDRGroups.Where(x => x.GroupEnabled == true)
                                                      .Select(g => new GroupDetailsEntity
                                                      {
                                                          GroupName = g.GroupName,
                                                          GroupId = g.GroupId,
                                                          GroupDescription = g.GroupDescription,
                                                          GroupFilter = g.GroupFilter,
                                                          Permission = g.Permission,
                                                          GroupCreatedOn = g.GroupCreatedOn,
                                                          GroupModifiedOn = g.GroupModifiedOn,
                                                          GroupEnabled = g.GroupEnabled,
                                                          GroupCreatedBy = g.GroupCreatedBy,
                                                          GroupModifiedBy = g.GroupModifiedBy
                                                      })
                                                      .OrderGroups(userGroupsQueryParameters)
                                                      .Skip((userGroupsQueryParameters.PageNumber - 1) * userGroupsQueryParameters.PageSize)
                                                      .Take(userGroupsQueryParameters.PageSize).ToList())
                                                      .FirstOrDefaultAsync().ConfigureAwait(false);
                }
                if (userGroups == null)
                {
                    _logger.LogWarning($"There is no group in {Constants.Collections.SDRGrouping} Collection");
                    return null;
                }
                else if (userGroups.Count == 0)
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
                                                       .Project(x => x.SDRGroups.Where(x => x.GroupEnabled == true)
                                                       .Select(g => new GroupListEntity
                                                       {
                                                           GroupName = g.GroupName,
                                                           GroupId = g.GroupId,
                                                       })
                                                       .OrderBy(x => x.GroupName).ToList())
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
                                                       .Project(x => x.SDRGroups.Where(x => x.GroupId == groupId))
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

        /// <summary>
        /// GET group by groupName
        /// </summary>        
        /// <returns> A <see cref="SDRGroupsEntity"/> with List of Group Name and Group Id <br></br>
        /// <see langword="null"/> if there are no groups
        /// </returns>      
        public async Task<SDRGroupsEntity> GetGroupByName(string groupName)
        {
            _logger.LogInformation($"Started Repository : {nameof(UserGroupMappingRepository)}; Method : {nameof(GetGroupByName)};");
            try
            {
                var collection = _database.GetCollection<UserGroupMappingEntity>(Constants.Collections.SDRGrouping);

                var userGroups = await collection.Find(_ => true)
                                                       .Project(x => x.SDRGroups.Where(x => x.GroupName.ToLower() == groupName.ToLower()))
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
                _logger.LogInformation($"Ended Repository : {nameof(UserGroupMappingRepository)}; Method : {nameof(GetGroupByName)};");
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
            _logger.LogInformation($"Started Repository : {nameof(UserGroupMappingRepository)}; Method : {nameof(AddAGroup)};");
            try
            {
                var collection = _database.GetCollection<UserGroupMappingEntity>(Constants.Collections.SDRGrouping);
                var updateDefinition = Builders<UserGroupMappingEntity>.Update
                                  .Push(s => s.SDRGroups, group);
                await collection.UpdateOneAsync(_ => true,
                                                   updateDefinition).ConfigureAwait(false);

                return (group);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Repository : {nameof(UserGroupMappingRepository)}; Method : {nameof(AddAGroup)};");
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
            _logger.LogInformation($"Started Repository : {nameof(UserGroupMappingRepository)}; Method : {nameof(UpdateAGroup)};");
            try
            {
                var collection = _database.GetCollection<UserGroupMappingEntity>(Constants.Collections.SDRGrouping);
                var filter = Builders<UserGroupMappingEntity>.Filter.Where(x => x.SDRGroups.Any(x => x.GroupId == group.GroupId));
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
                _logger.LogInformation($"Ended Repository : {nameof(UserGroupMappingRepository)}; Method : {nameof(UpdateAGroup)};");
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
            _logger.LogInformation($"Started Repository : {nameof(UserGroupMappingRepository)}; Method : {nameof(UpdateUsersToGroups)};");
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
                _logger.LogInformation($"Ended Repository : {nameof(UserGroupMappingRepository)}; Method : {nameof(UpdateUsersToGroups)};");
            }
        }
        #endregion
        #endregion
    }
}
