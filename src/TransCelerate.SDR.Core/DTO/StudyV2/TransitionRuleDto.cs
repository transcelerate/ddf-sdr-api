namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class TransitionRuleDto : Iid
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.TransitionRuleId)]
        public string Id { get; set; }
        public string TransitionRuleDescription { get; set; }
    }
}
