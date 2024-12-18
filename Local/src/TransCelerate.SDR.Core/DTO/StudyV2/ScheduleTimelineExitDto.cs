namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class ScheduleTimelineExitDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.ScheduleTimelineExitId)]
        public string Id { get; set; }
    }
}
