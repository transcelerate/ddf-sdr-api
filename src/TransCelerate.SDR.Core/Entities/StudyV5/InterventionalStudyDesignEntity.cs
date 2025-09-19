using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class InterventionalStudyDesignEntity : StudyDesignEntity
    {
        public List<CodeEntity> SubTypes { get; set; }
        public List<CodeEntity> IntentTypes { get; set; }
        public CodeEntity Model { get; set; }
        public AliasCodeEntity BlindingSchema { get; set; }
    }
}
