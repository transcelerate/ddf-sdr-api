using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.UserGroups
{
    public class PostUserToGroupsDTO
    {
        public string oid { get; set; }
        public string email { get; set; }
        public List<GroupsTaggedToUser> groups { get; set; }
    }

    public class GroupsTaggedToUser
    {
        public string groupId { get; set; }
        public string groupName { get; set; }
        public bool isActive { get; set; }
    }
}
