using JsonSubTypes;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV3
{
    [JsonConverter(typeof(JsonSubtypes), nameof(ScheduledInstanceEntity.ScheduledInstanceType))]
    [JsonSubtypes.KnownSubType(typeof(ScheduledActivityInstanceEntity), nameof(Utilities.ScheduledInstanceType.ACTIVITY))]
    [JsonSubtypes.KnownSubType(typeof(ScheduledDecisionInstanceEntity), nameof(Utilities.ScheduledInstanceType.DECISION))]
    [BsonIgnoreExtraElements]
    [BsonNoId]
    [BsonDiscriminator(nameof(ScheduledInstanceEntity.ScheduledInstanceType))]
    [BsonKnownTypes(typeof(ScheduledActivityInstanceEntity))]
    [BsonKnownTypes(typeof(ScheduledDecisionInstanceEntity))]
    public class ScheduledInstanceEntity : IId
    {
        [BsonElement(Utilities.Common.IdFieldPropertyName.StudyV3.ScheduledInstanceId)]
        public string Id { get; set; }
        public string ScheduleTimelineExitId { get; set; }
        public string ScheduledInstanceTimelineId { get; set; }
        public string DefaultConditionId { get; set; }
        public string EpochId { get; set; }
        public List<TimingEntity> ScheduledInstanceTimings { get; set; }
        public virtual string ScheduledInstanceType { get; set; }
    }
}
