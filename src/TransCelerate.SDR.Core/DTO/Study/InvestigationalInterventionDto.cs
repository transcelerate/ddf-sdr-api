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
    public class InvestigationalInterventionDTO
    {        
        public string id { get; set; }        
        public string description { get; set; }                      
        public string interventionModel { get; set; }
        public string status { get; set; }
        public List<CodingDTO> coding { get; set; }        
    }

}
