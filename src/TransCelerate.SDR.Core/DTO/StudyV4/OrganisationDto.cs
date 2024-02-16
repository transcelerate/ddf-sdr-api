namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class OrganisationDto : IId
    {        
        public string Id { get; set; }
        public string OrganisationIdentifier { get; set; }
        public string OrganisationIdentifierScheme { get; set; }
        public string OrganisationName { get; set; }
        public CodeDto OrganisationType { get; set; }
        public AddressDto OrganizationLegalAddress { get; set; }
        public string InstanceType { get; set; }
    }
}
