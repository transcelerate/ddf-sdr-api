using MongoDB.Bson.Serialization.Attributes;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class StudyEpochEntity
    {
        [BsonElement(Utilities.Common.IdFieldPropertyName.MVP.Id)]
        public string StudyEpochId { get; set; }
        public string EpochType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SequenceInStudy { get; set; }
    }
}
