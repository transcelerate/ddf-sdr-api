namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class StudyIdentifierEntity : IUuid
    {
        public string Uuid { get; set; }
        public string StudyIdentifier { get; set; }
        public StudyIdentifierScopeEntity StudyIdentifierScope { get; set; }
    }
}
