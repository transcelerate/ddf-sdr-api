using JsonSubTypes;
using Newtonsoft.Json;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    [JsonConverter(typeof(JsonSubtypes), nameof(SyntaxTemplateDto.InstanceType))]
    [JsonSubtypes.KnownSubType(typeof(ObjectiveDto), nameof(Utilities.SyntaxTemplateInstanceType.OBJECTIVE))]
    [JsonSubtypes.KnownSubType(typeof(EndpointDto), nameof(Utilities.SyntaxTemplateInstanceType.ENDPOINT))]
    [JsonSubtypes.KnownSubType(typeof(EligibilityCriteriaDto), nameof(Utilities.SyntaxTemplateInstanceType.ELIGIBILITY_CRITERIA))]
    public class SyntaxTemplateDto : IId
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
