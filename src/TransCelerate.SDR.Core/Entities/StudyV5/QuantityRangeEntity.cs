using JsonSubTypes;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using TransCelerate.SDR.Core.Utilities;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [JsonConverter(typeof(JsonSubtypes), nameof(InstanceType))]
    [JsonSubtypes.KnownSubType(typeof(QuantityEntity), nameof(QuantityRangeInstanceTypeV5.Quantity))]
    [JsonSubtypes.KnownSubType(typeof(RangeEntity), nameof(QuantityRangeInstanceTypeV5.Range))]
    [BsonIgnoreExtraElements]
    [BsonNoId]
    [BsonDiscriminator(nameof(InstanceType))]
    [BsonKnownTypes(typeof(QuantityEntity))]
    [BsonKnownTypes(typeof(RangeEntity))]
    public class QuantityRangeEntity : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
    }
}
