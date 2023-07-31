namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class StudyIdentifierDto : IId
    {        
        public string Id { get; set; }
        public string StudyIdentifier { get; set; }
        public OrganisationDto StudyIdentifierScope { get; set; }
    }
}
