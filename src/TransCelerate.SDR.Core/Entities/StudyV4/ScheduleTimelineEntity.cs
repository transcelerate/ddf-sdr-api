using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class ScheduleTimelineEntity : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }        
        public string Label { get; set; }
        public string Description { get; set; }
        public string EntryCondition { get; set; }
        public string EntryId { get; set; }
        public bool MainTimeline { get; set; }
        public List<ScheduleTimelineExitEntity> Exits { get; set; }
        public List<ScheduledInstanceEntity> Instances { get; set; }
    }
}
