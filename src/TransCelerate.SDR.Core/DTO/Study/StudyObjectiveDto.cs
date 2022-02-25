using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.Core.DTO.Study
{
    
    public class StudyObjectiveDTO
    {
      
        public string description { get; set; }
    
        public string id { get; set; }

        public string level { get; set; }
       
        public List<EndpointsDTO> endpoints { get; set; }        
    }
     
    
    public class EndpointsDTO
    {
      
        public string description { get; set; }

        public string purpose { get; set; }
       
        public string id { get; set; }

        public string outcomeLevel { get; set; }                
    }  
}
