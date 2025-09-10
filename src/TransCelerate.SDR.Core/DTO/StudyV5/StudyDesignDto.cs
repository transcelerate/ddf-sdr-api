using JsonSubTypes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    [JsonConverter(typeof(JsonSubtypes), "instanceType")]
    [JsonSubtypes.KnownSubType(typeof(InterventionalStudyDesignDto), "InterventionalStudyDesign")]
    [JsonSubtypes.KnownSubType(typeof(ObservationalStudyDesignDto), "ObservationalStudyDesign")]
    public abstract class StudyDesignDto : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public List<CodeDto> Characteristics { get; set; }
        public List<StudyCellDto> StudyCells { get; set; }
        public List<IndicationDto> Indications { get; set; }
        public List<string> StudyInterventionIds { get; set; }
        public StudyDesignPopulationDto Population { get; set; }
        public List<ObjectiveDto> Objectives { get; set; }
        public List<ScheduleTimelineDto> ScheduleTimelines { get; set; }
        public List<CodeDto> TherapeuticAreas { get; set; }
        public List<EstimandDto> Estimands { get; set; }
        public List<EncounterDto> Encounters { get; set; }
        public List<ActivityDto> Activities { get; set; }
        public List<EligibilityCriterionDto> EligibilityCriteria { get; set; }
        public string Rationale { get; set; }
        public List<BiospecimenRetentionDto> BiospecimenRetentions { get; set; }
        public List<StudyArmDto> Arms { get; set; }
        public List<StudyEpochDto> Epochs { get; set; }
        public List<StudyElementDto> Elements { get; set; }
        public List<string> DocumentVersionIds { get; set; }
        public string InstanceType { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
        public AliasCodeDto StudyPhase { get; set; }
        public CodeDto StudyType { get; set; }
        public List<AnalysisPopulationDto> AnalysisPopulations { get; set; }
    }
}
