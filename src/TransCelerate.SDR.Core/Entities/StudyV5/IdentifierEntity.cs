namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class IdentifierEntity : IId
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public OrganizationEntity Scope { get; set; }
        public string InstanceType { get; set; }
    }
}
