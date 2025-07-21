using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class ObservationalStudyDesignEntity : StudyDesignEntity
    {
        public CodeEntity TimePerspective { get; set; }
        public List<CodeEntity> SubTypes { get; set; }
        public CodeEntity Model { get; set; }
        public CodeEntity SamplingMethod { get; set; }
	}
}
