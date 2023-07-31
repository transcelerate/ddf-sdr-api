
namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class OrganisationEntity : IId
    {        
        public string Id { get; set; }
        public string OrganisationIdentifier { get; set; }
        public string OrganisationIdentifierScheme { get; set; }
        public string OrganisationName { get; set; }
        public CodeEntity OrganisationType { get; set; }
        public AddressEntity OrganizationLegalAddress { get; set; }
    }
}
