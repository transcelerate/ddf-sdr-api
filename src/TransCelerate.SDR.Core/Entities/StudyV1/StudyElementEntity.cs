using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class StudyElementEntity
    {
        public string Uuid { get; set; }
        public string StudyElementDesc { get; set; }
        public string StudyElementName { get; set; }
        public TransitionRuleEntity TransitionStartRule { get; set; }
        public TransitionRuleEntity TransitionEndRule { get; set; }
    }
}
