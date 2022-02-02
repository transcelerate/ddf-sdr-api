using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class GetStudySectionsDTO
    {
        public string studyId { get; set; }
        public int studyVersion { get; set; }
        public List<InvestigationalInterventionDTO> investigationalInterventions { get; set; }
        public List<GetStudyDesignsDTO> studyDesigns { get; set; }
        public List<StudyObjectiveDTO> objectives { get; set; }
        public List<StudyIndicationDTO> studyIndications { get; set; }    
        public StudyProtocolDTO studyProtocol { get; set; }
    }
}
