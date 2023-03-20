using System;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.UserGroups
{
    public class SDRGroupsEntity
    {
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public string Permission { get; set; }
        public List<GroupFilterEntity> GroupFilter { get; set; }
        public List<UsersEntity> Users { get; set; }
        public string GroupCreatedBy { get; set; }
        public DateTime GroupCreatedOn { get; set; }
        public string GroupModifiedBy { get; set; }
        public DateTime GroupModifiedOn { get; set; }
        public bool GroupEnabled { get; set; }
    }
}
