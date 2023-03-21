using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class CurrentSectionsDTO
    {
        public string Id { get; set; }
        public string SectionType { get; set; }
        public List<PlannedWorkflowDTO> PlannedWorkflows { get; set; }
        public List<StudyPopulationDTO> StudyPopulations { get; set; }
        public List<StudyCellDTO> StudyCells { get; set; }
        public List<InvestigationalInterventionDTO> InvestigationalInterventions { get; set; }
        public List<StudyDesignDTO> StudyDesigns { get; set; }
        public List<StudyObjectiveDTO> Objectives { get; set; }
        public List<StudyIndicationDTO> StudyIndications { get; set; }
    }
}
