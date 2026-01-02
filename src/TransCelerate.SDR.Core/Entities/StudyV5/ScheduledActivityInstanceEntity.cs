using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class ScheduledActivityInstanceEntity : ScheduledInstanceEntity
    {
        public override string InstanceType { get; set; } = nameof(Utilities.ScheduledInstanceTypeV4.ScheduledActivityInstance);
        public string TimelineExitId { get; set; }
        public string TimelineId { get; set; }
        public string EncounterId { get; set; }
        public List<string> ActivityIds { get; set; }
    }
}
