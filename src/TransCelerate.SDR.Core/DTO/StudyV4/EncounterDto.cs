using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class EncounterDto : IId
    {        
        public string Id { get; set; }
        public List<CodeDto> EncounterContactModes { get; set; }
        public string EncounterDescription { get; set; }
        public CodeDto EncounterEnvironmentalSetting { get; set; }
        public string EncounterName { get; set; }
        public CodeDto EncounterType { get; set; }
        public string NextEncounterId { get; set; }
        public string PreviousEncounterId { get; set; }
        public string EncounterScheduledAtTimingId { get; set; }
        public TransitionRuleDto TransitionStartRule { get; set; }
        public TransitionRuleDto TransitionEndRule { get; set; }
    }
}
