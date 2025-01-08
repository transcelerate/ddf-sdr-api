
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class CommentAnnotationEntity : IId
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public List<CodeEntity> Codes { get; set; }
        public string InstanceType { get; set; }
    }
}
