namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class StudyIdentifierDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.StudyIdentifierId)]
        public string Id { get; set; }
        public string StudyIdentifier { get; set; }
        public OrganisationDto StudyIdentifierScope { get; set; }
    }
}
