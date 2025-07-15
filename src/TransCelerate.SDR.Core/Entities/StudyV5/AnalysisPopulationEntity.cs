using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class AnalysisPopulationEntity : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string InstanceType { get; set; }
        public List<CommentAnnotationEntity> Notes { get; set; }
        public List<PopulationDefinitionEntity> SubsetOf { get; set; }
	}
}
