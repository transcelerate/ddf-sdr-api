using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.UserGroups
{
    public class GroupFilterDTO
    {
        public string GroupFieldName { get; set; }
        public List<GroupFilterValuesDTO> GroupFilterValues { get; set; }
    }
}
