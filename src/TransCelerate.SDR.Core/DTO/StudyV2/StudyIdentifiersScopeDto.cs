namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class StudyIdentifiersScopeDto : IUuid
    {
        public string Uuid { get; set; }
        public string OrganisationIdentifier { get; set; }
        public string OrganisationIdentifierScheme { get; set; }
        public string OrganisationName { get; set; }
        public CodeDto OrganisationType { get; set; }
    }
}
