using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.UserGroups
{
    public class GroupFilterEntity
    {
        public string groupFieldName { get; set; }
        public List<string> groupFilterValues { get; set; }
    }
}
