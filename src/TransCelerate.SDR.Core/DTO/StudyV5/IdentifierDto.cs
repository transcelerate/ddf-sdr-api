using JsonSubTypes;
using Newtonsoft.Json;
using TransCelerate.SDR.Core.Utilities;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    [JsonConverter(typeof(JsonSubtypes), nameof(InstanceType))]
    [JsonSubtypes.KnownSubType(typeof(StudyIdentifierDto), nameof(IdentifierInstanceTypeV5.StudyIdentifier))]
    [JsonSubtypes.KnownSubType(typeof(ReferenceIdentifierDto), nameof(IdentifierInstanceTypeV5.ReferenceIdentifier))]
    [JsonSubtypes.KnownSubType(typeof(MedicalDeviceIdentifierDto), nameof(IdentifierInstanceTypeV5.MedicalDeviceIdentifier))]
    [JsonSubtypes.KnownSubType(typeof(AdministrableProductIdentifierDto), nameof(IdentifierInstanceTypeV5.AdministrableProductIdentifier))]
    public class IdentifierDto : IId
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string ScopeId { get; set; }
        public string InstanceType { get; set; }
    }
}
