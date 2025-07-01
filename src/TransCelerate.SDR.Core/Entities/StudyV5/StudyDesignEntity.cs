using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyDesignEntity : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public List<CodeEntity> Characteristics { get; set; }
        public List<StudyCellEntity> StudyCells { get; set; }
        public List<IndicationEntity> Indications { get; set; }
        public List<StudyInterventionEntity> StudyInterventions { get; set; }
        public StudyDesignPopulationEntity Population { get; set; }
        public List<ObjectiveEntity> Objectives { get; set; }
        public List<ScheduleTimelineEntity> ScheduleTimelines { get; set; }
        public List<CodeEntity> TherapeuticAreas { get; set; }
        public List<EstimandEntity> Estimands { get; set; }
        public List<EncounterEntity> Encounters { get; set; }
        public List<ActivityEntity> Activities { get; set; }
        public string Rationale { get; set; }
        public List<BiomedicalConceptEntity> BiomedicalConcepts { get; set; }
        public List<BiospecimenRetentionEntity> BiospecimenRetentions { get; set; }
        public List<BiomedicalConceptCategoryEntity> BcCategories { get; set; }
        public List<BiomedicalConceptSurrogateEntity> BcSurrogates { get; set; }
        public List<StudyArmEntity> Arms { get; set; }
        public List<StudyEpochEntity> Epochs { get; set; }
        public List<StudyElementEntity> Elements { get; set; }        
        public string DocumentVersionId { get; set; }
        public List<SyntaxTemplateDictionaryEntity> Dictionaries { get; set; }
        public List<ConditionEntity> Conditions { get; set; }
        public AliasCodeEntity StudyPhase { get; set; }
        public string InstanceType { get; set; }
    }
}
