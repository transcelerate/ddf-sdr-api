using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class PointInTimeDTO
    {
        
        public string id { get; set; }
      
        public string type { get; set; }
      
        public string subjectStatusGrouping { get; set; }
         
        public string startDate { get; set; }
      
        public string endDate { get; set; }
    }
}
