using System;
using System.Collections.Generic;
using System.Linq;
using TransCelerate.SDR.Core.DTO.UserGroups;
using TransCelerate.SDR.Core.Entities.UserGroups;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    /// <summary>
    /// This class is a helper for sorting groups and users
    /// </summary>
    public static class UserGroupSortingHelper
    {
        /// <summary>
        /// Sorting a Group List
        /// </summary>
        /// <param name="groupDetails">Group List which needed to be sorted</param>
        /// <param name="userGroupsQueryParameters">parameters for sorting</param>
        /// <returns>
        /// A <see cref="IOrderedEnumerable{GroupDetailsEntity}"/> after sorting the groups      
        /// </returns>
        public static IOrderedEnumerable<GroupDetailsEntity> OrderGroups(this IEnumerable<GroupDetailsEntity> groupDetails, UserGroupsQueryParameters userGroupsQueryParameters)
        {
            if (!String.IsNullOrWhiteSpace(userGroupsQueryParameters.SortBy))
            {
                return userGroupsQueryParameters.SortBy.ToLower() switch
                {
                    "name" => userGroupsQueryParameters.SortOrder == SortOrder.asc.ToString() ? groupDetails.OrderBy(x => x.GroupName) : groupDetails.OrderByDescending(x => x.GroupName),
                    "modifiedon" => userGroupsQueryParameters.SortOrder == SortOrder.asc.ToString() ? groupDetails.OrderBy(x => x.GroupModifiedOn) : groupDetails.OrderByDescending(x => x.GroupModifiedOn),
                    "modifiedby" => userGroupsQueryParameters.SortOrder == SortOrder.asc.ToString() ? groupDetails.OrderBy(x => x.GroupModifiedBy) : groupDetails.OrderByDescending(x => x.GroupModifiedBy),
                    "createdby" => userGroupsQueryParameters.SortOrder == SortOrder.asc.ToString() ? groupDetails.OrderBy(x => x.GroupCreatedBy) : groupDetails.OrderByDescending(x => x.GroupCreatedBy),
                    "createdon" => userGroupsQueryParameters.SortOrder == SortOrder.asc.ToString() ? groupDetails.OrderBy(x => x.GroupCreatedOn) : groupDetails.OrderByDescending(x => x.GroupCreatedOn),
                    _ => userGroupsQueryParameters.SortOrder == SortOrder.asc.ToString() ? groupDetails.OrderBy(x => x.GroupModifiedOn) : groupDetails.OrderByDescending(x => x.GroupModifiedOn),
                };
            }
            else
            {
                return userGroupsQueryParameters.SortOrder == SortOrder.asc.ToString() ? groupDetails.OrderBy(x => x.GroupModifiedOn) : groupDetails.OrderByDescending(x => x.GroupModifiedOn);
            }
        }
        /// <summary>
        /// Sorting a User List
        /// </summary>
        /// <param name="users">User List which needed to be sorted</param>
        /// <param name="userGroupsQueryParameters">parameters for sorting</param>
        /// <returns>
        /// A <see cref="IOrderedEnumerable{PostUserToGroupsDTO}"/> after sorting the groups      
        /// </returns>
        public static IOrderedEnumerable<PostUserToGroupsDTO> OrderUsers(this IEnumerable<PostUserToGroupsDTO> users, UserGroupsQueryParameters userGroupsQueryParameters)
        {
            if (!String.IsNullOrWhiteSpace(userGroupsQueryParameters.SortBy))
            {
                return userGroupsQueryParameters.SortBy.ToLower() switch
                {
                    "email" => userGroupsQueryParameters.SortOrder == SortOrder.desc.ToString() ? users.OrderByDescending(x => x.Email) : users.OrderBy(x => x.Email),
                    _ => userGroupsQueryParameters.SortOrder == SortOrder.desc.ToString() ? users.OrderByDescending(x => 1) : users.OrderBy(x => 1),
                };
            }
            else
            {
                return userGroupsQueryParameters.SortOrder == SortOrder.desc.ToString() ? users.OrderByDescending(x => 1) : users.OrderBy(x => 1);
            }
        }
    }
}
