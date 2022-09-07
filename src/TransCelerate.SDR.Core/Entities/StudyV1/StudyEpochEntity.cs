using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class StudyEpochEntity
    {
        public string Uuid { get; set; }
        public string NextEpochId { get; set; }
        public string PreviousEpochId { get; set; }
        public string StudyEpochDesc { get; set; }
        public string StudyEpochName { get; set; }
        public List<CodeEntity> StudyEpochType { get; set; }               
        public List<EncounterEntity> Encounters { get; set; }
    }
}
