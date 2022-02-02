using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class StudyCellEntity
    {
        [BsonElement("id")]
        public string studyCellId { get; set; }
        public List<StudyElementEntity> studyElements { get; set; }
        public StudyArmEntity studyArm { get; set; }
        public StudyEpochEntity studyEpoch { get; set; }
    }
}
