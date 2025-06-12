
namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class OrganizationEntity : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Identifier { get; set; }
        public string IdentifierScheme { get; set; }        
        public CodeEntity Type { get; set; }
        public AddressEntity LegalAddress { get; set; }
        public StudySiteEntity ManagedSites { get; set; }
        public string InstanceType { get; set; }
    }
}
