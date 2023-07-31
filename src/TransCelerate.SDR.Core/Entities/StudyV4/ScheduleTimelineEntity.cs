using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class ScheduleTimelineEntity : IId
    {        
        public string Id { get; set; }
        public string ScheduleTimelineName { get; set; }
        public string ScheduleTimelineDescription { get; set; }
        public string EntryCondition { get; set; }
        public string ScheduleTimelineEntryId { get; set; }
        public bool MainTimeline { get; set; }
        public List<ScheduleTimelineExitEntity> ScheduleTimelineExits { get; set; }
        public List<ScheduledInstanceEntity> ScheduleTimelineInstances { get; set; }
    }
}
