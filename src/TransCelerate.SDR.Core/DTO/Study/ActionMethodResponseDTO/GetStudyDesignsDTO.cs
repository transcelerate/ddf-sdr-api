using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.Study
{
    /// <summary>
    /// This class is a DTO for GET Method for study design sections of a study
    /// </summary>
    public class GetStudyDesignsDTO
    {
        public string studyDesignId { get; set; }
        public string trialIntentType { get; set; }
        public string trialType { get; set; }

        public List<PlannedWorkflowDTO> plannedWorkflows { get; set; }

        public List<StudyPopulationDTO> studyPopulations { get; set; }

        public List<StudyCellDTO> studyCells { get; set; }

        public List<InvestigationalInterventionDTO> investigationalInterventions { get; set; }
    }
}
