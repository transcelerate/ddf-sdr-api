using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.Common
{
    public class SearchTitleParametersEntity
    {
        public string StudyTitle { get; set; }
        public string StudyId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public bool GroupByStudyId { get; set; }
        public string SortOrder { get; set; }
        public string SortBy { get; set; }
    }
}
