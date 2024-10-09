using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyEntity : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public List<StudyVersionEntity> Versions { get; set; }
        public StudyProtocolDocumentEntity DocumentedBy { get; set; }
        public string InstanceType { get; set; }
    }
}
