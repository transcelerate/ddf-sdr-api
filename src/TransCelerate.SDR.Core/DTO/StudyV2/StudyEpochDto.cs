using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class StudyEpochDto : IUuid
    {
        public string Uuid { get; set; }
        public string NextStudyEpochId { get; set; }
        public string PreviousStudyEpochId { get; set; }
        public string StudyEpochDescription { get; set; }
        public string StudyEpochName { get; set; }
        public List<CodeDto> StudyEpochType { get; set; }
        public List<EncounterDto> Encounters { get; set; }
    }
}
