using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class StudyArmDTO
    {
        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string description { get; set; }

        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string id { get; set; }

        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string studyArmType { get; set; }

        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string studyOriginType { get; set; }

        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string studyArmOrigin { get; set; }

        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string name { get; set; }
    }
}
