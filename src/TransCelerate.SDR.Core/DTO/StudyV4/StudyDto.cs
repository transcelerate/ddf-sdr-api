using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class StudyDto
    {
        public string Id { get; set; }
        public string StudyTitle { get; set; }
        public string StudyVersion { get; set; }
        public CodeDto StudyType { get; set; }
        public string StudyRationale { get; set; }
        public string StudyAcronym { get; set; }
        public List<StudyIdentifierDto> StudyIdentifiers { get; set; }
        public AliasCodeDto StudyPhase { get; set; }
        public List<CodeDto> BusinessTherapeuticAreas { get; set; }
        public List<StudyProtocolVersionDto> StudyProtocolVersions { get; set; }
        public List<StudyDesignDto> StudyDesigns { get; set; }
    }
}
