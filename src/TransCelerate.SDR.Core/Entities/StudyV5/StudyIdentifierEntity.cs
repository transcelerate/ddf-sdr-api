namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyIdentifierEntity : IId
    {        
        public string Id { get; set; }
        public string Text { get; set; }
        public OrganizationEntity Scope { get; set; }
        public string InstanceType { get; set; }
    }
}
// Study identifier should inherit from IdentifierEntity - this will give it scope and text automatically. 
// StudyIdentifierScope becomes scope - this is used everywhere, so rename it to scope first, and then change the inheritance
// Also rename studyIdentifier to Text first