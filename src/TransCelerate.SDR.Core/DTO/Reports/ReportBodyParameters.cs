using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.Reports
{
    public class ReportBodyParameters
    {
        public int days { get; set; }
        public string operation { get; set; }
        public int responseCode { get; set; }
        public int recordNumber { get; set; }
        public int pageSize { get; set; }
        public string sortBy { get; set; }
        public string sortOrder { get; set; }
        public bool FilterByTime { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
    }
}
