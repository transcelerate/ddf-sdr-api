using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class StudyCellEntity
    {
        [BsonElement(Utilities.Common.IdFieldPropertyName.MVP.Id)]
        public string StudyCellId { get; set; }
        public List<StudyElementEntity> StudyElements { get; set; }
        public StudyArmEntity StudyArm { get; set; }
        public StudyEpochEntity StudyEpoch { get; set; }
    }
}
