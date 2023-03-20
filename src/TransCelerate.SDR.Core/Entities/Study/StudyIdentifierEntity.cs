using MongoDB.Bson.Serialization.Attributes;

namespace TransCelerate.SDR.Core.Entities.Study
{
    [BsonIgnoreExtraElements]
    public class StudyIdentifierEntity
    {
        [BsonElement(Utilities.Common.IdFieldPropertyName.MVP.Id)]
        public string StudyIdentifierId { get; set; }
        public string OrgCode { get; set; }
        public string Name { get; set; }
        public string IdType { get; set; }

    }
}
