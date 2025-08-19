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
        public List<NarrativeContentItemDto> NarrativeContentItems { get; set; }
        public List<StudyRoleDto> Roles { get; set; }
        public List<AdministrableProductDto> AdministrableProducts { get; set; }
        public List<ProductOrganizationRoleDto> ProductOrganizationRoles { get; set; }
        public List<MedicalDeviceDto> MedicalDevices { get; set; }
        public List<EligibilityCriterionItemDto> EligibilityCriterionItems { get; set; }
        public List<ConditionDto> Conditions { get; set; }
        public List<BiomedicalConceptSurrogateDto> BcSurrogates { get; set; }
        public List<BiomedicalConceptCategoryDto> BcCategories { get; set; }
        public List<SyntaxTemplateDictionaryDto> Dictionaries { get; set; }
        public List<BiomedicalConceptDto> BiomedicalConcepts { get; set; }
    }
}
