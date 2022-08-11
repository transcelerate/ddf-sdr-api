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
        public string studyDesignId { get; set; }
        /// <summary>
        /// This property holds the value of Trial Intent Type for specific <see cref="studyDesignId"/>
        /// </summary>
        public string trialIntentType { get; set; }
        /// <summary>
        /// This property holds the value of Trial Type for specific <see cref="studyDesignId"/>
        /// </summary>
        public string trialType { get; set; }
        /// <summary>
        /// This property holds the Planned Workflows for specific <see cref="studyDesignId"/>
        /// </summary>
        public List<PlannedWorkflowDTO> plannedWorkflows { get; set; }
        /// <summary>
        /// This property holds the Study Populations for specific <see cref="studyDesignId"/>
        /// </summary>
        public List<StudyPopulationDTO> studyPopulations { get; set; }
        /// <summary>
        /// This property holds the Study Cells for specific <see cref="studyDesignId"/>
        /// </summary>
        public List<StudyCellDTO> studyCells { get; set; }
        /// <summary>
        /// This property holds the Investigational Interventions for specific <see cref="studyDesignId"/>
        /// </summary>
        public List<InvestigationalInterventionDTO> investigationalInterventions { get; set; }
    }
}
