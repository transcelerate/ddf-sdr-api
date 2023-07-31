namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class EndpointEntity : IId
    {        
        public string Id { get; set; }
        public string EndpointDescription { get; set; }
        public string EndpointPurposeDescription { get; set; }
        public CodeEntity EndpointLevel { get; set; }
    }
}
