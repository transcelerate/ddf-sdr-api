using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.UserGroups
{
    public  class GroupDetailsDTO
    {
        public string groupId { get; set; }
        public string groupName { get; set; }
        public string groupDescription { get; set; }
        public string permission { get; set; }
        public List<GroupFilterDTO> groupFilter { get; set; }
        public string groupModifiedOn { get; set; }
        public string groupModifiedBy { get; set; }
        public string groupCreatedOn { get; set; }
        public string groupCreatedBy { get; set; }
        public bool groupEnabled { get; set; }
    }
}
