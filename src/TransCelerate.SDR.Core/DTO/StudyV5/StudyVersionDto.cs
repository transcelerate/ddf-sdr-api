using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class StudyVersionDto : IId
    {
        public string Id { get; set; }
        public List<StudyTitleDto> Titles { get; set; }
        public string VersionIdentifier { get; set; }
        public CodeDto StudyType { get; set; }
        public string Rationale { get; set; }
        public List<string> DocumentVersionIds { get; set; }
        public List<GovernanceDateDto> DateValues { get; set; }
        public List<StudyAmendmentDto> Amendments { get; set; }
        public List<StudyIdentifierDto> StudyIdentifiers { get; set; }
        public AliasCodeDto StudyPhase { get; set; }
        public List<CodeDto> BusinessTherapeuticAreas { get; set; }
        public List<StudyDesignDto> StudyDesigns { get; set; }
        public string InstanceType { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
        public List<EligibilityCriterionDto> Criteria { get; set; }
        public List<NarrativeContentItemDto> NarrativeContentItems { get; set; }
        public List<AbbreviationDto> Abbreviations { get; set; }
        public List<ReferenceIdentifierDto> ReferenceIdentifiers { get; set; }
    }
}
