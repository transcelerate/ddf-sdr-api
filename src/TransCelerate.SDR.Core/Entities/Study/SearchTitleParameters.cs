using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class SearchTitleParameters
    {
        public string studyTitle { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public bool groupByStudyId { get; set; }
    }
}
