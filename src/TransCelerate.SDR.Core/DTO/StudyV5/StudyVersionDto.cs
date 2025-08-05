using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class StudyVersionDto : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string VersionIdentifier { get; set; }
        public List<CodeDto> BusinessTherapeuticAreas { get; set; }
        public string Rationale { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
        public List<AbbreviationDto> Abbreviations { get; set; }
        public List<GovernanceDateDto> DateValues { get; set; }
        public List<ReferenceIdentifierDto> ReferenceIdentifiers { get; set; }
        public List<StudyAmendmentDto> Amendments { get; set; }
        public List<string> DocumentVersionIds { get; set; }
        public List<StudyDesignDto> StudyDesigns { get; set; }
        public List<StudyIdentifierDto> StudyIdentifiers { get; set; }
        public List<StudyTitleDto> Titles { get; set; }
        public List<StudyInterventionDto> StudyInterventions { get; set; }
    }
}
