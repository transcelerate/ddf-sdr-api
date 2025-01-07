using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.UserGroups
{
    public class GroupDetailsDTO
    {
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public string Permission { get; set; }
        public List<GroupFilterDTO> GroupFilter { get; set; }
        public string GroupModifiedOn { get; set; }
        public string GroupModifiedBy { get; set; }
        public string GroupCreatedOn { get; set; }
        public string GroupCreatedBy { get; set; }
        public bool GroupEnabled { get; set; }
    }
}
