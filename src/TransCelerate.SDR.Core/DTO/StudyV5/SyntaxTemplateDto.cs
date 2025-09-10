using JsonSubTypes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    [JsonConverter(typeof(JsonSubtypes), "instanceType")]
    [JsonSubtypes.KnownSubType(typeof(CharacteristicDto), "Characteristic")]
    [JsonSubtypes.KnownSubType(typeof(EligibilityCriterionItemDto), "EligibilityCriterionItem")]
    [JsonSubtypes.KnownSubType(typeof(ConditionDto), "Condition")]
    [JsonSubtypes.KnownSubType(typeof(IntercurrentEventDto), "IntercurrentEvent")]
    [JsonSubtypes.KnownSubType(typeof(EndpointDto), "Endpoint")]
    [JsonSubtypes.KnownSubType(typeof(ObjectiveDto), "Objective")]
    public abstract class SyntaxTemplateDto : IId
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
