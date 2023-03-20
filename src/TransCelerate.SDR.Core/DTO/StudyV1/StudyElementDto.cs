namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    public class StudyElementDto : IUuid
    {
        public string Uuid { get; set; }
        public string StudyElementDesc { get; set; }
        public string StudyElementName { get; set; }
        public TransitionRuleDto TransitionStartRule { get; set; }
        public TransitionRuleDto TransitionEndRule { get; set; }
    }
}
