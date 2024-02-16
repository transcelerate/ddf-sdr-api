using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class EncounterDto : IId
    {        
        public string Id { get; set; }        
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }        
        public List<CodeDto> ContactModes { get; set; }
        public CodeDto EnvironmentalSetting { get; set; }
        public CodeDto Type { get; set; }
        public string NextId { get; set; }
        public string PreviousId { get; set; }
        public string ScheduledAtTimingId { get; set; }
        public TransitionRuleDto TransitionStartRule { get; set; }
        public TransitionRuleDto TransitionEndRule { get; set; }
        public string InstanceType { get; set; }
    }
}
