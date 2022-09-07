using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    public class StudyEpochDto
    {
        public string Uuid { get; set; }
        public string NextEpochId { get; set; }
        public string PreviousEpochId { get; set; }
        public string StudyEpochDesc { get; set; }
        public string StudyEpochName { get; set; }
        public List<CodeDto> StudyEpochType { get; set; }
        public List<EncounterDto> Encounters { get; set; }
    }
}
