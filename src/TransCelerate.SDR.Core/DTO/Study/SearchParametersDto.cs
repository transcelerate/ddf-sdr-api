using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class SearchParametersDTO
    {        
        public string studyId { get; set; }
     
        public string studyTitle { get; set; }

        public string indication { get; set; }
        public string interventionModel  { get; set; }
        public string phase { get; set; }
      
        public string fromDate { get; set; }      
        public string toDate { get; set; }
      
        public int pageSize { get; set; }
              
        public int pageNumber { get; set; }
        public string header { get; set; }
        public bool asc { get; set; }

        public SearchParametersDTO()
        {
            pageNumber = 1;
            pageSize = 5;
        }
    }

}
