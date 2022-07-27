using System;
using System.Collections.Generic;
using System.Linq;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.Utilities.Enums;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class GroupFilters
    {
        public static Tuple<List<string>,List<string>> GetGroupFilters(List<SDRGroupsEntity> groups)
        {           
            return Tuple.Create(GetFilterValues(groups, GroupFieldNames.studyType.ToString()), GetFilterValues(groups, GroupFieldNames.study.ToString()));
        }

        public static List<string> GetFilterValues(List<SDRGroupsEntity> groups, string field)
        {
            return groups.SelectMany(x => x.groupFilter)
                         .Where(x => x.groupFieldName == field)
                         .SelectMany(x => x.groupFilterValues)
                         .Select(x => field == GroupFieldNames.study.ToString() ? x.groupFilterValueId : x.groupFilterValueId.ToLower())
                         .ToList();
        }
    }
}
