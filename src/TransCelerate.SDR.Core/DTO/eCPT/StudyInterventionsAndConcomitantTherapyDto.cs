using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.eCPT
{
    public class StudyInterventionsAndConcomitantTherapyDto
    {
        public List<StudyInterventionsAdministeredDto> StudyInterventionsAdministered { get; set; }
        public List<StudyArmDto> StudyArms { get; set; }
    }
}
