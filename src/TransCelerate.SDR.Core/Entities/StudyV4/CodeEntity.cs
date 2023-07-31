namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class CodeEntity : IId
    {        
        public string Id { get; set; }
        public string Code { get; set; }
        public string CodeSystem { get; set; }
        public string CodeSystemVersion { get; set; }
        public string Decode { get; set; }
    }
}
