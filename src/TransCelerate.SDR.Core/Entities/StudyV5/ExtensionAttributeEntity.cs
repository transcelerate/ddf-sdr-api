using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class ExtensionAttributeEntity : IId
    {
        public string Id { get; set; }
        public string InterventionId { get; set; }
        public string SummaryMeasure { get; set; }
        public AnalysisPopulationEntity AnalysisPopulation { get; set; }
        public string VariableOfInterestId { get; set; }
        public List<IntercurrentEventEntity> IntercurrentEvents { get; set; }
        public string InstanceType { get; set; }
    }
}
