using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class ClinicalStudyDTO
    {
        public string StudyId { get; set; }
        public string StudyTitle { get; set; }
        public string StudyType { get; set; }

        public string StudyPhase { get; set; }
        public string StudyStatus { get; set; }
        public string StudyTag { get; set; }

        public List<StudyIdentifierDTO> StudyIdentifiers { get; set; }

        public List<CurrentSectionsDTO> CurrentSections { get; set; }

        public List<StudyProtocolDTO> StudyProtocolReferences { get; set; }


    }
}
