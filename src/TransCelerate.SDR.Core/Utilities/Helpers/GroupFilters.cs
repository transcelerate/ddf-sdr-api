using System;
using System.Collections.Generic;
using System.Linq;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.Utilities.Enums;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    /// <summary>
    /// This class is used for applying group filters
    /// </summary>
    public static class GroupFilters
    {
        /// <summary>
        /// This method is to return the group filter values
        /// </summary>
        /// <param name="groups">List of Groups which user was tagged into</param>
        /// <returns></returns>
        public static Tuple<List<string>, List<string>> GetGroupFilters(List<SDRGroupsEntity> groups)
        {
            return Tuple.Create(GetFilterValues(groups, GroupFieldNames.studyType.ToString()), GetFilterValues(groups, GroupFieldNames.study.ToString()));
        }
        /// <summary>
        /// Get filter values from the groups
        /// </summary>
        /// <param name="groups">List of Groups which user was tagged into</param>
        /// <param name="field">studyType or study</param>
        /// <returns></returns>
        public static List<string> GetFilterValues(List<SDRGroupsEntity> groups, string field)
        {
            return groups.SelectMany(x => x.GroupFilter)
                         .Where(x => x.GroupFieldName == field)
                         .SelectMany(x => x.GroupFilterValues)
                         .Select(x => field == GroupFieldNames.study.ToString() ? x.GroupFilterValueId : x.GroupFilterValueId.ToLower())
                         .ToList();
        }
    }
}
