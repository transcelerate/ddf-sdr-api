using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class CurrentSectionsDTO
    {
        public string id { get; set; }
        public string sectionType { get; set; }      
        public List<PlannedWorkflowDTO> plannedWorkflows { get; set; }
        public List<StudyPopulationDTO> studyPopulations { get; set; }
        public List<StudyCellDTO> studyCells { get; set; }
        public List<InvestigationalInterventionDTO> investigationalInterventions { get; set; } 
        public List<StudyDesignDTO> studyDesigns { get; set; }
        public List<StudyObjectiveDTO> objectives { get; set; }
        public List<StudyIndicationDTO> studyIndications { get; set; }
        public StudyProtocolDTO studyProtocol { get; set; }
    }
}
