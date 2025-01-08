namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class StudyElementDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.StudyElementId)]
        public string Id { get; set; }
        public string StudyElementDescription { get; set; }
        public string StudyElementName { get; set; }
        public TransitionRuleDto TransitionStartRule { get; set; }
        public TransitionRuleDto TransitionEndRule { get; set; }
    }
}
