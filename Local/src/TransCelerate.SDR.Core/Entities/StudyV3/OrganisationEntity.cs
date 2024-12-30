
namespace TransCelerate.SDR.Core.Entities.StudyV3
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class OrganisationEntity : IId
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV3.OrganisationId)]
        public string Id { get; set; }
        public string OrganisationIdentifier { get; set; }
        public string OrganisationIdentifierScheme { get; set; }
        public string OrganisationName { get; set; }
        public CodeEntity OrganisationType { get; set; }
        public AddressEntity OrganizationLegalAddress { get; set; }
    }
}
