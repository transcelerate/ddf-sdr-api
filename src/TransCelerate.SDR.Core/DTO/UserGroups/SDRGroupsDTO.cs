using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.UserGroups
{
    public class SDRGroupsDTO
    {
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public string Permission { get; set; }
        public List<GroupFilterDTO> GroupFilter { get; set; }
        public List<UsersDTO> Users { get; set; }
        public string GroupCreatedBy { get; set; }
        public string GroupCreatedOn { get; set; }
        public string GroupModifiedBy { get; set; }
        public string GroupModifiedOn { get; set; }
        public bool GroupEnabled { get; set; }
    }
}
