using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class GetClinicalStudyDTO
    {
        public string studyId { get; set; }
        public string studyTitle { get; set; }
        public string studyType { get; set; }

        public string studyPhase { get; set; }
        public string studyStatus { get; set; }
        public string studyTag { get; set; }

        public List<StudyIdentifierDTO> studyIdentifiers { get; set; }

        public List<GetStudyDesignsDTO> studyDesigns { get; set; }
        public List<StudyObjectiveDTO> objectives { get; set; }
        public List<StudyIndicationDTO> studyIndications { get; set; }
        //Removed Study Protocol
        //public StudyProtocolDTO studyProtocol { get; set; }       
    }
}
