namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class StudyIdentifierDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV3.StudyIdentifierId)]
        public string Id { get; set; }
        public string StudyIdentifier { get; set; }
        public OrganisationDto StudyIdentifierScope { get; set; }
    }
}
