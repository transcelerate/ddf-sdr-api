using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.UserGroups
{
    public class GroupFilterEntity
    {
        public string GroupFieldName { get; set; }
        public List<GroupFilterValuesEntity> GroupFilterValues { get; set; }
    }
}
