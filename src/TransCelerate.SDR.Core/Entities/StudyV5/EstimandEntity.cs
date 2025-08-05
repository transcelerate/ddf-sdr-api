using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class EstimandEntity : IId
    {        
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string PopulationSummary { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public List<string> InterventionIds { get; set; }
        public AnalysisPopulationEntity AnalysisPopulation { get; set; }
        public List<CommentAnnotationEntity> Notes { get; set; }
        public EndpointEntity VariableOfInterest { get; set; }
        public List<IntercurrentEventEntity> IntercurrentEvents { get; set; }
    }
}
