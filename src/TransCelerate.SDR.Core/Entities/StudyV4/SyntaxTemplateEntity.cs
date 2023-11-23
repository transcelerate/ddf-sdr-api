using JsonSubTypes;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [JsonConverter(typeof(JsonSubtypes), nameof(SyntaxTemplateEntity.InstanceType))]
    [JsonSubtypes.KnownSubType(typeof(ObjectiveEntity), nameof(Utilities.SyntaxTemplateInstanceType.OBJECTIVE))]
    [JsonSubtypes.KnownSubType(typeof(EndpointEntity), nameof(Utilities.SyntaxTemplateInstanceType.ENDPOINT))]
    [JsonSubtypes.KnownSubType(typeof(EligibilityCriteriaEntity), nameof(Utilities.SyntaxTemplateInstanceType.ELIGIBILITY_CRITERIA))]    
    [BsonIgnoreExtraElements]
    [BsonNoId]
    [BsonDiscriminator(nameof(SyntaxTemplateEntity.InstanceType))]
    [BsonKnownTypes(typeof(ObjectiveEntity))]
    [BsonKnownTypes(typeof(EndpointEntity))]
    [BsonKnownTypes(typeof(EligibilityCriteriaEntity))]
    public class SyntaxTemplateEntity : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string DictionaryId { get; set; }
        public virtual string InstanceType { get; set; }
    }
}
