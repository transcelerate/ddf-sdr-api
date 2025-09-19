using JsonSubTypes;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using TransCelerate.SDR.Core.Utilities;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [JsonConverter(typeof(JsonSubtypes), nameof(InstanceType))]
    [JsonSubtypes.KnownSubType(typeof(StudyIdentifierEntity), nameof(IdentifierInstanceTypeV5.StudyIdentifier))]
    [JsonSubtypes.KnownSubType(typeof(ReferenceIdentifierEntity), nameof(IdentifierInstanceTypeV5.ReferenceIdentifier))]
    [JsonSubtypes.KnownSubType(typeof(MedicalDeviceIdentifierEntity), nameof(IdentifierInstanceTypeV5.MedicalDeviceIdentifier))]
    [JsonSubtypes.KnownSubType(typeof(AdministrableProductIdentifierEntity), nameof(IdentifierInstanceTypeV5.AdministrableProductIdentifier))]
    [BsonIgnoreExtraElements]
    [BsonNoId]
    [BsonDiscriminator(nameof(InstanceType))]
    [BsonKnownTypes(typeof(StudyIdentifierEntity))]
    [BsonKnownTypes(typeof(ReferenceIdentifierEntity))]
    [BsonKnownTypes(typeof(MedicalDeviceIdentifierEntity))]
    [BsonKnownTypes(typeof(AdministrableProductIdentifierEntity))]
    public class IdentifierEntity : IId
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string ScopeId { get; set; }
        public string InstanceType { get; set; }
    }
}
