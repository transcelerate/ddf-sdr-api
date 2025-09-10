using JsonSubTypes;
using Newtonsoft.Json;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    [JsonConverter(typeof(JsonSubtypes), "instanceType")]
    [JsonSubtypes.KnownSubType(typeof(StudyIdentifierDto), "StudyIdentifier")]
    [JsonSubtypes.KnownSubType(typeof(ReferenceIdentifierDto), "ReferenceIdentifier")]
    [JsonSubtypes.KnownSubType(typeof(MedicalDeviceIdentifierDto), "MedicalDeviceIdentifier")]
    [JsonSubtypes.KnownSubType(typeof(AdministrableProductIdentifierDto), "AdministrableProductIdentifier")]
    public abstract class IdentifierDto : IId
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string ScopeId { get; set; }
        public string InstanceType { get; set; }
    }
}
