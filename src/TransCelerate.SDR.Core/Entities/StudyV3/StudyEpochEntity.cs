using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV3
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyEpochEntity : IId
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV3.StudyEpochId)]
        public string Id { get; set; }
        public string NextStudyEpochId { get; set; }
        public string PreviousStudyEpochId { get; set; }
        public string StudyEpochDescription { get; set; }
        public string StudyEpochName { get; set; }
        public CodeEntity StudyEpochType { get; set; }
    }
}
