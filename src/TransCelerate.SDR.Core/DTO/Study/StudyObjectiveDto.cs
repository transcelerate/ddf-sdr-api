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
        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string description { get; set; }

        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string id { get; set; }

        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string level { get; set; }
        
        public List<EndpointsDTO> endpoints { get; set; }        
    }
     
    
    public class EndpointsDTO
    {
        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string description { get; set; }

        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string purpose { get; set; }

        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string id { get; set; }

        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string outcomeLevel { get; set; }                
    }  
}
