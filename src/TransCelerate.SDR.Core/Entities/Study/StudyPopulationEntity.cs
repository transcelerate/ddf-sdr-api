using MongoDB.Bson.Serialization.Attributes;


namespace TransCelerate.SDR.Core.Entities.Study
{

    [BsonIgnoreExtraElements]
    public class StudyPopulationEntity
    {
        [BsonElement(Utilities.Common.IdFieldPropertyName.MVP.Id)]
        public string StudyPopulationId { get; set; }
        public string Description { get; set; }
    }
}
