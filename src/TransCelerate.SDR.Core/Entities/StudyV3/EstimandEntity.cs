using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV3
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class EstimandEntity : IId
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV3.EstimandId)]
        public string Id { get; set; }
        public string Treatment { get; set; }
        public string SummaryMeasure { get; set; }
        public AnalysisPopulationEntity AnalysisPopulation { get; set; }
        public string VariableOfInterest { get; set; }
        public List<InterCurrentEventEntity> IntercurrentEvents { get; set; }
    }
}
