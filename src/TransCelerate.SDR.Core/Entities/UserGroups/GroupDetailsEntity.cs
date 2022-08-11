using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.UserGroups
{
    public class GroupDetailsEntity
    {
        public string groupId { get; set; }
        public string groupName { get; set; }
        public string groupDescription { get; set; }
        public string permission { get; set; }
        public List<GroupFilterEntity> groupFilter { get; set; }
        public DateTime groupModifiedOn { get; set; }
        public string groupModifiedBy { get; set; }
        public DateTime groupCreatedOn { get; set; }
        public string groupCreatedBy { get; set; }
        public bool groupEnabled    { get; set; }
    }
}
