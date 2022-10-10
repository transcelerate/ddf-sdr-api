using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class StudyCellEntity : IUuid
    {
        public string Uuid { get; set; }
        public StudyArmEntity StudyArm { get; set; }
        public StudyEpochEntity StudyEpoch { get; set; }
        public List<StudyElementEntity> StudyElements { get; set; }
    }
}
