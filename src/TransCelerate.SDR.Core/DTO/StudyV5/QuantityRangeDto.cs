using JsonSubTypes;
using Newtonsoft.Json;
using TransCelerate.SDR.Core.Utilities;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    [JsonConverter(typeof(JsonSubtypes), nameof(InstanceType))]
    [JsonSubtypes.KnownSubType(typeof(QuantityDto), nameof(QuantityRangeInstanceTypeV5.Quantity))]
    [JsonSubtypes.KnownSubType(typeof(RangeDto), nameof(QuantityRangeInstanceTypeV5.Range))]
    public class QuantityRangeDto : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
    }
}
