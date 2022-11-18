using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class EstimandEntity : Iid
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.EstimandId)]
        public string Id { get; set; }
        public InvestigationalInterventionEntity Treatment { get; set; }
        public string SummaryMeasure { get; set; }
        public AnalysisPopulationEntity AnalysisPopulation { get; set; }
        public EndpointEntity VariableOfInterest { get; set; }
        public List<InterCurrentEventEntity> IntercurrentEvents { get; set; }
    }
}
