using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyProtocolDocumentVersionEntity : IId
    {
        public string Id { get; set; }
        public CodeEntity ProtocolStatus { get; set; }
        public string ProtocolVersion { get; set; }
        public List<GovernanceDateEntity> DateValues { get; set; }
        public List<NarrativeContentEntity> Contents { get; set; }
        public List<string> ChildIds { get; set; }
        public string InstanceType { get; set; }
    }
}
