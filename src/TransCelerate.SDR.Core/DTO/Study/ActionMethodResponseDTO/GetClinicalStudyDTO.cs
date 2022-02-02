using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class GetClinicalStudyDTO
    {  
        public string studyTitle { get; set; }

        public int? studyVersion { get; set; }
        public string studyType { get; set; }
        public string interventionModel { get; set; }

        public string studyPhase { get; set; }
        public string status { get; set; }
        public string tag { get; set; }

        public List<StudyIdentifierDTO> studyIdentifiers { get; set; }

        public List<InvestigationalInterventionDTO> investigationalInterventions { get; set; }
        public List<GetStudyDesignsDTO> studyDesigns { get; set; }
        public List<StudyObjectiveDTO> objectives { get; set; }
        public List<StudyIndicationDTO> studyIndications { get; set; }
        public StudyProtocolDTO studyProtocol { get; set; }

        public string studyId { get; set; }
    }
}
