using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.DTO.UserGroups;
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
                if (userGroupsEntity == null)
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
                    userGroupsEntity.SDRGroups.RemoveAll(x => x.GroupEnabled == false);
                    var userGroupsDTO = _mapper.Map<UserGroupMappingDTO>(userGroupsEntity);

                    //Get list of users with group name
                    List<UsersDTO> users = new();
                    foreach (var userGroup in userGroupsDTO.SDRGroups)
                    {
                        if (userGroup.Users != null)
                        {
                            if (users.Count == 0)
                                users = userGroup.Users;
                            else
                                users.AddRange(userGroup.Users);
                        }
                    }

                    //remove disable group for a user
                    users.RemoveAll(x => x.IsActive == false);

                    int userCount = users.Count;
                    if (userGroupsQueryParameters.PageNumber == 0 || userGroupsQueryParameters.PageSize == 0)
                    {
                        userGroupsQueryParameters.PageSize = userCount;
                        userGroupsQueryParameters.PageNumber = 1;
                    }

                    //conversion of group list to user list
                    var userWithListOfGroups = users.GroupBy(x => x.Email)
                                                     .Select(g => new
                                                     {
                                                         email = g.Key,
                                                         oid = g.Select(x => x.Oid).First(),
                                                         groups = g.Select(x => new GroupsTaggedToUser { GroupName = x.GroupName, GroupId = x.GroupId, IsActive = x.IsActive }).ToList(),
                                                         date = g.Max(x => x.GroupModifiedOn)
                                                     })
                                                     .OrderByDescending(x => x.date)
                                                     .Select(x => new PostUserToGroupsDTO
                                                     {
                                                         Email = x.email,
                                                         Oid = x.oid,
                                                         Groups = x.groups
                                                     })
                                                     .OrderUsers(userGroupsQueryParameters)
                                                     .Skip((userGroupsQueryParameters.PageNumber - 1) * userGroupsQueryParameters.PageSize)
                                                     .Take(userGroupsQueryParameters.PageSize).ToList();
                    if (userWithListOfGroups.Count == 0)
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

                if (userGroupListEntity == null || userGroupListEntity.Count == 0)
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
                    return new { groupName, isExists = false };

                else
                    return new { groupName, isExists = true };
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
        /// <param name="user">Logged In User</param>
        /// <returns> A <see cref="object"/> Group that was added/modified <br />        
        /// </returns>  
        public async Task<object> PostGroup(SDRGroupsDTO groupDTO, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(UserGroupMappingService)}; Method : {nameof(PostGroup)};");
                var groupEntity = _mapper.Map<SDRGroupsEntity>(groupDTO);
                groupEntity.GroupModifiedBy = user.UserName;
                groupEntity.GroupModifiedOn = DateTime.UtcNow;

                if (String.IsNullOrWhiteSpace(groupDTO.GroupId))
                {
                    //create a group
                    var existingUserGroups = await _userGroupMappingRepository.GetGroupList();
                    if (existingUserGroups != null && existingUserGroups.Count > 0)
                    {
                        if (existingUserGroups.Any(x => x.GroupName.ToLower() == groupDTO.GroupName.ToLower()))
                            return Constants.ErrorMessages.GroupNameExists;
                    }
                    groupEntity.GroupId = IdGenerator.GenerateId();
                    groupEntity.GroupCreatedBy = user.UserName;
                    groupEntity.GroupCreatedOn = DateTime.UtcNow;

                    await _userGroupMappingRepository.AddAGroup(groupEntity);
                }
                else
                {
                    //update the group
                    var existingUserGroup = await _userGroupMappingRepository.GetAGroupById(groupEntity.GroupId);
                    if (existingUserGroup == null)
                        return Constants.ErrorMessages.GroupIdError;
                    existingUserGroup.GroupDescription = groupEntity.GroupDescription;
                    existingUserGroup.Permission = groupEntity.Permission;
                    existingUserGroup.GroupFilter = groupEntity.GroupFilter;
                    existingUserGroup.GroupModifiedBy = groupEntity.GroupModifiedBy;
                    existingUserGroup.GroupModifiedOn = groupEntity.GroupModifiedOn;
                    existingUserGroup.GroupEnabled = groupEntity.GroupEnabled;

                    await _userGroupMappingRepository.UpdateAGroup(existingUserGroup);
                    groupEntity = existingUserGroup;
                    groupEntity.Users = null;
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
        /// <param name="loggedInUser">Logged In User</param>
        /// <returns> A <see cref="object"/> which has user group mapping <br />        
        /// </returns>  
        public async Task<object> PostUserToGroups(PostUserToGroupsDTO userToGroupsDTO, LoggedInUser loggedInUser)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(UserGroupMappingService)}; Method : {nameof(PostUserToGroups)};");
                var userGroupsEntity = await _userGroupMappingRepository.GetAllUserGroups();
                var responseUserToGroups = JsonConvert.DeserializeObject<PostUserToGroupsDTO>(JsonConvert.SerializeObject(userToGroupsDTO));
                foreach (var groups in userToGroupsDTO.Groups)
                {
                    if (userGroupsEntity.SDRGroups.Any(x => x.GroupId == groups.GroupId))
                    {
                        if (userGroupsEntity.SDRGroups.Find(x => x.GroupId == groups.GroupId).Users != null)
                        {
                            if (userGroupsEntity.SDRGroups.Find(x => x.GroupId == groups.GroupId).Users.Any(x => x.Email == userToGroupsDTO.Email))
                            {
                                userGroupsEntity.SDRGroups.Find(x => x.GroupId == groups.GroupId)
                                                    .Users.Find(x => x.Email == userToGroupsDTO.Email)
                                                    .Email = userToGroupsDTO.Email;
                                userGroupsEntity.SDRGroups.Find(x => x.GroupId == groups.GroupId)
                                                    .Users.Find(x => x.Email == userToGroupsDTO.Email)
                                                    .Oid = userToGroupsDTO.Oid;
                                userGroupsEntity.SDRGroups.Find(x => x.GroupId == groups.GroupId)
                                                    .Users.Find(x => x.Email == userToGroupsDTO.Email)
                                                    .IsActive = groups.IsActive;
                            }
                            else
                            {
                                UsersEntity user = new()
                                {
                                    Email = userToGroupsDTO.Email,
                                    Oid = userToGroupsDTO.Oid,
                                    IsActive = groups.IsActive
                                };
                                userGroupsEntity.SDRGroups.Find(x => x.GroupId == groups.GroupId)
                                                    .Users.Add(user);
                            }
                        }
                        else
                        {
                            userGroupsEntity.SDRGroups.Find(x => x.GroupId == groups.GroupId).Users = new List<UsersEntity>();
                            UsersEntity user = new()
                            {
                                Email = userToGroupsDTO.Email,
                                Oid = userToGroupsDTO.Oid,
                                IsActive = groups.IsActive
                            };
                            userGroupsEntity.SDRGroups.Find(x => x.GroupId == groups.GroupId)
                                                .Users.Add(user);
                        }
                        userGroupsEntity.SDRGroups.Find(x => x.GroupId == groups.GroupId).GroupModifiedBy = loggedInUser.UserName;
                        userGroupsEntity.SDRGroups.Find(x => x.GroupId == groups.GroupId).GroupModifiedOn = DateTime.UtcNow;
                    }
                    else
                    {
                        responseUserToGroups.Groups.RemoveAll(x => x.GroupId == groups.GroupId);
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
