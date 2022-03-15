using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.Study
{
    /// <summary>
    /// This class is a DTO for GET Method for study Level sections
    /// </summary>
    public class GetStudySectionsDTO
    {
        public string studyId { get; set; }
        public int studyVersion { get; set; }

        public List<GetStudyDesignsDTO> studyDesigns { get; set; }

        public List<StudyObjectiveDTO> objectives { get; set; }
       
        public List<StudyIndicationDTO> studyIndications { get; set; }
        //Removed Study Protocol
        //public StudyProtocolDTO studyProtocol { get; set; }
    }
}
