using JsonSubTypes;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [JsonConverter(typeof(JsonSubtypes), nameof(ScheduledInstanceEntity.InstanceType))]
    [JsonSubtypes.KnownSubType(typeof(ScheduledActivityInstanceEntity), nameof(Utilities.ScheduledInstanceTypeV4.ScheduledActivityInstance))]
    [JsonSubtypes.KnownSubType(typeof(ScheduledDecisionInstanceEntity), nameof(Utilities.ScheduledInstanceTypeV4.ScheduledDecisionInstance))]
    [BsonIgnoreExtraElements]
    [BsonNoId]
    [BsonDiscriminator(nameof(ScheduledInstanceEntity.InstanceType))]
    [BsonKnownTypes(typeof(ScheduledActivityInstanceEntity))]
    [BsonKnownTypes(typeof(ScheduledDecisionInstanceEntity))]
    public class ScheduledInstanceEntity : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string TimelineExitId { get; set; }
        public string TimelineId { get; set; }
        public string DefaultConditionId { get; set; }
        public string EpochId { get; set; }
        public virtual string InstanceType { get; set; }
    }
}
