using MongoDB.Bson.Serialization.Attributes;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class StudyDataCollectionEntity
    {
        [BsonElement(Utilities.Common.IdFieldPropertyName.MVP.Id)]
        public string StudyDataCollectionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ECRFLink { get; set; }
    }
}
