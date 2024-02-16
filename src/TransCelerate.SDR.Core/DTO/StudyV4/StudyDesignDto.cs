using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class StudyDesignDto : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public List<CodeDto> TrialIntentTypes { get; set; }
        public List<CodeDto> TrialType { get; set; }
        public CodeDto InterventionModel { get; set; }
        public List<StudyCellDto> StudyCells { get; set; }
        public List<IndicationDto> Indications { get; set; }
        public List<StudyInterventionDto> StudyInterventions { get; set; }
        public List<StudyDesignPopulationDto> Populations { get; set; }
        public List<ObjectiveDto> Objectives { get; set; }
        public List<ScheduleTimelineDto> ScheduleTimelines { get; set; }
        public List<CodeDto> TherapeuticAreas { get; set; }
        public List<EstimandDto> Estimands { get; set; }
        public List<EncounterDto> Encounters { get; set; }
        public List<ActivityDto> Activities { get; set; }
        public string Rationale { get; set; }
        public AliasCodeDto BlindingSchema { get; set; }
        public List<BiomedicalConceptDto> BiomedicalConcepts { get; set; }
        public List<BiomedicalConceptCategoryDto> BcCategories { get; set; }
        public List<BiomedicalConceptSurrogateDto> BcSurrogates { get; set; }
        public List<StudyArmDto> Arms { get; set; }
        public List<StudyEpochDto> Epochs { get; set; }
        public List<StudyElementDto> Elements { get; set; }        
        public string DocumentVersionId { get; set; }
        public List<SyntaxTemplateDictionaryDto> Dictionaries { get; set; }
        public string InstanceType { get; set; }
    }
}
