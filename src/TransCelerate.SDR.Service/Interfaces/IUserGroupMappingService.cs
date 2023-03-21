using System.Collections.Generic;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.DTO.UserGroups;

namespace TransCelerate.SDR.Services.Interfaces
{
    public interface IUserGroupMappingService
    {
        /// <summary>
        /// GET All Groups
        /// </summary>        
        /// <returns> A <see cref="List{GroupDetailsDTO}"/> with List of Groups <br />
        /// <see langword="null"/> if there are no groups
        /// </returns>
        Task<List<GroupDetailsDTO>> GetUserGroups(UserGroupsQueryParameters userGroupsQueryParameters);

        /// <summary>
        /// GET All Users for a group
        /// </summary>        
        /// <returns> A <see cref="object"/> with List of users with groups assined <br />
        /// <see langword="null"/> if there are no users in the group
        /// </returns>    
        Task<object> GetUsersList(UserGroupsQueryParameters userGroupsQueryParameters);

        /// <summary>
        /// GET All groups
        /// </summary>        
        /// <returns> A <see cref="List{GroupListDTO}"/> with List of groupId and groupName <br />
        /// <see langword="null"/> if there are no groups
        /// </returns>   
        Task<List<GroupListDTO>> ListGroups();
        /// <summary>
        /// Check GroupName
        /// </summary>        
        /// <returns> A <see cref="object"/> with groupId and isExists <br />
        /// <see langword="null"/> if there are no groups
        /// </returns>   
        Task<object> CheckGroupName(string groupName);

        /// <summary>
        /// Add/Modify A Group 
        /// </summary>
        /// <param name="groupDTO">Group that needs to be added/modified</param> 
        /// <param name="user">Logged In User</param>
        /// <returns> A <see cref="object"/> Group that was added/modified <br />        
        /// </returns>    
        Task<object> PostGroup(SDRGroupsDTO groupDTO, LoggedInUser user);

        /// <summary>
        /// Add/Update User Group Mapping
        /// </summary>
        /// <param name="userToGroupsDTO">User Group Mapping</param> 
        /// <param name="user">Logged In User</param>
        /// <returns> A <see cref="object"/> which has user group mapping <br />        
        /// </returns>  
        Task<object> PostUserToGroups(PostUserToGroupsDTO userToGroupsDTO, LoggedInUser user);
    }
}
