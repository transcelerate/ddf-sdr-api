using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class StudyCellDTO
    {

        public string Id { get; set; }

        public List<StudyElementDTO> StudyElements { get; set; }

        public StudyArmDTO StudyArm { get; set; }

        public StudyEpochDTO StudyEpoch { get; set; }
    }
}
