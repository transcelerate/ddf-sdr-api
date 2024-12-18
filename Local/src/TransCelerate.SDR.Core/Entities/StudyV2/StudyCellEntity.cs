using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyCellEntity : IId
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.StudyCellId)]
        public string Id { get; set; }
        public StudyArmEntity StudyArm { get; set; }
        public StudyEpochEntity StudyEpoch { get; set; }
        public List<StudyElementEntity> StudyElements { get; set; }
    }
}
