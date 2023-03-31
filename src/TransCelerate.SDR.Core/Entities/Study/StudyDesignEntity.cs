using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class StudyDesignEntity
    {
        public List<CurrentSectionsEntity> CurrentSections { get; set; }
        public string StudyDesignId { get; set; }
        public string TrialIntentType { get; set; }
        public string TrialType { get; set; }
    }
}
