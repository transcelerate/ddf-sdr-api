namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class TransitionRuleDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV3.TransitionRuleId)]
        public string Id { get; set; }
        public string TransitionRuleDescription { get; set; }
    }
}
