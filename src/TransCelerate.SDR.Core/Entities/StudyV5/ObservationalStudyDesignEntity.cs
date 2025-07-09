using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class ObservationalStudyDesignEntity : StudyDesignEntity
    {
        public AliasCodeEntity StudyPhase { get; set; }
        public CodeEntity StudyType { get; set; }
        public List<CommentAnnotationEntity> Notes { get; set; }
        public CodeEntity TimePerspective { get; set; }
        public List<CodeEntity> SubTypes { get; set; }
        public CodeEntity Model { get; set; }
        public CodeEntity SamplingMethod { get; set; }
	}
}
