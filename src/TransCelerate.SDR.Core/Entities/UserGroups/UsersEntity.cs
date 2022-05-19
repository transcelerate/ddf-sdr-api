using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.UserGroups
{
    public class UsersEntity
    {
        public string oid { get; set; }
        public string email { get; set; }
        public bool isActive { get; set; }
    }
}
