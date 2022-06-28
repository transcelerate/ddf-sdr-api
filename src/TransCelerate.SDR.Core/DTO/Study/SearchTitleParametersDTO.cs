using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class SearchTitleParametersDTO
    {
        public string studyTitle { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public bool groupByStudyId { get; set; }
        public string sortOrder { get; set; }
        public string sortBy { get; set; }

        public SearchTitleParametersDTO()
        {
            pageNumber = 1;
            pageSize = 20;
        }
    }
}
