using MongoDB.Bson.Serialization.Attributes;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class EncounterEntity
    {
        [BsonElement(Utilities.Common.IdFieldPropertyName.MVP.Id)]
        public string EncounterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContactMode { get; set; }
        public string EnvironmentalSetting { get; set; }
        public RuleEntity StartRule { get; set; }
        public RuleEntity EndRule { get; set; }
        public EpochEntity Epoch { get; set; }
    }
}
