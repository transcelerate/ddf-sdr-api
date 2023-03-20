using System;

namespace TransCelerate.SDR.Core.DTO.UserGroups
{
    public class UsersDTO
    {
        public string Oid { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public DateTime GroupModifiedOn { get; set; }
    }
}
