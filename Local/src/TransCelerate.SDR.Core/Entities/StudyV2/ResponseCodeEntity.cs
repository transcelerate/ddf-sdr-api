namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class ResponseCodeEntity : IId
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.ResponseCodeId)]
        public string Id { get; set; }
        public bool ResponseCodeEnabled { get; set; }
        public CodeEntity Code { get; set; }
    }
}
