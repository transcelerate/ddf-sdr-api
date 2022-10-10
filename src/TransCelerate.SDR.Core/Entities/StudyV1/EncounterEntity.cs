using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class EncounterEntity : IUuid
    {
        public string Uuid { get; set; }
        public List<CodeEntity> EncounterContactMode { get; set; }
        public string EncounterDesc { get; set; }
        public List<CodeEntity> EncounterEnvironmentalSetting { get; set; }
        public string EncounterName { get; set; }
        public List<CodeEntity> EncounterType { get; set; }
        public TransitionRuleEntity TransitionStartRule { get; set; }
        public TransitionRuleEntity TransitionEndRule { get; set; }
        public string NextEncounterId { get; set; }
        public string PreviousEncounterId { get; set; }

    }
}
