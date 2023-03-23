using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class ScheduleTimelineDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.ScheduleTimelineId)]
        public string Id { get; set; }
        public string ScheduleTimelineName { get; set; }
        public string ScheduleTimelineDescription { get; set; }
        public string EntryCondition { get; set; }
        public string ScheduleTimelineEntryId { get; set; }
        public List<ScheduleTimelineExitDto> ScheduleTimelineExits { get; set; }
        public List<ScheduledInstanceDto> ScheduleTimelineInstances { get; set; }
    }
}
