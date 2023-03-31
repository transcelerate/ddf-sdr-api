using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.Study
{
    /// <summary>
    /// This class is a DTO for GET Method for study design sections of a study
    /// </summary>
    public class GetStudyDesignsDTO
    {
        /// <summary>
        /// This property holds the value of Study Design ID
        /// </summary>
        public string StudyDesignId { get; set; }
        /// <summary>
        /// This property holds the value of Trial Intent Type for specific <see cref="StudyDesignId"/>
        /// </summary>
        public string TrialIntentType { get; set; }
        /// <summary>
        /// This property holds the value of Trial Type for specific <see cref="StudyDesignId"/>
        /// </summary>
        public string TrialType { get; set; }
        /// <summary>
        /// This property holds the Planned Workflows for specific <see cref="StudyDesignId"/>
        /// </summary>
        public List<PlannedWorkflowDTO> PlannedWorkflows { get; set; }
        /// <summary>
        /// This property holds the Study Populations for specific <see cref="StudyDesignId"/>
        /// </summary>
        public List<StudyPopulationDTO> StudyPopulations { get; set; }
        /// <summary>
        /// This property holds the Study Cells for specific <see cref="StudyDesignId"/>
        /// </summary>
        public List<StudyCellDTO> StudyCells { get; set; }
        /// <summary>
        /// This property holds the Investigational Interventions for specific <see cref="StudyDesignId"/>
        /// </summary>
        public List<InvestigationalInterventionDTO> InvestigationalInterventions { get; set; }
    }
}
