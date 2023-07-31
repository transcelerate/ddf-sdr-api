using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class ScheduledActivityInstanceEntity : ScheduledInstanceEntity
    {
        public override string ScheduledInstanceType { get; set; } = nameof(Utilities.ScheduledInstanceType.ACTIVITY);
        public string ScheduledActivityInstanceEncounterId { get; set; }
        public List<string> ActivityIds { get; set; }
    }
}
