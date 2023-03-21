using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class StudyDesignDTO
    {
        public List<CurrentSectionsDTO> CurrentSections { get; set; }
        public string StudyDesignId { get; set; }
        public string TrialIntentType { get; set; }
        public string TrialType { get; set; }
    }
}
