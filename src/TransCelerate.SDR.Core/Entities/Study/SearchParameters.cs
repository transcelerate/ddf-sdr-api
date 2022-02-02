using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class SearchParameters
    {        
        public string studyId { get; set; }        
        public string studyTitle { get; set; }        
        public string briefTitle { get; set; }        
        public string indication { get; set; }
        public string interventionModel { get; set; }
        public string phase { get; set; }
        
        public DateTime fromDate { get; set; }
        
        public DateTime toDate { get; set; }
        
        public int pageSize { get; set; }        
        public int pageNumber { get; set; }
        public string header { get; set; }
        public bool asc { get; set; }
    }
}
