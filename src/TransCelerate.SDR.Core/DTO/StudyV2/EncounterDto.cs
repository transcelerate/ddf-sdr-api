using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class EncounterDto : Iid
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.EncounterId)]
        public string Id { get; set; }
        public List<CodeDto> EncounterContactModes { get; set; }
        public string EncounterDescription { get; set; }
        public List<CodeDto> EncounterEnvironmentalSetting { get; set; }
        public string EncounterName { get; set; }
        public List<CodeDto> EncounterType { get; set; }
        public string NextEncounterId { get; set; }
        public string PreviousEncounterId { get; set; }
        public string EncounterScheduledAt { get; set; }
        public TransitionRuleDto TransitionStartRule { get; set; }
        public TransitionRuleDto TransitionEndRule { get; set; }        
    }
}
