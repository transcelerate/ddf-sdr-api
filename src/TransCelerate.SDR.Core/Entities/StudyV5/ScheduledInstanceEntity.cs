using JsonSubTypes;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using TransCelerate.SDR.Core.Utilities;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [JsonConverter(typeof(JsonSubtypes), nameof(InstanceType))]
    [JsonSubtypes.KnownSubType(typeof(ScheduledActivityInstanceEntity), nameof(ScheduledInstanceTypeV5.ScheduledActivityInstance))]
    [JsonSubtypes.KnownSubType(typeof(ScheduledDecisionInstanceEntity), nameof(ScheduledInstanceTypeV5.ScheduledDecisionInstance))]
    [BsonIgnoreExtraElements]
    [BsonNoId]
    [BsonDiscriminator(nameof(InstanceType))]
    [BsonKnownTypes(typeof(ScheduledActivityInstanceEntity))]
    [BsonKnownTypes(typeof(ScheduledDecisionInstanceEntity))]
    public class ScheduledInstanceEntity : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string DefaultConditionId { get; set; }
        public string EpochId { get; set; }
        public virtual string InstanceType { get; set; }
    }
}
