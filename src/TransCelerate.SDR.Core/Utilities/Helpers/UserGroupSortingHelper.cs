using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static IOrderedEnumerable<GroupDetailsEntity> OrderGroups(this IEnumerable<GroupDetailsEntity> groupDetails,UserGroupsQueryParameters userGroupsQueryParameters)
        {
            if (!String.IsNullOrWhiteSpace(userGroupsQueryParameters.sortBy))
            {
                return userGroupsQueryParameters.sortBy.ToLower() switch
                {
                    "name" => userGroupsQueryParameters.sortOrder == SortOrder.asc.ToString() ? groupDetails.OrderBy(x => x.groupName) : groupDetails.OrderByDescending(x => x.groupName),
                    "modifiedon" => userGroupsQueryParameters.sortOrder == SortOrder.asc.ToString() ? groupDetails.OrderBy(x => x.groupModifiedOn) : groupDetails.OrderByDescending(x => x.groupModifiedOn),
                    "modifiedby" => userGroupsQueryParameters.sortOrder == SortOrder.asc.ToString() ? groupDetails.OrderBy(x => x.groupModifiedBy) : groupDetails.OrderByDescending(x => x.groupModifiedBy),
                    "createdby" => userGroupsQueryParameters.sortOrder == SortOrder.asc.ToString() ? groupDetails.OrderBy(x => x.groupCreatedBy) : groupDetails.OrderByDescending(x => x.groupCreatedBy),
                    "createdon" => userGroupsQueryParameters.sortOrder == SortOrder.asc.ToString() ? groupDetails.OrderBy(x => x.groupCreatedOn) : groupDetails.OrderByDescending(x => x.groupCreatedOn),
                    _ => userGroupsQueryParameters.sortOrder == SortOrder.asc.ToString() ? groupDetails.OrderBy(x => x.groupModifiedOn) : groupDetails.OrderByDescending(x => x.groupModifiedOn),
                };
            }
            else
            {
                return userGroupsQueryParameters.sortOrder == SortOrder.asc.ToString() ? groupDetails.OrderBy(x => x.groupModifiedOn) : groupDetails.OrderByDescending(x => x.groupModifiedOn);
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
            if (!String.IsNullOrWhiteSpace(userGroupsQueryParameters.sortBy))
            {
                return userGroupsQueryParameters.sortBy.ToLower() switch
                {
                    "email" => userGroupsQueryParameters.sortOrder == SortOrder.desc.ToString() ? users.OrderByDescending(x => x.email) : users.OrderBy(x => x.email),
                    _ => userGroupsQueryParameters.sortOrder == SortOrder.desc.ToString() ? users.OrderByDescending(x => 1) : users.OrderBy(x => 1),
                };
            }
            else
            {
                return userGroupsQueryParameters.sortOrder == SortOrder.desc.ToString() ? users.OrderByDescending(x => 1) : users.OrderBy(x => 1);
            }
        }
    }
}
