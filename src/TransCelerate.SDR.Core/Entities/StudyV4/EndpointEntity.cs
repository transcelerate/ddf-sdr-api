namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class EndpointEntity : SyntaxTemplateEntity
    {
        public override string InstanceType { get; set; } = nameof(Utilities.SyntaxTemplateInstanceType.ENDPOINT);
        public string EndpointPurpose { get; set; }
        public CodeEntity Level { get; set; }
    }
}
