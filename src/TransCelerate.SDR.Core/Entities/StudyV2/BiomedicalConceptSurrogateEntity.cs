namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class BiomedicalConceptSurrogateEntity : IId
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.BcSurrogateId)]
        public string Id { get; set; }
        public string BcSurrogateName { get; set; }
        public string BcSurrogateDescription { get; set; }
        public string BcSurrogateReference { get; set; }
    }
}
