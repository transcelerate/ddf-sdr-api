namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class StudyIdentifierDto : IUuid
    {
        public string Uuid { get; set; }
        public string StudyIdentifier { get; set; }
        public StudyIdentifiersScopeDto StudyIdentifierScope { get; set; }
    }
}
