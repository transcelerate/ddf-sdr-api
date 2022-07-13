using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class EstimandEntity
    {
        public string Uuid { get; set; }
        public InvestigationalInterventionEntity Treatment { get; set; }
        public string SummaryMeasure { get; set; }
        public AnalysisPopulationEntity AnalysisPopulation { get; set; }
        public EncounterEntity VariableOfInterest { get; set; }
        public List<InterCurrentEventEntity> InterCurrentEvents { get; set; }
    }
}
