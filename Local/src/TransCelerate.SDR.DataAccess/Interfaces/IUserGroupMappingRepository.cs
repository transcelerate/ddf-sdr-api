using System.Collections.Generic;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.UserGroups;
using TransCelerate.SDR.Core.Entities.UserGroups;

namespace TransCelerate.SDR.DataAccess.Interfaces
{
    public interface IUserGroupMappingRepository
    {
        /// <summary>
        /// GET All Users for a group
        /// </summary>        
        /// <returns> A <see cref="UserGroupMappingEntity"/> with List of Groups <br></br>
        /// <see langword="null"/> if there are no groups
        /// </returns>   
        Task<UserGroupMappingEntity> GetAllUserGroups();
        /// <summary>
        /// GET All groups
        /// </summary>        
        /// <returns> A <see cref="SDRGroupsEntity"/> with List of Groups <br></br>
        /// <see langword="null"/> if there are no groups
        /// </returns>      
        Task<List<GroupDetailsEntity>> GetGroups(UserGroupsQueryParameters userGroupsQueryParameters);
        /// <summary>
        /// GET group list
        /// </summary>        
        /// <returns> A <see cref="List{GroupListEntity}"/> with List of Group Name and Group Id <br></br>
        /// <see langword="null"/> if there are no groups
        /// </returns>  
        Task<List<GroupListEntity>> GetGroupList();
        /// <summary>
        /// GET a group with groupId
        /// </summary>        
        /// <returns> A <see cref="SDRGroupsEntity"/> with List of Group Name and Group Id <br></br>
        /// <see langword="null"/> if there are no groups
        /// </returns>   
        Task<SDRGroupsEntity> GetAGroupById(string groupId);

        /// <summary>
        /// GET a group by groupName
        /// </summary>        
        /// <returns> A <see cref="SDRGroupsEntity"/> with List of Group Name and Group Id <br></br>
        /// <see langword="null"/> if there are no groups
        /// </returns>   
        Task<SDRGroupsEntity> GetGroupByName(string groupName);
        /// <summary>
        /// Add a Group 
        /// </summary>
        /// <param name="group">Update User Group Mapping</param>
        /// <returns>
        /// A <see cref="SDRGroupsEntity"/> <br></br>        
        /// </returns>
        Task<SDRGroupsEntity> AddAGroup(SDRGroupsEntity group);
        /// <summary>
        /// Updates A Group 
        /// </summary>
        /// <param name="group">Add User Group Mapping</param>
        /// <returns>
        /// A <see cref="SDRGroupsEntity"/> <br></br>        
        /// </returns>
        Task<SDRGroupsEntity> UpdateAGroup(SDRGroupsEntity group);
        /// <summary>
        /// Updates User Group Mapping
        /// </summary>
        /// <param name="userGroupMappingEntity">Add User Group Mapping</param>
        /// <returns>
        /// A <see cref="UserGroupMappingEntity"/> <br></br>         
        /// </returns>
        Task<UserGroupMappingEntity> UpdateUsersToGroups(UserGroupMappingEntity userGroupMappingEntity);
    }
}
