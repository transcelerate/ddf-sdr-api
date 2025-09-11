using JsonSubTypes;
using Newtonsoft.Json;
using System.Collections.Generic;
using TransCelerate.SDR.Core.Utilities;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    [JsonConverter(typeof(JsonSubtypes), nameof(InstanceType))]
    [JsonSubtypes.KnownSubType(typeof(CharacteristicDto), nameof(SyntaxTemplateInstanceTypeV5.Characteristic))]
    [JsonSubtypes.KnownSubType(typeof(EligibilityCriterionItemDto), nameof(SyntaxTemplateInstanceTypeV5.EligibilityCriterionItem))]
    [JsonSubtypes.KnownSubType(typeof(ConditionDto), nameof(SyntaxTemplateInstanceTypeV5.Condition))]
    [JsonSubtypes.KnownSubType(typeof(IntercurrentEventDto), nameof(SyntaxTemplateInstanceTypeV5.IntercurrentEvent))]
    [JsonSubtypes.KnownSubType(typeof(EndpointDto), nameof(SyntaxTemplateInstanceTypeV5.Endpoint))]
    [JsonSubtypes.KnownSubType(typeof(ObjectiveDto), nameof(SyntaxTemplateInstanceTypeV5.Objective))]
    public class SyntaxTemplateDto : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string DictionaryId { get; set; }
        public string InstanceType { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
    }
}
