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
        public string studyId { get; set; }
        public string studyTitle { get; set; }
        public string studyType { get; set; }        

        public string studyPhase { get; set; }
        public string studyStatus { get; set; }
        public string studyTag { get; set; }

        public List<StudyIdentifierDTO> studyIdentifiers { get; set; }

        public List<CurrentSectionsDTO> currentSections { get; set; }
      
    }
}
