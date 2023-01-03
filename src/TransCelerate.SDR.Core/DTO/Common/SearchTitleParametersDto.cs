using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.Common
{
    public class SearchTitleParametersDto
    {
        public string StudyTitle { get; set; }
        public string StudyId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public bool GroupByStudyId { get; set; }
        public string SortOrder { get; set; }
        public string SortBy { get; set; }

        public SearchTitleParametersDto()
        {
            PageNumber = 1;
            PageSize = 20;
        }
    }
}
