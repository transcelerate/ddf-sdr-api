namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class InterCurrentEventDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV3.IntercurrentEventId)]
        public string Id { get; set; }
        public string IntercurrentEventDescription { get; set; }
        public string IntercurrentEventName { get; set; }
        public string IntercurrentEventStrategy { get; set; }
    }
}
