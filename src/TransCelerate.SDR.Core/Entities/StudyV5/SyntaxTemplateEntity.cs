using JsonSubTypes;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using TransCelerate.SDR.Core.Utilities;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [JsonConverter(typeof(JsonSubtypes), nameof(InstanceType))]
    [JsonSubtypes.KnownSubType(typeof(CharacteristicEntity), nameof(SyntaxTemplateInstanceTypeV5.Characteristic))]
    [JsonSubtypes.KnownSubType(typeof(EligibilityCriterionItemEntity), nameof(SyntaxTemplateInstanceTypeV5.EligibilityCriterionItem))]
    [JsonSubtypes.KnownSubType(typeof(ConditionEntity), nameof(SyntaxTemplateInstanceTypeV5.Condition))]
    [JsonSubtypes.KnownSubType(typeof(IntercurrentEventEntity), nameof(SyntaxTemplateInstanceTypeV5.IntercurrentEvent))]
    [JsonSubtypes.KnownSubType(typeof(EndpointEntity), nameof(SyntaxTemplateInstanceTypeV5.Endpoint))]
    [JsonSubtypes.KnownSubType(typeof(ObjectiveEntity), nameof(SyntaxTemplateInstanceTypeV5.Objective))]
    [BsonIgnoreExtraElements]
    [BsonNoId]
    [BsonDiscriminator(nameof(InstanceType))]
    [BsonKnownTypes(typeof(CharacteristicEntity))]
    [BsonKnownTypes(typeof(EligibilityCriterionItemEntity))]
    [BsonKnownTypes(typeof(ConditionEntity))]
    [BsonKnownTypes(typeof(IntercurrentEventEntity))]
    [BsonKnownTypes(typeof(EndpointEntity))]
    [BsonKnownTypes(typeof(ObjectiveEntity))]
    public class SyntaxTemplateEntity : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string DictionaryId { get; set; }
        public string InstanceType { get; set; }
        public List<CommentAnnotationEntity> Notes { get; set; }
    }
}
