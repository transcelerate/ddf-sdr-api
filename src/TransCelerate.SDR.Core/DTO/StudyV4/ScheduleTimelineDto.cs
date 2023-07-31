using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class ScheduleTimelineDto : IId
    {        
        public string Id { get; set; }
        public string ScheduleTimelineName { get; set; }
        public string ScheduleTimelineDescription { get; set; }
        public string EntryCondition { get; set; }
        public string ScheduleTimelineEntryId { get; set; }
        public List<ScheduleTimelineExitDto> ScheduleTimelineExits { get; set; }
        public object MainTimeline { get; set; }
        public List<ScheduledInstanceDto> ScheduleTimelineInstances { get; set; }
    }
}
