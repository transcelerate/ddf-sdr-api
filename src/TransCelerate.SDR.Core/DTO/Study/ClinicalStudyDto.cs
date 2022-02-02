using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class ClinicalStudyDTO
    {
        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string studyTitle { get; set; }

        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string studyType { get; set; }
        public string interventionModel { get; set; }

        public string studyPhase { get; set; }
        public string status { get; set; }
        public string tag { get; set; }

        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        [EmptyListValidationHelper(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public List<StudyIdentifierDTO> studyIdentifiers { get; set; }

        public List<CurrentSectionsDTO> currentSections { get; set; }

        public string studyId { get; set; }
    }
}
