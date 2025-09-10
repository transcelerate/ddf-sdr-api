using JsonSubTypes;
using Newtonsoft.Json;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    [JsonConverter(typeof(JsonSubtypes), "instanceType")]
    [JsonSubtypes.KnownSubType(typeof(QuantityDto), "Quantity")]
    [JsonSubtypes.KnownSubType(typeof(RangeDto), "Range")]
    public abstract class QuantityRangeDto : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
    }
}
