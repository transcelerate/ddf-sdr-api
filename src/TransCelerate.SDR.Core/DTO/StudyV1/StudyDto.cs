using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    public class StudyDto : IUuid
    {
        public string Uuid { get; set; }
        public string StudyTitle { get; set; }
        public CodeDto StudyType { get; set; }
        public List<StudyIdentifierDto> StudyIdentifiers { get; set; }
        public CodeDto StudyPhase { get; set; }
        public List<StudyProtocolVersionDto> StudyProtocolVersions { get; set; }
        public List<StudyDesignDto> StudyDesigns { get; set; }
        public string StudyVersion { get; set; }
    }
}
