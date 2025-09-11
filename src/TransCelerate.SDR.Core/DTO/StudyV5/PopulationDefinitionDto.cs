using JsonSubTypes;
using Newtonsoft.Json;
using System.Collections.Generic;
using TransCelerate.SDR.Core.Utilities;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    [JsonConverter(typeof(JsonSubtypes), nameof(InstanceType))]
    [JsonSubtypes.KnownSubType(typeof(StudyCohortDto), nameof(PopulationDefinitionInstanceTypeV5.StudyCohort))]
    [JsonSubtypes.KnownSubType(typeof(StudyDesignPopulationDto), nameof(PopulationDefinitionInstanceTypeV5.StudyDesignPopulation))]
    public class PopulationDefinitionDto : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public bool IncludesHealthySubjects { get; set; }
        public RangeDto PlannedAge { get; set; }
        public QuantityRangeDto PlannedCompletionNumber { get; set; }
        public QuantityRangeDto PlannedEnrollmentNumber { get; set; }
        public List<CodeDto> PlannedSex { get; set; }
        public List<string> CriterionIds { get; set; }
        public string InstanceType { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
    }
}
