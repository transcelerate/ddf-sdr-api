using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.UserGroups
{
    public class PostUserToGroupsDTO
    {
        public string Oid { get; set; }
        public string Email { get; set; }
        public List<GroupsTaggedToUser> Groups { get; set; }
    }

    public class GroupsTaggedToUser
    {
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public bool IsActive { get; set; }
    }
}
