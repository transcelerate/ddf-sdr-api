
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class AbbreviationEntity : IId
    {
        public string Id { get; set; }
        public string ExpandedText { get; set; }
        public string AbbreviatedText { get; set; }
        public string InstanceType { get; set; }
        public List<CommentAnnotationEntity> Notes { get; set; }
    }
}
