using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Utilities;

namespace TransCelerate.SDR.Core.DTO.UserGroups
{
    public class UserGroupsQueryParameters
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string sortOrder { get; set; }
        public string sortBy { get; set; }
    }
}
