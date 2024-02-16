using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class ScheduleTimelineDto : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string EntryCondition { get; set; }
        public string EntryId { get; set; }
        public List<ScheduleTimelineExitDto> Exits { get; set; }
        public object MainTimeline { get; set; }
        public List<ScheduledInstanceDto> Instances { get; set; }
        public string InstanceType { get; set; }
    }
}
