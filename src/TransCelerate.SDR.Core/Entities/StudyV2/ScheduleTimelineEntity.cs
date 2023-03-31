using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class ScheduleTimelineEntity : IId
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.ScheduleTimelineId)]
        public string Id { get; set; }
        public string ScheduleTimelineName { get; set; }
        public string ScheduleTimelineDescription { get; set; }
        public string EntryCondition { get; set; }
        public string ScheduleTimelineEntryId { get; set; }
        public List<ScheduleTimelineExitEntity> ScheduleTimelineExits { get; set; }
        public List<ScheduledInstanceEntity> ScheduleTimelineInstances { get; set; }
    }
}
