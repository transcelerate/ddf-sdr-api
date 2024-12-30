using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class StudyDesignDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.StudyDesignId)]
        public string Id { get; set; }
        public string StudyDesignName { get; set; }
        public string StudyDesignDescription { get; set; }
        public List<CodeDto> TrialIntentType { get; set; }
        public List<CodeDto> TrialType { get; set; }
        public CodeDto InterventionModel { get; set; }
        public List<StudyCellDto> StudyCells { get; set; }
        public List<IndicationDto> StudyIndications { get; set; }
        public List<InvestigationalInterventionDto> StudyInvestigationalInterventions { get; set; }
        public List<StudyDesignPopulationDto> StudyPopulations { get; set; }
        public List<ObjectiveDto> StudyObjectives { get; set; }
        public List<ScheduleTimelineDto> StudyScheduleTimelines { get; set; }
        public List<CodeDto> TherapeuticAreas { get; set; }
        public List<EstimandDto> StudyEstimands { get; set; }
        public List<EncounterDto> Encounters { get; set; }
        public List<ActivityDto> Activities { get; set; }
        public string StudyDesignRationale { get; set; }
        public AliasCodeDto StudyDesignBlindingScheme { get; set; }
        public List<BiomedicalConceptDto> BiomedicalConcepts { get; set; }
        public List<BiomedicalConceptCategoryDto> BcCategories { get; set; }
        public List<BiomedicalConceptSurrogateDto> BcSurrogates { get; set; }
    }
}
