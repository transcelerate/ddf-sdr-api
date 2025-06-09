using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyInterventionEntity : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public List<CodeEntity> Codes { get; set; }
        public CodeEntity Role { get; set; }
        public CodeEntity Type { get; set; }
        public QuantityEntity MinimumResponseDuration { get; set; }
        public CodeEntity ProductDesignation { get; set; }
        public CodeEntity PharmacologicClass { get; set; }
        public string InstanceType { get; set; }
		public List<CommentAnnotationEntity> Notes { get; set; }
	}
}
