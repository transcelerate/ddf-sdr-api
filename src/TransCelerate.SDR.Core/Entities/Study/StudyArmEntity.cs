using MongoDB.Bson.Serialization.Attributes;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class StudyArmEntity
    {

        [BsonElement(Utilities.Common.IdFieldPropertyName.MVP.Id)]
        public string StudyArmId { get; set; }
        public string Description { get; set; }
        public string StudyArmType { get; set; }
        public string StudyOriginType { get; set; }
        public string StudyArmOrigin { get; set; }
        public string Name { get; set; }
    }
}
