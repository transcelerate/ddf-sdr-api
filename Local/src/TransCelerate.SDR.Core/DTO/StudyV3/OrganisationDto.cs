namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class OrganisationDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV3.OrganisationId)]
        public string Id { get; set; }
        public string OrganisationIdentifier { get; set; }
        public string OrganisationIdentifierScheme { get; set; }
        public string OrganisationName { get; set; }
        public CodeDto OrganisationType { get; set; }
        public AddressDto OrganizationLegalAddress { get; set; }
    }
}
