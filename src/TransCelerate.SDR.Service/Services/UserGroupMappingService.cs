using AutoMapper;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.UserGroups;
using TransCelerate.SDR.Core.Entities.Study;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Interfaces;

namespace TransCelerate.SDR.Services.Services
{
    public class UserGroupMappingService : IUserGroupMappingService
    {
        #region Variables
        private readonly IUserGroupMappingRepository _userGroupMappingRepository;
        private readonly IMapper _mapper;
        private readonly ILogHelper _logger;
        #endregion

        #region Constructor
        public UserGroupMappingService(IUserGroupMappingRepository userGroupMappingRepository, IMapper mapper, ILogHelper logger)
        {
            _userGroupMappingRepository = userGroupMappingRepository;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion

        #region Action Methods

        #region GET
        /// <summary>
        /// GET All Groups
        /// </summary>        
        /// <returns> A <see cref="List{GroupDetailsDTO}"/> with List of Groups <br />
        /// <see langword="null"/> if there are no groups
        /// </returns>
        public async Task<List<GroupDetailsDTO>> GetUserGroups(UserGroupsQueryParameters userGroupsQueryParameters)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(UserGroupMappingService)}; Method : {nameof(GetUserGroups)};");
                var userGroupsEntity = await _userGroupMappingRepository.GetGroups(userGroupsQueryParameters);
                if(userGroupsEntity == null)
                {
                    return null;
                }
                else
                {
                    var userGroupsDTO = _mapper.Map<List<GroupDetailsDTO>>(userGroupsEntity);
                    return userGroupsDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(UserGroupMappingService)}; Method : {nameof(GetUserGroups)};");
            }
        }

        /// <summary>
        /// GET All Users for a group
        /// </summary>        
        /// <returns> A <see cref="object"/> with List of users with groups assined <br />
        /// <see langword="null"/> if there are no users in the group
        /// </returns>  
        public async Task<object> GetUsersList(UserGroupsQueryParameters userGroupsQueryParameters)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(UserGroupMappingService)}; Method : {nameof(GetUsersList)};");
                var userGroupsEntity = await _userGroupMappingRepository.GetAllUserGroups();
                if (userGroupsEntity == null)
                {
                    return null;
                }
                else
                {
                    //remove disabled groups
                    if (userGroupsEntity.SDRGroups == null)
                        return null;
                    userGroupsEntity.SDRGroups.RemoveAll(x => x.groupEnabled == false);                    
                    var userGroupsDTO = _mapper.Map<UserGroupMappingDTO>(userGroupsEntity);

                    //Get list of users with group name
                    List<UsersDTO> users = new List<UsersDTO>();
                    foreach (var userGroup in userGroupsDTO.SDRGroups)
                    {
                        if(userGroup.users!=null)
                        {
                            if (users.Count == 0)
                                users = userGroup.users;
                            else
                                users.AddRange(userGroup.users);
                        }
                    }

                    //remove disable group for a user
                    users.RemoveAll(x => x.isActive == false);

                    int userCount = users.Count;
                    if(userGroupsQueryParameters.pageNumber == 0 || userGroupsQueryParameters.pageSize==0)
                    {
                        userGroupsQueryParameters.pageSize = userCount;
                        userGroupsQueryParameters.pageNumber = 1;
                    }

                    //conversion of group list to user list
                    var userWithListOfGroups = users.GroupBy(x => x.email)
                                                     .Select(g => new 
                                                     {
                                                         email = g.Key,
                                                         oid = g.Select(x => x.oid).First(),
                                                         groups = g.Select(x => new GroupsTaggedToUser { groupName = x.groupName, groupId = x.groupId, isActive = x.isActive }).ToList(),
                                                         date = g.Max(x=>x.groupModifiedOn)
                                                     })
                                                     .OrderByDescending(x=>x.date)
                                                     .Select(x=> new PostUserToGroupsDTO
                                                     {
                                                         email = x.email,oid = x.oid, groups = x.groups
                                                     })                                                    
                                                     .OrderUsers(userGroupsQueryParameters)
                                                     .Skip((userGroupsQueryParameters.pageNumber - 1) * userGroupsQueryParameters.pageSize)
                                                     .Take(userGroupsQueryParameters.pageSize).ToList();
                    if (userWithListOfGroups.Count() == 0)
                        return null;
                    else
                        return userWithListOfGroups;

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(UserGroupMappingService)}; Method : {nameof(GetUsersList)};");
            }
        }

        /// <summary>
        /// GET All groups
        /// </summary>        
        /// <returns> A <see cref="List{GroupListDTO}"/> with List of groupId and groupName <br />
        /// <see langword="null"/> if there are no groups
        /// </returns>  
        public async Task<List<GroupListDTO>> ListGroups()
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(UserGroupMappingService)}; Method : {nameof(ListGroups)};");
                var userGroupListEntity = await _userGroupMappingRepository.GetGroupList();
                
                if(userGroupListEntity == null || userGroupListEntity.Count==0)
                    return new List<GroupListDTO>();

                var userGroupListDTO = _mapper.Map<List<GroupListDTO>>(userGroupListEntity);

                return userGroupListDTO;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(UserGroupMappingService)}; Method : {nameof(ListGroups)};");
            }
        }
        /// <summary>
        /// GET Group by groupName
        /// </summary>        
        /// <returns> A <see cref="object"/> with List of groupId and groupName <br />
        /// <see langword="null"/> if there are no groups
        /// </returns>  
        public async Task<object> CheckGroupName(string groupName)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(UserGroupMappingService)}; Method : {nameof(ListGroups)};");
                var userGroupEntity = await _userGroupMappingRepository.GetGroupByName(groupName);

                if (userGroupEntity == null)
                    return new {groupName = groupName,isExists = false};

                else
                    return new { groupName = groupName, isExists = true };
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(UserGroupMappingService)}; Method : {nameof(ListGroups)};");
            }
        }
        #endregion
        #region POST
        /// <summary>
        /// Add/Modify A Group 
        /// </summary>
        /// <param name="groupDTO">Group that needs to be added/modified</param> 
        /// <returns> A <see cref="object"/> Group that was added/modified <br />        
        /// </returns>  
        public async Task<object> PostGroup(SDRGroupsDTO groupDTO)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(UserGroupMappingService)}; Method : {nameof(PostGroup)};");
                var groupEntity = _mapper.Map<SDRGroupsEntity>(groupDTO);
                groupEntity.groupModifiedBy = Config.UserName;
                groupEntity.groupModifiedOn = DateTime.UtcNow;

                if (String.IsNullOrWhiteSpace(groupDTO.groupId))
                {
                    //create a group
                    var existingUserGroups = await _userGroupMappingRepository.GetGroupList();
                    if(existingUserGroups != null && existingUserGroups.Count > 0)
                    {
                        if (existingUserGroups.Any(x => x.groupName.ToLower() == groupDTO.groupName.ToLower()))
                            return Constants.ErrorMessages.GroupNameExists;
                    }
                    groupEntity.groupId = IdGenerator.GenerateId();                    
                    groupEntity.groupCreatedBy = Config.UserName;
                    groupEntity.groupCreatedOn = DateTime.UtcNow;                 

                    await _userGroupMappingRepository.AddAGroup(groupEntity);
                }                
                else
                {
                    //update the group
                    var existingUserGroup = await _userGroupMappingRepository.GetAGroupById(groupEntity.groupId);
                    if (existingUserGroup == null)
                        return Constants.ErrorMessages.GroupIdError;
                    existingUserGroup.groupDescription = groupEntity.groupDescription;
                    existingUserGroup.permission = groupEntity.permission;
                    existingUserGroup.groupFilter = groupEntity.groupFilter;
                    existingUserGroup.groupModifiedBy = groupEntity.groupModifiedBy;
                    existingUserGroup.groupModifiedOn = groupEntity.groupModifiedOn;    
                    existingUserGroup.groupEnabled = groupEntity.groupEnabled;    

                    await _userGroupMappingRepository.UpdateAGroup(existingUserGroup);
                    groupEntity = existingUserGroup;
                    groupEntity.users = null;
                }                
                groupDTO = _mapper.Map<SDRGroupsDTO>(groupEntity);
                return groupDTO;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(UserGroupMappingService)}; Method : {nameof(PostGroup)};");
            }
        }

        /// <summary>
        /// Add/Update User Group Mapping
        /// </summary>
        /// <param name="userToGroupsDTO">User Group Mapping</param> 
        /// <returns> A <see cref="object"/> which has user group mapping <br />        
        /// </returns>  
        public async Task<object> PostUserToGroups(PostUserToGroupsDTO userToGroupsDTO)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(UserGroupMappingService)}; Method : {nameof(PostUserToGroups)};");
                var userGroupsEntity = await _userGroupMappingRepository.GetAllUserGroups();
                var responseUserToGroups = JsonConvert.DeserializeObject<PostUserToGroupsDTO>(JsonConvert.SerializeObject(userToGroupsDTO));
                foreach(var groups in userToGroupsDTO.groups)
                {
                    if(userGroupsEntity.SDRGroups.Any(x=>x.groupId==groups.groupId))
                    {
                        if(userGroupsEntity.SDRGroups.Find(x => x.groupId == groups.groupId).users!=null)
                        {
                            if(userGroupsEntity.SDRGroups.Find(x => x.groupId == groups.groupId).users.Any(x=>x.email==userToGroupsDTO.email))
                            {
                                userGroupsEntity.SDRGroups.Find(x => x.groupId == groups.groupId)
                                                    .users.Find(x=>x.email==userToGroupsDTO.email)
                                                    .email = userToGroupsDTO.email;
                                userGroupsEntity.SDRGroups.Find(x => x.groupId == groups.groupId)
                                                    .users.Find(x => x.email == userToGroupsDTO.email)
                                                    .oid = userToGroupsDTO.oid;
                                userGroupsEntity.SDRGroups.Find(x => x.groupId == groups.groupId)
                                                    .users.Find(x => x.email == userToGroupsDTO.email)
                                                    .isActive = groups.isActive;
                            }
                            else
                            {
                                UsersEntity user = new UsersEntity
                                {
                                    email = userToGroupsDTO.email,oid = userToGroupsDTO.oid, isActive = groups.isActive
                                };                                
                                userGroupsEntity.SDRGroups.Find(x => x.groupId == groups.groupId)
                                                    .users.Add(user);
                            }                            
                        }
                        else
                        {
                            userGroupsEntity.SDRGroups.Find(x => x.groupId == groups.groupId).users = new List<UsersEntity>();
                            UsersEntity user = new UsersEntity
                            {
                                email = userToGroupsDTO.email,
                                oid = userToGroupsDTO.oid,
                                isActive = groups.isActive
                            };
                            userGroupsEntity.SDRGroups.Find(x => x.groupId == groups.groupId)
                                                .users.Add(user);
                        }
                        userGroupsEntity.SDRGroups.Find(x => x.groupId == groups.groupId).groupModifiedBy = Config.UserName;
                        userGroupsEntity.SDRGroups.Find(x => x.groupId == groups.groupId).groupModifiedOn = DateTime.UtcNow;
                    }
                    else
                    {
                        responseUserToGroups.groups.RemoveAll(x=>x.groupId == groups.groupId);
                    }
                }
                await _userGroupMappingRepository.UpdateUsersToGroups(userGroupsEntity).ConfigureAwait(false);
                return responseUserToGroups;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(UserGroupMappingService)}; Method : {nameof(PostUserToGroups)};");
            }
        }
        #endregion
        #endregion
    }
}
