namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class ScheduleTimelineExitEntity : IId
    {        
        public string Id { get; set; }
        public string InstanceType { get; set; }
    }
}
