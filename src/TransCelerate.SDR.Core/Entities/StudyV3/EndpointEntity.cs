namespace TransCelerate.SDR.Core.Entities.StudyV3
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class EndpointEntity : IId
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV3.EndpointId)]
        public string Id { get; set; }
        public string EndpointDescription { get; set; }
        public string EndpointPurposeDescription { get; set; }
        public CodeEntity EndpointLevel { get; set; }
    }
}
