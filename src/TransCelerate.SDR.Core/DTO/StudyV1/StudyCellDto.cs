using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    public class StudyCellDto : IUuid
    {
        public string Uuid { get; set; }
        public StudyArmDto StudyArm { get; set; }
        public StudyEpochDto StudyEpoch { get; set; }
        public List<StudyElementDto> StudyElements { get; set; }
    }
}
