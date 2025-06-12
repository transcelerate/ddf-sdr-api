namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class OrganizationDto : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Identifier { get; set; }
        public string IdentifierScheme { get; set; }
        public CodeDto Type { get; set; }
        public AddressDto LegalAddress { get; set; }
        public StudySiteDto ManagedSites { get; set; }
        public string InstanceType { get; set; }
    }
}
