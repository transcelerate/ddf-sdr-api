using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class EstimandEntity : IUuid
    {
        public string Uuid { get; set; }
        public InvestigationalInterventionEntity Treatment { get; set; }
        public string SummaryMeasure { get; set; }
        public AnalysisPopulationEntity AnalysisPopulation { get; set; }
        public EndpointEntity VariableOfInterest { get; set; }
        public List<InterCurrentEventEntity> IntercurrentEvents { get; set; }
    }
}
