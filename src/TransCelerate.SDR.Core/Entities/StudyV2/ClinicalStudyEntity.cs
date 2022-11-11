using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class ClinicalStudyEntity : IUuid
    {
        public string Uuid { get; set; }
        public string StudyTitle { get; set; }        
        public CodeEntity StudyType { get; set; }
        public List<StudyIdentifierEntity> StudyIdentifiers { get; set; }
        public CodeEntity StudyPhase { get; set; }
        public List<StudyProtocolVersionEntity> StudyProtocolVersions { get; set; }
        public List<StudyDesignEntity> StudyDesigns { get; set; }
        public string StudyVersion { get; set; }
    }
}
