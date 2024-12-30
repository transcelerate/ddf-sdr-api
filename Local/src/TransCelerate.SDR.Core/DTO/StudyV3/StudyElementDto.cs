namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class StudyElementDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV3.StudyElementId)]
        public string Id { get; set; }
        public string StudyElementDescription { get; set; }
        public string StudyElementName { get; set; }
        public TransitionRuleDto TransitionStartRule { get; set; }
        public TransitionRuleDto TransitionEndRule { get; set; }
    }
}
