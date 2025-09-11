using JsonSubTypes;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using TransCelerate.SDR.Core.Utilities;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [JsonConverter(typeof(JsonSubtypes), nameof(InstanceType))]
    [JsonSubtypes.KnownSubType(typeof(StudyCohortEntity), nameof(PopulationDefinitionInstanceTypeV5.StudyCohort))]
    [JsonSubtypes.KnownSubType(typeof(StudyDesignPopulationEntity), nameof(PopulationDefinitionInstanceTypeV5.StudyDesignPopulation))]
    [BsonIgnoreExtraElements]
    [BsonNoId]
    [BsonDiscriminator(nameof(InstanceType))]
    [BsonKnownTypes(typeof(StudyCohortEntity))]
    [BsonKnownTypes(typeof(StudyDesignPopulationEntity))]
    public class PopulationDefinitionEntity : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public bool IncludesHealthySubjects { get; set; }
        public RangeEntity PlannedAge { get; set; }
        public QuantityRangeEntity PlannedCompletionNumber { get; set; }
        public QuantityRangeEntity PlannedEnrollmentNumber { get; set; }
        public List<CodeEntity> PlannedSex { get; set; }
        public List<string> CriterionIds { get; set; }
        public string InstanceType { get; set; }
        public List<CommentAnnotationEntity> Notes { get; set; }
    }
}
