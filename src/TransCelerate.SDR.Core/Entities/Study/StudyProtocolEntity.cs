using MongoDB.Bson.Serialization.Attributes;

namespace TransCelerate.SDR.Core.Entities.Study
{

    [BsonIgnoreExtraElements]
    public class StudyProtocolEntity
    {
        public string StudyProtocolId { get; set; }
        public string StudyProtocolVersion { get; set; }
    }
}
