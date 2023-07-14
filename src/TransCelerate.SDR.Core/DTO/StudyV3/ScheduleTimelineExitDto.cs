namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class ScheduleTimelineExitDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV3.ScheduleTimelineExitId)]
        public string Id { get; set; }
    }
}
