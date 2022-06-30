using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.Reports
{
    public class SystemUsageRawReport
    {
        public List<Tables> Tables { get; set; }
    }

    public class Tables
    {
        public string Name { get; set; }
        public List<Columns> Columns { get; set; }
        public List<List<string>>  Rows { get; set; }
    }

    public class Columns
    {
        public string name { get; set; }
        public string type { get; set; }
    }

    public class CustomDimension
    {
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
    }
}
