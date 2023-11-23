using JsonSubTypes;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [JsonConverter(typeof(JsonSubtypes), nameof(ScheduledInstanceEntity.InstanceType))]
    [JsonSubtypes.KnownSubType(typeof(ScheduledActivityInstanceEntity), nameof(Utilities.ScheduledInstanceType.ACTIVITY))]
    [JsonSubtypes.KnownSubType(typeof(ScheduledDecisionInstanceEntity), nameof(Utilities.ScheduledInstanceType.DECISION))]
    [BsonIgnoreExtraElements]
    [BsonNoId]
    [BsonDiscriminator(nameof(ScheduledInstanceEntity.InstanceType))]
    [BsonKnownTypes(typeof(ScheduledActivityInstanceEntity))]
    [BsonKnownTypes(typeof(ScheduledDecisionInstanceEntity))]
    public class ScheduledInstanceEntity : IId
    {        
        public string Id { get; set; }
        public string ExitId { get; set; }
        public string TimelineId { get; set; }
        public string DefaultConditionId { get; set; }
        public string EpochId { get; set; }
        public List<TimingEntity> Timings { get; set; }
        public virtual string InstanceType { get; set; }
    }
}
