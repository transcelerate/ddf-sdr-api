using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    [BsonIgnoreExtraElements]
    public class StudyEpochEntity : IUuid
    {
        public string Uuid { get; set; }
        public string NextStudyEpochId { get; set; }
        public string PreviousStudyEpochId { get; set; }
        public string StudyEpochDesc { get; set; }
        public string StudyEpochName { get; set; }
        public List<CodeEntity> StudyEpochType { get; set; }
        public List<EncounterEntity> Encounters { get; set; }
    }
}
