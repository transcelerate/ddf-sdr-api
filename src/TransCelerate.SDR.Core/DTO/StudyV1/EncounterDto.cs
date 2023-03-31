using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    public class EncounterDto : IUuid
    {
        public string Uuid { get; set; }
        public List<CodeDto> EncounterContactMode { get; set; }
        public string EncounterDesc { get; set; }
        public List<CodeDto> EncounterEnvironmentalSetting { get; set; }
        public string EncounterName { get; set; }
        public List<CodeDto> EncounterType { get; set; }
        public string NextEncounterId { get; set; }
        public string PreviousEncounterId { get; set; }
        public TransitionRuleDto TransitionStartRule { get; set; }
        public TransitionRuleDto TransitionEndRule { get; set; }
    }
}
