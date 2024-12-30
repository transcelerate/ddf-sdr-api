using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class EncounterDto : IId
    {        
        public string Id { get; set; }        
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }        
        public List<CodeDto> ContactModes { get; set; }
        public List<CodeDto> EnvironmentalSettings { get; set; }
        public CodeDto Type { get; set; }
        public string NextId { get; set; }
        public string PreviousId { get; set; }
        public string ScheduledAtId { get; set; }
        public TransitionRuleDto TransitionStartRule { get; set; }
        public TransitionRuleDto TransitionEndRule { get; set; }
        public string InstanceType { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
    }
}
