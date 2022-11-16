using System;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyDesignEntity : Iid
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.StudyDesignId)]
        public string Id { get; set; }
        public List<CodeEntity> InterventionModel { get; set; }
        public List<CodeEntity> TrialIntentType { get; set; }
        public List<CodeEntity> TherapeuticAreas { get; set; }
        public List<CodeEntity> TrialType { get; set; }
        public List<IndicationEntity> StudyIndications { get; set; }
        public List<InvestigationalInterventionEntity> StudyInvestigationalInterventions { get; set; }
        public List<ObjectiveEntity> StudyObjectives { get; set; }
        public List<StudyDesignPopulationEntity> StudyPopulations { get; set; }
        public List<StudyCellEntity> StudyCells { get; set; }
        public List<WorkflowEntity> StudyWorkflows { get; set; }
        public List<EstimandEntity> StudyEstimands { get; set; }
    }
}
