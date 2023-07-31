using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class EstimandEntity : IId
    {        
        public string Id { get; set; }
        public string Treatment { get; set; }
        public string SummaryMeasure { get; set; }
        public AnalysisPopulationEntity AnalysisPopulation { get; set; }
        public string VariableOfInterest { get; set; }
        public List<InterCurrentEventEntity> IntercurrentEvents { get; set; }
    }
}
