namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class StudyElementDto : IId
    {        
        public string Id { get; set; }
        public string StudyElementDescription { get; set; }
        public string StudyElementName { get; set; }
        public TransitionRuleDto TransitionStartRule { get; set; }
        public TransitionRuleDto TransitionEndRule { get; set; }
    }
}
