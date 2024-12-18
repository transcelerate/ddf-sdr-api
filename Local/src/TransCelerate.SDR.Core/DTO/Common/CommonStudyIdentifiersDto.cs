namespace TransCelerate.SDR.Core.DTO.Common
{
    public class CommonStudyIdentifiersDto
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.StudyIdentifierId)]
        public string Id { get; set; }
        public string StudyIdentifier { get; set; }
        public CommonOrganisationDto StudyIdentifierScope { get; set; }
    }

    public class CommonOrganisationDto
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.OrganisationId)]
        public string Id { get; set; }
        public string OrganisationIdentifier { get; set; }
        public string OrganisationIdentifierScheme { get; set; }
        public string OrganisationName { get; set; }
        public CommonCodeDto OrganisationType { get; set; }
    }

    public class CommonCodeDto
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.CodeId)]
        public string Id { get; set; }
        public string Code { get; set; }
        public string CodeSystem { get; set; }
        public string CodeSystemVersion { get; set; }
        public string Decode { get; set; }
    }
}
