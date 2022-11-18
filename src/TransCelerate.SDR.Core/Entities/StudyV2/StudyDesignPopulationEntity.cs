namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyDesignPopulationEntity : Iid
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.StudyDesignPopulationId)]
        public string Id { get; set; }
        public string PopulationDescription { get; set; }
    }
}
