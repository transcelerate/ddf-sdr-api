namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class StudyElementDto : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }        
        public TransitionRuleDto TransitionStartRule { get; set; }
        public TransitionRuleDto TransitionEndRule { get; set; }
        public string StudyInterventionId { get; set; }
        public string InstanceType { get; set; }
    }
}
