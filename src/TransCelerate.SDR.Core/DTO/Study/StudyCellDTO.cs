using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class StudyCellDTO
    {
        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string id { get; set; }

        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        [EmptyListValidationHelper(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public List<StudyElementDTO> studyElements { get; set; }

        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public StudyArmDTO studyArm { get; set; }

        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public StudyEpochDTO studyEpoch { get; set; }
    }
}
