using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.Utilities.Enums;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class GroupFilters
    {
        public static Tuple<List<string>,List<string>> GetGroupFilters(List<SDRGroupsEntity> groups)
        {
            List<string> studyTypeFilterValues = new List<string>();
            List<string> studyIdFilterValues = new List<string>();
            studyTypeFilterValues.AddRange(groups.SelectMany(x => x.groupFilter)
                                                            .Where(x => x.groupFieldName == GroupFieldNames.studyType.ToString())
                                                            .SelectMany(x => x.groupFilterValues)
                                                            .Select(x => x.groupFilterValueId.ToLower())
                                                            .ToList());
            studyIdFilterValues.AddRange(groups.SelectMany(x => x.groupFilter)
                                                 .Where(x => x.groupFieldName == GroupFieldNames.study.ToString())
                                                 .SelectMany(x => x.groupFilterValues)
                                                 .Select(x => x.groupFilterValueId)
                                                 .ToList());

            return Tuple.Create(studyTypeFilterValues, studyIdFilterValues);
        }
    }
}
