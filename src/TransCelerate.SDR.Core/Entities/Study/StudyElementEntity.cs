using MongoDB.Bson.Serialization.Attributes;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class StudyElementEntity
    {
        [BsonElement(Utilities.Common.IdFieldPropertyName.MVP.Id)]
        public string StudyElementId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RuleEntity StartRule { get; set; }
        public RuleEntity EndRule { get; set; }
    }
}
