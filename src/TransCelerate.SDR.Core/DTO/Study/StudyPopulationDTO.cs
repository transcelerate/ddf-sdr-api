using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class StudyPopulationDTO
    {
        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string id { get; set; }

        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string description { get; set; }
    }
}
