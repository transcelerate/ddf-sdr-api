namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class StudyIdentifierScopeEntity : IUuid
    {
        public string Uuid { get; set; }
        public string OrganisationIdentifier { get; set; }
        public string OrganisationIdentifierScheme { get; set; }
        public string OrganisationName { get; set; }
        public CodeEntity OrganisationType { get; set; }        
    }
}
